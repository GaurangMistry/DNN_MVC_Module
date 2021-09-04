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
    public class CompanyLocationsController : DnnController
    {
        private readonly ICompanyLocationRepository _repository;

        /// <summary>
        /// Default Constructor constructs a new CompanyLocationController
        /// </summary>
        public CompanyLocationsController() : this(CompanyLocationRepository.Instance) { }

        /// <summary>
        /// Constructor constructs a new CompanyLocationController with a passed in repository
        /// </summary>
        public CompanyLocationsController(ICompanyLocationRepository repository)
        {
            Requires.NotNull(repository);

            _repository = repository;
        }

        /// <summary>
        /// The Delete View
        /// </summary>
        /// <param name="CompanyLocationId">The Id of the CompanyLocation to delete</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int CompanyLocationId)
        {
            var CompanyLocation = (CompanyLocationId == -1)
                        ? new CompanyLocation { PortalId = PortalSettings.PortalId }
                        : _repository.GetCompanyLocation(CompanyLocationId, PortalSettings.PortalId);

            return View(CompanyLocation);
        }
        /// <summary>
        /// Actual Delete 
        /// </summary>
        /// <param name="CompanyLocation">the CompanyLocation to delete</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Delete(CompanyLocation CompanyLocation)
        {
            if (ModelState.IsValid)
            {
                _repository.DeleteCompanyLocation(CompanyLocation);
                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyLocation);
            }
        }

        /// <summary>
        /// This Edit method is used to set up editing a CompanyLocation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int CompanyLocationId = -1)
        {
            var CompanyLocation = (CompanyLocationId == -1)
                        ? new CompanyLocation { PortalId = PortalSettings.PortalId }
                        : _repository.GetCompanyLocation(CompanyLocationId, PortalSettings.PortalId);

            return View(CompanyLocation);
        }

        /// <summary>
        /// This Edit method is used to save the posted CompanyLocation
        /// </summary>
        /// <param name="CompanyLocation">The CompanyLocation to save</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyLocation CompanyLocation)
        {
            if (ModelState.IsValid)
            {
                CompanyLocation.PortalId = PortalSettings.PortalId;

                if (CompanyLocation.CompanyLocationId == -1)
                {
                    _repository.AddCompanyLocation(CompanyLocation);
                }
                else
                {
                    _repository.UpdateCompanyLocation(CompanyLocation);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyLocation);
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
            var CompanyLocations = _repository.GetCompanyLocations(searchTerm, PortalSettings.PortalId, pageIndex, ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("PageSize", 10));

            return View(CompanyLocations);
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
            CompanyLocation c = new CompanyLocation();
            return View("Test", c);
        }
    }
}
