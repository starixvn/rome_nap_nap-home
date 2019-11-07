using System.ComponentModel.DataAnnotations;

namespace Fanex.NAP.Home.User.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Old Password")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Old Password field must be at least 2 and maximum 50 characters long.")]
        public string OldUserPwd { get; set; }

        [Display(Name = "New Password")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The New Password field must be at least 2 and maximum 50 characters long.")]
        public string UserPwd { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Confirm Password field must be at least 2 and maximum 50 characters long.")]
        [System.ComponentModel.DataAnnotations.Compare("UserPwd")]
        public string ConfirmUserPwd { get; set; }

        public string Token { get; set; }
    }
}
