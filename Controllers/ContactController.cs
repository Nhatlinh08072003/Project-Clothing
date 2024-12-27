using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;

namespace Project_Clothing.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;

    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }
    
 public IActionResult Lienhe()
    {
        return View();
    }
   
  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
