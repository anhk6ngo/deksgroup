using DTour.Client.Constants;
using SharedExtension.PaymentGateway;

namespace DTour.Client.Pages;

public partial class Train
{
    [SupplyParameterFromQuery] public string? From { get; set; }
    [SupplyParameterFromQuery] public string? Dep { get; set; }
    [SupplyParameterFromQuery] public string? Arr { get; set; }
    [SupplyParameterFromQuery] public string? To { get; set; }
    [SupplyParameterFromQuery] public string? Adt { get; set; }
    [SupplyParameterFromQuery] public string? Chd { get; set; }
    [SupplyParameterFromQuery] public string? dRange { get; set; }
    [Inject] private IHttpServiceClient? _http { get; set; }
    private bool _blnValidate;
    private RailOfferValidate _rqValid = new();
    private string _selectTime = "07:00";
    private string _selectDepTime = "07:00";
    private string _selectReturnTime = "07:00";
    private List<RailStation> _cities = [];
    private List<RailStation> _filterDepcities = [];
    private List<RailStation> _filterArrcities = [];
    private List<RailStation> _defaultCities = [];
    private RailResult<List<RailStationList>> _stationsResult = new();
    private RailSearchResponse? _searchResult;
    private RailCreateBooking _rqCreate = new();
    private List<TrainOfferClass>? _selectOfferClass = [];
    private List<TrainOfferClass>? _selectOfferByClass = [];
    private List<TrainOfferClass>? _selectGroupClass = [];
    private List<string>? _urls = [];
    private TrainList _selectTrain = new();
    private TrainList _selectReturnTrain = new();
    private string? _selectClass;
    private int _iStep;
    private bool _blnInitCarousel = false;
    private FluentValidationValidator _fluentValidationValidator = default!;
    private List<RailCreateBookingResponse>? _createBookingResult = [];
    private Dictionary<string, List<string>> _dicAmen = new();
    private List<RailAmenity> _storeAmenities = [];
    private StoreBookingDto _data = new();
    private int _onewayStep = 0;
    private StoreData _storeData = new();

    private SearchRequest _request = new()
    {
        DateRange = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
    };

    private Dictionary<int, List<string>> dicCondition = new();
    private Dictionary<int, List<string>> dicFareCondition = new();
    private RailAmenityRequest _requestAmenity = new();
    private int iSelectCon = 0;

    protected override void OnInitialized()
    {
        _request.DateRange = dRange;
        _request.SlugDep = From;
        _request.SlugArr = To;
        _request.Dep = Dep;
        _request.Arr = Arr;
        _request.IsRoundTrip = $"{dRange}".Contains("to");
        var iAdt = $"{Adt}".ConvertToInt();
        if (iAdt > 0)
        {
            _request.Adult = iAdt;
            for (int i = 0; i < iAdt; i++)
            {
                _request.PaxAge.Add(30);
            }
        }

        if (Chd.NotIsNullOrEmpty())
        {
            var aChd = $"{Chd}".SplitExt("-").Select(s => s.ConvertToInt()).ToList();
            _request.Child = aChd.Count;
            _request.PaxAge.AddRange(aChd);
        }

        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await CallJs("initTheme");
            await SearchTrain();
        }

