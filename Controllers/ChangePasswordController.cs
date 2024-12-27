using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;

namespace Project_Clothing.Controllers;

public class ChangePasswordController : Controller
{
    private readonly ILogger<ChangePasswordController> _logger;

    public ChangePasswordController(ILogger<ChangePasswordController> logger)
    {
        _logger = logger;
    }
    
 public IActionResult DoiMatKhau()
    {
        return View();
    }
   
  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
