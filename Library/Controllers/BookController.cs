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
    public class BookController : Controller
    {
        IRepository<BookModel> bRepo = BooksRepo.Repository;
        IRepository<AuthorModel> aRepo = AuthorsRepo.Repository;
        IRepository<PublisherModel> pRepo = PublishersRepo.Repository;

        // GET: Book
        public ActionResult Index()
        {
            ViewBag.Title = "Library :: Книги";
            ViewBag.Caption = "Книги";
            ViewBag.Books = bRepo.GetAll();

            return View();
        }

        // GET: Create book
        [HttpGet]
        public ViewResult CreateBook()
        {
            ViewBag.Title = "Library :: Книги";
            ViewBag.Caption = "Создать книгу";
            ViewBag.Publishers = pRepo.GetAll();
            ViewBag.Authors = aRepo.GetAll();

            return View("BookForm");
        }

        //POST: Create book
        [HttpPost]
        public ActionResult CreateBook(FormCollection req)
        {
            ViewBag.Title = "Library :: Книги";
            ViewBag.Caption = "Создать книгу";
            ViewBag.Publishers = pRepo.GetAll();
            ViewBag.Authors = aRepo.GetAll();

            BookModel book = new BookModel();

            string text = @"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$";
            book.Name = req["Name"];

            if (!Regex.Match(book.Name, text).Success)
            {
                ViewBag.NameError = "Не менее 2 символов; Цифры не допустимы.";
            }

            book.Publisher =  pRepo.GetAll().ToList().Find(_publisher => _publisher.Name == req["Publisher"]);

            if (book.Publisher == null)
            {
                ViewBag.PublisherError = "Издатель ны выбран!";
            }

            AuthorModel aModel = aRepo.GetAll().ToList().Find(_author => _author.Name == req["Authors"]);

            if (aModel == null)
            {
                ViewBag.AuthorsError = "Автор ны выбран!";
            }
            else
            {
                List<AuthorModel> atrs = new List<AuthorModel>();
                atrs.Add(aModel);
                book.Authors = atrs;
            }

            try
            {
                book.PublishDate = Convert.ToDateTime(req["PublishDate"]).Date;
            }
            catch (Exception)
            {
                ViewBag.PublDateError = "Проверьте правльность введения даты";
            }

            if (req["PageCount"] == "")
            {
                ViewBag.PageCountError = "Введите колличество страниц";
            }
            else
            {
                book.PageCount = Convert.ToInt16(req["PageCount"]);
            }

            text = @"(?=.{13})\d{1,5}([- ])\d{1,7}\1\d{1,6}\1(\d|X)$";
            book.ISBN = req["ISBN"];

            if (!Regex.Match(book.ISBN, text).Success)
            {
                ViewBag.IsbnError = "13 цыфр";
            }

            if (ViewBag.IsbnError != null || ViewBag.PageCountError != null || ViewBag.PublDateError != null ||
                ViewBag.AuthorsError != null || ViewBag.PublisherError != null || ViewBag.NameError != null)
            {
                ViewBag.Book = book;
                return View("BookForm");
            }

            book.Id = (bRepo.GetAll().LastOrDefault()?.Id ?? 0) + 1;

            bRepo.Add(book);

            return RedirectToAction("EditBook", new { id = book.Id });
        }

        //GET: Edit Book
        [HttpGet]
        public ViewResult EditBook(int id)
        {
            ViewBag.Title = "MVC CRUD :: Редакирование книги";
            ViewBag.Caption = "Редактирование книги";
            ViewBag.Book = bRepo.GetOne(id);
            ViewBag.Publishers = pRepo.GetAll();
            ViewBag.Authors = aRepo.GetAll();

            return View("BookForm");
        }

        //POST: Edit Book
        [HttpPost]
        public ActionResult EditBook(int id, FormCollection req)
        {
            ViewBag.Title = "Library :: Книги";
            ViewBag.Caption = "Редактировать книгу";
            ViewBag.Publishers = pRepo.GetAll();
            ViewBag.Authors = aRepo.GetAll();

            BookModel book = bRepo.GetOne(id);

            string text = @"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$";
            book.Name = req["Name"];

            if (!Regex.Match(book.Name, text).Success)
            {
                ViewBag.NameError = "Не менее 2 символов; Цифры не допустимы.";
            }

            book.Publisher = pRepo.GetAll().ToList().Find(_publisher => _publisher.Name == req["Publisher"]);

            if (book.Publisher == null)
            {
                ViewBag.PublisherError = "Издатель ны выбран!";
            }

            if (book.Authors.Count() == 1)
            {
                AuthorModel aModel = aRepo.GetAll().ToList().Find(_author => _author.Name == req["Authors"]);

                if (aModel == null)
                {
                    ViewBag.AuthorsError = "Автор ны выбран!";
                }
                else
                {
                    List<AuthorModel> atrs = new List<AuthorModel>();
                    atrs.Add(aModel);
                    book.Authors = atrs;
                }
            }

            try
            {
                book.PublishDate = Convert.ToDateTime(req["PublishDate"]).Date;
            }
            catch (Exception)
            {
                ViewBag.PublDateError = "Проверьте правльность введения даты";
            }

            if (req["PageCount"] == "")
            {
                ViewBag.PageCountError = "Введите колличество страниц";
            }
            else
            {
                book.PageCount = Convert.ToInt16(req["PageCount"]);
            }

            text = @"(?=.{13})\d{1,5}([- ])\d{1,7}\1\d{1,6}\1(\d|X)$";
            book.ISBN = req["ISBN"];

            if (!Regex.Match(book.ISBN, text).Success)
            {
                ViewBag.IsbnError = "13 цыфр";
            }

            if (ViewBag.IsbnError != null || ViewBag.PageCountError != null || ViewBag.PublDateError != null ||
                ViewBag.AuthorsError != null || ViewBag.PublisherError != null || ViewBag.NameError != null)
            {
                ViewBag.Book = book;
                return View("BookForm");
            }

            return RedirectToAction("EditBook", new { id = book.Id });
        }

        //GET: Delete role
        public ActionResult DeleteBook(int id)
        {
            BookModel book = bRepo.GetOne(id);

            bRepo.Delete(id);

            return RedirectToAction("Index");
        }

        //GET: Add author to book
        [HttpGet]
        public ViewResult AddBookAuthor()
        {
            ViewBag.Title = "Library :: Книги";
            ViewBag.Caption = "Добавить автора книги";
            //ViewBag.Book = bRepo.GetOne(id);
            ViewBag.Authors = aRepo.GetAll();

            return View("AddAuthor");
        }

        //POST: Add author to book
        [HttpPost]
        public ActionResult AddBookAuthor(int id, string Authors )
        {
            BookModel book = bRepo.GetOne(id);
            AuthorModel atr = null;

            ViewBag.Title = "Library :: Книги";
            ViewBag.Caption = "Добавить автора книги";
            ViewBag.Book = book;
            ViewBag.Authors = aRepo.GetAll();

            if (Authors != "")
            {
                atr = aRepo.GetAll().ToList().Find(item => item.Name == Authors);
            }
            else
            {
                ViewBag.AuthorsError = "Выберите автора";
                return View("AddAuthor");
            }

            if (book.Authors.ToList().Find(item => item.Name == Authors) == null)
            {
                List<AuthorModel> tmp = new List<AuthorModel>();

                foreach(AuthorModel a in book.Authors)
                {
                    tmp.Add(a);
                }
                tmp.Add(atr);

                book.Authors = tmp;
            }
            else
            {
                ViewBag.AuthorsError = "Автор уже добавлен";
                return View("AddAuthor");
            }

            return RedirectToAction("EditBook", new { id = book.Id });
        }

        //Get: Add author to book
        [HttpGet]
        public ActionResult RemoveBookAuthor(string id)
        {
            string[] substring = id.Split(',');

            AuthorModel atr = aRepo.GetOne(Convert.ToInt16(substring[0]));
            BookModel book = bRepo.GetOne(Convert.ToInt16(substring[1]));
            

            ViewBag.Title = "Library :: Книги";
            ViewBag.Caption = "Добавить автора книги";
            ViewBag.Book = book.Id;
            ViewBag.Authors = aRepo.GetAll();

            book.Authors = book.Authors.Where(item => item != atr);

            return RedirectToAction("EditBook", new { id = book.Id });
        }
    }
}