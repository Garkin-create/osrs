using System.ComponentModel.DataAnnotations;

namespace OSRS.Application.Models.User.Model.Input
{
    public class AddUserInputModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}