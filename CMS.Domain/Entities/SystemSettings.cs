using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Domain.Entities
{
    public class SystemSettings
    {
        private string domainName;
        private byte[] imageBinary;
        private string barColor;
        private byte[] defaultPhoto;


        public string DomainName
        {
            get
            {
                return domainName;
            }
            set
            {
                domainName = value;
            }
        }

        public byte[] ImageBinary
        {
            get
            {
                return imageBinary;
            }
            set
            {
                imageBinary = value;
            }
        }

        public string BarColor
        {
            get
            {
                return barColor;
            }
            set
            {
                barColor = value;
            }
        }

        public byte[] DefaultPhoto
        {
            get
            {
                return defaultPhoto;
            }
            set
            {
                defaultPhoto = value;
            }
        }
    }
}