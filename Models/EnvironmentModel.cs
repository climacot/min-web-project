using Postgrest.Attributes;
using Supabase;

namespace min_web_project_v2.Models
{
    [Table("environment")]
    public class EnvironmentModel : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public string id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("type")]
        public string type { get; set; }

        [Column("capacity")]
        public double capacity { get; set; }

        [Column("location")]
        public string location { get; set; }

        [Column("state")]
        public bool state { get; set; }
    }
}