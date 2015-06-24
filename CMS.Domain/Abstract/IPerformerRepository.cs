using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Domain.Abstract
{
    public interface IPerformerRepository
    {
        void Create(Performer m_Performer);
        Performer RetrieveOne(int id);
        List<Performer> RetrieveAll();
        void Update(Performer m_Performer);
        void Delete(int id);
        List<Performer> FilterPerformer(int m_Filter, string m_Order);
        double GetAverageRating(int id);
        int GetNumReviews(int id);
    }
}
