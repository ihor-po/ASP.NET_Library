using Library.Models;
using Library.Models.Interfaces;
using Library.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        IRepository<AuthorModel> aRepo = AuthorsRepo.Repository;
        IRepository<BookModel> bRepo = BooksRepo.Repository;

        // GET: Author
        public ActionResult Index()
        {
            ViewBag.Title = "Library :: Авторы";
            ViewBag.Caption = "Авторы";
            ViewBag.Url = "/Author/CreateAuthor";

            return View(aRepo.GetAll());
        }

        // GET: Create author
        [HttpGet]
        public ViewResult CreateAuthor()
        {
            ViewBag.Title = "Library :: Авторы";
            ViewBag.Caption = "Создать автора";
            AuthorModel author = new AuthorModel();

            return View("AuthorForm", author);
        }

        //POST: Create author
        [HttpPost]
        public ActionResult CreateAuthor(AuthorModel atr)
        {
            ViewBag.Title = "Library :: Авторы";
            ViewBag.Caption = "Создать автора";

            if (ModelState.IsValid)
            {
                atr.Id = (aRepo.GetAll().LastOrDefault()?.Id ?? 0) + 1;

                aRepo.Add(atr);

                return RedirectToAction("EditAuthor", new { id = atr.Id });
            }
            else
            {
                return View("AuthorForm", atr);
            }      
        }

        //GET: Edit author
        [HttpGet]
        public ViewResult EditAuthor(int id)
        {
            ViewBag.Title = "Library :: Редакирование автора";
            ViewBag.Caption = "Редактирование автора";

            return View("AuthorForm", aRepo.GetOne(id));
        }

        //GET: Edit author
        [HttpPost]
        public ActionResult EditAuthor(int id, AuthorModel atr)
        {
            ViewBag.Title = "Library :: Редакирование автора";
            ViewBag.Caption = "Редактирование издателя";

            if (ModelState.IsValid)
            {
                AuthorModel author = aRepo.GetOne(id);

                foreach (BookModel book in bRepo.GetAll())
                {
                    AuthorModel oldAuthor = book?.Authors?.ToList().Find(_atr => _atr.Name == author.Name);
                    if (oldAuthor != null)
                    {
                        oldAuthor.Name = atr.Name;
                        oldAuthor.DateOfBirth = atr.DateOfBirth;
                        oldAuthor.DateOfDeath = atr.DateOfDeath;
                    }
                }

                author.Name = atr.Name;
                author.DateOfBirth = atr.DateOfBirth;
                author.DateOfDeath = atr.DateOfDeath;

                return RedirectToAction("EditAuthor", new { id = atr.Id });
            }
            else
            {
                return View("AuthorForm", atr);
            }
        }

        //GET: Delete author
        public ActionResult DeleteAuthor(int id)
        {
            AuthorModel atr = aRepo.GetOne(id);

            foreach(BookModel book in bRepo.GetAll())
            {
                if (book.Authors?.Any() == true)
                {
                    book.Authors = book.Authors.Where(item => item != atr);
                }
            }

            aRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}