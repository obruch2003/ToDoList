using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ToDoList.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int id {  get; set; }
        [Required]
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public User() { }
        public User(int id, string name, string email, string password,string role)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.role = role;
        }
        public User Get(string email, string password)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                User user = Db.Users.FirstOrDefault(x => x.email == email && x.password == password);

                return user;
            }
        }
        public User GetByEmail(string email)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                User user = Db.Users.FirstOrDefault(x => x.email == email);
                return user;
            }
        }
        public User GetById(int id)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                User user = Db.Users.FirstOrDefault(x => x.id == id);
                return user;
            }
        }
        public void UpDate(User user)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                Db.Users.Update(user);
                Db.SaveChanges();
            }
        }
        public void Add(User user)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                user.role = "user";
                Db.Users.Add(user);
                Db.SaveChanges();
            }
        }
        public  List<Project> UserInProject(int iduser)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                List<UserTask> userTasks = Db.UserTasks.Where(x => x.iduser == iduser).ToList();
                List<Task> tasks = new List<Task>();
                foreach (var task in userTasks)
                {
                    tasks.Add(Db.Tasks.FirstOrDefault(x => x.id == task.idtask));
                }
                List<Sprint> sprints = new List<Sprint>();
                foreach (var task in tasks)
                {
                    sprints.Add(Db.Sprints.FirstOrDefault(x => x.id == task.idsprint));
                }
                List<Project> projects = new List<Project>();
                foreach (var sprint in sprints)
                {
                    if(!projects.Contains(Db.Projects.FirstOrDefault(x => x.id == sprint.idproject)))
                    projects.Add(Db.Projects.FirstOrDefault(x => x.id == sprint.idproject));
                }
                return projects;
            }
        }      
    }
}


