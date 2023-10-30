using blog_test.Abstractions;
using blog_test.Data;
using blog_test.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection");
var services = builder.Services;

services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString));
services.AddDefaultIdentity<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

services.AddControllersWithViews();
services.AddRazorPages();
services.AddTransient<IPostService,PostService>();
services.AddTransient<ICommentService, CommentService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();