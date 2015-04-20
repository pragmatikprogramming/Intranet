using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;

namespace CMS.Domain.Models
{
    public class ReviewRepository : IReviewRepository
    {
        public void Create(Review m_Review)
        {
            DBReview.Create(m_Review);
        }

        public Review RetrieveOne(int id)
        {
            return DBReview.RetrieveOne(id);
        }

        public List<Review> RetrieveAll(int id)
        {
            return DBReview.RetrieveAll(id);
        }

        public void Update(Review m_Review)
        {
            DBReview.Update(m_Review);
        }

        public void Delete(int id)
        {
            DBReview.Delete(id);
        }
    }
}