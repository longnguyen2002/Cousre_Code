using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class SettingSystemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/SettingSystem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Parial_System()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel res)
        {
            SystemSetting set = null;
            var checkTitle = db.systemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            if (checkTitle == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = res.SettingTitle;
                db.systemSettings.Add(set);
            }
            else
            {
                checkTitle.SettingValue = res.SettingTitle;
                db.Entry(checkTitle).State = System.Data.Entity.EntityState.Modified;
            }
            //Logo
            var checkLogo = db.systemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = res.SettingLogo;
                db.systemSettings.Add(set);
            }
            else
            {
                checkLogo.SettingValue = res.SettingLogo;
                db.Entry(checkLogo).State=System.Data.Entity.EntityState.Modified;  
            }
            //Email
            var checkEmail = db.systemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (checkEmail == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = res.SettingEmail;
                db.systemSettings.Add(set);
            }
            else
            {
                checkEmail.SettingValue = res.SettingEmail;
                db.Entry(checkEmail).State = System.Data.Entity.EntityState.Modified;
            }
            //Hotline
            var checkHotline = db.systemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (checkHotline == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = res.SettingHotline;
                db.systemSettings.Add(set);
            }
            else
            {
                checkHotline.SettingValue = res.SettingHotline;
                db.Entry(checkHotline).State = System.Data.Entity.EntityState.Modified;
            }
            //TitleSeo
            var checkTitleSeo = db.systemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (checkTitleSeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitleSeo";
                set.SettingValue = res.SettingTitleSeo;
                db.systemSettings.Add(set);
            }
            //DessSeo
            var checkDesSeo = db.systemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (checkDesSeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingDesSeo";
                set.SettingValue = res.SettingDesSeo;
                db.systemSettings.Add(set);
            }
            else
            {
                checkDesSeo.SettingValue = res.SettingDesSeo;
                db.Entry(checkDesSeo).State = System.Data.Entity.EntityState.Modified;
            }
            //KeySeo
            var CheckKeySeo = db.systemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (CheckKeySeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingKeySeo";
                set.SettingValue= res.SettingKeySeo;
                db.systemSettings.Add(set);
            }
            else
            {
                CheckKeySeo.SettingValue = res.SettingKeySeo;
                db.Entry(CheckKeySeo).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
             
            return View("Index");
        }
    }
}
