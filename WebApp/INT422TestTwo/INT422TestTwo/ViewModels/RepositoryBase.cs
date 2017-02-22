using INT422TestTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INT422TestTwo.ViewModels
{
    /// <summary>
    /// Will be a base class for all repositories and will hold DataContext instance
    /// </summary>
    public class RepositoryBase
    {
        protected DataContext dc;

        /// <summary>
        /// Constructor initializes DataContext 
        /// plus adds additional configuration
        /// </summary>
        public RepositoryBase()
        {
            dc = new DataContext();

            dc.Configuration.LazyLoadingEnabled = false;
            dc.Configuration.ProxyCreationEnabled = false;
        }
    }
}