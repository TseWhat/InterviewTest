using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Repositories;
using FundsLibrary.InterviewTest.Web.Models.Errors;
using FundsLibrary.InterviewTest.Web.Resources;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class FundManagerController : Controller
    {
        private readonly IFundManagerModelRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public FundManagerController()
            : this(new FundManagerModelRepository())
        {}

        public FundManagerController(IFundManagerModelRepository repository)
        {
            _repository = repository;
        }

        // GET: FundManager
        public async Task<ActionResult> Index()
        {
            var response = await _repository.GetAll();

            if (response != null)
                return View(response);

            return View("NotFound", new ErrorModel {  ErrorHeader = Application.NoManagersFound, ErrorMessage = Application.NoManagersFoundMessage});
        }

        // GET: FundManager/Details/id
        public async Task<ActionResult> Details(Guid id)
        {
            if (id != Guid.Empty)
            {
                var response = await _repository.Get(id);

                if (response != null) 
                    return View(response);
            }

            return RedirectToAction("NotFound", new ErrorModel { ErrorHeader = Application.ManagerNotFound, ErrorMessage = Application.NotFoundMessage });
        }

        // GET: FundManager/NotFound
        public ActionResult NotFound(ErrorModel model)
        {
            return View("NotFound", model);
        }
    }
}
