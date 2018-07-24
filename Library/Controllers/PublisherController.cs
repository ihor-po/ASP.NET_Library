using Library.Models;
using Library.Models.Interfaces;
using Library.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class PublisherController : Controller
    {
        IRepository<PublisherModel> pRepo = PublishersRepo.Repository;
        IRepository<BookModel> bRepo = BooksRepo.Repository;

        // GET: Publisher
        public ActionResult Index()
        {
            ViewBag.Title = "Library :: Издатели";
            ViewBag.Caption = "Издатели";
            ViewBag.Url = "/Publisher/CreatePublisher";

            return View(pRepo.GetAll());
        }

        // GET: Create publisher
        [HttpGet]
        public ViewResult CreatePublisher()
        {
            ViewBag.Title = "Library :: Издатели";
            ViewBag.Caption = "Создать издателя";
            PublisherModel newPublisher = new PublisherModel();

            return View("PublisherForm", newPublisher);
        }

        //POST: Create publisher
        [HttpPost]
        public ActionResult CreatePublisher(string Name)
        {
            PublisherModel publisher = null;

            ViewBag.Title = "Library :: Издатели";
            ViewBag.Caption = "Создать издателя";
            

            publisher = new PublisherModel();

            string text = @"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$";
            publisher.Name = Name;


            if (!Regex.Match(publisher.Name, text).Success)
            {
                ViewBag.NameError = "Не менее 2 символов; Цифры не допустимы.";
                ViewBag.Publisher = publisher;

                return View("PublisherForm");
            }
        
            publisher.Id = (pRepo.GetAll().LastOrDefault()?.Id ?? 0) + 1;

            pRepo.Add(publisher);

            return RedirectToAction("EditPublisher", new { id = publisher.Id});
        }

        //GET: Edit Publisher
        [HttpGet]
        public ViewResult EditPublisher(int id)
        {
            ViewBag.Title = "MVC CRUD :: Редакирование издателя";
            ViewBag.Caption = "Редактирование издателя";
            ViewBag.Publisher = pRepo.GetOne(id);

            return View("PublisherForm");
        }

        //GET: Edit Publisher
        [HttpPost]
        public ActionResult EditPublisher(int id, string Name)
        {
            PublisherModel publisher = pRepo.GetOne(id);

            ViewBag.Title = "MVC CRUD :: Редакирование роли";
            ViewBag.Caption = "Редактирование издателя";
            ViewBag.Publisher = publisher;

            string text = @"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$";

            if (!Regex.Match(Name, text).Success)
            {
                ViewBag.NameError = "Не менее 2 символов; Цифры не допустимы.";
                return View("PublisherForm");
            }

            BookModel book = bRepo.GetAll().ToList().Find(_book => _book.Publisher?.Name == publisher.Name);

            if (book != null)
            {
                book.Publisher.Name = Name;
            }

            publisher.Name = Name;

            return RedirectToAction("EditPublisher", new { id = publisher.Id });
        }

        //GET: Delete Publisher
        public ActionResult DeletePublisher(int id)
        {
            PublisherModel publisher = pRepo.GetOne(id);

            BookModel book = bRepo.GetAll().ToList().Find(_book => _book.Publisher?.Name == publisher.Name);

            if (book != null)
            {
                book.Publisher = null;
            }

            pRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}