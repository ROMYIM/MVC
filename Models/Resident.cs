using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("resident")]
    public class Resident
    {
        [Key, Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("UnitNumber")]
        public int UnitNumber { get; set; }
    }
}