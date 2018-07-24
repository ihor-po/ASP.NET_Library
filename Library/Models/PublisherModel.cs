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
        public string Name { get => name; set => name = value; }
    }
}