namespace FoodForYou.Web.ViewModels.Contacts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ContactFormViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the title of the message")]
        [StringLength(100, ErrorMessage = "The title must be at least {2} and not more than {1} characters.", MinimumLength = 5)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the content of the message")]
        [StringLength(10000, ErrorMessage = "The content must be at least {2} characters.", MinimumLength = 20)]
        public string Content { get; set; }
    }
}
