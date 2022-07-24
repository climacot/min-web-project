using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using min_web_project_v2.Models;

namespace min_web_project_v2.Controllers;

public class EnvironmentController : Controller
{
    private readonly ILogger<EnvironmentController> _logger;

    public EnvironmentController(ILogger<EnvironmentController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        UserModel user;
        Supabase.Gotrue.User session;
        session = Supabase.Client.Instance.Auth.CurrentUser;

        if (session == null)
            return RedirectToAction("Index", "Login");

        user = await Supabase.Client.Instance
            .From<UserModel>()
            .Filter("id", Postgrest.Constants.Operator.Equals, session.Id)
            .Single();

        if (user.Role.Equals("coordinador"))
        {
            return View();
        }

        if (user.Role.Equals("docente"))
        {
            return RedirectToAction("Index", "Teacher");
        }

        return View();
    }

    public IActionResult Create()
    {
        return RedirectToAction("Index", "Coordinator");
    }

    public IActionResult Read()
    {
        return RedirectToAction("Index", "Coordinator");
    }

    public IActionResult Update()
    {
        return RedirectToAction("Index", "Coordinator");
    }

    public IActionResult Delete()
    {
        return RedirectToAction("Index", "Coordinator");
    }    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}