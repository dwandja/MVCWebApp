using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace CodeFirstIdentity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual Person Person { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }

}