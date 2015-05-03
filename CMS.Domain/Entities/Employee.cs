using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public class Employee
    {
        private int id;
        private string firstName;
        private string lastName;
        private int jobTitle;
        private string phone;
        private string intercom;
        private string fax;
        private string email;
        private int location;
        private byte[] photo;
        private List<int> skills;
        private string info;
        private string about;



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

        [Required(ErrorMessage = "Please enter a Last Name")]
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

        public int JobTitle
        {
            get
            {
                return jobTitle;
            }
            set
            {
                jobTitle = value;
            }
        }

        [Required(ErrorMessage = "Please enter a Phone Number")]
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

        public string Intercom
        {
            get
            {
                return intercom;
            }
            set
            {
                intercom = value;
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

        public int Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }     
        }

        public byte[] Photo
        {
            get
            {
                return photo;
            }
            set
            {
                photo = value;
            }
        }

        public List<int> Skills
        {
            get
            {
                return skills;
            }
            set
            {
                skills = value;
            }
        }

        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
            }
        }

        public string About
        {
            get
            {
                return about;
            }
            set
            {
                about = value;

            }
        }
    }
}