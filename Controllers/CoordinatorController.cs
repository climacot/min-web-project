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
            List<EnvironmentModel> environments = Supabase.Client.Instance
                .From<EnvironmentModel>()
                .Filter("state", Postgrest.Constants.Operator.Equals, "true")
                .Get().Result.Models;
            List<UserModel> teachers = Supabase.Client.Instance
                .From<UserModel>()
                .Filter("state", Postgrest.Constants.Operator.Equals, "true")
                .Filter("role", Postgrest.Constants.Operator.Equals, "docente")
                .Get().Result.Models;
            List<PeriodModel> periods = Supabase.Client.Instance.From<PeriodModel>().Get().Result.Models;
            List<ProgramModel> programs = Supabase.Client.Instance.From<ProgramModel>().Get().Result.Models;
            List<ScheduleModel> schedule = Supabase.Client.Instance.From<ScheduleModel>().Get().Result.Models;

            List<AsociateModel> asociate = Supabase.Client.Instance
                .From<AsociateModel>()
                .Select("program(*), competencies(*)")
                .Filter("state", Postgrest.Constants.Operator.Equals, "true")
                .Get().Result.Models;

            LogicModel logic = new LogicModel();

            for (int i = 0; i < schedule.Count; i++)
            {
                if (schedule[i].Docente.Equals("climaco"))
                {
                    schedule[i].Color = "bg-blue-300";
                }

                if (schedule[i].Docente.Equals("cristian"))
                {
                    schedule[i].Color = "bg-orange-300";
                }

                if (schedule[i].Docente.Equals("daniela"))
                {
                    schedule[i].Color = "bg-yellow-300";
                }

                if (schedule[i].Docente.Equals("climaco fernando"))
                {
                    schedule[i].Color = "bg-indigo-300";
                }
            }

            logic.EnvironmentModels = environments;
            logic.PeriodModels = periods;
            logic.AsociateModel = asociate;
            logic.UserModels = teachers;
            logic.ScheduleModel = schedule;

            // ViewBag.Alert = "dddd";
            return View(logic);
        }

        if (user.Role.Equals("docente"))
        {
            return RedirectToAction("Index", "Teacher");
        }
        // ViewBag.Alert = "dddd";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(ScheduleModel schedule)
    {
        try
        {
            int diarias = 0;
            int semanales = 0;
            bool existe = false;
            bool existep = false;
            int maxdiarias;
            int maxSemanales;

            List<ScheduleModel> schedules = Supabase.Client.Instance.From<ScheduleModel>().Get().Result.Models;

            foreach (var item in schedules)
            {
                if (schedule.Docente.Equals(item.Docente) && schedule.Periodo.Equals(item.Periodo))
                {
                    semanales = semanales + 2;
                }

                if (schedule.Docente.Equals(item.Docente) && schedule.Dia.Equals(item.Dia) && schedule.Periodo.Equals(item.Periodo))
                {
                    diarias = diarias + 2;
                }

                if (schedule.Dia.Equals(item.Dia) && schedule.Periodo.Equals(item.Periodo) && schedule.Horario.Equals(item.Horario) && schedule.Ambiente.Equals(item.Ambiente))
                {
                    existe = true;
                }

                if (schedule.Docente.Equals(item.Docente) && schedule.Dia.Equals(item.Dia) && schedule.Periodo.Equals(item.Periodo) && schedule.Horario.Equals(item.Horario))
                {
                    existep = true;
                }
            }

            UserModel user = await Supabase.Client.Instance
                .From<UserModel>()
                .Filter("name", Postgrest.Constants.Operator.Equals, schedule.Docente)
                .Single();

            if (existep)
            {
                return RedirectToAction("Index", "Coordinator");
            }

            if (existe)
            {
                return RedirectToAction("Index", "Coordinator");
            }

            if (user.TypeOfContract == "PT")
            {
                maxdiarias = 8;
                maxSemanales = 32;
            }
            else
            {
                maxdiarias = 10;
                maxSemanales = 40;
            }

            if (diarias >= maxdiarias)
            {
                // ViewBag.Alert = "Horas excedidas";
                return RedirectToAction("Index", "Coordinator");
            }

            if (semanales >= maxSemanales)
            {
                // ViewBag.Alert = "Horas excedidas";
                return RedirectToAction("Index", "Coordinator");
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
