using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;

namespace Project_Clothing.Controllers;

public class AuctionController : Controller
{
    private readonly ILogger<AuctionController> _logger;

    public AuctionController(ILogger<AuctionController> logger)
    {
        _logger = logger;
    }
    
 public IActionResult Index()
    {
        return View();
    }
  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
