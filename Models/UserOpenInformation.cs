using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("user_open_information")]
    public class UserOpenInformation
    {
        [Key, Column("OpenNumber"), Display(Name = "开门号")]
        public int OpenNumber { get; set; }

        [Column("EquipmentID"), Display(Name = "设备号")]
        public string EquipmentID { get; set; }
        [ForeignKey("EquipmentID")]
        public Equipment Equipment { get; set; }
        
        [Column("time"), DataType(DataType.DateTime), Display(Name = "开门时间")]
        public DateTime? Time { get; set; }

        [Column("Result"), Display(Name = "开门结果")]
        public bool Result { get; set; }

        [Column("UserID"), Display(Name = "用户号")]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }


        public UserOpenInformation()
        {
            
        }

        public UserOpenInformation(User user, int openNumber)
        {
            this.OpenNumber = openNumber;
            this.User = user;
            this.UserID = user.ID;
            this.Result = false;
        }
    }

    public enum OpenResult
    {
        Failed, Success
    }
}