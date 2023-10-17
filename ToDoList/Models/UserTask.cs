using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ToDoList.Models
{
    [Table("UserTask")]
    public class UserTask
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int iduser { get; set; }
        public int idtask { get; set; }
        public UserTask()
        {
            
        }
        public UserTask(int id,int iduser,int idtask)
        {
            this.id = id;
            this.iduser = iduser;
            this.idtask = idtask;
        }
        public UserTask GetById(int id)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                UserTask usertask = Db.UserTasks.FirstOrDefault(x => x.id == id);
                return usertask;
            }
        }
        public void Add(int iduser, int idtask)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                UserTask usertask = new UserTask();
                usertask.iduser = iduser;
                usertask.idtask = idtask;
                bool f= false;
                foreach(var task in Db.UserTasks.ToList())
                {
                    if(task.iduser == iduser&&task.idtask==idtask)
                    {
                        f= true; break;
                    }    
                }
                if(!f)
                {
                    Db.UserTasks.Add(usertask);
                    Db.SaveChanges();
                }
                
            }
        }
        public void Delete(int id)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                UserTask usertask = new UserTask();
                usertask = usertask.GetById(id);
                Db.UserTasks.Remove(usertask);
                Db.SaveChanges();
            }
        }
    }
}
