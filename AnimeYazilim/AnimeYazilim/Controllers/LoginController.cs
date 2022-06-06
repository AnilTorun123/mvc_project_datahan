using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft;
using System.Net;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace AnimeYazilim.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        WriterLoginManager wlm = new WriterLoginManager(new EfWriterDal());
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            Context c = new Context();
            var adminuserinfo = c.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);
            if(adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName,false);
                Session["AdminUserName"] = adminuserinfo.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                //mvc proje kampı 70: 8.05 / eğer kullanıcı adı yada şifre herhangi biri doğru değil ise burda bir mesaj verdirmemizi istiyor. Bir pop-up olabilir viewbaglerle tasınan olabilir bir sekilde bir mesaj verdirip kullanıcıyı bilgilendirmeliyiz.
                //baska bir view acılıp bunun için else kısmına o actıgımız sayfaya yönlendirebiliriz. en kolayı bu herhalde.

                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public ActionResult AnimeLoginIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AnimeLoginIndex(Writer p)
        {

            Context c = new Context();
            var writeruserinfo = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
            if (writeruserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;
                return RedirectToAction("Index", "Anime");
            }
            else
            {
                return RedirectToAction("AnimeLoginIndex");
            }

        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            //Context c = new Context();
            //var writeruserinfo = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
            var writeruserinfo = wlm.GetWriter(p.WriterMail, p.WriterPassword);
            if (writeruserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                return RedirectToAction("WriterLogin");
            }
        }

        //
        [HttpPost]
        public ActionResult FormSubmit()
        {
            Context c = new Context();
            var response = Request["g-recaptcha-response"];
            string secretKey = "6LdV1SMgAAAAAA4MwZBJ5bhdPP2MrA4jFrIionp-";
            var client = new WebClient();

            ViewData["Message"] = "Google reCaptcha validation success";

            return View("Index");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}

// Sitenin iki farklı bölümü için 2 farklı login giriş sistemi yapmak istedim tasarımsal olarak. Fakat 2 sinden birini kabul ediyor web config kısmında ki bu bölüm;

    //< authentication mode = "Forms" >
       //< forms loginUrl = "/Login/Index/" ></ forms >
      //</ authentication >    

//fakat çalışıyor webconfig deki kısmı değiştirmemiş olsamda. Neden?? sadece tahminimce üstteki controllerdakiler ile örtüştüğü için sorun yaratmıyor. ama tamamen bağımsız olsun diye yola cıkmıstım fakat bir sorunda yok yani. bilmiyorum bir seyler yanlıs gibi.. ama >HİÇ BİR SORUN YOK< Hatalarla boğuşmadan rahat edemiyormuyum ne..