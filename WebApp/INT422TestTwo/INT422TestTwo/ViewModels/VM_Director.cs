using INT422TestTwo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INT422TestTwo.ViewModels
{
    /// <summary>
    /// DirectorForList will be used in List
    /// </summary>
    public class DirectorForList
    {
        /// <summary>
        /// Director Id = Primary Key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Director's Name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }

    /// <summary>
    /// DirectorFull ViewModel to be used in Details and Create 
    /// </summary>
    public class DirectorFull : DirectorForList
    {
        /// <summary>
        /// Constructor will initialize list of Movies
        /// </summary>
        public DirectorFull()
        {
            Movies = new List<MovieForList>();
        }

        /// <summary>
        /// List of Director's Movies
        /// </summary>
        public List<MovieForList> Movies { get; set; }
    }
}