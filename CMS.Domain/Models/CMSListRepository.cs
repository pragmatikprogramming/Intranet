using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.DataAccess;
using CMS.Domain.Entities;

namespace CMS.Domain.Models
{
    public class CMSListRepository : IListRepository
    {
        public void Create(CMSList m_List)
        {
            DBListRepository.Create(m_List);
        }

        public void CreateChild(ListChild m_Child)
        {
            DBListChildren.CreateChild(m_Child);
        }

        public CMSList RetrieveOne(int id)
        {
            CMSList m_List = DBListRepository.RetrieveOne(id);
            return m_List;
        }

        public ListChild RetrieveChild(int id)
        {
            ListChild m_Child = DBListChildren.RetrieveChild(id);
            return m_Child;
        }

        public List<CMSList> RetrieveAll()
        {
            List<CMSList> m_Lists = DBListRepository.RetrieveAll();
            return m_Lists;
        }

        public List<ListChild> RetrieveAllChildren(int id)
        {
            List<ListChild> m_Children = DBListChildren.RetrieveAllChildren(id);
            return m_Children;
        }

        public void Update(CMSList m_List)
        {
            DBListRepository.Update(m_List);
        }

        public void UpdateChild(ListChild m_Child)
        {
            DBListChildren.UpdateChild(m_Child);
        }

        public void Delete(int id)
        {
            DBListRepository.Delete(id);
        }

        public void DeleteChild(int id)
        {
            DBListChildren.DeleteChild(id);
        }
    }
}