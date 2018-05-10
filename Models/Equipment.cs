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

        [Column("WorkingTime")]
        public int WorkingTime { get; set; }

        [Column("OpenNumber"), Required]
        public int OpenNumber { get; set; }
    }
}