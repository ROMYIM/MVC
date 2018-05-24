using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("equipment")]
    public class Equipment
    {
        [Key, Column("EquipmentID"), Required, Display(Name = "设备号")]
        public string EquipmentID { get; set; }

        [Column("WorkingTime"), Display(Name = "工作次数")]
        public int WorkingTime { get; set; }

        public ICollection<UserOpenInformation> Informations { get; set; }
    }
}