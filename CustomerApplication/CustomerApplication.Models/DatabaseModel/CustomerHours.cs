using CustomerApplication.Models.RequestModel;

namespace CustomerApplication.Models.DatabaseModel;

public class CustomerHours
{
    public CustomerHours()
    {
    }

    public CustomerHours(CustomerHoursRequest model)
    {
        this.Id = model.Id;
        this.CustomerId = model.CustomerId;
        this.DayOfWeek = model.DayOfWeek.HasValue ? model.DayOfWeek.Value : 0;
        this.Opening = model.OpeningFormatted.TimeOfDay;
        this.Closing = model.ClosingFormatted.TimeOfDay;
    }

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan Opening { get; set; }
    public TimeSpan Closing { get; set; }
}
