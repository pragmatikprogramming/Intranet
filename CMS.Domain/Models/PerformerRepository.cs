using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;

namespace CMS.Domain.Models
{
    public class PerformerRepository : IPerformerRepository
    {
        public void Create(Performer m_Performer)
        {
            DBPerformer.Create(m_Performer);
        }

        public Performer RetrieveOne(int id)
        {
            return DBPerformer.RetrieveOne(id);
        }

        public List<Performer> RetrieveAll()
        {
            return DBPerformer.RetrieveAll();
        }

        public void Update(Performer m_Performer)
        {
            DBPerformer.Update(m_Performer);
        }

        public void Delete(int id)
        {
            DBPerformer.Delete(id);
        }

        public List<Performer> FilterPerformer(int m_Filter, string m_Order)
        {
            List<Performer> m_Performers = DBPerformer.RetrieveFiltered(m_Filter, m_Order);
            return m_Performers;
        }

        public double GetAverageRating(int id)
        {
            double avg = DBPerformer.getAverageRating(id);
            return avg;
        }

        public int GetNumReviews(int id)
        {
            int num = DBPerformer.numReviews(id);
            return num;
        }

    }
}