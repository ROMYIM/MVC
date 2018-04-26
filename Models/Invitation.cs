using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    [Table("invitation")]
    public class Invitation
    {
        [Key, Column("InvitationCode")]
        public string InvitationCode { get; set; }
    }
}