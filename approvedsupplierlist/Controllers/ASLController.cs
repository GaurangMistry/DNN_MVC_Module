// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Web.Mvc;
using WebXMS.DAL.ASLApp;
using WebXMS.DAL.ASLApp.Models;
using WebXMS.DAL.UniversalSettings;
using WebXMS.DAL.UniversalSettings.Models;
using WebXMS.DAL.DNN;
using WebXMS.DAL.DNN.Models;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System.Collections.Generic;
namespace WebXMS.Modules.ASLApp.Controllers
{
    /// <summary>
    /// ContactController is the MVC Controller class for managing Contacts in the UI
    /// </summary>
    [DnnHandleError]
    public class ASLController : DnnController
    {
        private readonly IASLRepository _repository;
        private readonly IASLCategoryRepository _catrepository;
        private readonly ICountryListRepository _countryrepository;
        private readonly IRegionListRepository _regionrepository;
        private readonly WebXMS.DAL.UniversalSettings.IInitialEvaluationResultRepository _resultsrepository;
        private readonly IUserRepository _userrepository;
        private readonly ICompanyLocationRepository _locationrepository;
        private readonly IASLSecondaryLocationRepository _AslSecondaryLocationRepository;
        private readonly IASLAttachmentRepository _AttachmentsRepository;

        /// <summary>
        /// Default Constructor constructs a new ASLController
        /// </summary>
        public ASLController() : this(ASLRepository.Instance, ASLCategoryRepository.Instance,CountryListRepository.Instance
            ,RegionListRepository.Instance, InitialEvaluationResultRepository.Instance, UserRepository.Instance
            ,CompanyLocationRepository.Instance, ASLSecondaryLocationRepository.Instance, ASLAttachmentRepository.Instance) { }

        /// <summary>
        /// Constructor constructs a new ASLController with a passed in repository
        /// </summary>
        public ASLController(IASLRepository repository, IASLCategoryRepository catrepository, 
            ICountryListRepository countryrepository, IRegionListRepository regionrepository,
            IInitialEvaluationResultRepository resrepo,IUserRepository userepo,ICompanyLocationRepository locrepo
            ,IASLSecondaryLocationRepository seclocrepo, IASLAttachmentRepository attrepo)
        {
            Requires.NotNull(repository);

            _repository = repository;
            _catrepository = catrepository;
            _countryrepository = countryrepository;
            _regionrepository = regionrepository;
            _resultsrepository = resrepo;
            _userrepository = userepo;
            _locationrepository = locrepo;
            _AslSecondaryLocationRepository = seclocrepo;
            _AttachmentsRepository = attrepo;
        }

        /// <summary>
        /// The Delete View
        /// </summary>
        /// <param name="ASLId">The Id of the ASL to delete</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int ASLId)
        {
            var ASL = (ASLId == -1)
                        ? new ASL { PortalId = PortalSettings.PortalId }
                        : _repository.GetASL(ASLId);

            return View(ASL);
        }
        /// <summary>
        /// Actual Delete 
        /// </summary>
        /// <param name="ASL">the ASL to delete</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Delete(ASL ASL)
        {
            if (ModelState.IsValid)
            {
                _repository.DeleteASL(ASL);
                return RedirectToAction("Index");
            }
            else
            {
                return View(ASL);
            }
        }

