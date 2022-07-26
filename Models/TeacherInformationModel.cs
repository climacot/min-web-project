namespace min_web_project_v2.Models
{
    public class TeacherInformationModel
    {
        public UserModel UserModel { get; set; }
        public List<ScheduleModel> ScheduleModel { get; set; }
        public string[] days = { "lunes", "martes", "miercoles", "jueves", "viernes", "sabado" };
        public string[] colors = { "bg-blue-300",
                                    "bg-yellow-300",
                                    "bg-orange-300",
                                    "bg-violet-300",
                                    "bg-indigo-300",
                                    "bg-red-300",
                                    "bg-gray-300",
                                    "bg-purple-300",
                                };
    }
}