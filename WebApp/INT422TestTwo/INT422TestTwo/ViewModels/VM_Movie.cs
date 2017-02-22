using INT422TestTwo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INT422TestTwo.ViewModels
{
    /// <summary>
    /// MovieForList ViewModel to be used in List
    /// </summary>
    public class MovieForList
    {
        /// <summary>
        /// Movie Id = Primary Key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Movie's title
        /// </summary>
        [Required]
        public string Title { get; set; }
    }

    /// <summary>
    /// MovieFull ViewModel to be used in Details and Create 
    /// </summary>
    public class MovieFull : MovieForList
    {
        /// <summary>
        /// Constructor will initialize list of Genres
        /// </summary>
        public MovieFull()
        {
            Genres = new List<GenreFull>();
        }

        /// <summary>
        /// Ticket's price
        /// </summary>
        [Required]
        [Display(Name="Ticket Price")]
        public decimal TicketPrice { get; set; }

        /// <summary>
        /// Movie's Director
        /// </summary>
        public DirectorFull Director { get; set; }

        /// <summary>
        /// List of Movie's Genres
        /// </summary>
        public List<GenreFull> Genres { get; set; }
    }

    public class MovieForDetails : MovieForList
    {
        /// <summary>
        /// Ticket's price
        /// </summary>
        [Required, Display(Name="Ticket Price")]
        public decimal TicketPrice { get; set; }

        /// <summary>
        /// Movie's Director
        /// </summary>
        public DirectorForList Director { get; set; }
    }
}