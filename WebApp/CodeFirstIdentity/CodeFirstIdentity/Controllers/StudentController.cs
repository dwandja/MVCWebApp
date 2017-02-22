using System.Web.Mvc;
using CodeFirstIdentity.ViewModels;

namespace CodeFirstIdentity.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        public ActionResult Index()
        {
           var ctx = System.Web.HttpContext.Current;

           if (ctx.User.IsInRole("Admin"))
               return View("IndexWithEdit", repo.getListOfStudentName(ctx.User));
           else
               return View(repo.getListOfStudentName(ctx.User));
        }

        public ActionResult Details(int? id)
        {
            var result = repo.getStudentFull(id);
            if (result == null)
            {
              var errMsg = "Invalid Student id. Did you forget to specify one?";
              var errors = new ViewModels.VM_Error();
              
              errors.ErrorMessages["ExceptionMessage"] = errMsg;

              return View("Error", errors);
            }
            return View(result);
        }

        [Authorize(Roles="Admin, INT422")]
        public ActionResult List()
        {
            return View(repo.getListOfStudentFull());
        }

        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            // initialize select list appropriately before calling view
            studentToCreate.CourseSelectList = repo.getSelectList();
            return View(studentToCreate);
        }

        [HttpPost]
        public ActionResult Create(StudentCreateForHttpPost newItem)
        {
            if (ModelState.IsValid )
            {
              var createdStudent = repo.createStudent(newItem);
              if (createdStudent == null)
              {
                return View("Error", vme.GetErrorModel(ModelState));
              }
              else
              {
                studentToCreate.Clear();
                return RedirectToAction("Details", new { Id = createdStudent.StudentId });
              }
            }
            else
            {
              var errorMessage = "Student should be enrolled in at least one Course";
              if (newItem.CourseIds == null) ModelState.AddModelError("CourseSelectList", errorMessage);

              studentToCreate.FirstName = newItem.FirstName;
              studentToCreate.LastName = newItem.LastName;
              studentToCreate.Phone = newItem.Phone;
              studentToCreate.StudentNumber = newItem.StudentNumber;

              return View(studentToCreate);
            }
        }

        public ActionResult Error()
        {
            return View();
        }	
        
        // Implementation details
        public StudentController()
        {
           repo = new Repo_Student();
        }

        // IMP: MUST be static otherwise the Razor view fails when shuttling between Student/Create POST and GET
        static ViewModels.StudentCreateForHttpGet studentToCreate = new ViewModels.StudentCreateForHttpGet();


        private VM_Error vme = new VM_Error();
        private Repo_Student repo;
    }
}