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
    public class ImageHandlerController : Controller
    {
        IImageRepository ImageRepository;
        ISystemSettingsRepository SystemSettingsRepository;
        IEmployeeDirectoryRepository EmployeeDirectoryRepository;

        public ImageHandlerController(IImageRepository ImageRepo, ISystemSettingsRepository SystemSettingsRepo, IEmployeeDirectoryRepository EmployeeDirectoryRepo)
        {
            ImageRepository = ImageRepo;
            SystemSettingsRepository = SystemSettingsRepo;
            EmployeeDirectoryRepository = EmployeeDirectoryRepo;
        }

        [CMSAuth]
        public ActionResult GetAdminLogo()
        {
            SystemSettings m_Settings = SystemSettingsRepository.GetSystemSettings();
            return File(m_Settings.ImageBinary, "image/png");
        }

        public ActionResult GetEmployeePhoto(int id)
        {
            Employee m_Employee = EmployeeDirectoryRepository.RetrieveOne(id);

            if (m_Employee.Photo != null && m_Employee.Photo.Length > 0)
            {
                return File(m_Employee.Photo, "image/png");
            }
            else
            {
                SystemSettings m_Settings = SystemSettingsRepository.GetSystemSettings();
                return File(m_Settings.DefaultPhoto, "image/png");
            }
        }

    }
}
