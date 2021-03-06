using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimeYazilim.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ContactManager cm = new ContactManager(new EfContactDal());
        ContactValidator cv = new ContactValidator();
        MessageManager mm = new MessageManager(new EfMessageDal());
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }
        public PartialViewResult PartialMessageListMenu()
        {
            string p = (string)Session["WriterMail"];
            var contactvalues = cm.GetList();
            ViewBag.sayi = contactvalues.Count();

            var deger1 = mm.GetListInbox(p);
            ViewBag.sayi1 = deger1.Count();

            var deger2 = mm.GetListSendbox(p);
            ViewBag.sayi2 = deger2.Count();

            var deger3 = mm.GetListDraft(p);
            ViewBag.sayi3 = deger3.Count();

            var deger4 = mm.GetListTrash(p);
            ViewBag.sayi4 = deger4.Count();

            var deger5 = mm.GetReadList(p);
            ViewBag.sayi5 = deger5.Count();

            var deger6 = mm.GetUnReadList(p);
            ViewBag.sayi6 = deger6.Count();
            return PartialView();
        }
    }
}