using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using min_web_project_v2.Models;

namespace min_web_project_v2.Controllers;

public class CoordinatorController : Controller
{
    private readonly ILogger<CoordinatorController> _logger;

    public CoordinatorController(ILogger<CoordinatorController> logger)
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
            List<EnvironmentModel> environments = Supabase.Client.Instance.From<EnvironmentModel>().Get().Result.Models;
            List<UserModel> teachers = Supabase.Client.Instance.From<UserModel>().Get().Result.Models;
            List<PeriodModel> periods = Supabase.Client.Instance.From<PeriodModel>().Get().Result.Models;
            List<ProgramModel> programs = Supabase.Client.Instance.From<ProgramModel>().Get().Result.Models;
            List<ScheduleModel> schedule = Supabase.Client.Instance.From<ScheduleModel>().Get().Result.Models;

            List<AsociateModel> asociate = Supabase.Client.Instance
                .From<AsociateModel>()
                .Select("program(*), competencies(*)")
                .Get().Result.Models;

            LogicModel logic = new LogicModel();

            logic.EnvironmentModels = environments;
            logic.PeriodModels = periods;
            logic.AsociateModel = asociate;
            logic.UserModels = teachers;
            logic.ScheduleModel = schedule;

            return View(logic);
        }

        if (user.Role.Equals("docente"))
        {
            return RedirectToAction("Index", "Teacher");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(ScheduleModel schedule)
    {
        try
        {
            int diarias = 0;
            int semanales = 0;
            int existe = 0;

            List<ScheduleModel> schedulef = Supabase.Client.Instance
                .From<ScheduleModel>()
                .Filter("teacher", Postgrest.Constants.Operator.Equals, schedule.Docente)
                .Get().Result.Models;

            foreach (var item in schedulef)
            {
                if (item.Dia.Equals(schedule.Dia))
                {
                    
                }
            }

            var instance = Supabase.Client.Instance;
            var channels = await instance.From<ScheduleModel>().Insert(schedule);
            return RedirectToAction("Index", "Coordinator");
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<IActionResult> Close()
    {
        await Supabase.Client.Instance.Auth.SignOut();
        return RedirectToAction("Index", "Coordinator");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
