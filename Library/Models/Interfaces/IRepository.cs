using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Interfaces
{
    interface IRepository<T>
    {
        /// <summary>
        /// Получение всего репозитория
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Получение одного объекта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetOne(int id);

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
