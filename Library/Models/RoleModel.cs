using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class RoleModel
    {
        private int id;
        private string roleName;

        /// <summary>
        /// Id роли
        /// </summary>
        [Display(Name="ID")]
        [ScaffoldColumn(false)]
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Название роли
        /// </summary>
        [Display(Name = "Роль")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Введите название роли")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 25 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$", ErrorMessage = "Некоректное название роли")]
        public string RoleName { get => roleName; set => roleName = value; }
    }
}