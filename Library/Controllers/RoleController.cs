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
    public class RoleController : Controller
    {
        IRepository<RoleModel> rRepo = RolesRepo.Repository;
        //IRepository<BookModel> bRepo = BooksRepo.Repository;

        // GET: Role
        public ActionResult Index()
        {
            ViewBag.Title = "Library :: Роли пользователей";
            ViewBag.Caption = "Роли";
            ViewBag.Url = "/Role/CreateRole";

            return View(rRepo.GetAll());
        }

        // GET: Create role
        [HttpGet]
        public ViewResult CreateRole()
        {
            ViewBag.Title = "Library :: Роли пользователей";
            ViewBag.Caption = "Создать роль";
            RoleModel newRole = new RoleModel();

            return View("RoleForm", newRole);
        }

        //POST: Create role
        [HttpPost]
        public ActionResult CreateRole(RoleModel role)
        {
            ViewBag.Title = "Library :: Роли пользователей";
            ViewBag.Caption = "Создать роль";

            if (ModelState.IsValid)
            {
                role.Id = (rRepo.GetAll().LastOrDefault()?.Id ?? 0) + 1;

                rRepo.Add(role);

                return RedirectToAction("EditRole", new { id = role.Id });
            }
            else
            {
                return View("RoleForm", role);
            }

        }

        //GET: Edit Role
        [HttpGet]
        public ViewResult EditRole(int id)
        {
            ViewBag.Title = "Library :: Редакирование роли";
            ViewBag.Caption = "Редактирование роли";

            return View("RoleForm", rRepo.GetOne(id));
        }

        //GET: Edit Role
        [HttpPost]
        public ActionResult EditRole(int id, RoleModel role)
        {
            ViewBag.Title = "Library :: Редакирование роли";
            ViewBag.Caption = "Редактирование роли";

            if (ModelState.IsValid)
            {
                rRepo.GetOne(id).RoleName = role.RoleName;

                //BookModel book = bRepo.GetAll().ToList().Find(_book => _book.Publisher?.Name == publisher.Name);

                //while (book != null)
                //{
                //    book.Publisher.Name = publisher.Name;
                //    book = bRepo.GetAll().ToList().Find(_book => _book.Publisher?.Name == publisher.Name);
                //}

                return RedirectToAction("EditRole", new { id = role.Id });
            }
            else
            {
                //ViewBag.NameError = "Не менее 2 символов; Цифры не допустимы.";
                //ViewBag.Role = role;

                return View("RoleForm", role);
            }
        }

        //GET: Delete Publisher
        public ActionResult DeleteRole(int id)
        {
            RoleModel publisher = rRepo.GetOne(id);

            //BookModel book = bRepo.GetAll().ToList().Find(_book => _book.Publisher?.Name == publisher.Name);

            //while (book != null)
            //{
            //    book.Publisher = null;
            //    book = bRepo.GetAll().ToList().Find(_book => _book.Publisher?.Name == publisher.Name);
            //}

            rRepo.Delete(id);

            return RedirectToAction("Index");
        }
    }
}