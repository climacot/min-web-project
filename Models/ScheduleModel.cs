using Postgrest.Attributes;
using Supabase;

namespace min_web_project_v2.Models
{
    [Table("schedule")]
    public class ScheduleModel : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("teacher")]
        public string Docente { get; set; }

        [Column("day")]
        public string Dia { get; set; }

        [Column("color")]
        public string Color { get; set; }

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