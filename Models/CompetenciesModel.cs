using Postgrest.Attributes;
using Supabase;

namespace min_web_project_v2.Models
{
    [Table("competencies")]
    public class CompetenciesModel : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Tipo { get; set; }
    }
}