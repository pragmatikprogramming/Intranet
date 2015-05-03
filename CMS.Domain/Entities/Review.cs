using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public class Review
    {
        private int id;
        private int actId;
        private string name;
        private string comments;
        private int rating;

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

        public int ActId
        {
            get
            {
                return actId;
            }
            set
            {
                actId = value;
            }
        }

        [Required(ErrorMessage = "Please enter a Name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [Required(ErrorMessage = "Please enter Comments")]
        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

        public int Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
            }
        }
    }
}