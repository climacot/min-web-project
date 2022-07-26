using Postgrest.Attributes;
using Supabase;

namespace min_web_project_v2.Models
{
    [Table("program")]
    public class ProgramModel : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}