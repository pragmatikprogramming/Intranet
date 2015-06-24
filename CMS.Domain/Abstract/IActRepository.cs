using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Domain.Abstract
{
    public interface IActRepository
    {
        void Create(Act m_Act);
        Act RetrieveOne(int id);
        List<Act> RetrieveAll(int performerId);
        void Update(Act m_Act);
        void Delete(int id);
        double GetAverageRatingAct(int id);
        int GetNumReviewsAct(int id);
    }
}
