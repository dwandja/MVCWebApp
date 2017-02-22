using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcWebApp.Models;

namespace MvcWebApp.ViewModels
{
    public class Repository
    {

        protected MyDbContext context;

        public Repository()
        {

            context = new MyDbContext();

            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

        }

    }

}