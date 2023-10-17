using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using System.Data;
using Microsoft.Extensions.Primitives;

namespace ToDoList.Models
{
    [Table("Task")]
    public class Task
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; } 
        public DateTime timestart { get; set; }
        public DateTime timeend { get; set; }
        public int idsprint { get; set; }
        public string status { get; set; }
        public Task() { }
        public Task(int id,string name, string description, DateTime timestart, DateTime timeend,int idsprint,string status)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.timestart = timestart;
            this.timeend = timeend;
            this.idsprint = idsprint;
            this.status = status;
        }
        public Task GetById(int idtask)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Task task = Db.Tasks.FirstOrDefault(x => x.id == idtask);
                return task;
            }
        }
        public void Add(Task task,int idsprint)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                task.idsprint = idsprint;
                task.status = "incomplete";
                Db.Tasks.Add(task);
                Db.SaveChanges();
            }
        }
        public void Delete(int idtask)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Models.Task task = new Models.Task();
                task = task.GetById(idtask);
                Db.Tasks.Remove(task);
                Db.SaveChanges();
            }
        }
        public void Update(Task task)
        {
            using (ApplicationContext Db = new ApplicationContext())
            { 
                if(task.status == "incomplete")
                {
                    task.status = "complete";
                }
                else
                {
                    task.status = "incomplete";
                }
                Db.Tasks.Update(task);
                Db.SaveChanges();
            }
        }
    }
}
