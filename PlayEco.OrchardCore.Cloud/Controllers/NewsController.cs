using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlayEco.OrchardCore.Cloud.Models;
using PlayEco.OrchardCore.Cloud.Services;
using StrangeCloud.Service.Client.Contracts;

namespace PlayEco.OrchardCore.Cloud.Controllers;

public class NewsController(ILogger<AccountController> logger, IHttpContextAccessor session) : Controller
{
    private readonly ILogger<AccountController> _logger = logger;

    // GET
    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> GetNews()
    {
        var userToken = session.HttpContext?.Session.GetString("Token") ?? string.Empty;
        var newsService = new NewsService(userToken);
        var news = await newsService.GetNews();
        return this.View("Index", news);
    }
}