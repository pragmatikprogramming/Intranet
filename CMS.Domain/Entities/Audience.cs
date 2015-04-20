using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Domain.Entities
{
    public class Audience
    {
        private int id;
        private string audience;

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

        public string m_Audience
        {
            get
            {
                return audience;
            }
            set
            {
                audience = value;
            }
        }
    }
}