using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;

namespace CMS.Domain.Models
{
    public class SkillsRegistryRepository : ISkillsRegistryRepository
    {
        public void Create(SkillsRegistry m_Skill)
        {
            DBSkillsRegistry.Create(m_Skill);
        }

        public SkillsRegistry RetrieveOne(int id)
        {
            SkillsRegistry m_Skill = DBSkillsRegistry.RetrieveOne(id);
            return m_Skill;
        }

        public List<SkillsRegistry> RetrieveAll()
        {
            List<SkillsRegistry> m_Skills = DBSkillsRegistry.RetrieveAll();
            return m_Skills;
        }

        public void Update(SkillsRegistry m_Skill)
        {
            DBSkillsRegistry.Update(m_Skill);
        }

        public void Delete(int id)
        {
            DBSkillsRegistry.Delete(id);
        }
    }
}