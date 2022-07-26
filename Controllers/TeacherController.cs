using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using min_web_project_v2.Models;

namespace min_web_project_v2.Controllers;

public class TeacherController : Controller
{
    private readonly ILogger<TeacherController> _logger;

    public TeacherController(ILogger<TeacherController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        UserModel user;
        TeacherInformationModel TheacherInformation = new TeacherInformationModel();
        Supabase.Gotrue.User session;
        session = Supabase.Client.Instance.Auth.CurrentUser;

        if (session == null)
            return RedirectToAction("Index", "Login");

        user = await Supabase.Client.Instance
            .From<UserModel>()
            .Filter("id", Postgrest.Constants.Operator.Equals, session.Id)
            .Single();

        if (user.Role.Equals("docente"))
        {
            TheacherInformation.UserModel = user;
            TheacherInformation.ScheduleModel = Supabase.Client.Instance.From<ScheduleModel>().Filter("teacher", Postgrest.Constants.Operator.Equals, session.Id).Get().Result.Models;
            
            return View(TheacherInformation);
        }

        if (user.Role.Equals("coordinador"))
        {
            return RedirectToAction("Index", "Coordinator");
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}