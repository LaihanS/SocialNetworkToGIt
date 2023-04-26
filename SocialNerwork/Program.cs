using SocialNetwork.Core.Application;
using SocialNetwork.Infrastructure.Persistence;
using SocialNetwork.Infrastructure.Shared;
using SocialNetwork.WebApp.Helpers;
using SocialNetwork.WebApp.ValidateSession;

var builder = WebApplication.CreateBuilder(args);

//d Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddPersistenceInfrastructure();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateSession, ValidateSession>();
builder.Services.AddTransient<ReturnPostList, ReturnPostList>();
builder.Services.AddTransient<IsLoggedIn, IsLoggedIn>();
builder.Services.AddSharedInfrastructure(builder.Configuration);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Index}/{id?}");

app.Run();
