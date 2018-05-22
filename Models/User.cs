using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("user")]
    public class User
    {
        [Key, Column("ID"), Display(Name = "用户号"), Required(ErrorMessage = "用户号不能为空"), MaxLength(11), MinLength(11)]
        public string ID { get; set; }

        [Column("PASSWORD"), Display(Name = "密码"), Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }


        [Column("Status"), Display(Name = "身份"), Required(ErrorMessage = "请出示身份标识")]
        public Status Status { get; set; }

        public ICollection<UserOpenInformation> Informations { get; set; }

        public User()
        {
            Status = Status.Normal_User;
        }

    }

    public enum Status
    {
        Administrator, Normal_User
    }
}