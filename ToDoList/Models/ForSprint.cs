namespace ToDoList.Models
{
    public class ForSprint
    {
        public User user { get; set; }
        public Sprint sprint { get; set; }
        public Project project { get; set; }
        public List<User> usersList { get; set; }
        public List<UserTask> userTasks { get; set; }
        public List<Task> tasks { get; set; }
        public List<Sprint> sprints { get; set; }       
    }
}