using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;

namespace CMS.Domain.Models
{
    public class ActRepository : IActRepository
    {
        public void Create(Act m_Act)
        {
            DBAct.Create(m_Act);
        }

        public Act RetrieveOne(int id)
        {
            return DBAct.RetrieveOne(id);
        }

        public List<Act> RetrieveAll(int performerId)
        {
            return DBAct.RetrieveAll(performerId);
        }

        public void Update(Act m_Act)
        {
            DBAct.Update(m_Act);
        }

        public void Delete(int id)
        {
            DBAct.Delete(id);
        }

    }
}