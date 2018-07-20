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
        private DateTime dateOfBirth;
        private DateTime dateOfDeath;

        /// <summary>
        /// Id автора
        /// </summary>
        [Display(Name="ID")]
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Имя автора
        /// </summary>
        [Display(Name = "Имя")]
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Дата рождения автора
        /// </summary>
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        /// <summary>
        /// Дата смерти автора
        /// </summary>
        [Display(Name = "Дата смерти")]
        public DateTime DateOfDeath { get => dateOfDeath; set => dateOfDeath = value; }
    }
}