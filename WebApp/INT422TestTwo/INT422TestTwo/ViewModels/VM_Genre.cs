using INT422TestTwo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INT422TestTwo.ViewModels
{
    /// <summary>
    /// GenreForList ViewModel to be used in List
    /// </summary>
    public class GenreForList
    {
        /// <summary>
        /// Genre's Id = Primary Key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Genre's Name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    /// GenreFull ViewModel to be used in Details and Create 
    /// </summary>
    public class GenreFull : GenreForList
    {
        /// <summary>
        /// Constructor will initialize list of Movies
        /// </summary>
        public GenreFull()
        {
            Movies = new List<MovieForList>();
        }

        /// <summary>
        /// All Movies with specified Genre
        /// </summary>
        public List<MovieForList> Movies { get; set; }
    }
}