        /// <summary>
        /// This Edit method is used to set up editing a ASL
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int ASLId = -1)
        {
            var ASL = (ASLId == -1)
                        ? new ASL { PortalId = PortalSettings.PortalId }
                        : _repository.GetASL(ASLId);
            
            var categories = _catrepository.GetASLCategory(PortalSettings.PortalId).ToList();
            var catsList = from cat in categories.Cast<ASLCategory>().ToList()
                        select new SelectListItem { Text = cat.CategoryName, Value = cat.ASLCategoryId.ToString() };
            var countries = _countryrepository.GetCountryLists();
            var countryList = from country in countries.Cast<CountryList>().ToList()
                              select new SelectListItem { Text = country.Text, Value = country.Value};

            var results = _resultsrepository.GetInitialEvaluationResults(PortalSettings.PortalId);
            var resultsList = from res in results.Cast<InitialEvaluationResult>().ToList()
                              select new SelectListItem { Text = res.InitialEvaluationResultName,
                                  Value = res.InitialEvaluationResultId.ToString(), Selected= (res.InitialEvaluationResultId == ASL.InitialEvaluationResultId)};

            var users = _userrepository.GetUsers(PortalSettings.PortalId);
            var usersList = from user in users.Cast<User>().ToList()
                              select new SelectListItem { Text = user.FirstName + user.LastName, Value = user.UserID.ToString(), Selected=ASL.InitialEvaluationBy == user.UserID };

            var locations = _locationrepository.GetCompanyLocations(PortalSettings.PortalId);
            var locationsList = from loc in locations.Cast<CompanyLocation>().ToList()
                            select new SelectListItem { Text = loc.Location, Value = loc.CompanyLocationId.ToString(), Selected= ASL.SuppliedCompanyLocations!=null? ASL.SuppliedCompanyLocations.Split(new char[] { ',' }).ToList().Contains(loc.CompanyLocationId.ToString()):false };

            if (ASLId > 0)
            { 
                var suppliersecondatylocs = _AslSecondaryLocationRepository.GetLocationsByParentASL(ASLId);
                ViewBag.SavedSupplierSecondaryLocations = suppliersecondatylocs;
            }
            if (ASLId > 0)
            {
                var attachments = _AttachmentsRepository.GetAttachmentsByParentASL(ASLId);
                ViewBag.SavedAttachments = attachments;
            }
            ViewBag.ASLCategories = catsList;
            ViewBag.MainLocationCountries = countryList;
            ViewBag.SecondaryLocationCountry = countryList;
            if (ASL.MainLocationCountry != null)
            {
                var states = _regionrepository.GetRegionLists(ASL.MainLocationCountry);
                var statesList = from loc in states.Cast<RegionList>().ToList()
                                 select new SelectListItem { Text = loc.Text, Value = loc.Value.ToString() };

                ViewBag.MainLocationStates = statesList;
            }
            else
            {
                ViewBag.MainLocationStates = new List<SelectListItem>();
            }
            ViewBag.SecondaryLocationState = new List<SelectListItem>();
            ViewBag.InitialEvaluationResults = resultsList;
            ViewBag.InitialEvaluationByUsers = usersList;
            ViewBag.VMSuppliedCompanyLocations = locationsList;
            return View(ASL);
        }

