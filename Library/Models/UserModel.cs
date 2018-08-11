using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class UserModel
    {
        private int id;
        private string login;
        private string password;
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private RoleModel role;

        /// <summary>
        /// Id роли
        /// </summary>
        [Display(Name="ID")]
        [ScaffoldColumn(false)]
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Логин")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Введите логин")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина Логина должна быть от 2 до 25 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$", ErrorMessage = "Некоректный логин")]
        public string Login { get => login; set => login = value; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Пароль")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 25 символов")]
        [RegularExpression(@"(?=.*\d).$", ErrorMessage = "Не менее 6 символов; Хотя бы одна цифра")]
        public string Password { get => password; set => password = value; }

        //TODO firstname, lastname, email, phone

        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Display(Name = "Роль")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Выберите роль")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина роли должна быть от 2 до 25 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$", ErrorMessage = "Некоректная роль")]
        public RoleModel Role { get => role; set => role = value; }
    }
}