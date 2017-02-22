using INT422TestTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INT422TestTwo.ViewModels
{
    public class RepoDirector : RepositoryBase
    {
        /// <summary>
        /// Creates List of DirectorForList to be presented in the Director List View
        /// </summary>
        /// <returns>List of DirectorForList</returns>
        public IEnumerable<DirectorForList> GetDirectorsForList()
        {
            var forList = dc.Directors.OrderBy(director => director.Name);

            List<DirectorForList> directorsForList = new List<DirectorForList>();

            foreach (Director d in forList)
            {
                DirectorForList dfl = new DirectorForList();
                dfl.Id = d.Id;
                dfl.Name = d.Name;
                directorsForList.Add(dfl);
            }

            return directorsForList;
        }

        /// <summary>
        /// Creates a DirectorFull object based on provided Id
        /// </summary>
        /// <param name="id">Director Id</param>
        /// <returns>DirectorFull object based on id</returns>
        public DirectorFull GetDirectorFull(int? id)
        {
            RepoMovie Repo_Movie = new RepoMovie();
            Director director = dc.Directors.Include("Movies").FirstOrDefault(d => d.Id == id);
            DirectorFull df = new DirectorFull();

            df.Id = director.Id;
            df.Name = director.Name;

            List<MovieForList> mfList = new List<MovieForList>();

            foreach (Movie m in director.Movies)
            {
                MovieForList mf = Repo_Movie.GetMovieForList(m.Id);
                mfList.Add(mf);
            }

            df.Movies = mfList;

            return df;
        }

        /// <summary>
        /// Creates a DirectorForList object based on provided Id
        /// </summary>
        /// <param name="id">Director Id</param>
        /// <returns>DirectorForList object based on id</returns>
        public DirectorForList GetDirectorForList(int? id)
        {
            Director director = dc.Directors.Include("Movies").FirstOrDefault(d => d.Id == id);
            DirectorForList df = new DirectorForList();

            df.Id = director.Id;
            df.Name = director.Name;

            return df;
        }
    }
}