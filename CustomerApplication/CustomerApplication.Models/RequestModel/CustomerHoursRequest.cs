using CustomerApplication.Models.DatabaseModel;

namespace CustomerApplication.Models.RequestModel;

public class CustomerHoursRequest
{
    public CustomerHoursRequest()
    {
    }

    public CustomerHoursRequest(CustomerHours hours)
    {
        this.Id = hours.Id;
        this.CustomerId = hours.CustomerId; 
        this.DayOfWeek = hours.DayOfWeek;
        this.OpeningFormatted = DateTime.Today.Add(hours.Opening);
        this.ClosingFormatted = DateTime.Today.Add(hours.Closing);
        this.Opening = this.OpeningFormatted.ToString("hh:mm tt");
        this.Closing = this.ClosingFormatted.ToString("hh:mm tt");
    }

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int? DayOfWeek { get; set; }
    public string Opening { get; set; }
    public DateTime OpeningFormatted { get; set; }
    public string Closing { get; set; }
    public DateTime ClosingFormatted { get; set; }

    public string FullHoursText
    {
        get
        {
            return $"{Opening} - {Closing}";
        }
    }
    public string DayOfWeekText 
    { 
        get
        {
            if(DayOfWeek.HasValue) return Enum.GetName(typeof(DayOfWeek), DayOfWeek);
            return Enum.GetName(typeof(DayOfWeek), 0);
        }
    }

    public void FormatHours()
    {
        if (DateTime.TryParse(this.Opening, out DateTime openingDt) == false)
            throw new InvalidCastException("Customer opening is not a valid time");
        if (DateTime.TryParse(this.Closing, out DateTime closingDt) == false)
            throw new InvalidCastException("Customer closing is not a valid time");

        this.OpeningFormatted = openingDt;
        this.ClosingFormatted = closingDt;
    }
}