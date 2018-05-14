using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("user_open_information")]
    public class UserOpenInformation
    {
        [Key, Column("ID"), Display(Name = "用户号")]
        public string ID { get; set; }

        [Column("EquipmentID"), Display(Name = "设备号")]
        public string EquipmentID { get; set; }
        
        [Column("time"), DataType(DataType.Time), Display(Name = "开门时间")]
        public DateTime? Time { get; set; }

        public UserOpenInformation()
        {
            
        }

        public UserOpenInformation(string id)
        {
            this.ID = id;
        }

    }
}