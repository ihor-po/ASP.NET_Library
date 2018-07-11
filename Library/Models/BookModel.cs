using System;
using System.Collections.Generic;
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
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Название книги
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Издатель книги
        /// </summary>
        public PublisherModel Publisher { get => publisher; set => publisher = value; }

        /// <summary>
        /// Список авторов
        /// </summary>
        public IEnumerable<AuthorModel> Authors { get => authors; set => authors = value; }

        /// <summary>
        /// Дата публикации книги
        /// </summary>
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get => pageCount; set => pageCount = value; }

        /// <summary>
        /// Уникальный номер книги
        /// </summary>
        public string ISBN { get => isbn; set => isbn = value; }
    }
}