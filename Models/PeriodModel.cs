using Postgrest.Attributes;
using Supabase;

namespace min_web_project_v2.Models
{
    [Table("period")]
    public class PeriodModel : SupabaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("period")]
        public int Period { get; set; }

        [Column("startDate")]
        public DateTime startDate { get; set; }

        [Column("endDate")]
        public DateTime endDate { get; set; }
    }
}