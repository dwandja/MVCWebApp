using INT422TestTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INT422TestTwo.ViewModels
{
    public class RepoGenre : RepositoryBase
    {
        /// <summary>
        /// Creates List of GenreForList to be presented in the Genre List View
        /// </summary>
        /// <returns>List of GenreForList</returns>
        public IEnumerable<GenreForList> GetGenresForList()
        {
            var forList = dc.Genres.OrderBy(genre => genre.Name);

            List<GenreForList> genresForList = new List<GenreForList>();

            foreach (Genre g in forList)
            {
                GenreForList gfl = new GenreForList();
                gfl.Id = g.Id;
                gfl.Name = g.Name;
                genresForList.Add(gfl);
            }

            return genresForList;
        }

        /// <summary>
        /// Creates a GenreFull object based on provided Id
        /// </summary>
        /// <param name="id">Genre Id</param>
        /// <returns>GenreFull object based on id</returns>
        public GenreFull GetGenreFull(int? id)
        {
            RepoMovie Repo_Movies = new RepoMovie();
            Genre genre = dc.Genres.Include("Movies").FirstOrDefault(g => g.Id == id);
            GenreFull gf = new GenreFull();
            gf.Id = genre.Id;
            gf.Name = genre.Name;

            List<MovieForList> movieFullList = new List<MovieForList>();
             
            foreach(Movie m in genre.Movies)
            {
                MovieForList mf = Repo_Movies.GetMovieForList(m.Id);
                movieFullList.Add(mf);
            }

            gf.Movies = movieFullList;

            return gf;
        }
    }
}