using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("user_open_information")]
    public class UserOpenInformation
    {
        [Key, Column("ID")]
        public string ID { get; set; }

        [Column("EquipmentID")]
        public string EquipmentID { get; set; }

        [Column("time"), DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}