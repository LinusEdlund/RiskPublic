using RiskPublic.Components;
using Microsoft.AspNetCore.ResponseCompression;
using Risk.Hubs;
using RiskLibrary.Service.Attack.Interface;
using RiskLibrary.Service.Attack;
using RiskLibrary.Service.Move.Interface;
using RiskLibrary.Service.Move;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IAttackDictionaryService, AttackDictionaryService>();
builder.Services.AddScoped<IResolveAttackService, ResolveAttackService>();
builder.Services.AddScoped<IPathAvailable, PathAvailable>();


builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stran" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapHub<ChatHub>("/chathub");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
