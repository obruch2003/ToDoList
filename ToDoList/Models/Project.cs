using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public Project()
        {
            
        }
        public Project(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        public Project GetById(int id)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Project project = Db.Projects.FirstOrDefault(x => x.id == id);
                return project;
            }
        }
        public void Add(Project project)
        {
            using(ApplicationContext Db = new ApplicationContext())
            {
                
                Db.Projects.Add(project);
                Db.SaveChanges();
            }
        }
        public void Delete(int idproject)
        {
            using (ApplicationContext Db = new ApplicationContext())
            {
                Project project = new Project();
                project = project.GetById(idproject);
                Db.Projects.Remove(project);
                Db.SaveChanges();
            }
        }
    }
}
