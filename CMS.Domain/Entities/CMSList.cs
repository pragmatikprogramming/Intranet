using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public class CMSList
    {
        private int id;
        private string listName;
        private int listType;
        private Dictionary<string, string> listLinks;

        /*
        LIST TYPES
        1 = Forms List
         
        */

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

        [Required(ErrorMessage = "Please Enter a Name")]
        public string ListName
        {
            get
            {
                return listName;
            }
            set
            {
                listName = value;
            }
        }

        public int ListType
        {
            get
            {
                return listType;
            }
            set
            {
                listType = value;
            }
        }
    }
}