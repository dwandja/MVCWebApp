using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstIdentity.Models;

namespace CodeFirstIdentity.ViewModels
{
    public class RepositoryBase
    {
        public RepositoryBase()
        {
            dc = new DataContext();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            dc.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            dc.Configuration.LazyLoadingEnabled = false;
        }

        // implementation details, needs to be protected otherwise
        // Repo_Courses and Repo_Student wont find it.
        protected DataContext dc;
    }
}