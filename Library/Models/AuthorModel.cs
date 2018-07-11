using System;
using System.Collections.Generic;
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

        public int Id { get => id; set => id = value; }
        /// <summary>
        /// Имя автора
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Дата рождения автора
        /// </summary>
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        /// <summary>
        /// Дата смерти автора
        /// </summary>
        public DateTime DateOfDeath { get => dateOfDeath; set => dateOfDeath = value; }
    }
}