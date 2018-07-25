using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class PublisherModel
    {
        private int id;
        private string name;

        /// <summary>
        /// Publisher ID
        /// </summary>
        [Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public int Id { get => id; set => id = value; }


        /// <summary>
        /// Piblisher Name
        /// </summary>
        [Display(Name = "Издатель")]
        [ScaffoldColumn(true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Введите Издателя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 25 символов")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯіІїЇ' \-]{2,25}$", ErrorMessage = "Некоректное название издателя")]
        public string Name { get => name; set => name = value; }
    }
}