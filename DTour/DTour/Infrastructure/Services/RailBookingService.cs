using System.Net;
using System.Text;
using DTour.Application.Features.Data.Queries;
using SharedKernel.Models;

namespace DTour.Infrastructure.Services;

public class RailBookingService(
    IHttpClientFactory clientFactory,
    IHttpServiceClient httpServiceClient,
    ILoadFile loadFile,
    IMediator mediator,
    IMailService mailService,
    IAppCache cache)
    : IRailBookingService
{
    public async Task<RailResult<List<RailSearchResponse>>> Search(RailSearch request)
    {
        var searchResult =
            await httpServiceClient.PostAsync<RailResult<List<RailSearchResponse>>>("api/v1/search", request);
        if (searchResult.Status != 200) return searchResult;

        #region Calculate price

        var lstOfferId = new List<string>();
        var rqPrice = new RailOfferPrice();
        var data = searchResult.Data?.FirstOrDefault();
        if (data?.TrainList is { Count: 0 }) return searchResult;
        var fList = new List<TrainList>();
        foreach (var train in data!.TrainList!)
        {
            if (train.Summary?.Saleable != true)
            {
                var blnAdd = false;
                foreach (var offer in train.Segments!.Where(segment => segment.Offers is { Count: > 0 })
                             .SelectMany(segment => segment.Offers!))
                {
                    lstOfferId.Add($"{offer.OfferId}");
                    blnAdd = true;
                }

                if (!blnAdd) continue;
                if (rqPrice.ApiProviderId.IsNullOrEmpty())
                {
                    rqPrice.ApiProviderId = train.Summary?.ApiProviderId;
                    rqPrice.BookingSessionId = data.BookingSessionId;
                }
            }

            fList.Add(train);
        }

        if (lstOfferId is { Count: > 0 })
        {
            rqPrice.Offers = lstOfferId;
            var resultOffer = await OfferPrice(rqPrice);
            if (resultOffer.Status == 200)
            {
                var offerPrice = resultOffer.Data?.FirstOrDefault();
                foreach (var train in fList)
                {
                    foreach (var segment in train.Segments!)
                    {
                        foreach (var offer in segment.Offers!)
                        {
                            foreach (var tariff in offer.Tariffs!)
                            {
                                foreach (var sFind in tariff.Passengers!.Select(passenger =>
                                             $"{offerPrice?.List?.FirstOrDefault(w => w.PassengerId == passenger.Id)?.Price}"))
                                {
                                    tariff.Price = tariff.Price.PlusNumber(sFind.ConvertToDouble());
                                }
                            }
                        }
                    }
                }
            }
        }

        var dPer = 1.0;
        var fees = cache.GetOrAdd<FeeConfig?>("store-fee", () => loadFile.LoadFileAsync<FeeConfig?>("fees"),
            TimeSpan.FromMinutes(15));
        var exchangeRates = await mediator.Send(new GetActiveExchangeRateQuery());
        if (request.ConvertVnd)
        {
            var oFind = exchangeRates.FirstOrDefault(w =>
                $"{w.Currency}".Contains("eur", StringComparison.CurrentCultureIgnoreCase));
            if (oFind != null)
            {
                searchResult.ExchangeRate = oFind.Rate;
                dPer *= oFind.Rate;
            }
        }

        foreach (var train in fList)
        {
            foreach (var tariff in train.Segments!.SelectMany(segment =>
                         segment.Offers!.SelectMany(offer => offer.Tariffs!)))
            {
                tariff.Price *= dPer;
            }

            var oTariff = train.Segments?.SelectMany(s => s.Offers!.SelectMany(sm => sm.Tariffs!.Select(si => new
            {
                sm.OfferId,
                si.Guid,
                si.Price
            }))).Where(w => w.Price > 0).GroupBy(g => new
            {
                g.Guid,
                g.OfferId,
            }).Select(s => new
            {
                s.Key.OfferId,
                s.Key.Guid,
                Price = s.Sum(sm => sm.Price),
            }).OrderBy(o => o.Price).FirstOrDefault();
            train.Amount = oTariff?.Price ?? 0;
            train.TariffId = oTariff?.Guid;
            train.OfferId = oTariff?.OfferId;
        }

        data.TrainList = fList.Where(w => w.Amount > 0).ToList();

        #endregion

        searchResult.FeeConfig = new FeeConfig();
        fees.Adapt(searchResult.FeeConfig);
        if (fees != null)
        {
            searchResult.FeeConfig!.SystemFee *= searchResult.ExchangeRate;
        }

        return searchResult;
    }

    public async Task<RailResult<List<RailAmenityList>>> GetAmenity(RailAmenityRequest request)
    {
        return await httpServiceClient.PostAsync<RailResult<List<RailAmenityList>>>("api/v1/amenity/list", request);
    }

    public async Task<RailResult<List<RailOfferPriceResponse>>> OfferPrice(RailOfferPrice request)
    {
        return await httpServiceClient.PostAsync<RailResult<List<RailOfferPriceResponse>>>("api/v1/offer/prices",
            request);
    }

    public async Task<RailResult<List<RailOfferValidateResponse>>> OfferValidate(RailOfferValidate request)
    {
        return await httpServiceClient.PostAsync<RailResult<List<RailOfferValidateResponse>>>("api/v1/offer/validate",
            request);
    }

    public async Task<RailResult<List<RailCreateBookingResponse>>> CreateBooking(RailCreateBooking request)
    {
        return await httpServiceClient.PostAsync<RailResult<List<RailCreateBookingResponse>>>("api/v1/booking/create",
            request);
    }

    public async Task<RailResult<List<RailConfirmBookingResponse>>> BookingConfirm(RailConfirm request)
    {
        return await httpServiceClient.PostAsync<RailResult<List<RailConfirmBookingResponse>>>("api/v1/booking/confirm",
            request);
    }

    public async Task<RailResult<List<RailConfirmBookingResponse>>> RetrieveBooking(string? bookingSessionId)
    {
        return await httpServiceClient.GetAsync<RailResult<List<RailConfirmBookingResponse>>>(
            $"api/v1/booking/{bookingSessionId}");
    }

    public async Task<RailResult<List<RailCancelBookingResponse>>> CancellationBookingDetails(string? bookingSessionId)
    {
        return await httpServiceClient.GetAsync<RailResult<List<RailCancelBookingResponse>>>(
            $"api/v1/booking/{bookingSessionId}/cancel/details");
    }

    public async Task<RailResult<List<RailCancelBookingSessionResponse>>> CancelBooking(string? bookingSessionId)
    {
        return await httpServiceClient.GetAsync<RailResult<List<RailCancelBookingSessionResponse>>>(
            $"api/v1/booking/{bookingSessionId}/cancel");
    }

    public async Task<RailResult<List<RailLocationOnline>>> GetStation(string input)
    {
        var client = clientFactory.CreateClient("RailOnline");
        var response = await client.GetAsync($"/geo-station-city/suggestions?type=origin&search={input}");
        if (response.StatusCode != HttpStatusCode.OK) return new RailResult<List<RailLocationOnline>>();
        var content = await response.Content.ReadAsStringAsync();
        return content.ToObject<RailResult<List<RailLocationOnline>>>();
    }

    public async Task SendEmail(SendEmailRequest request)
    {
        var lstAttatchs = new List<AttachmentFileInfo>();
        if (request.Urls is { Count: > 0 })
        {
            var client = clientFactory.CreateClient();
            var i = 0;
            foreach (var url in request.Urls)
            {
                i++;
                var response = await client.GetAsync(url);
                if(response.StatusCode != HttpStatusCode.OK) continue;
                var content = await response.Content.ReadAsByteArrayAsync();
                lstAttatchs.Add( new AttachmentFileInfo
                {
                    Content = content,
                    FileName = $"{request.BookingCode}_{i}.pdf",
                });
            }
        }

        var sBody = new StringBuilder();
        sBody.AppendLine("<h1>Thank for your purchase!</h1>");
        sBody.AppendLine($"This is a booking code: {request.BookingCode}<br/>");
        sBody.AppendLine("Thanks & best regards.<br/>");
        sBody.AppendLine("Note: This is a robot email. Please do not reply to this email.");
        var msg = new MailRequest()
        {
            To = request.Email,
            Subject = $"{request.BookingCode}",
            Attachments = lstAttatchs,
            Body = sBody.ToString()
        };
        await mailService.SendAsync(msg);
    }
}