using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("user")]
    public class User
    {
        [Key, Column("ID")]
        public string ID { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("UnitNumber")]
        public string UnitNumber { get; set; }

        [Column("Status")]
        public int Status { get; set; }

    }
}