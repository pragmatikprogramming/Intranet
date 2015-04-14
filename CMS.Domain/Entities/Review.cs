using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public string Rating
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