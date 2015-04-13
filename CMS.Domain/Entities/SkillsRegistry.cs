using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public class SkillsRegistry
    {
        private int id;
        private string skillName;

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

        [Required(ErrorMessage = "Please enter a SkillName")]
        public string SkillName
        {
            get
            {
                return skillName;
            }
            set
            {
                skillName = value;
            }
        }
    }
}