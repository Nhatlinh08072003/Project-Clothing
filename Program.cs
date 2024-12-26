var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
    name: "Login",
    pattern: "/login",
    defaults: new { controller = "Account", action = "Login" }
);
 app.MapControllerRoute(
    name: "Register",
    pattern: "/register",
    defaults: new { controller = "Account", action = "Register" }
);
app.MapControllerRoute(
    name: "ForgotPassword",
    pattern: "/forgotpassword",
    defaults: new { controller = "Account", action = "ForgotPassword" }
);
app.MapControllerRoute(
    name: "ChangePassword",
    pattern: "/changepassword",
    defaults: new { controller = "Account", action = "ChangePassword" }
);
app.MapControllerRoute(
    name: "ProfileUser",
    pattern: "/profileuser",
    defaults: new { controller = "Account", action = "ProfileUser" }
);
app.MapControllerRoute(
    name: "PurchaseHistory",
    pattern: "/purchasehistory",
    defaults: new { controller = "Account", action = "PurchaseHistory" }
);
app.MapControllerRoute(
    name: "Contact",
    pattern: "/contact",
    defaults: new { controller = "Home", action = "Contact" }
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
