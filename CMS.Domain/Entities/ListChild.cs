using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public class ListChild
    {
        private int id;
        private int listId;
        private string label;
        private string link;

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

        [Required(ErrorMessage = "Something went terribly wrong.  Please contact your admin!")]
        public int ListId
        {
            get
            {
                return listId;
            }
            set
            {
                listId = value;
            }
        }

        [Required(ErrorMessage = "Please Enter a Label")]
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }

        [Required(ErrorMessage = "Please Enter a Link")]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", ErrorMessage = "Please entire a complete URL")]
        public string Link
        {
            get
            {
                return link; 
            }
            set
            {
                link = value;
            }
        }
    }
}