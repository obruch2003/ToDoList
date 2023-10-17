namespace ToDoList.Models
{
    public class ForProject
    {
        public User user { get; set; }
        public Project project { get; set; }
        public List<Project> projects { get; set; }
        public List<User> usersList { get; set; }
    }
}
