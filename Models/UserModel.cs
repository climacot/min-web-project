using Postgrest.Attributes;
using Postgrest.Models;

namespace min_web_project_v2.Models
{
    [Table("user")]
    public class UserModel : BaseModel
    {
        [PrimaryKey("id", false)]
        [Column("id")]
        public string Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("identificationType")]
        public string IdentificationType { get; set; }

        [Column("identification")]
        public string Identification { get; set; }

        [Column("typeOfTeaching")]
        public string TypeOfTeaching { get; set; }

        [Column("typeOfContract")]
        public string TypeOfContract { get; set; }

        [Column("area")]
        public string Area { get; set; }

        [Column("state")]
        public bool State { get; set; }

        [Column("color")]
        public string Color { get; set; }
    }
}