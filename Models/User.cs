using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("user")]
    public class User
    {
        [Key, Column("ID"), Display(Name = "用户号"), Required(ErrorMessage = "用户号不能为空")]
        public string ID { get; set; }

        [Column("PASSWORD"), Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        [Column("UnitNumber"), Display(Name = "单元号"), Required(ErrorMessage = "单元号不能为空")]
        public string UnitNumber { get; set; }

        [Column("Status"), Display(Name = "状态号")]
        public int Status { get; set; }

    }
}