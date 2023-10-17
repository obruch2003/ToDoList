using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    [Table("Comments")]
    public class Comments
    {

        [Key]
        public int id { get; set; }
        [Required]
        public string text { get; set; }
        public int iduser { get; set; }

        public int idtask { get; set; }
        public Comments()
        {
            
        }
        public Comments(int id,string text,int iduser)
        {
            this.id = id;
            this.text = text;
            this.iduser = iduser;
        }
    }
}
