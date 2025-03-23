namespace DTour.Client.Shared.Dtos.Response;

public class RailLocationOnline
{
    public string? city_slug { get; set; }
    public string? city_slug_url { get; set; }
    public string? city_name { get; set; }
    public string? country_name { get; set; }
    public string? country_code { get; set; }
    public RailStationOnline[]? stations { get; set; }
}

public class RailStationOnline
{
    public string? order_city { get; set; }
    public string? order_station { get; set; }
    public string? station_name { get; set; }
    public string? station_slug_url { get; set; }
    public string? city_name { get; set; }
    public string? city_slug_url { get; set; }
    public string? country_name { get; set; }
    public string? station_slug { get; set; }
    public string? city_slug { get; set; }
    public string? country_code { get; set; }
    public bool station_main { get; set; }
    public string? station_type { get; set; }
    public bool flag_all_station { get; set; }
    public bool verified { get; set; }
}

