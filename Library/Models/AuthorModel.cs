using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class AuthorModel
    {
        private int id;
        private string name;
        private DateTime dateOfDeath;

        /// <summary>
        /// Id автора
        /// </summary>
        [Display(Name="ID")]
        [ScaffoldColumn(false)]
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Имя автора
        /// </summary>
        [Display(Name = "Имя")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Введите имя автора")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 25 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$", ErrorMessage = "Некоректное имя автора")]
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Дата рождения автора
        /// </summary>
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Дата смерти автора
        /// </summary>
        [Display(Name = "Дата смерти")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfDeath { get => dateOfDeath; set => dateOfDeath = value; }
    }
}