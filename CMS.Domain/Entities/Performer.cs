using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public class Performer
    {
        private int id;
        private string firstName;
        private string lastName;
        private string address;
        private string phone;
        private string fax;
        private string email;
        private string website;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [Required(ErrorMessage = "Please enter a First Name")]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        [Required(ErrorMessage = "Please enter an Address")]
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        [Required(ErrorMessage = "Please enter a Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number")]
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        public string Fax
        {
            get
            {
                return fax;
            }
            set
            {
                fax = value;
            }
        }

        [Required(ErrorMessage = "Please enter an Email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid Email Address")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        [Required(ErrorMessage = "Please enter a Website")]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", ErrorMessage = "Please enter a valid URL")]
        public string Website
        {
            get
            {
                return website;
            }
            set
            {
                website = value;
            }
        }
    }
}