        /// <summary>
        /// This Edit method is used to save the posted ASL
        /// </summary>
        /// <param name="ASL">The ASL to save</param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(ASL ASL)
        {
            if (ModelState.IsValid)
            {
                ASL.PortalId = PortalSettings.PortalId;
                ASL.SuppliedCompanyLocations = string.Join(",", ASL.VMSuppliedCompanyLocations.Select(x => x.ToString()).ToArray());
                
                if (ASL.ASLId == -1)
                {
                    _repository.AddASL((ASL)ASL);
                   
                }
                else
                {
                    _repository.UpdateASL(ASL);
                }
                //secondary locations are posted as JSON string 
                if (ASL.SecondaryLocations.Length > 0)
                {
                    List<ASLSecondaryLocation> lstlocs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ASLSecondaryLocation>>(ASL.SecondaryLocations);
                    foreach (ASLSecondaryLocation loc in lstlocs)
                    {
                        loc.ParentASLId = ASL.ASLId;
                        _AslSecondaryLocationRepository.AddASLSecondaryLocation(loc);
                    }
                }
                //Files are posted as JSON string 
                if (ASL.upload.Length > 0)
                {

                    foreach (string jsonfile in ASL.upload)
                    {
                        if (jsonfile != null) { 
                        PostFile fileJson = Newtonsoft.Json.JsonConvert.DeserializeObject<PostFile>(jsonfile);

                        ASLAttachment att = new ASLAttachment();
                        att.FileName = fileJson.name;
                        att.MimeType = fileJson.type;
                        att.ParentASLId = ASL.ASLId;
                        att.PortalId = PortalSettings.PortalId;
                        att.FileBinary = Convert.FromBase64String(fileJson.data);
                        _AttachmentsRepository.AddASLAttachment(att);
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                var categories = _catrepository.GetASLCategory(PortalSettings.PortalId).ToList();
                var catsList = from cat in categories.Cast<ASLCategory>().ToList()
                               select new SelectListItem { Text = cat.CategoryName, Value = cat.ASLCategoryId.ToString() };
                var countries = _countryrepository.GetCountryLists();
                var countryList = from country in countries.Cast<CountryList>().ToList()
                                  select new SelectListItem { Text = country.Text, Value = country.Value };

                var results = _resultsrepository.GetInitialEvaluationResults(PortalSettings.PortalId);
                var resultsList = from res in results.Cast<InitialEvaluationResult>().ToList()
                                  select new SelectListItem { Text = res.InitialEvaluationResultName, Value = res.InitialEvaluationResultId.ToString() };

                var users = _userrepository.GetUsers(PortalSettings.PortalId);
                var usersList = from user in users.Cast<User>().ToList()
                                select new SelectListItem { Text = user.FirstName + user.LastName, Value = user.UserID.ToString() };

                var locations = _locationrepository.GetCompanyLocations(PortalSettings.PortalId);
                var locationsList = from loc in locations.Cast<CompanyLocation>().ToList()
                                    select new SelectListItem { Text = loc.Location, Value = loc.CompanyLocationId.ToString() };

                ViewBag.ASLCategories = catsList;
                ViewBag.MainLocationCountries = countryList;
                ViewBag.SecondaryLocationCountry = countryList;
                if (ASL.MainLocationCountry != null)
                {
                    var states = _regionrepository.GetRegionLists(ASL.MainLocationCountry);
                    var statesList = from loc in states.Cast<RegionList>().ToList()
                                        select new SelectListItem { Text = loc.Text, Value = loc.Value.ToString() };

                    ViewBag.MainLocationStates = statesList;
                }
                else {
                    ViewBag.MainLocationStates = new List<SelectListItem>();
                }
                
                ViewBag.SecondaryLocationState = new List<SelectListItem>();
                ViewBag.InitialEvaluationResults = resultsList;
                ViewBag.InitialEvaluationByUsers = usersList;
                ViewBag.VMSuppliedCompanyLocations = locationsList;
                ASL.ErrorFields = new List<string>();
               var Errors = ModelState.Keys.Where(i => ModelState[i].Errors.Count > 0);
                if (Errors.Count() > 0)
                {
                    foreach (var err in Errors)
                        ASL.ErrorFields.Add(err);
                }
                return View(ASL);
            }
        }

        /// <summary>
        /// The Index method is the default Action method.
        /// </summary>
        /// <param name="searchTerm">Term to search.</param>
        /// <param name="pageIndex">Index of the current page.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <returns></returns>
        /// 
        [ModuleAction(ControlKey = "Categories", TitleKey = "ManageCategories")]
        public ActionResult Index(string searchTerm = "", int pageIndex = 0)
        {
            var ASL = _repository.GetASL(searchTerm, PortalSettings.PortalId, pageIndex, ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("PageSize", 10));

            return View(ASL);
        }


        public JsonResult GetRegions(string countryCode)
        {
            var data = _regionrepository.GetRegionLists(countryCode);

            return new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DeleteSupplierLocation(int SupplierLocationId)
        {
            var asl = _AslSecondaryLocationRepository.GetASLSecondaryLocation(SupplierLocationId);
            _AslSecondaryLocationRepository.DeleteASLSecondaryLocation(asl);
            
            return new JsonResult()
            {
                Data = 1,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DeleteAttachment(int AttachmentId)
        {
            var att = _AttachmentsRepository.GetASLAttachment(AttachmentId);
            _AttachmentsRepository.DeleteASLAttachment(att);

            return new JsonResult()
            {
                Data = 1,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// Download ASL Attachment.
        /// </summary>
        /// <returns></returns>
        /// 
        public FileResult GetFile(int ASLAttachmentId)
        {
            var att = _AttachmentsRepository.GetASLAttachment(ASLAttachmentId);
            byte[] fileBytes = att.FileBinary;
            string fileName = att.FileName;
            return File(att.FileBinary, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
         }
    }
}
