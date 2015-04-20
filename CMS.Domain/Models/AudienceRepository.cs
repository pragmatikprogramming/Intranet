using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;

namespace CMS.Domain.Models
{
    public class AudienceRepository : IAudienceRepository
    {
        public void Create(Audience m_Audience)
        {
            DBAudience.Create(m_Audience);
        }

        public Audience RetrieveOne(int id)
        {
            return DBAudience.RetrieveOne(id);
        }

        public List<Audience> RetrieveAll()
        {
            return DBAudience.RetrieveAll();
        }

        public void Update(Audience m_Audience)
        {
            DBAudience.Update(m_Audience);
        }

        public void Delete(int id)
        {
            DBAudience.Delete(id);
        }
    }
}