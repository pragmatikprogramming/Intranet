using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Domain.Abstract
{
    public interface IReviewRepository
    {
        void Create(Review m_Review);
        Review RetrieveOne(int id);
        List<Review> RetrieveAll(int id);
        void Update(Review m_Review);
        void Delete(int id);
    }
}
