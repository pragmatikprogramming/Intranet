using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Domain.Entities
{
    public class Branch
    {
        private int id;
        private string branchName;
        private string abreviation;

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

        public string BranchName
        {
            get 
            { 
                return branchName; 
            }
            set 
            { 
                branchName = value; 
            }
        }

        public string Abreviation
        {
            get
            {
                return abreviation;
            }
            set
            {
                abreviation = value;
            }
        }
    }
}