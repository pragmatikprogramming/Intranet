using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public class JobTitles
    {
        private int id;
        private string jobTitle;

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

        [Required(ErrorMessage = "Please enter a Job Title")]
        public string JobTitle
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
    }
}