using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace ToDoList.Models
{
    [Table("Sprint")]
    public class Sprint
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public DateTime timestart { get; set; }
        public DateTime timeend { get; set; }
        public int idproject { get; set; }
        public Sprint()
        {
            
        }
        public Sprint(int id,string name, string description,DateTime timestart,DateTime timeend, int idproject)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.timestart = timestart;
            this.timeend = timeend;
            this.idproject = idproject;
        }
        public Sprint GetById(int idsprint)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Sprint sprint = Db.Sprints.FirstOrDefault(x => x.id == idsprint);
                return sprint;
            }
        }
        public void Add(Sprint sprint, int idproject)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                sprint.idproject = idproject;
                Db.Sprints.Add(sprint);
                Db.SaveChanges();
            }
        }
        public void Delete(int idsprint)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Sprint sprint = new Sprint();
                sprint = sprint.GetById(idsprint);
                Db.Sprints.Remove(sprint);
                Db.SaveChanges();
            }
        }
    }
}
