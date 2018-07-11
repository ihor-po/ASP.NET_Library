using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class PublisherModel
    {
        private int id;
        private string name;

        /// <summary>
        /// Publisher ID
        /// </summary>
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Piblisher Name
        /// </summary>
        public string Name { get => name; set => name = value; }
    }
}