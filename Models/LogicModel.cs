using Postgrest.Attributes;
using Supabase;

namespace min_web_project_v2.Models
{
    [Table("schedule")]
    public class LogicModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        public List<PeriodModel> PeriodModels { get; set; }

        public List<UserModel> UserModels { get; set; }

        public List<EnvironmentModel> EnvironmentModels { get; set; }

        public List<AsociateModel> AsociateModel { get; set; }

        public List<ScheduleModel> ScheduleModel { get; set; }

        public string[] days = { "lunes", "martes", "miercoles", "jueves", "viernes", "sabado" };

        public string[] schedule = { "7 am - 1 pm", "2 pm - 10 pm" };

        [Column("teacher")]
        public string Docente { get; set; }

        [Column("day")]
        public string Dia { get; set; }

        [Column("schecule")]
        public string Horario { get; set; }

        [Column("period")]
        public string Periodo { get; set; }

        [Column("environment")]
        public string Ambiente { get; set; }

        [Column("program_environment")]
        public string ProgramaAmbiente { get; set; }
    }
}
