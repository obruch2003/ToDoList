using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    [Table("Files")]
    public class Files
    {
        
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public int idtask{ get; set; }

        [NotMapped]
        public IFormFile FileToUpload { get; set; }
    }
}

