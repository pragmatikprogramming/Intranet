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
    public class PerformerDirectoryController : Controller
    {
        IActRepository ActRepository;
        IAudienceRepository AudienceRepository;
        IPerformerRepository PerformerRepository;
        IReviewRepository ReviewRepository;

        public PerformerDirectoryController(IActRepository ActRepo, IAudienceRepository AudienceRepo, IPerformerRepository PerformerRepo, IReviewRepository ReviewRepo)
        {
            ActRepository = ActRepo;
            AudienceRepository = AudienceRepo;
            PerformerRepository = PerformerRepo;
            ReviewRepository = ReviewRepo;
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult Index()
        {
            List<Performer> m_Performers = PerformerRepository.RetrieveAll();
            return View("Index", m_Performers);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddPerformer()
        {
            Performer m_Performer = new Performer();
            return View("AddPerformer", m_Performer);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddPerformer(Performer m_Performer)
        {
            if (ModelState.IsValid)
            {
                PerformerRepository.Create(m_Performer);
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddPerformer", m_Performer);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditPerformer(int id)
        {
            Performer m_Performer = PerformerRepository.RetrieveOne(id);
            return View("EditPerformer", m_Performer);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditPerformer(Performer m_Performer)
        {
            if(ModelState.IsValid)
            {
                PerformerRepository.Update(m_Performer);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditPerformer", m_Performer);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeletePerformer(int id)
        {
            PerformerRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult Acts(int id)
        {
            ViewBag.PerformerId = id;
            List<Act> m_Acts = ActRepository.RetrieveAll(id);
            return View("Acts", m_Acts);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddAct(int id)
        {
            ViewBag.PerformerId = id;
            ViewBag.Audiences = AudienceRepository.RetrieveAll();
            ViewBag.Locations = Utility.BranchNames();
            Act m_Act = new Act();
            return View("AddAct", m_Act);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddAct(Act m_Act)
        {
            ViewBag.PerformerId = m_Act.PerformerId;
            if (ModelState.IsValid)
            {
                ActRepository.Create(m_Act);
                return RedirectToAction("Acts", new { id = m_Act.PerformerId });
            }
            else
            {
                ViewBag.PerformerId = m_Act.Id;
                ViewBag.Audiences = AudienceRepository.RetrieveAll();
                ViewBag.Locations = Utility.BranchNames();
                return View("AddAct", m_Act);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditAct(int id)
        {
            ViewBag.Audiences = AudienceRepository.RetrieveAll();
            ViewBag.Locations = Utility.BranchNames();
            Act m_Act = ActRepository.RetrieveOne(id);
            return View("EditAct", m_Act);

        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditAct(Act m_Act)
        {
            if (ModelState.IsValid)
            {
                ActRepository.Update(m_Act);
                return RedirectToAction("Acts", new { id = m_Act.PerformerId });
            }
            else
            {
                ViewBag.Audiences = AudienceRepository.RetrieveAll();
                ViewBag.Locations = Utility.BranchNames();
                return View("EditAct", m_Act);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeleteAct(int id)
        {
            Act m_Act = ActRepository.RetrieveOne(id);
            ActRepository.Delete(id);
            return RedirectToAction("Acts", new{id = m_Act.PerformerId});
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult Reviews(int id)
        {
            ViewBag.ActId = id;
            List<Review> m_Reviews = ReviewRepository.RetrieveAll(id);
            return View("Reviews", m_Reviews);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddReview(int id)
        {
            ViewBag.ActId = id;
            Review m_Review = new Review();
            return View("AddReview", m_Review);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddReview(Review m_Review)
        {
            if (ModelState.IsValid)
            {
                ReviewRepository.Create(m_Review);
                return RedirectToAction("Reviews", new { id = m_Review.ActId });
            }
            else
            {
                return View("AddReview", m_Review);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditReview(int id)
        {
            Review m_Review = ReviewRepository.RetrieveOne(id);
            ViewBag.ActId = m_Review.ActId;
            return View("EditReview", m_Review);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditReview(Review m_Review)
        {
            if(ModelState.IsValid)
            {
                ReviewRepository.Update(m_Review);
                return RedirectToAction("Reviews", new { id = m_Review.ActId });
            }
            else
            {
                return View("EditReview", m_Review);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeleteReview(int id)
        {
            Review m_Review = ReviewRepository.RetrieveOne(id);
            ReviewRepository.Delete(id);
            return RedirectToAction("Reviews", new { id = m_Review.ActId });
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult Audiences()
        {
            List<Audience> m_Audiences = AudienceRepository.RetrieveAll();
            return View("Audiences", m_Audiences);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddAudience()
        {
            Audience m_Audience = new Audience();
            return View("AddAudience", m_Audience);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddAudience(Audience m_Audience)
        {
            if (ModelState.IsValid)
            {
                AudienceRepository.Create(m_Audience);
                return RedirectToAction("Audiences");
            }
            else
            {
                return View("AddAudience", m_Audience);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditAudience(int id)
        {
            Audience m_Audience = AudienceRepository.RetrieveOne(id);
            return View("EditAudience", m_Audience);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditAudience(Audience m_Audience)
        {
            if(ModelState.IsValid)
            {
                AudienceRepository.Update(m_Audience);
                return RedirectToAction("Audiences");
            }
            else
            {
                return View("EditAudience", m_Audience);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeleteAudience(int id)
        {
            AudienceRepository.Delete(id);
            return RedirectToAction("Audiences");
        }

        [HttpGet]
        public ActionResult Display()
        {
            return View("Display");
        }
    }
}
