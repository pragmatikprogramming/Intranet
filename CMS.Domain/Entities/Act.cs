using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Domain.Entities
{
    public class Act
    {
        private int id;
        private int performerId;
        private string programTitle;
        private string description;
        private float cost;
        private float duration;
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

        public float Cost
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

        public float Duration
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