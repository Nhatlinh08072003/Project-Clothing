using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_Clothing.Models;

namespace Project_Clothing.Controllers;

public class ForgotPasswordController : Controller
{
    private readonly ILogger<ForgotPasswordController> _logger;

    public ForgotPasswordController(ILogger<ForgotPasswordController> logger)
    {
        _logger = logger;
    }
    
 public IActionResult QuenMatKhau()
    {
        return View();
    }
   
  
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}