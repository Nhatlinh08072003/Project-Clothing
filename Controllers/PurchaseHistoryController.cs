using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;

namespace Project_Clothing.Controllers;

public class PurchaseHistoryController : Controller
{
    private readonly ILogger<PurchaseHistoryController> _logger;

    public PurchaseHistoryController(ILogger<PurchaseHistoryController> logger)
    {
        _logger = logger;
    }
    
 public IActionResult Daugia()
    {
        return View();
    }
   
  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
