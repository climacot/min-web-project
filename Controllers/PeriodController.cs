using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using min_web_project_v2.Models;

namespace min_web_project_v2.Controllers;

public class PeriodController : Controller
{
    private readonly ILogger<PeriodController> _logger;

    public PeriodController(ILogger<PeriodController> logger)
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

    public async Task<IActionResult> Create(PeriodModel periodp)
    {
        try
        {
            int diference = Math.Abs((periodp.endDate.Month - periodp.startDate.Month) + 12 * (periodp.endDate.Year - periodp.startDate.Year));
            periodp.Period = diference;

            if (diference < 3)
            {
                return RedirectToAction("Index", "Period");
            }

            var instance = Supabase.Client.Instance;
            var channels = await instance.From<PeriodModel>().Insert(periodp);
            return RedirectToAction("Index", "Coordinator");
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}