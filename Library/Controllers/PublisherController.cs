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
        public ActionResult CreatePublisher(PublisherModel publisher)
        {
            ViewBag.Title = "Library :: Издатели";
            ViewBag.Caption = "Создать издателя";
            
            if (ModelState.IsValid)
            {
                publisher.Id = (pRepo.GetAll().LastOrDefault()?.Id ?? 0) + 1;

                pRepo.Add(publisher);

                return RedirectToAction("EditPublisher", new { id = publisher.Id });
            }
            else
            {
                return View("PublisherForm", publisher);
            }

        }

        //GET: Edit Publisher
        [HttpGet]
        public ViewResult EditPublisher(int id)
        {
            ViewBag.Title = "MVC CRUD :: Редакирование издателя";
            ViewBag.Caption = "Редактирование издателя";

            return View("PublisherForm", pRepo.GetOne(id));
        }

        //GET: Edit Publisher
        [HttpPost]
        public ActionResult EditPublisher(int id, PublisherModel publisher)
        {
            //PublisherModel publisher = pRepo.GetOne(id);

            ViewBag.Title = "MVC CRUD :: Редакирование роли";
            ViewBag.Caption = "Редактирование издателя";

            if (ModelState.IsValid)
            {
                pRepo.GetOne(id).Name = publisher.Name;

                return RedirectToAction("EditPublisher", new { id = publisher.Id });
            }
            else
            {
                ViewBag.NameError = "Не менее 2 символов; Цифры не допустимы.";
                ViewBag.Publisher = publisher;

                return View("PublisherForm", publisher);
            }
        }

        //GET: Delete Publisher
        public ActionResult DeletePublisher(int id)
        {
            PublisherModel publisher = pRepo.GetOne(id);

            BookModel book = bRepo.GetAll().ToList().Find(_book => _book.Publisher?.Name == publisher.Name);

            while (book != null)
            {
                book.Publisher = null;
                book = bRepo.GetAll().ToList().Find(_book => _book.Publisher?.Name == publisher.Name);
            }

            pRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}