using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public class Act
    {
        private int id;
        private int performerId;
        private string programTitle;
        private string description;
        private double cost;
        private double duration;
        private List<int> branches;
        private List<int> audiences;
        private string notes;

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

        public int PerformerId
        {
            get
            {
                return performerId;
            }
            set
            {
                performerId = value;
            }
        }

        [Required(ErrorMessage = "Please Enter a program title")]
        public string ProgramTitle
        {
            get
            {
                return programTitle;
            }
            set
            {
                programTitle = value;
            }
        }

        [Required(ErrorMessage = "Please Enter a description")]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        [Required(ErrorMessage = "Please Enter a cost")]
        [RegularExpression(@"\d*\.?\d+%?", ErrorMessage = "Please enter a valid cost")]
        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }

        [Required(ErrorMessage = "Please Enter a duration")]
        [RegularExpression(@"\d*\.?\d+%?", ErrorMessage = "Please enter a valid duration")]
        public double Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        public List<int> Branches
        {
            get
            {
                return branches;
            }
            set
            {
                branches = value;
            }
        }

        public List<int> Audiences
        {
            get
            {
                return audiences;
            }
            set
            {
                audiences = value;
            }
        }

        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
            }
        }
    }
}