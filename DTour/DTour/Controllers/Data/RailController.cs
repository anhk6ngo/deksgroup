using DTour.Application.Features.Data.Commands;
using DTour.Application.Features.Data.Queries;
using Hangfire;
using Microsoft.AspNetCore.Authorization;

namespace DTour.Controllers.Data;

public class RailController(ILoadFile loadFile, IRailBookingService bookingService, IVnPayService vnPayService)
    : AnonymousApiController
{
    [HttpGet("default-location")]
    public IActionResult GetLocation()
    {
        var result = loadFile!.LoadFileAsync<RailResult<List<RailStationList>>>("station");
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLocation([FromQuery] string search)
    {
        if (search.IsNullOrEmpty())
        {
            return BadRequest();
        }

        var result = await bookingService.GetStation(search);
        return Ok(result);
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchTrain(RailSearch input)
    {
        var result = await bookingService.Search(input);
        return Ok(result);
    }

    [HttpPost("amenity")]
    public async Task<IActionResult> GetAmenity(RailAmenityRequest input)
    {
        var result = await bookingService.GetAmenity(input);
        return Ok(result);
    }

    [HttpPost("offer-validate")]
    public async Task<IActionResult> OfferValidate(RailOfferValidate input)
    {
        var result = await bookingService.OfferValidate(input);
        return Ok(result);
    }

    [HttpPost("create-booking")]
    public async Task<IActionResult> CreateBooking(RailCreateBooking input)
    {
        var saveBooking = new StoreBookingDto();
        input.Data.Adapt(saveBooking);
        input.Data = null;
        var result = await bookingService.CreateBooking(input);
        if (result.Status != 200) return Ok(result);
        if (result.Data != null)
        {
            foreach (var item in result.Data)
            {
                if (item.Tickets == null) continue;
                foreach (var ticket in item.Tickets)
                {
                    ticket.Origin = saveBooking.From;
                    ticket.Destination = saveBooking.To;
                    if (saveBooking.Pnr.IsNullOrEmpty())
                    {
                        saveBooking.Pnr = ticket.Pnr;
                    }
                }
            }
        }

        var sUserId = User.GetUserId();
        saveBooking.UserId = sUserId;
        saveBooking.ApiProviderId = input.ApiProviderId;
        saveBooking.DisplayName = User.GetClaimByName("DisplayName");
        saveBooking.Amount = input.Amount ?? 0;
        saveBooking.SaveObject = result.ConvertObjectToString();
        saveBooking.ToEmail = $"{input.Buyer?.Email}";
        var resultAdd = await _mediator!.Send(new AddEditBookingCommand()
        {
            Request = new AddEditDataRequest<StoreBookingDto>()
            {
                Action = ActionCommandType.Add,
                Data = saveBooking
            }
        });
        if (!resultAdd.Succeeded)
        {
            return Ok(new RailResult<List<RailCreateBookingResponse>>
            {
                Status = -1,
                Errors =
                [
                    new RailError
                    {
                        ErrorMessage = resultAdd.Messages.Aggregate((a, b) => $"{a} {b}")
                    }
                ]
            });
        }

        result.Id = $"{resultAdd.Data}";
        if (saveBooking.PaymentType != 0) return Ok(result);
        var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
        var sId = $"{resultAdd.Data}".Replace("-", "_");
        var rq = new MakePayNow()
        {
            Id = sId,
            Amount = (input.Amount ?? 0).ToRound(),
            Path = $"{baseUrl}/checkout/vnpay",
        };
        result.Id = vnPayService.PayNow(rq);
        return Ok(result);
    }

    [HttpPost("issue-ticket")]
    [Authorize]
    public async Task<IActionResult> IssueTicket(RailCreateBooking input)
    {
        var sUserId = User.GetUserId();
        var checkBalance = await _mediator!.Send(new GetBalanceByUserQuery
        {
            UserId = User.GetUserId(),
        });
        ;
        if (checkBalance == null || checkBalance.Amount < input.Amount)
        {
            return Ok(new RailResult<List<RailCreateBookingResponse>>
            {
                Status = -1,
                Errors =
                [
                    new RailError
                    {
                        ErrorMessage = "Unfortunately, your account has insufficient funds for this transaction"
                    }
                ]
            });
        }

        var saveBooking = new StoreBookingDto();
        input.Data.Adapt(saveBooking);
        input.Data = null;
        var result = await bookingService.CreateBooking(input);
        if (result.Status != 200) return Ok(result);
        if (result.Data != null)
        {
            foreach (var item in result.Data)
            {
                if (item.Tickets == null) continue;
                foreach (var ticket in item.Tickets)
                {
                    ticket.Origin = saveBooking.From;
                    ticket.Destination = saveBooking.To;
                    if (saveBooking.Pnr.IsNullOrEmpty())
                    {
                        saveBooking.Pnr = ticket.Pnr;
                    }
                }
            }
        }

        var resultConfirm = await bookingService.BookingConfirm(new RailConfirm()
        {
            ApiProviderId = input.ApiProviderId,
            BookingSessionId = input.BookingSessionId,
        });
        if (resultConfirm.Status != 200)
        {
            result.Status = resultConfirm.Status;
            result.Errors = resultConfirm.Errors;
            return Ok(result);
        }
        var lstLink = new List<string>();
        foreach (var item in resultConfirm.Data!.Where(item => item.PdfTickets is { Count: > 0 }))
        {
            lstLink.AddRange(item.PdfTickets!);
        }

        result.Urls = lstLink;
        saveBooking.UserId = sUserId;
        saveBooking.ApiProviderId = input.ApiProviderId;
        saveBooking.DisplayName = User.GetClaimByName("DisplayName");
        saveBooking.Status = 2;
        saveBooking.Amount = input.Amount ?? 0;
        saveBooking.SaveObject = result.ConvertObjectToString();
        saveBooking.ToEmail = $"{input.Buyer?.Email}";
        await _mediator!.Send(new FuncBalanceCommand()
        {
            Input = new BalanceRequest()
            {
                UserId = sUserId,
                Amount = saveBooking.Amount,
                Action = ActionCommandType.Claim
            },
        });
        var agentComs = await _mediator!.Send(new GetActiveCommissionQuery());
        var dNow = DateTime.UtcNow;
        var oFind = agentComs.FirstOrDefault(w => $"{w.Email}".Contains($"{User.GetEmail()}")
                                                  && w.From <= dNow && (w.To >= dNow || w.To == null));
        saveBooking.CommitAmount = saveBooking.Amount * (oFind != null ? (oFind.Percent ?? 0) : 5) / 100;
        await _mediator!.Send(new AddEditBookingCommand()
        {
            Request = new AddEditDataRequest<StoreBookingDto>()
            {
                Action = ActionCommandType.Add,
                Data = saveBooking
            }
        });
        var rqSendEmail = new SendEmailRequest()
        {
            Email = $"{input.Buyer?.Email}",
            BookingCode = saveBooking.Pnr,
            Urls = lstLink,
        };
        BackgroundJob.Enqueue<IRailBookingService>(x => x.SendEmail(rqSendEmail));
        return Ok(result);
    }

    [HttpPost("confirm-booking/{id}")]
    [Authorize]
    public async Task<IActionResult> ConfirmBooking(Guid id)
    {
        var sUserId = User.GetUserId();
        var oFind = await _mediator!.Send(new GetStoreBookingIdQuery()
        {
            Id = id,
            UserId = sUserId,
        });
        if (oFind == null)
        {
            return Ok(new RailResult<List<RailConfirmBookingResponse>>()
            {
                Status = -1,
                Errors =
                [
                    new RailError()
                    {
                        ErrorMessage = "Not found the booking"
                    }
                ]
            });
        }

        if (oFind.Status == 2)
        {
            return Ok(new RailResult<List<RailConfirmBookingResponse>>()
            {
                Status = -1,
                Errors =
                [
                    new RailError()
                    {
                        ErrorMessage = "The booking is confirmed"
                    }
                ]
            });
        }

        var checkBalance = await _mediator!.Send(new GetBalanceByUserQuery
        {
            UserId = sUserId,
        });

        if (checkBalance == null || checkBalance.Amount < oFind.Amount)
        {
            return Ok(new RailResult<List<RailConfirmBookingResponse>>
            {
                Status = -1,
                Errors =
                [
                    new RailError
                    {
                        ErrorMessage = "Unfortunately, your account has insufficient funds for this transaction"
                    }
                ]
            });
        }

        var result = await bookingService.BookingConfirm(new RailConfirm()
        {
            ApiProviderId = oFind.ApiProviderId,
            BookingSessionId = oFind.BookingSessionId,
        });
        if (result.Status != 200) return Ok(result);
        oFind.Status = 2;
        if (sUserId.NotIsNullOrEmpty())
        {
            await _mediator!.Send(new FuncBalanceCommand()
            {
                Input = new BalanceRequest()
                {
                    UserId = sUserId,
                    Amount = oFind.Amount,
                    Action = ActionCommandType.Claim
                },
            });
            var agentComs = await _mediator!.Send(new GetActiveCommissionQuery());
            var dNow = DateTime.UtcNow;
            var oFindCom = agentComs.FirstOrDefault(w => $"{w.Email}".Contains($"{User.GetEmail()}")
                                                         && w.From <= dNow && (w.To >= dNow || w.To == null));
            oFind.CommitAmount = oFind.Amount * (oFindCom != null ? (oFindCom.Percent ?? 0) : 5) / 100;
        }

        await _mediator!.Send(new AddEditBookingCommand()
        {
            Request = new AddEditDataRequest<StoreBookingDto>()
            {
                Action = ActionCommandType.Edit,
                Data = oFind
            }
        });
        return Ok(result);
    }

    [HttpGet("get-result-online/{id}")]
    public async Task<IActionResult> GetResultOnline(Guid id)
    {
        var oFind = await _mediator!.Send(new GetStoreBookingIdQuery()
        {
            Id = id,
        });
        return Ok(oFind);
    }

    [HttpGet("retrieve-booking/{id}")]
    public async Task<IActionResult> RetrieveBooking(string id)
    {
        var tmpId = id;
        if (tmpId.Length == 6)
        {
            tmpId = await _mediator!.Send(new GetBookingSessionIdQuery()
            {
                Pnr = id
            });
        }

        var result = await bookingService.RetrieveBooking(tmpId);
        return Ok(result);
    }

    [HttpGet("cancellation-detail/{id}")]
    public async Task<IActionResult> CancellationDetail(string id)
    {
        var result = await bookingService.CancellationBookingDetails(id);
        return Ok(result);
    }

    [HttpGet("cancel/{id}")]
    public async Task<IActionResult> Cancel(string id)
    {
        var result = await bookingService.CancelBooking(id);
        return Ok(result);
    }
}