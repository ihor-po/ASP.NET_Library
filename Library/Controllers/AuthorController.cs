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
            ViewBag.Title = "Library :: Издатели";
            ViewBag.Caption = "Создать издателя";

            return View("AuthorForm");
        }

        //POST: Create author
        [HttpPost]
        public ActionResult CreateAuthor(FormCollection req)
        {
            ViewBag.Title = "Library :: Издатели";
            ViewBag.Caption = "Создать издателя";
            
            AuthorModel atr = new AuthorModel();

            string text = @"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$";
            atr.Name = req["Name"];

            if (!Regex.Match(atr.Name, text).Success)
            {
                ViewBag.NameError = "Не менее 2 символов; Цифры не допустимы.";
                ViewBag.Author = atr;
                return View("AuthorForm");
            }

            try
            {
                atr.DateOfBirth = Convert.ToDateTime(req["DateOfBirth"]).Date;
            }
            catch (Exception)
            {
                ViewBag.BirthDayError = "Проверьте правльность введения даты";
                ViewBag.Author = atr;
                return View("AuthorForm");
            }

            if (req["DateOfDeath"] != "")
            {
                try
                {
                    atr.DateOfDeath = Convert.ToDateTime(req["DateOfDeath"]).Date;
                }
                catch (Exception)
                {
                    ViewBag.DeathError = "Проверьте правльность введения даты";
                    ViewBag.Author = atr;
                    return View("AuthorForm");
                }
            }

            atr.Id = (aRepo.GetAll().LastOrDefault()?.Id ?? 0) + 1;

            aRepo.Add(atr);

            return RedirectToAction("EditAuthor", new { id = atr.Id });
        }

        //GET: Edit author
        [HttpGet]
        public ViewResult EditAuthor(int id)
        {
            ViewBag.Title = "MVC CRUD :: Редакирование автора";
            ViewBag.Caption = "Редактирование автора";
            ViewBag.Author = aRepo.GetOne(id);

            return View("AuthorForm");
        }

        //GET: Edit author
        [HttpPost]
        public ActionResult EditAuthor(int id, FormCollection req)
        {
            AuthorModel atr = aRepo.GetOne(id);

            ViewBag.Title = "MVC CRUD :: Редакирование автора";
            ViewBag.Caption = "Редактирование издателя";

            string text = @"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$";

            string oldName = atr.Name;

            atr.Name = req["Name"];

            if (!Regex.Match(atr.Name, text).Success)
            {
                ViewBag.NameError = "Не менее 2 символов; Цифры не допустимы.";
                ViewBag.Author = atr;
                return View("AuthorForm");
            }

            try
            {
                atr.DateOfBirth = Convert.ToDateTime(req["DateOfBirth"]).Date;
            }
            catch (Exception)
            {
                ViewBag.BirthDayError = "Проверьте правльность введения даты";
                ViewBag.Author = atr;
                return View("AuthorForm");
            }

            try
            {
                atr.DateOfDeath = Convert.ToDateTime(req["DateOfDeath"]).Date;
            }
            catch (Exception)
            {
                ViewBag.DeathError = "Проверьте правльность введения даты";
                ViewBag.Author = atr;
                return View("AuthorForm");
            }

            foreach (BookModel book in bRepo.GetAll())
            {
                AuthorModel oldAuthor = book?.Authors?.ToList().Find(_atr => _atr.Name == oldName);
                if (oldAuthor != null)
                {
                    oldAuthor = atr;
                }
                
            }

            return RedirectToAction("EditAuthor", new { id = atr.Id });
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