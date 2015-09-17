using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Repositories;
using FundsLibrary.InterviewTest.Web.Models.Errors;
using FundsLibrary.InterviewTest.Web.Resources;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Common;

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

            return View("GenericError", new ErrorModel { ErrorHeader = Application.NoManagersFound, ErrorMessage = Application.NoManagersFoundMessage });
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

            return RedirectToAction("GenericError", new ErrorModel { ErrorHeader = Application.ManagerNotFound, ErrorMessage = Application.NotFoundMessage });
        }

        public ActionResult Add()
        {
            return View(new FundManagerModel());
        }

        [HttpPost]
        public async Task<ActionResult> Add(FundManagerModel model)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                var response = await _repository.Create(model);

                if (response != Guid.Empty)
                {
                    ViewBag.SuccessMessage = Application.SuccessfullyCreated;    
                    return RedirectToAction("Details/" + response);
                }
                else
                {
                    return RedirectToAction("GenericError", new ErrorModel { ErrorHeader = Application.AddManagerError, ErrorMessage = Application.AddManagerErrorMessage });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("GenericError", new ErrorModel { ErrorHeader = Application.AddManagerError, ErrorMessage = Application.AddManagerErrorMessage });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid managerId)
        {
            try
            {
                var manager = await _repository.Get(managerId);

                if (manager != null)
                    return View(manager);

                return RedirectToAction("GenericError", new ErrorModel { ErrorHeader = Application.ManagerNotFound, ErrorMessage = Application.NotFoundMessage });
            }
            catch (Exception)
            {
                return RedirectToAction("GenericError", new ErrorModel { ErrorHeader = Application.Error, ErrorMessage = Application.ErrorMessage });
            }
        }

        public async Task<ActionResult> Edit(FundManagerModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                await _repository.Edit(model);
                ViewBag.SuccessMessage = Application.SuccessfullyEdited;    
            }
            catch (Exception ex)
            {
                return RedirectToAction("GenericError", new ErrorModel(ex.Message));
            }

            return View();
        }


        public ActionResult GenericError(ErrorModel model)
        {
            return View(model);
        }
    }
}
