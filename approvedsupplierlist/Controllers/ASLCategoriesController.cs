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

namespace WebXMS.Modules.ASLApp.Controllers
{
    /// <summary>
    /// ContactController is the MVC Controller class for managing Contacts in the UI
    /// </summary>
    [DnnHandleError]
    public class ASLCategoriesController : DnnController
    {
        private readonly IASLCategoryRepository _repository;

        /// <summary>
        /// Default Constructor constructs a new ASLCategoryController
        /// </summary>
        public ASLCategoriesController() : this(ASLCategoryRepository.Instance) { }

        /// <summary>
        /// Constructor constructs a new ASLCategoryController with a passed in repository
        /// </summary>
        public ASLCategoriesController(IASLCategoryRepository repository)
        {
            Requires.NotNull(repository);

            _repository = repository;
        }

        /// <summary>
        /// The Delete View
        /// </summary>
        /// <param name="ASLCategoryId">The Id of the ASLCategory to delete</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int ASLCategoryId)
        {
            var ASLCategory = (ASLCategoryId == -1)
                        ? new ASLCategory { PortalId = PortalSettings.PortalId }
                        : _repository.GetASLCategory(ASLCategoryId, PortalSettings.PortalId);

            return View(ASLCategory);
        }
        /// <summary>
        /// Actual Delete 
        /// </summary>
        /// <param name="ASLCategory">the ASLCategory to delete</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Delete(ASLCategory ASLCategory)
        {
            if (ModelState.IsValid)
            {
                _repository.DeleteASLCategory(ASLCategory);
                return RedirectToAction("Index");
            }
            else
            {
                return View(ASLCategory);
            }
        }

        /// <summary>
        /// This Edit method is used to set up editing a ASLCategory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int ASLCategoryId = -1)
        {
            var ASLCategory = (ASLCategoryId == -1)
                        ? new ASLCategory { PortalId = PortalSettings.PortalId }
                        : _repository.GetASLCategory(ASLCategoryId, PortalSettings.PortalId);

            return View(ASLCategory);
        }

        /// <summary>
        /// This Edit method is used to save the posted ASLCategory
        /// </summary>
        /// <param name="ASLCategory">The ASLCategory to save</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(ASLCategory ASLCategory)
        {
            if (ModelState.IsValid)
            {
                ASLCategory.PortalId = PortalSettings.PortalId;

                if (ASLCategory.ASLCategoryId == -1)
                {
                    _repository.AddASLCategory(ASLCategory);
                }
                else
                {
                    _repository.UpdateASLCategory(ASLCategory);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(ASLCategory);
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
            var ASLCategory = _repository.GetASLCategory(searchTerm, PortalSettings.PortalId, pageIndex, ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("PageSize", 10));

            return View(ASLCategory);
        }

        public ActionResult PartialIndex(string searchTerm = "", int pageIndex = 0)
        {
            var ASLCategory = _repository.GetASLCategory(searchTerm, PortalSettings.PortalId, pageIndex, ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("PageSize", 10));

            return PartialView(ASLCategory);
        }
    }
}