        if (_blnInitCarousel)
        {
            _blnInitCarousel = false;
            await CallJs("initSlider");
        }
    }

    private async Task SearchTrain()
    {
        if (_request.PaxAge is { Count: 0 }) return;
        var aDate = $"{_request.DateRange}".SplitExt("to");
        var rq = new RailSearch()
        {
            DepartureDate = aDate[0].Trim(),
            Origin = _request.SlugDep,
            Destination = _request.SlugArr,
            DepartureTime = $"{_selectDepTime}:00",
            Passengers = _request.PaxAge.Select(s => new RailPassenger { Age = s }).ToList(),
        };
        if (_onewayStep == 1)
        {
            rq.Direction = "inbound";
            rq.ReturnDate = aDate[1].Trim();
            rq.ReturnTime = $"{_selectReturnTime}:00";
        }

        if (_storeData.BookingSessionId.NotIsNullOrEmpty())
        {
            rq.BookingSessionId = _storeData.BookingSessionId;
        }

        IsLoading = true;
        StateHasChanged();
        var result =
            await _http!.PostAsync<RailResult<List<RailSearchResponse>>>($"{RailEndpoints.Rail}{RailEndpoints.Search}",
                rq);
        if (result.Status == 200)
        {
            _searchResult = result.Data?.FirstOrDefault();
            _storeData.BookingSessionId = _searchResult?.BookingSessionId;
            if (_onewayStep == 0)
            {
                _data.BookingSessionId = _storeData.BookingSessionId;
                _data.TicketType = 1;
                _data.Adt = $"{Adt}".ConvertToInt();
                _data.Chd = $"{Chd}".ConvertToInt();
                _data.DepartDate = aDate[0].ConvertToDate() ?? DateTime.Today;
            }
            else if (_onewayStep == 1)
            {
                _data.ReturnDate = aDate[1].ConvertToDate() ?? DateTime.Today;
            }

            var searchAmen = new List<string>();
            if (_searchResult?.TrainList is { Count: > 0 })
            {
                _storeData.Data![_onewayStep] = _searchResult.TrainList;
                if (_searchResult.AmenityListByTariff != null)
                {
                    var sAmen = _searchResult.AmenityListByTariff?.ToString();
                    var tmpDic = $"{sAmen}".ToObject<Dictionary<string, List<string>>>();
                    if (tmpDic is { Count: > 0 })
                    {
                        foreach (var item in tmpDic.Where(item => !_dicAmen.ContainsKey(item.Key)))
                        {
                            _dicAmen.Add(item.Key, item.Value);
                            searchAmen.AddRange(item.Value);
                        }
                    }
                }

                if (searchAmen.Count > 0)
                {
                    _requestAmenity.Ids = searchAmen;
                    var dataAmen =
                        await _http.PostAsync<RailResult<List<RailAmenityList>>>(
                            $"{RailEndpoints.Rail}{RailEndpoints.Amenity}", _requestAmenity);
                    if (dataAmen is { Status: 200, Data.Count: > 0 })
                    {
                        _storeAmenities.AddRange(dataAmen.Data.SelectMany(s => s.AmenityList!));
                    }
                }

                _iStep = 1;
            }
            else
            {
                await SweetAlert($"{_searchResult?.BookingSessionStatus}", icon: "error");
            }
        }
        else
        {
            var msg = result.Errors?.Select(s => s.Message).Aggregate((a, b) => $"<p>{a}</p><p>{b}</p>");
            Console.WriteLine(msg);
            await SweetAlert($"{msg}", icon: "error");
        }

        IsLoading = false;
        StateHasChanged();
    }

    private async Task SetSelectTrain(TrainList data)
    {
        _selectTrain = data;
        _storeData.SelectTrain![_onewayStep] = data;
        if (_onewayStep == 0)
        {
            _rqValid = new RailOfferValidate()
            {
                ApiProviderId = data.Summary?.ApiProviderId,
                BookingSessionId = _storeData.BookingSessionId,
                SelectedOutbound = [$"{data.OfferId}"]
            };
            _rqCreate.ApiProviderId = _rqValid.ApiProviderId;
            _rqCreate.BookingSessionId = _storeData.BookingSessionId;
            var sumF = data.Summary?.TransfersDetails?.FirstOrDefault();
            _data.From = sumF?.Origin?.Label;
            _data.To = sumF?.Destination?.Label;
            if (_request.IsRoundTrip)
            {
                _onewayStep = 1;
                await SearchTrain();
                return;
            }
        }
        else
        {
            _rqValid.SelectedInbound = [$"{data.OfferId}"];
        }

        _iStep = 2;
        _blnValidate = true;
        var validateResult =
            await _http!.PostAsync<RailResult<List<RailOfferValidateResponse>>>(
                $"{RailEndpoints.Rail}{RailEndpoints.OfferValidate}", _rqValid);
        _blnValidate = false;
        if (validateResult.Status != 200)
        {
            var msg = validateResult.Errors?.Select(s => s.Message)
                .Aggregate((a, b) => $"<p>{a}</p><p>{b}</p>");
            await SweetAlert($"{msg}", icon: "error");
            _iStep = 1;
        }
        else
        {
            if (_rqCreate.Passengers is null or { Count: 0 })
            {
                _rqCreate.Passengers = _selectTrain.Segments?.FirstOrDefault()?.Offers?.FirstOrDefault()?.Tariffs
                    ?.SelectMany(s => s.Passengers!).Select(s => new RailPassenger()
                    {
                        Id = s.Id,
                        Type = s.Type,
                    }).ToList() ?? [];
                if (Chd.NotIsNullOrEmpty())
                {
                    var i = 0;
                    foreach (var pax in _rqCreate.Passengers)
                    {
                        if (pax.Type != "adult")
                        {
                            pax.Age = _request.PaxAge[i];
                            pax.Dob = new DateTime(DateTime.Now.Year - pax.Age, 1, 1);
                        }

                        i++;
                    }
                }

                _rqCreate.Buyer = new RailBuyer();
            }

            _onewayStep = 0;
            SetClass(_onewayStep);
        }
    }

    private void SetClass(int index)
    {
        _selectOfferClass = _storeData.SelectTrain![index].Segments?.SelectMany(s => s.Offers?.SelectMany(sm =>
            sm.Tariffs?.Select(st => new TrainOfferClass()
            {
                Order = s.Order,
                OfferId = sm.OfferId,
                Guid = sm.Class?.Guid,
                Name = sm.Class?.Name,
                Id = sm.Class?.Id,
                Supplier = sm.Carrier?.Supplier,
                TariffName = st.Name,
                TariffGuid = st.Guid,
                Price = st.Price
            })!)!).Where(w => w.Price > 0).ToList();
        _selectClass = (_selectOfferClass?.FirstOrDefault(w => w.OfferId == _storeData.SelectTrain[index].OfferId)!)
            .Guid;
        _storeData.SelectClass![index] = $"{_selectClass}";
        _selectOfferByClass = _selectOfferClass.Where(w => w.Guid == _selectClass)
            .GroupBy(g => new
            {
                g.OfferId,
                g.TariffName,
                g.TariffGuid
            }).Select(s => new TrainOfferClass()
            {
                TariffGuid = s.Key.TariffGuid,
                OfferId = s.Key.OfferId,
                TariffName = s.Key.TariffName,
                Price = s.Sum(sm => sm.Price),
            }).ToList();
        _selectGroupClass = _selectOfferClass?.GroupBy(g => new
        {
            g.Guid,
            g.Name
        }).Select(s => new TrainOfferClass
        {
            Name = s.Key.Name,
            Guid = s.Key.Guid,
            Price = s.Sum(sm => sm.Price)
        }).OrderBy(o => o.Price).ToList();
        if (_selectOfferByClass is { Count: > 0 })
        {
            if (_onewayStep == 0)
            {
                _rqCreate.SelectedOutbound = [$"{_selectOfferClass![index].OfferId}"];
            }
            else
            {
                _rqCreate.SelectedInbound = [$"{_selectOfferClass![index].OfferId}"];
            }

            _storeData.SelectOffer![index] = $"{_selectOfferClass![index].OfferId}";
            _selectTrain!.Amount = _selectOfferByClass[index].Price;
        }

        _blnInitCarousel = true;
        StateHasChanged();
    }

    private void GoPassenger()
    {
        _iStep = 3;
    }

    private void SetSelectClass(string? classId)
    {
        _selectClass = classId;
        _selectOfferByClass = _selectOfferClass?.Where(w => w.Guid == _selectClass).GroupBy(g => new
        {
            g.OfferId,
            g.TariffName,
            g.TariffGuid
        }).Select(s => new TrainOfferClass()
        {
            TariffGuid = s.Key.TariffGuid,
            OfferId = s.Key.OfferId,
            TariffName = s.Key.TariffName,
            Price = s.Sum(sm => sm.Price),
        }).ToList();
        var sOfferId = $"{_selectOfferByClass?.FirstOrDefault()?.OfferId}";
        if (_onewayStep == 0)
        {
            _rqCreate.SelectedOutbound = [sOfferId];
        }
        else
        {
            _rqCreate.SelectedInbound = [sOfferId];
        }

        _storeData.SelectTrain![_onewayStep].Amount = _selectOfferByClass?[0].Price;
        _storeData.SelectClass![_onewayStep] = $"{classId}";
        _storeData.SelectOffer![_onewayStep] = sOfferId;
        StateHasChanged();
    }

    private void SetTariff(TrainOfferClass offer)
    {
        if (_storeData.SelectOffer!.Contains($"{offer.OfferId}"))
        {
            return;
        }

        if (_onewayStep == 0)
        {
            _rqCreate.SelectedOutbound = [$"{offer.OfferId}"];
        }
        else
        {
            _rqCreate.SelectedInbound = [$"{offer.OfferId}"];
        }

        _storeData.SelectOffer[_onewayStep] = $"{offer.OfferId}";
        _storeData.SelectTrain![_onewayStep].Amount = offer.Price;
        StateHasChanged();
    }

    private async Task ShowEditConfirm(TrainList item)
    {
        _selectTrain = item;
        await ToggleModal();
    }

    private async Task ShowTicketCondition(int iIndex)
    {
        iSelectCon = iIndex;
        await ToggleModal("#TicketCondition");
    }

    private void UpdateDob(ChangeEventArgs e, int i, RailPassenger pax)
    {
        var value = e.Value?.ToString() ?? string.Empty;
        if (value.IsNullOrEmpty()) return;
        pax.Dob = i switch
        {
            0 => $"{pax.Dob.Day}-{value}-{pax.Dob.Year}".ConvertToDate() ?? pax.Dob,
            1 => $"{value}-{pax.Dob:MMM}-{pax.Dob.Year}".ConvertToDate() ?? pax.Dob,
            2 => $"{pax.Dob.Day}-{pax.Dob:MMM}-{value}".ConvertToDate() ?? pax.Dob,
            _ => pax.Dob
        };
        pax.BirthDate = pax.Dob.ToString("yyyy-MM-dd");
    }

    private async Task GotoPay(int iMethod = 0)
    {
        var isValid = await _fluentValidationValidator.ValidateAsync(o => o.IncludeAllRuleSets());
        if (!isValid) return;
        _data.PaymentType = iMethod;
        _rqCreate.Data = _data;
        IsLoading = true;
        StateHasChanged();
        var result =
            await _http!.PostAsync<RailResult<List<RailCreateBookingResponse>>>(
                $"{RailEndpoints.Rail}{(iMethod == 0 ? RailEndpoints.CreateBooking : RailEndpoints.IssueTicket)}",
                _rqCreate);
        IsLoading = false;
        StateHasChanged();
        _createBookingResult = [];
        if (result.Status != 200)
        {
            var msg = result.Errors?.Select(s => s.Message).Aggregate((a, b) => $"<p>{a}</p><p>{b}</p>");
            await SweetAlert($"{msg}", icon: "error");
        }
        else
        {
            if (iMethod == 0)
            {
                _navigationManager.NavigateTo($"{result.Id}", replace:true);
                return;
            }
            _iStep = 4;
            _createBookingResult = result.Data;
            _urls = result.Urls;
        }
    }

    private async Task SearchTrainByTime()
    {
        if (_onewayStep == 0)
        {
            _selectDepTime = _selectTime;
        }
        else
        {
            _selectReturnTime = _selectTime;
        }

        await SearchTrain();
    }

    private void ShowFareClass(int tripType)
    {
        if (tripType == 0 && _onewayStep == 1 || tripType == 1 && _onewayStep == 0)
        {
            _onewayStep = tripType;
            _selectClass = _storeData.SelectClass![tripType];
            _selectOfferClass = _storeData.SelectTrain![tripType].Segments?.SelectMany(s => s.Offers?.SelectMany(sm =>
                sm.Tariffs?.Select(st => new TrainOfferClass()
                {
                    Order = s.Order,
                    OfferId = sm.OfferId,
                    Guid = sm.Class?.Guid,
                    Name = sm.Class?.Name,
                    Id = sm.Class?.Id,
                    Supplier = sm.Carrier?.Supplier,
                    TariffName = st.Name,
                    TariffGuid = st.Guid,
                    Price = st.Price
                })!)!).ToList();
            if (_selectClass.IsNullOrEmpty())
            {
                _selectClass =
                    (_selectOfferClass?.FirstOrDefault(w => w.OfferId == _storeData.SelectTrain[tripType].OfferId)!)
                    .Guid;
                var sOfferId = $"{_selectOfferClass.FirstOrDefault()?.OfferId}";
                if (_onewayStep == 0)
                {
                    _rqCreate.SelectedOutbound = [sOfferId];
                }
                else
                {
                    _rqCreate.SelectedInbound = [sOfferId];
                }

                _storeData.SelectClass![tripType] = $"{_selectClass}";
                _storeData.SelectOffer![tripType] = sOfferId;
            }

            _selectOfferByClass = _selectOfferClass?.Where(w => w.Guid == _selectClass).ToList();
            _selectGroupClass = _selectOfferClass?.GroupBy(g => new
            {
                g.Guid,
                g.Name
            }).Select(s => new TrainOfferClass
            {
                Name = s.Key.Name,
                Guid = s.Key.Guid,
                Price = s.Min(sm => sm.Price)
            }).OrderBy(o => o.Price).ToList();

            StateHasChanged();
        }
    }
}