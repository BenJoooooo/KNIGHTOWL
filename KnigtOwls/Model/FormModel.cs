using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KnigtOwls.Model
{
    public class FormModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MinLength(5, ErrorMessage = "First name must be atleast 5 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(5, ErrorMessage = "Last name must be atleast 5 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MinLength(10, ErrorMessage = "Email must be atleast 10 characters")]
        public string Email  { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Input is not a valid phone number")]
        [MinLength(10, ErrorMessage = "Phone number must be atleast 10 characters")]
        public string Contact { get; set; }

        public bool terms { get; set; }

        public ICommand SubmitCommand { get; }

        public  FormModel() 
        {
            SubmitCommand = new Command(async () =>
            {
                await App.Current.MainPage.DisplayAlert("Submitted", "Successfull", "Ok");
            });
        }
    }
}
