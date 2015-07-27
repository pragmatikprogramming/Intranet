using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Domain.Abstract
{
    public interface IListRepository
    {
        void Create(CMSList m_List);
        void CreateChild(ListChild m_Child);
        CMSList RetrieveOne(int id);
        ListChild RetrieveChild(int id);
        List<CMSList> RetrieveAll();
        List<ListChild> RetrieveAllChildren(int id);
        void Update(CMSList m_List);
        void UpdateChild(ListChild m_Child);
        void Delete(int id);
        void DeleteChild(int id);
    }
}
