using Library.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models.Repositories
{
    public class BooksRepo : IRepository<BookModel>
    {
        private static BooksRepo _repository;
        private static PublishersRepo _publRepository = PublishersRepo.Repository;
        private static AuthorsRepo _atrRepository = AuthorsRepo.Repository;

        private List<BookModel> bRepo;

        private BooksRepo()
        {
            bRepo = new List<BookModel>
            {
                new BookModel
                {
                    Id = 1,
                    Name = "Новая Книга",
                    Publisher = _publRepository.GetOne(1),
                    Authors = new List<AuthorModel>
                    {
                        _atrRepository.GetOne(1),
                        _atrRepository.GetOne(3),
                        _atrRepository.GetOne(4)
                    },
                    PublishDate = DateTime.Now.Date,
                    PageCount = 1024,
                    ISBN = "ISBN 1 56389 778 0"
                },
                new BookModel
                {
                    Id = 2,
                    Name = "New Book",
                    Publisher = _publRepository.GetOne(2),
                    Authors = new List<AuthorModel>
                    {
                        _atrRepository.GetOne(4),
                        _atrRepository.GetOne(2)
                    },
                    PublishDate = DateTime.Now.Date,
                    PageCount = 2048,
                    ISBN = "ISBN 1-43432-668-3"
                },
                                new BookModel
                {
                    Id = 3,
                    Name = "New Book",
                    Publisher = null,
                    Authors = null,
                    PublishDate = DateTime.Now.Date,
                    PageCount = 2048,
                    ISBN = "ISBN 1-12332-876-1"
                }
            };
        }

        /// <summary>
        /// Получение экземпляра коллекции книг
        /// </summary>
        public static BooksRepo Repository = _repository ?? (_repository = new BooksRepo());

        /// <summary>
        /// Добавление в коллекцию новой книги
        /// </summary>
        /// <param name="item"></param>
        public void Add(BookModel item) => bRepo.Add(item);

        /// <summary>
        /// Удаление из коллекции книги по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id) => bRepo.Remove(GetOne(id));

        /// <summary>
        /// Получение всех книг
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BookModel> GetAll() => bRepo;

        /// <summary>
        /// Получение автора по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookModel GetOne(int id) => bRepo.Find(item => item.Id == id);
    }
}