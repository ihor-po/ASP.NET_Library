using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BookModel
    {
        private int id;
        private string name;
        private PublisherModel publisher;
        private IEnumerable<AuthorModel> authors;
        private DateTime publishDate;
        private int pageCount;
        private string isbn;

        /// <summary>
        /// Id книги
        /// </summary>
        [Display(Name="ID")]
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Название книги
        /// </summary>
        [Display(Name = "Название")]
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Издатель книги
        /// </summary>
        [Display(Name = "Издатель")]
        public PublisherModel Publisher { get => publisher; set => publisher = value; }

        /// <summary>
        /// Список авторов
        /// </summary>
        [Display(Name = "Авторы")]
        public IEnumerable<AuthorModel> Authors { get => authors; set => authors = value; }

        /// <summary>
        /// Дата публикации книги
        /// </summary>
        [Display(Name = "Дата публикации")]
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        [Display(Name = "Страницы")]
        public int PageCount { get => pageCount; set => pageCount = value; }

        /// <summary>
        /// Уникальный номер книги
        /// </summary>
        [Display(Name = "ISBN")]
        public string ISBN { get => isbn; set => isbn = value; }
    }
}