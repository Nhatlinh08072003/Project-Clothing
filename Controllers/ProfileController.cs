using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;

namespace Project_Clothing.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }
    
 public IActionResult ThongTin()
    {
        return View();
    }
   
  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
