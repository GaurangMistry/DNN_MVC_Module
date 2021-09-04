// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Web.Mvc;
using WebXMS.DAL.UniversalSettings;
using WebXMS.DAL.UniversalSettings.Models;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;

namespace WebXMS.Modules.UniversalSettings.Controllers
{
    /// <summary>
    /// ContactController is the MVC Controller class for managing Contacts in the UI
    /// </summary>
    [DnnHandleError]
    public class InitialEvaluationResultsController : DnnController
    {
        private readonly IInitialEvaluationResultRepository _repository;

        /// <summary>
        /// Default Constructor constructs a new InitialEvaluationResultController
        /// </summary>
        public InitialEvaluationResultsController() : this(InitialEvaluationResultRepository.Instance) { }

        /// <summary>
        /// Constructor constructs a new InitialEvaluationResultController with a passed in repository
        /// </summary>
        public InitialEvaluationResultsController(IInitialEvaluationResultRepository repository)
        {
            Requires.NotNull(repository);

            _repository = repository;
        }

        /// <summary>
        /// The Delete View
        /// </summary>
        /// <param name="InitialEvaluationResultId">The Id of the InitialEvaluationResult to delete</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int InitialEvaluationResultId)
        {
            var InitialEvaluationResult = (InitialEvaluationResultId == -1)
                        ? new InitialEvaluationResult { PortalId = PortalSettings.PortalId }
                        : _repository.GetInitialEvaluationResult(InitialEvaluationResultId);

            return View(InitialEvaluationResult);
        }
        /// <summary>
        /// Actual Delete 
        /// </summary>
        /// <param name="InitialEvaluationResult">the InitialEvaluationResult to delete</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Delete(InitialEvaluationResult InitialEvaluationResult)
        {
            if (ModelState.IsValid)
            {
                _repository.DeleteInitialEvaluationResult(InitialEvaluationResult);
                return RedirectToAction("Index");
            }
            else
            {
                return View(InitialEvaluationResult);
            }
        }

        /// <summary>
        /// This Edit method is used to set up editing a InitialEvaluationResult
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int InitialEvaluationResultId = -1)
        {
            var InitialEvaluationResult = (InitialEvaluationResultId == -1)
                        ? new InitialEvaluationResult { PortalId = PortalSettings.PortalId }
                        : _repository.GetInitialEvaluationResult(InitialEvaluationResultId);

            return View(InitialEvaluationResult);
        }

        /// <summary>
        /// This Edit method is used to save the posted InitialEvaluationResult
        /// </summary>
        /// <param name="InitialEvaluationResult">The InitialEvaluationResult to save</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(InitialEvaluationResult InitialEvaluationResult)
        {
            if (ModelState.IsValid)
            {
                InitialEvaluationResult.PortalId = PortalSettings.PortalId;

                if (InitialEvaluationResult.InitialEvaluationResultId == -1)
                {
                    _repository.AddInitialEvaluationResult(InitialEvaluationResult);
                }
                else
                {
                    _repository.UpdateInitialEvaluationResult(InitialEvaluationResult);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(InitialEvaluationResult);
            }
        }

        /// <summary>
        /// The Index method is the default Action method.
        /// </summary>
        /// <param name="searchTerm">Term to search.</param>
        /// <param name="pageIndex">Index of the current page.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <returns></returns>
        public ActionResult Index(string searchTerm = "", int pageIndex = 0)
        {
            var InitialEvaluationResults = _repository.GetInitialEvaluationResults(searchTerm, PortalSettings.PortalId, pageIndex, ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("PageSize", 10));

            return View(InitialEvaluationResults);
        }

        public JsonResult GetJsonResult()
        {
            return new JsonResult()
            {
                Data = new
                {
                    User.UserID,
                    PortalSettings.PortalId,
                    Alias = PortalSettings.PortalAlias.HTTPAlias,
                    Time = DateTime.Now.ToString("HH:mm:ss ttt")
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult GetDemoPartial()
        {
            TempData["UserID"] = User.UserID;
            ViewData["PortalId"] = PortalSettings.PortalId;
            ViewBag.Alias = PortalSettings.PortalAlias.HTTPAlias;

            return PartialView("_DemoPartial", DateTime.Now);
        }

        public ActionResult TestView()
        {
            InitialEvaluationResult c = new InitialEvaluationResult();
            return View("Test", c);
        }
    }
}
