using Library.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models.Repositories
{
    public class PublishersRepo : IRepository<PublisherModel>
    {
        private static PublishersRepo _repository;

        private List<PublisherModel> _publishers;

        private PublishersRepo()
        {
            _publishers = new List<PublisherModel>();

            _publishers.Add(new PublisherModel { Id = 1, Name = "PublisherHouse" });
            _publishers.Add(new PublisherModel { Id = 2, Name = "Pikabu" });
            _publishers.Add(new PublisherModel { Id = 3, Name = "PeoplesIncorporated" });
        }

        /// <summary>
        /// Получение экземпляра коллекции издателей
        /// </summary>
        public static PublishersRepo Repository = _repository ?? (_repository = new PublishersRepo());

        /// <summary>
        /// Добавление в коллекцию нового издателя
        /// </summary>
        /// <param name="item"></param>
        public void Add(PublisherModel item)
        {
            _publishers.Add(item);
        }

        /// <summary>
        /// Удаление из коллекции издателя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id) => _publishers.Remove(GetOne(id));

        /// <summary>
        /// Получение всех издателей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PublisherModel> GetAll() => _publishers;

        /// <summary>
        /// Получение издателя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PublisherModel GetOne(int id) => _publishers.Find(item => item.Id == id);
    }
}