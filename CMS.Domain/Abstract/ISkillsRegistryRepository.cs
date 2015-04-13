using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Domain.Abstract
{
    public interface ISkillsRegistryRepository
    {
        void Create(SkillsRegistry m_Skill);
        SkillsRegistry RetrieveOne(int id);
        List<SkillsRegistry> RetrieveAll();
        void Update(SkillsRegistry m_Skill);
        void Delete(int id);
    }
}
