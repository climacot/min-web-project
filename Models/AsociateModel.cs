using Postgrest.Attributes;
using Supabase;

namespace min_web_project_v2.Models
{
    [Table("asociate")]
    public class AsociateModel : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        public ProgramModel program { get; set; }

        public CompetenciesModel competencies { get; set; }
    }
}