var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "admin",
    defaults: new { controller = "Admin", action = "Index" });

app.MapControllerRoute(
    name: "admin-duzenle",
    pattern: "admin/duzenle",
    defaults: new { controller = "Admin", action = "Edit" });

app.MapControllerRoute(
    name: "rol-duzenle",
    pattern: "admin/rol",
    defaults: new { controller = "Admin", action = "Role" });

app.MapControllerRoute(
    name: "giris",
    pattern: "hesap/giris-yap",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "kayit",
    pattern: "hesap/kayit-ol",
    defaults: new { controller = "Account", action = "Register" });

app.MapControllerRoute(
    name: "liste",
    pattern: "liste/{exam}/{category}",
    defaults: new { controller = "List", action = "Index" });

app.MapControllerRoute(
    name: "video",
    pattern: "liste/{exam}/{category}/{videoId}",
    defaults: new { controller = "List", action = "Video" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
