using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("equipment")]
    public class Equipment
    {
        [Key, Column("EquipmentID"), Required]
        public string EquipmentID { get; set; }

        [Column("WorkTime")]
        public int WorkingTime { get; set; }

        [Column("Time"), DataType(DataType.Time)]
        public DateTime Time { get; set; }
    }
}