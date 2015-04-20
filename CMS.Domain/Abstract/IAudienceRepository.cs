using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Domain.Abstract
{
    public interface IAudienceRepository
    {
        void Create(Audience m_Audience);
        Audience RetrieveOne(int id);
        List<Audience> RetrieveAll();
        void Update(Audience m_Audience);
        void Delete(int id);
    }
}
