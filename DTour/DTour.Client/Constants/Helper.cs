namespace DTour.Client.Constants;

public static class Helper
{
    public static string GetKindTitle(int? id)
    {
        return id switch
        {
            1 => "Travel Guide",
            2 => "Careers",
            3 => "MICE",
            4 => "Team Building",
            5 => "Tour By Requested",
            _ => "DTour News"
        };
    }
    public static string GetTourGroup(int? id)
    {
        return id switch
        {
            1 => "China",
            2 => "Asia",
            3 => "Japan",
            4 => "Korea",
            5 => "India Nepal",
            _ => "Others"
        };
    }
}