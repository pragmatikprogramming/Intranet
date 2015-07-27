using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.Domain.HelperClasses;
using CMS.WebUI.Infrastructure;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;

namespace CMS.WebUI.Controllers
{
    public class EmployeeDirectoryController : Controller
    {
        IEmployeeDirectoryRepository EmployeeDirectoryRepository;
        IJobTitleRepository JobTitleRepository;
        ISkillsRegistryRepository SkillsRegistryRepository;
        IImageRepository ImageRepository;
        
        public EmployeeDirectoryController(IEmployeeDirectoryRepository EmployeeDirectoryRepo, IJobTitleRepository JobTitleRepo, ISkillsRegistryRepository SkillsRegistryRepo, IImageRepository ImageRepo)
        {
            EmployeeDirectoryRepository = EmployeeDirectoryRepo;
            JobTitleRepository = JobTitleRepo;
            SkillsRegistryRepository = SkillsRegistryRepo;
            ImageRepository = ImageRepo;
        }

        /*[HttpGet]
        public ActionResult Display(string filter = "")
        {

        }*/

        [CMSAuth]
        [HttpGet]
        public ActionResult Index()
        {
            List<Employee> m_Employees = EmployeeDirectoryRepository.RetrieveAll();
            return View("Index", m_Employees);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult Skills()
        {
            List<SkillsRegistry> m_Skills = new List<SkillsRegistry>();
            try
            {
                m_Skills = SkillsRegistryRepository.RetrieveAll();
            }
            catch
            {
            }

            return View("Skills", m_Skills);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddSkill()
        {
            SkillsRegistry m_Skill = new SkillsRegistry();
            return View("AddSkill", m_Skill);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddSkill(SkillsRegistry m_Skill)
        {
            if (ModelState.IsValid)
            {
                SkillsRegistryRepository.Create(m_Skill);
                return RedirectToAction("Skills");
            }
            else
            {
                return View("AddSkill", m_Skill);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditSkill(int id)
        {
            SkillsRegistry m_Skill = SkillsRegistryRepository.RetrieveOne(id);
            return View("EditSkill", m_Skill);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditSkill(SkillsRegistry m_Skill)
        {
            if (ModelState.IsValid)
            {
                SkillsRegistryRepository.Update(m_Skill);
                return RedirectToAction("Skills");
            }
            else
            {
                return View("EditSkill", m_Skill);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeleteSkill(int id)
        {
            SkillsRegistryRepository.Delete(id);
            return RedirectToAction("Skills");
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult JobTitles()
        {
            List<JobTitles> m_JobTitles = JobTitleRepository.RetrieveAll();
            return View("JobTitles", m_JobTitles);
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddTitle()
        {
            JobTitles m_JobTitle = new JobTitles();
            return View("AddJobTitle", m_JobTitle);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddTitle(JobTitles m_JobTitle)
        {
            if (ModelState.IsValid)
            {
                JobTitleRepository.Create(m_JobTitle);
                return RedirectToAction("JobTitles");
            }
            else
            {
                return View("AddJobTitle", m_JobTitle);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditTitle(int id)
        {
            JobTitles m_JobTitle = JobTitleRepository.RetrieveOne(id);
            return View("EditJobTitle", m_JobTitle);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditTitle(JobTitles m_JobTitle)
        {
            if (ModelState.IsValid)
            {
                JobTitleRepository.Update(m_JobTitle);
                return RedirectToAction("JobTitles");
            }
            else
            {
                return View("JobTitles", m_JobTitle);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeleteTitle(int id)
        {
            JobTitleRepository.Delete(id);
            return RedirectToAction("JobTitles");
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult AddEmployee()
        {
            List<SkillsRegistry> m_Skills = SkillsRegistryRepository.RetrieveAll();
            List<JobTitles> m_JobTitles = JobTitleRepository.RetrieveAll();
            List<Branch> m_Branches = Utility.BranchNames();

            ViewBag.Skills = m_Skills;
            ViewBag.JobTitles = m_JobTitles;
            ViewBag.Branches = m_Branches;

            Employee m_Employee = new Employee();

            return View("AddEmployee", m_Employee);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult AddEmployee(Employee m_Employee, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                if (fileUpload.ContentType != "image/jpg" && fileUpload.ContentType != "image/jpeg" && fileUpload.ContentType != "image/png" && fileUpload.ContentType != "image/gif" && fileUpload.ContentType != "image/bmp")
                {
                    ModelState.AddModelError("Name", "An invalid file type was uploaded.  Please select a different file");
                }
            }

            if (ModelState.IsValid)
            {
                m_Employee.Photo = ImageRepository.ToBinary(fileUpload);
                EmployeeDirectoryRepository.Create(m_Employee);
                return RedirectToAction("Index");

            }
            else
            {
                List<SkillsRegistry> m_Skills = SkillsRegistryRepository.RetrieveAll();
                List<JobTitles> m_JobTitles = JobTitleRepository.RetrieveAll();
                List<Branch> m_Branches = Utility.BranchNames();

                ViewBag.Skills = m_Skills;
                ViewBag.JobTitles = m_JobTitles;
                ViewBag.Branches = m_Branches;

                return View("AddEmployee", m_Employee);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            Employee m_Employee = EmployeeDirectoryRepository.RetrieveOne(id);
            List<SkillsRegistry> m_Skills = SkillsRegistryRepository.RetrieveAll();
            List<JobTitles> m_JobTitles = JobTitleRepository.RetrieveAll();
            List<Branch> m_Branches = Utility.BranchNames();

            ViewBag.Skills = m_Skills;
            ViewBag.JobTitles = m_JobTitles;
            ViewBag.Branches = m_Branches;

            return View("EditEmployee", m_Employee);
        }

        [CMSAuth]
        [HttpPost]
        public ActionResult EditEmployee(Employee m_Employee, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                if (fileUpload.ContentType != "image/jpg" && fileUpload.ContentType != "image/jpeg" && fileUpload.ContentType != "image/png" && fileUpload.ContentType != "image/gif" && fileUpload.ContentType != "image/bmp")
                {
                    ModelState.AddModelError("Name", "An invalid file type was uploaded.  Please select a different file");
                }
            }
            if (ModelState.IsValid)
            {
                m_Employee.Photo = ImageRepository.ToBinary(fileUpload);
                EmployeeDirectoryRepository.Update(m_Employee);
                return RedirectToAction("Index");
            }
            else
            {
                List<SkillsRegistry> m_Skills = SkillsRegistryRepository.RetrieveAll();
                List<JobTitles> m_JobTitles = JobTitleRepository.RetrieveAll();
                List<Branch> m_Branches = Utility.BranchNames();

                ViewBag.Skills = m_Skills;
                ViewBag.JobTitles = m_JobTitles;
                ViewBag.Branches = m_Branches;

                return View("EditEmployee", m_Employee);
            }
        }

        [CMSAuth]
        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            EmployeeDirectoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult getLocation(int id)
        {
            ViewBag.Location = EmployeeDirectoryRepository.getLocation(id);
            return View("getLocation");
        }

        public ActionResult getJobTitle(int id)
        {
            ViewBag.JobTitle = EmployeeDirectoryRepository.getJobTitle(id);
            return View("getJobTitle");
        }


        [HttpGet]
        public ActionResult Display(int id)
        {
            List<SkillsRegistry> m_Skills = SkillsRegistryRepository.RetrieveAll();
            ViewBag.Skills = m_Skills;
            Employee m_Employee = EmployeeDirectoryRepository.RetrieveOne(id);
            return View("Display", m_Employee);
        }

        [HttpGet]
        public ActionResult Filter(int m_Filter, string m_Order)
        {
            List<Employee> m_Employee = EmployeeDirectoryRepository.getEmployeeFiltered(m_Filter, m_Order);
            ViewBag.Order = m_Order;
            ViewBag.Filter = m_Filter;
            return View("Filter", m_Employee);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee m_Employee = EmployeeDirectoryRepository.RetrieveOne(id);
            List<SkillsRegistry> m_Skills = SkillsRegistryRepository.RetrieveAll();
            List<JobTitles> m_JobTitles = JobTitleRepository.RetrieveAll();
            List<Branch> m_Branches = Utility.BranchNames();

            ViewBag.Skills = m_Skills;
            ViewBag.JobTitles = m_JobTitles;
            ViewBag.Branches = m_Branches;

            return View("Edit", m_Employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee m_Employee, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                if (fileUpload.ContentType != "image/jpg" && fileUpload.ContentType != "image/jpeg" && fileUpload.ContentType != "image/png" && fileUpload.ContentType != "image/gif" && fileUpload.ContentType != "image/bmp")
                {
                    ModelState.AddModelError("Name", "An invalid file type was uploaded.  Please select a different file");
                }
            }
            if (ModelState.IsValid)
            {
                m_Employee.Photo = ImageRepository.ToBinary(fileUpload);
                EmployeeDirectoryRepository.Update(m_Employee);
                return RedirectToAction("Display", new { id = m_Employee.Id });
            }
            else
            {
                List<SkillsRegistry> m_Skills = SkillsRegistryRepository.RetrieveAll();
                List<JobTitles> m_JobTitles = JobTitleRepository.RetrieveAll();
                List<Branch> m_Branches = Utility.BranchNames();

                ViewBag.Skills = m_Skills;
                ViewBag.JobTitles = m_JobTitles;
                ViewBag.Branches = m_Branches;

                return View("Edit", m_Employee);
            }
        }

        [HttpPost]
        public ActionResult ReportsFilter(List<int> JobTitles, List<int> Skills, List<int> Locations)
        {
            if(JobTitles == null && Skills == null && Locations == null)
            {
                return Redirect("/employee-directory");
            }
            else
            {
                List<Employee> m_Employees = EmployeeDirectoryRepository.ReportsFilter(JobTitles, Skills, Locations);
                ViewBag.JobTitles = JobTitleRepository.RetrieveAll();
                ViewBag.Skills = SkillsRegistryRepository.RetrieveAll();
                ViewBag.Locations = Utility.BranchNames();
                return View("ReportsFilter", m_Employees);
            }
        }

    }
}
