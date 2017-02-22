using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstIdentity.ViewModels
{
    public class Repo_Courses : RepositoryBase
    {
        public static List<CourseFull> getListOfCourseFull(List<Models.Course> ls)
        {
            List<CourseFull> nls = new List<CourseFull>();
            foreach (var item in ls)
            {
                CourseFull co = new CourseFull();
                co.CourseCode = item.CourseCode;
                co.CourseId = item.Id;
                nls.Add(co);
            }

            return nls;
        }
    }
}