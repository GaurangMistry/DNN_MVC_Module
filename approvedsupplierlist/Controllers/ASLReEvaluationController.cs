// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Web.Mvc;
using WebXMS.DAL.ASLApp;
using WebXMS.DAL.ASLApp.Models;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;

using WebXMS.DAL.UniversalSettings;
using WebXMS.DAL.UniversalSettings.Models;
using WebXMS.DAL.DNN;
using WebXMS.DAL.DNN.Models;
using DotNetNuke.Web.Mvc.Routing;

namespace WebXMS.Modules.ASLApp.Controllers
{
    /// <summary>
    /// ContactController is the MVC Controller class for managing Contacts in the UI
    /// </summary>
    [DnnHandleError]
    public class ASLReEvaluationsController : DnnController
    {
        private readonly IASLReEvaluationRepository _repository;
        private readonly WebXMS.DAL.UniversalSettings.IInitialEvaluationResultRepository _resultsrepository;
        private readonly IUserRepository _userrepository;
        private readonly IASLRepository _aslrepository;
        /// <summary>
        /// Default Constructor constructs a new ASLReEvaluationController
        /// </summary>
        public ASLReEvaluationsController() : this(ASLReEvaluationRepository.Instance, InitialEvaluationResultRepository.Instance
            , UserRepository.Instance,ASLRepository.Instance) { }

        /// <summary>
        /// Constructor constructs a new ASLReEvaluationController with a passed in repository
        /// </summary>
        public ASLReEvaluationsController(IASLReEvaluationRepository repository,IInitialEvaluationResultRepository resrepo,
            IUserRepository userrepo, IASLRepository aslrepo)
        {
            Requires.NotNull(repository);

            _repository = repository;
            _resultsrepository = resrepo;
            _userrepository = userrepo;
            _aslrepository = aslrepo;
        }

        /// <summary>
        /// The Delete View
        /// </summary>
        /// <param name="ASLReEvaluationId">The Id of the ASLReEvaluation to delete</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int ASLReEvaluationId)
        {
            var ASLReEvaluation = (ASLReEvaluationId == -1)
                        ? new ASLReEvaluation { PortalId = PortalSettings.PortalId }
                        : _repository.GetASLReEvaluation(ASLReEvaluationId);

            return View(ASLReEvaluation);
        }
        /// <summary>
        /// Actual Delete 
        /// </summary>
        /// <param name="ASLReEvaluation">the ASLReEvaluation to delete</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Delete(ASLReEvaluation ASLReEvaluation)
        {
            Requires.NotNegative("ASLReEvaluationId", ASLReEvaluation.ASLReEvaluationId);
            Requires.NotNegative("ParentASLId", ASLReEvaluation.ParentASLId);
        
                _repository.DeleteASLReEvaluation(ASLReEvaluation);
            return RedirectToAction("Index", "ASLReEvaluations", new
            {
                ParentASLId = ASLReEvaluation.ParentASLId
            });

        }

        /// <summary>
        /// This Edit method is used to set up editing a ASLReEvaluation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int ParentASLId,int ASLReEvaluationId = -1)
        {
            Requires.NotNull(ParentASLId);
            var asl = _aslrepository.GetASL(ParentASLId);
            ViewBag.ParentASL = asl;

            var ASLReEvaluation = (ASLReEvaluationId == -1)
                        ? new ASLReEvaluation { PortalId = PortalSettings.PortalId, ParentASLId=ParentASLId }
                        : _repository.GetASLReEvaluation(ASLReEvaluationId);

            var results = _resultsrepository.GetInitialEvaluationResults(PortalSettings.PortalId);
            var resultsList = from res in results.Cast<InitialEvaluationResult>().ToList()
                              select new SelectListItem
                              {
                                  Text = res.InitialEvaluationResultName,
                                  Value = res.InitialEvaluationResultId.ToString(),
                                  Selected = (res.InitialEvaluationResultId == ASLReEvaluation.InitialEvaluationResultId)
                              };
            var users = _userrepository.GetUsers(PortalSettings.PortalId);
            var usersList = from user in users.Cast<User>().ToList()
                            select new SelectListItem { Text = user.FirstName + user.LastName, Value = user.UserID.ToString(), Selected = ASLReEvaluation.EvaluationBy == user.UserID };

            ViewBag.ReEvaluationResults = resultsList;
            ViewBag.ReEvaluationByUsers = usersList;
            return View(ASLReEvaluation);
        }

        /// <summary>
        /// This Edit method is used to save the posted ASLReEvaluation
        /// </summary>
        /// <param name="ASLReEvaluation">The ASLReEvaluation to save</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(ASLReEvaluation ASLReEvaluation)
        {
            int parentASLId = ASLReEvaluation.ParentASLId;
            
            if (ModelState.IsValid)
            {
                ASLReEvaluation.PortalId = PortalSettings.PortalId;

                if (ASLReEvaluation.ASLReEvaluationId == -1)
                {
                    _repository.AddASLReEvaluation(ASLReEvaluation);
                }
                else
                {
                    _repository.UpdateASLReEvaluation(ASLReEvaluation);
                }

                return RedirectToAction("Index", "ASLReEvaluations", new
                {
                    ParentASLId= parentASLId
                });
            }
            else
            {
                return View(ASLReEvaluation);
            }
        }

        /// <summary>
        /// The Index method is the default Action method.
        /// </summary>
        /// <param name="searchTerm">Term to search.</param>
        /// <param name="pageIndex">Index of the current page.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <returns></returns>
        public ActionResult Index(int ParentASLId,string searchTerm = "", int pageIndex = 0)
        {
            var asl = _aslrepository.GetASL(ParentASLId);
            ViewBag.ParentASL = asl;
            var ASLReEvaluation = _repository.GetASLReEvaluations(ParentASLId,searchTerm, PortalSettings.PortalId, pageIndex, ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("PageSize", 10));
            ViewBag.ParentASLId = ParentASLId;
            return View(ASLReEvaluation);
        }

        public ActionResult PartialIndex(int ParentASLId, string searchTerm = "", int pageIndex = 0)
        {
            var asl = _aslrepository.GetASL(ParentASLId);
            ViewBag.ParentASL = asl;
            var ASLReEvaluation = _repository.GetASLReEvaluations(ParentASLId,searchTerm, PortalSettings.PortalId, pageIndex, ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("PageSize", 10));
            ViewBag.ParentASLId = ParentASLId;
            return PartialView(ASLReEvaluation);
        }

        public new ActionResult RedirectToAction(string actionName, string controllerName, object routeValues)
        {
            var routeVals = DotNetNuke.Web.Mvc.Common.TypeHelper.ObjectToDictionary(routeValues);
            routeVals["controller"] = controllerName;
            routeVals["action"] = actionName;
            return Redirect(ModuleRoutingProvider.Instance().GenerateUrl(routeVals, ModuleContext));
        }
    }
}
