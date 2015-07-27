using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using CMS.WebUI.Infrastructure;

namespace CMS.WebUI.Controllers
{
    public class ListController : Controller
    {
        IListRepository ListRepository;

        public ListController(IListRepository ListRepo)
        {
            ListRepository = ListRepo;
        }


        [CMSAuth]
        [HttpGet]
        public ActionResult Index()
        {
            List<CMSList> m_Lists = ListRepository.RetrieveAll();
            return View("Index", m_Lists);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DisplayChildren(int id)
        {
            ViewBag.ListId = id;
            List<ListChild> m_Children = ListRepository.RetrieveAllChildren(id);
            return View("DisplayChildren", m_Children);
        }

        public ActionResult Display(int id)
        {
            ViewBag.ListId = id;
            List<ListChild> m_Children = ListRepository.RetrieveAllChildren(id);
            return View("Display", m_Children);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddList()
        {
            CMSList m_List = new CMSList();
            return View("AddList", m_List);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddList(CMSList m_List)
        {
            if (ModelState.IsValid)
            {
                ListRepository.Create(m_List);
                return Redirect("/List/Index");
            }
            else
            {
                return View("AddList", m_List);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddChild(int id)
        {
            ViewBag.ListId = id;
            ListChild m_Child = new ListChild();
            return View("AddChild", m_Child);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddChild(ListChild m_Child)
        {
            if(ModelState.IsValid)
            {
                ListRepository.CreateChild(m_Child);
                return Redirect("/List/DisplayChildren/" + m_Child.ListId);
            }
            else
            {
                ViewBag.ListId = m_Child.ListId;
                return View("AddChild", m_Child);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditList(int id)
        {
            CMSList m_List = ListRepository.RetrieveOne(id);
            return View("EditList", m_List);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditList(CMSList m_List)
        {
            if(ModelState.IsValid)
            {
                ListRepository.Update(m_List);
                return Redirect("/List/Index");
            }
            else
            {
                return View("EditLIst", m_List);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditChild(int id)
        {
            ListChild m_Child = ListRepository.RetrieveChild(id);
            return View("EditChild", m_Child);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditChild(ListChild m_Child)
        {
            if(ModelState.IsValid)
            {
                ListRepository.UpdateChild(m_Child);
                return Redirect("/List/DisplayChildren/" + m_Child.ListId);
            }
            else
            {
                return View("EditChild", m_Child);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeleteList(int id)
        {
            ListRepository.Delete(id);
            return Redirect("/List/Index");
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeleteChild(int id)
        {
            ListChild m_Child = ListRepository.RetrieveChild(id);
            ListRepository.DeleteChild(id);
            return Redirect("/List/DisplayChildren/" + m_Child.ListId);
        }

    }
}
