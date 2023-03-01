using CustomerApplication.Models.RequestModel;
using CustomerApplication.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApplication.WebApp.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        var model = new DashboardIndexViewModel { MenuIndex = "1", ViewTitle = "Customer Dashboard", Customer = new CustomerRequest () };
        return View(model);
    }

    public IActionResult Availability()
    {
        return View(new DashboardAvailabilityViewModel { MenuIndex = "2", ViewTitle = "Operating Hours", CustomerIdString = "null" });
    }

    [Route("/Dashboard/Availability/{customerId}")]
    public IActionResult Availability(int customerId)
    {
        return View(new DashboardAvailabilityViewModel { MenuIndex = "2", ViewTitle = "Operating Hours", CustomerIdString = customerId.ToString() });
    }

    public IActionResult Error()
    {
        var model = new ErrorViewModel { MenuIndex = null, ViewTitle = "Error" };
        return View(model);
    }
}
