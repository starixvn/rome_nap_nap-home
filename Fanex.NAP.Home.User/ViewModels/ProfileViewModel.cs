using System.ComponentModel.DataAnnotations;

namespace Fanex.NAP.Home.User.ViewModels
{
    public class ProfileViewModel
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "The Email field is invalid.")]
        public string Email { get; set; }

        public int GroupId { get; set; }
    }
}
