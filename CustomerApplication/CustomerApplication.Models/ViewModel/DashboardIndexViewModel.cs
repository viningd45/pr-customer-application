using CustomerApplication.Models.RequestModel;

namespace CustomerApplication.Models.ViewModel;

public class DashboardIndexViewModel : LayoutViewModel
{
    public CustomerRequest Customer { get; set; }
}
