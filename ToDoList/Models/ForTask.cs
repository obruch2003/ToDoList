namespace ToDoList.Models
{
    public class ForTask
    {
        public User user { get; set; }
        public Task task { get; set; }
        public Sprint sprint { get; set; }
        public Project project { get; set; }
        public List<User> usersList { get; set; }
        public List<User> AllusersList { get; set; }
        public List<UserTask> userTasks { get; set; }
        public List<UserTask> AlluserTasks { get; set; }
        public Files fileuploades { get; set; } 
        public List<Files> filesList { get; set; }
        public Comments comment { get; set; }
        public List<Comments> commentsList { get;set; }
    }
}
