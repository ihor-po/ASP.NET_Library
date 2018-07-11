using Library.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models.Repositories
{
    public class AuthorsRepo : IRepository<AuthorModel>
    {
        private static AuthorsRepo _repository;

        private List<AuthorModel> _authors;

        private AuthorsRepo()
        {
            _authors = new List<AuthorModel>
            {
                new AuthorModel {Id = 1, Name = "Уильям Шекспир", DateOfBirth = (new DateTime(1564, 4, 23)).Date, DateOfDeath = new DateTime(1616, 4, 23)},
                new AuthorModel {Id = 2, Name = "Виктор Мари Гюго", DateOfBirth = (new DateTime(1802, 2, 26)).Date, DateOfDeath = new DateTime(1885, 5, 22)},
                new AuthorModel {Id = 3, Name = "Лев Николаевич Толстой", DateOfBirth = (new DateTime(1828, 8, 28)).Date, DateOfDeath = new DateTime(1910, 11, 7)},
                new AuthorModel {Id = 4, Name = "Иван Сергеевич Тургенев", DateOfBirth = (new DateTime(1818, 10, 28)).Date, DateOfDeath = new DateTime(1883, 9, 3)}
            };
        }

        /// <summary>
        /// Получение экземпляра коллекции авторов
        /// </summary>
        public static AuthorsRepo Repository = _repository ?? (_repository = new AuthorsRepo());

        /// <summary>
        /// Добавление в коллекцию нового автора
        /// </summary>
        /// <param name="item"></param>
        public void Add(AuthorModel item) => _authors.Add(item);

        /// <summary>
        /// Удаление из коллекции автора по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id) => _authors.Remove(GetOne(id));

        /// <summary>
        /// Получение всех авторов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AuthorModel> GetAll() => _authors;

        /// <summary>
        /// Получение автора по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AuthorModel GetOne(int id) => _authors.Find(item => item.Id == id);
    }
}