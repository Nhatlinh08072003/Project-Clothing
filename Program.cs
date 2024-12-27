using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Project_Clothing.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<WeatheredContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  
    .AddCookie(options =>  
    {  
         options.LoginPath = "/Login/Dangnhap"; // Set the login page path
         options.LogoutPath = "/Login/logout"; // Set the logout path
         options.Cookie.Name = "YourAppCookie";  
         options.Cookie.HttpOnly = true;  
         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  
         options.SlidingExpiration = true;  
         options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  
    });  

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
    name: "Dangnhap",
    pattern: "/login",
    defaults: new { controller = "Login", action = "Dangnhap" }
);
 app.MapControllerRoute(
    name: "Dangki",
    pattern: "/register",
    defaults: new { controller = "Register", action = "Dangki" }
);
app.MapControllerRoute(
    name: "QuenMatKhau",
    pattern: "/forgotpassword",
    defaults: new { controller = "ForgotPassword", action = "QuenMatKhau" }
);
app.MapControllerRoute(
    name: "DoiMatKhau",
    pattern: "/changepassword",
    defaults: new { controller = "ChangePassword", action = "DoiMatKhau" }
);
app.MapControllerRoute(
    name: "ThongTin",
    pattern: "/profileuser",
    defaults: new { controller = "Profile", action = "ThongTin" }
);
app.MapControllerRoute(
    name: "Daugia",
    pattern: "/purchasehistory",
    defaults: new { controller = "PruchaseHistory", action = "Daugia" }
);
app.MapControllerRoute(
    name: "Lienhe",
    pattern: "/contact",
    defaults: new { controller = "Contact", action = "Lienhe" }
);
app.MapControllerRoute(
    name: "Index",
    pattern: "/auction",
    defaults: new { controller = "Auction", action = "Index" }
);
app.MapControllerRoute(
    name: "Index",
    pattern: "/product",
    defaults: new { controller = "Product", action = "Index" }
);
app.MapControllerRoute(
    name: "NewMerch",
    pattern: "/newmerch",
    defaults: new { controller = "Product", action = "NewMerch" }
);
app.MapControllerRoute(
    name: "Index",
    pattern: "/cart",
    defaults: new { controller = "Cart", action = "Index" }
);
app.MapControllerRoute(
    name: "DetailProduct",
    pattern: "/detailproduct",
    defaults: new { controller = "Product", action = "DetailProduct" }
);
app.Run();
