using Library.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models.Repositories
{
    public class RolesRepo : IRepository<RoleModel>
    {
        private static RolesRepo _repository;

        private List<RoleModel> _roles;

        private RolesRepo()
        {
            _roles = new List<RoleModel>();

            _roles.Add(new RoleModel { RoleName = "Администратор" });
            _roles.Add(new RoleModel { RoleName = "Пользователь" });
        }

        /// <summary>
        /// Получение экземпляра коллекции ролей
        /// </summary>
        public static RolesRepo Repository => _repository ?? (_repository = new RolesRepo());

        /// <summary>
        /// Добавление роли в коллекцию
        /// </summary>
        /// <param name="item"></param>
        public void Add(RoleModel item)
        {
            //Получение значения id последней роли в коллекции
            //Если пользователей еще нет - то значение 0
            //Прибавление к значению + 1
            item.Id = (_roles.LastOrDefault()?.Id ?? 0) + 1;

            _roles.Add(item);
        }

        /// <summary>
        /// Удаление роли из коллекции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id) => _roles.Remove(GetOne(id));

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleModel> GetAll() => _roles;

        /// <summary>
        /// Получение роли из коллекции
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleModel GetOne(int id) => _roles.Find(item => item.Id == id);
    }
}