using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace INT422TestTwo.Models
{
    /// <summary>
    /// Movie Model class
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Movie()
        {
            this.Genres = new List<Genre>();
        }

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

        /// <summary>
        /// Ticket's price
        /// </summary>
        [Required]
        public decimal TicketPrice { get; set; }
        
        /// <summary>
        /// Movie's Director
        /// </summary>
        public Director Director { get; set; }
        
        /// <summary>
        /// List of Movie's Genres
        /// </summary>
        public List<Genre> Genres { get; set; }
    }

    /// <summary>
    /// Class Director
    /// </summary>
    public class Director
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Director()
        {
            this.Name = string.Empty;
            this.Movies = new List<Movie>();
        }

        /// <summary>
        /// Constructor that will accept Director's name
        /// </summary>
        /// <param name="directorName">Director's name</param>
        public Director(string directorName)
        {
            this.Name = directorName;
            this.Movies = new List<Movie>();
        }

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

        /// <summary>
        /// List of Director's Movies
        /// </summary>
        public List<Movie> Movies { get; set; }
    }

    /// <summary>
    /// Class Genre
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Genre()
        {
            this.Name = string.Empty;
            this.Movies = new List<Movie>();
        }

        /// <summary>
        /// Constructor that will accept Genre's name 
        /// </summary>
        /// <param name="genreName"></param>
        public Genre(string genreName)
        {
            this.Name = genreName;
            this.Movies = new List<Movie>();
        }
        
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

        /// <summary>
        /// All Movies with specified genre
        /// </summary>
        public List<Movie> Movies { get; set; }
    }
}