using System.Web.Mvc;
using CodeFirstIdentity.ViewModels;

namespace CodeFirstIdentity.Controllers
{
    [Authorize]
    public class FacultyController : Controller
    {
        //
        // GET: /Student/
        public ActionResult Index()
        {
            var ctx = System.Web.HttpContext.Current;
            
            if (ctx.User.IsInRole("Admin"))
                return View("IndexWithEdit", repo.getListOfFacultyName(ctx.User));
                
            else
                return View(repo.getListOfFacultyName(ctx.User));
        }

        public ActionResult Details(int? id)
        {
            var result = repo.getFacultyFull(id);
            if (result == null)
            {
                var errMsg = "Invalid Student id. Did you forget to specify one?";
                var errors = new ViewModels.VM_Error();

                errors.ErrorMessages["ExceptionMessage"] = errMsg;

                return View("Error", errors);
            }
            return View(result);
        }

        [Authorize(Roles = "Admin, INT422")]
        public ActionResult List()
        {
            return View(repo.getListOfFacultyFull());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            // initialize select list appropriately before calling view
            FacultyToCreate.CourseSelectList = repo.getSelectList();
            return View(FacultyToCreate);
        }

        [HttpPost]
        public ActionResult Create(FacultyCreateForHttpPost newItem)
        {
            if (ModelState.IsValid)
            {
                var createdFaculty = repo.createFaculty(newItem);
                if (createdFaculty == null)
                {
                    return View("Error", vme.GetErrorModel(ModelState));


                }
                else
                {
                    FacultyToCreate.Clear();
                    return RedirectToAction("Details", new { Id = createdFaculty.FacultyId });
                }
            }
            else
            {
                var errorMessage = "Student should be enrolled in at least one Course";
                if (newItem.CourseIds == null) ModelState.AddModelError("CourseSelectList", errorMessage);

                FacultyToCreate.FirstName = newItem.FirstName;
                FacultyToCreate.LastName = newItem.LastName;
                FacultyToCreate.Phone = newItem.Phone;
                FacultyToCreate.FacultyNumber = newItem.FacultyNumber;

                return View(FacultyToCreate);
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        // Implementation details
        public FacultyController()
        {
            repo = new Repo_Faculty();
        }

        // IMP: MUST be static otherwise the Razor view fails when shuttling between Student/Create POST and GET
        static ViewModels.FacultyCreateForHttpGet FacultyToCreate = new ViewModels.FacultyCreateForHttpGet();


        private VM_Error vme = new VM_Error();
        private Repo_Faculty repo;
    }
}