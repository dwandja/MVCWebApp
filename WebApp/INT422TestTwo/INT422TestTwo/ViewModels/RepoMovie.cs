using INT422TestTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INT422TestTwo.ViewModels
{
    public class RepoMovie : RepositoryBase
    {
        /// <summary>
        /// Creates List of MovieForList to be presented in the Movie List View
        /// </summary>
        /// <returns>List of MovieForList</returns>
        public IEnumerable<MovieForList> GetMoviesForList()
        {
            var forList = dc.Movies.OrderBy(movie => movie.Title);

            List<MovieForList> moviesForList = new List<MovieForList>();

            foreach (Movie m in forList)
            {
                MovieForList mfl = new MovieForList();
                mfl.Id = m.Id;
                mfl.Title = m.Title;
                moviesForList.Add(mfl);
            }

            return moviesForList;
        }

        /// <summary>
        /// Creates a MovieFull object based on provided Id
        /// </summary>
        /// <param name="id">Movie Id</param>
        /// <returns>MovieFull object based on id</returns>
        public MovieFull GetMovieFull(int? id)
        {
            RepoDirector Repo_Director = new RepoDirector();
            RepoGenre Repo_Genre = new RepoGenre();

            Movie movie = dc.Movies.FirstOrDefault(m => m.Id == id);
            MovieFull mf = new MovieFull();

            mf.Id = movie.Id;
            mf.TicketPrice = movie.TicketPrice;
            mf.Title = movie.Title;
            mf.Director = Repo_Director.GetDirectorFull(movie.Director.Id);

            List<GenreFull> genreFullList = new List<GenreFull>();

            foreach (Genre g in movie.Genres)
            {
                GenreFull gf = Repo_Genre.GetGenreFull(g.Id);
                genreFullList.Add(gf);
            }

            mf.Genres = genreFullList;

            return mf;
        }

        /// <summary>
        /// Creates a MovieFull object based on provided Id
        /// </summary>
        /// <param name="id">Movie Id</param>
        /// <returns>MovieFull object based on id</returns>
        public MovieForList GetMovieForList(int? id)
        {
            Movie movie = dc.Movies.FirstOrDefault(m => m.Id == id);
            MovieForList mf = new MovieForList();

            mf.Id = movie.Id;
            mf.Title = movie.Title;

            return mf;
        }

        /// <summary>
        /// Create a MovieForDetails object based on provided id
        /// This method is used to present movie details
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <returns>MovieForDetails object</returns>
        public MovieForDetails GetMovieForDetails(int? id)
        {
            RepoDirector Repo_Director = new RepoDirector();
            Movie movie = dc.Movies.Include("Director").FirstOrDefault(m => m.Id == id);
            MovieForDetails mfd = new MovieForDetails();

            mfd.Id = movie.Id;
            mfd.TicketPrice = movie.TicketPrice;
            mfd.Title = movie.Title;
            mfd.Director = Repo_Director.GetDirectorForList(movie.Director.Id);

            return mfd;
        }

        /// <summary>
        /// Creates Movie and add to Database
        /// </summary>
        /// <param name="mf">Movie Full</param>
        /// <param name="gen">String of genre's id sepreated by comma</param>
        /// <param name="dir">Director id</param>
        /// <returns>Create Movie</returns>
        public MovieFull CreateMovie(MovieFull mf, string gen = "", string dir = "")
        {
            Movie movie = new Movie();
            movie.Title = mf.Title;
            movie.TicketPrice = mf.TicketPrice;
            movie.Id = dc.Movies.Max(m => m.Id) + 1;

            List<Int32> genres = new List<int>();
            List<Int32> directors = new List<int>();

            if (gen != "")
            {
                var genSplit = gen.Split(',');

                // fill the list of all genres
                foreach (var g in genSplit)
                {
                    genres.Add(Convert.ToInt32(g));
                }

                // convert genres ids into genres and add to movies
                foreach (var item in genres)
                {
                    movie.Genres.Add(dc.Genres.FirstOrDefault(g => g.Id == item));
                }
            }

            // get director's id
            int directorId = Convert.ToInt32(dir);
            movie.Director = dc.Directors.FirstOrDefault(d => d.Id == directorId);

            dc.Movies.Add(movie);
            dc.SaveChanges();

            return GetMovieFull(movie.Id);
        }

        /// <summary>
        /// Gets list of Directors Names
        /// </summary>
        /// <returns>Directors Names List</returns>
        public SelectList getSelectDirectorsList()
        {
            SelectList directorsList = new SelectList(dc.Directors, "Id", "Name");
            return directorsList;
        }

        /// <summary>
        /// Get list of Genres
        /// </summary>
        /// <returns>Genres List</returns>
        public SelectList getSelectGenresList()
        {
            SelectList genresList = new SelectList(dc.Genres, "Id", "Name");
            return genresList;
        }
    }
}