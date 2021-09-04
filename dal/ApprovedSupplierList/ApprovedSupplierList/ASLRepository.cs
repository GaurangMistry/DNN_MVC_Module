// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using WebXMS.DAL.ASLApp.Models;
using WebXMS.DAL.UniversalSettings;
using WebXMS.DAL.WebXMS.Models;
namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// ASLRepository provides a concrete implemetation of the IASLRepository interface for interacting with the ASL Repository(Database)
    /// </summary>
    public class ASLRepository : ServiceLocator<IASLRepository, ASLRepository>, IASLRepository
    {
        protected override Func<IASLRepository> GetFactory()
        {
            return () => new ASLRepository();
        }

        /// <summary>
        /// AddASL adds a ASL to the repository
        /// </summary>
        /// <param name="ASL">The ASL to add</param>
        /// <returns>The Id of the ASL</returns>
        public int AddASL(ASL ASL)
        {
            Requires.NotNull(ASL);
            Requires.PropertyNotNegative(ASL, "PortalId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASL>();

                rep.Insert(ASL);
            }

            return ASL.ASLId;
        }

        /// <summary>
        /// DeleteASL deletes a ASL from the repository
        /// </summary>
        /// <param name="ASL">The ASL to delete</param>
        public void DeleteASL(ASL ASL)
        {
            Requires.NotNull(ASL);
            Requires.PropertyNotNegative(ASL, "ASLId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASL>();

                rep.Delete(ASL);
            }
        }

        public ASL GetASL(int ASLId)
        {
            Requires.NotNegative("ASLReEvaluationId", ASLId);
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASL>();
                return rep.GetById(ASLId);
            }
        }


        /// <summary>
        /// This GetASLs overload retrieves a page of ASLs for a portal
        /// </summary>
        /// <remarks>ASLs are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLs</returns>

        public XMSPagedList<ASL> GetASL(string searchTerm, int portalId, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = "";
            }
            Requires.NotNegative("portalId", portalId);
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASL>();
                var ASLs = rep.Get(portalId).Where(c => c.SupplierName.Contains(searchTerm)
                                                    || c.Status.Contains(searchTerm) ||
                                                    c.ContactName.Contains(searchTerm));
                //var ASLs = rep.GetPage(portalId,pageIndex,pageSize).AsQueryable();
                foreach (ASL a in ASLs)
                {
                    a.CategoryName = ASLCategoryRepository.Instance.GetASLCategory(a.ASLCategoryId.Value, portalId).CategoryName;
                    a.InitialEvaluationResultName = InitialEvaluationResultRepository.Instance.GetInitialEvaluationResult(a.InitialEvaluationResultId.Value).InitialEvaluationResultName;
                    a.ReEvaluations = ASLReEvaluationRepository.Instance.GetASLReEvaluations(a.ASLId, "", portalId, 0, 10000).ToList();
                    if (a.ReEvaluations.Count > 0)
                    {
                        ASLReEvaluation lastReEval = a.ReEvaluations.OrderByDescending(i => i.EvaluationDate).First();
                        a.NextReEvaluationDate = lastReEval.NextReEvaluationDate.Value;
                        a.LastReEvaluationDate = lastReEval.EvaluationDate.Value;
                        a.Status = lastReEval.EvaluationResultName;
                        a.LastReEvaluationBy = lastReEval.EvaluationBy;
                    }
                    else
                    {
                        a.NextReEvaluationDate = a.InitialEvaluationDate.Value.AddMonths(a.ReevaluationInterval.Value);
                        a.LastReEvaluationDate = a.InitialEvaluationDate.Value;
                        a.Status = a.InitialEvaluationResultName;
                    }


                }

                var list =  new PagedList<ASL>(ASLs, pageIndex, pageSize);
                XMSPagedList<ASL> xmsList = new XMSPagedList<ASL>();
                xmsList.filter = new ASL();
                xmsList.results = list;
                return xmsList;

            }
            
        }


        public XMSPagedList<ASL> GetASL(ASL filter)
        {
            Requires.NotNegative("portalId", filter.PortalId);
            if (filter.FilterSortBy == null)
            { filter.FilterSortBy = nameof(ASL.ASLId); }
            if (filter.FilterSortDirection == null)
            {
                filter.FilterSortDirection = "DESC";
            }
            if (filter.FilterPageIndex == null)
            { filter.FilterPageIndex = 0; }
            if (filter.FilterPageSize == null)
            { filter.FilterPageSize = 10; }
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASL>();
                IQueryable<ASL> ASLs = null;
                if (filter.FilterSortDirection == "ASC" && filter.FilterSortBy != null)
                    ASLs = rep.Get(filter.PortalId).OrderBy(i => i.SupplierName).AsQueryable();// ASLs.OrderBy(i => filter.FilterSortBy);
                if (filter.FilterSortDirection == "DESC" && filter.FilterSortBy != null)
                    ASLs = rep.Get(filter.PortalId).OrderByDescending(i => i.SupplierName).AsQueryable();// ASLs.(i => filter.FilterSortBy);
                
                if (filter.SupplierName!=null && filter.SupplierName.Length>0)
                    ASLs = ASLs.Where(c => c.SupplierName.ToUpper().Contains(filter.SupplierName.ToUpper()));
                if (filter.ASLCategoryId != null)
                    ASLs = ASLs.Where(c => c.ASLCategoryId== filter.ASLCategoryId);
                if (filter.ScopeOfService != null)
                    ASLs = ASLs.Where(c => c.ScopeOfService.ToUpper().Contains(filter.ScopeOfService.ToUpper()));
                if (filter.IsActive != null)
                    ASLs = ASLs.Where(c => c.IsActive == filter.IsActive);
                if (filter.ContactName != null)
                    ASLs = ASLs.Where(c => c.ContactName.ToUpper().Contains(filter.ContactName.ToUpper()));
                if (filter.ContactEmail != null)
                    ASLs = ASLs.Where(c => c.ContactEmail.ToUpper().Contains(filter.ContactEmail.ToUpper()));
                if (filter.ContactPhone != null)
                    ASLs = ASLs.Where(c => c.ContactPhone.ToUpper().Contains(filter.ContactPhone.ToUpper()));
                if (filter.InitialEvaluationResultId != null)
                    ASLs = ASLs.Where(c => c.InitialEvaluationResultId == filter.InitialEvaluationResultId);
                if (filter.FilterInitialEvalFrom != null)
                    ASLs = ASLs.Where(c => c.InitialEvaluationDate> filter.FilterInitialEvalFrom);
                if (filter.FilterInitialEvalTo != null)
                    ASLs = ASLs.Where(c => c.InitialEvaluationDate < filter.FilterInitialEvalTo);
  
                foreach (ASL a in ASLs)
                {
                    a.CategoryName = ASLCategoryRepository.Instance.GetASLCategory(a.ASLCategoryId.Value, filter.PortalId).CategoryName;
                    a.InitialEvaluationResultName = InitialEvaluationResultRepository.Instance.GetInitialEvaluationResult(a.InitialEvaluationResultId.Value).InitialEvaluationResultName;
                    a.ReEvaluations = ASLReEvaluationRepository.Instance.GetASLReEvaluations(a.ASLId, "", filter.PortalId, 0, 10000).ToList();
                    if (a.ReEvaluations.Count > 0)
                    {
                        ASLReEvaluation lastReEval = a.ReEvaluations.OrderByDescending(i => i.EvaluationDate).First();
                        a.NextReEvaluationDate = lastReEval.NextReEvaluationDate.Value;
                        a.LastReEvaluationDate = lastReEval.EvaluationDate.Value;
                        a.Status = lastReEval.EvaluationResultName;
                        a.LastReEvaluationBy = lastReEval.EvaluationBy;
                    }
                    else
                    {
                        a.NextReEvaluationDate = a.InitialEvaluationDate.Value.AddMonths(a.ReevaluationInterval.Value);
                        a.LastReEvaluationDate = a.InitialEvaluationDate.Value;
                        a.Status = a.InitialEvaluationResultName;
                    }
                }
                if (filter.FilterReEvaluationFrom != null)
                    ASLs = ASLs.Where(c => c.LastReEvaluationDate > filter.FilterReEvaluationFrom);

                if (filter.FilterReEvaluationTo != null)
                    ASLs = ASLs.Where(c => c.LastReEvaluationDate < filter.FilterReEvaluationTo);

                if (filter.FilterReEvaluationBy != null)
                    ASLs = ASLs.Where(c => c.LastReEvaluationBy == filter.FilterReEvaluationBy);





                var list = new PagedList<ASL>(ASLs, filter.FilterPageIndex.Value, filter.FilterPageSize.Value);
                XMSPagedList<ASL> xmsList = new XMSPagedList<ASL>();
                xmsList.filter = filter;
                xmsList.results = list;
                return xmsList;

            }

        }

        public XMSChartModel GetChartSupplierByCategory(int portalId, string catName="") {
            XMSChartModel chart = new XMSChartModel();
            
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASL>();
                var repCats = context.GetRepository<ASLCategory>();
                IQueryable<ASLCategory> cats = repCats.Get(portalId).AsQueryable();
                foreach (ASLCategory cat in cats)
                {
                    chart.labels.Add(cat.CategoryName);
                    int total = rep.Get(portalId).Where(i => i.ASLCategoryId == cat.ASLCategoryId).Count();
                    chart.data.Add(total);
                    if (total > chart.max)
                        chart.max = total;
                    if (total < chart.min)
                        chart.min = total;
                }
            }
            return chart;
        }

        public XMSChartModel GetChartSupplierByStatus(int portalId, string catName = "")
        {
            XMSChartModel chart = new XMSChartModel();

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASL>();
                 var stats = InitialEvaluationResultRepository.Instance.GetInitialEvaluationResults(portalId);
                foreach (UniversalSettings.Models.InitialEvaluationResult st in stats)
                {
                    chart.labels.Add(st.InitialEvaluationResultName);
                    int total = rep.Get(portalId).Where(i => i.InitialEvaluationResultId == st.InitialEvaluationResultId).Count();
                    chart.data.Add(total);
                    if (total > chart.max)
                        chart.max = total;
                    if (total < chart.min)
                        chart.min = total;
                }
            }
            return chart;
        }

        public XMSChartModel GetChartSupplierByReEval(int portalId, string catName = "")
        {
            XMSChartModel chart = new XMSChartModel();

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluation>();
                var stats = InitialEvaluationResultRepository.Instance.GetInitialEvaluationResults(portalId);
                var repeval = context.GetRepository<ASLReEvaluation>();

                foreach (UniversalSettings.Models.InitialEvaluationResult st in stats)
                {
                    chart.labels.Add(st.InitialEvaluationResultName);
                    
                    int total = repeval.Get().Where(i=>i.PortalId==portalId && i.InitialEvaluationResultId == st.InitialEvaluationResultId).Count();
                    chart.data.Add(total);
                    if (total > chart.max)
                        chart.max = total;
                    if (total < chart.min)
                        chart.min = total;
                }
            }
            return chart;
        }

        public XMSChartModel GetChartByReEvaluationDate(int portalId, string catName = "")
        {
            XMSChartModel chart = new XMSChartModel();

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluation>();
                var stats = InitialEvaluationResultRepository.Instance.GetInitialEvaluationResults(portalId);
                var repeval = context.GetRepository<ASLReEvaluation>();
                var numberOfComingDue= repeval.Get().Where(i => i.PortalId == portalId && i.NextReEvaluationDate > System.DateTime.Now).Count();
                var numberOfPastDue = repeval.Get().Where(i => i.PortalId == portalId && i.NextReEvaluationDate < System.DateTime.Now).Count();
                chart.data.Add(numberOfComingDue);
                chart.data.Add(numberOfPastDue);
            }
            return chart;
        }
        /// <summary>
        /// UpdateASL updates a ASL in the repository
        /// </summary>
        /// <param name="ASL">The ASL to update</param>
        public void UpdateASL(ASL ASL)
        {
            Requires.NotNull(ASL);
            Requires.PropertyNotNegative(ASL, "ASLId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASL>();

                rep.Update(ASL);
            }
        }
    }
}
