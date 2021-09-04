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
using WebXMS.DAL.UniversalSettings.Models;
namespace WebXMS.DAL.UniversalSettings
{
    /// <summary>
    /// InitialEvaluationResultRepository provides a concrete implemetation of the IInitialEvaluationResultRepository interface for interacting with the InitialEvaluationResult Repository(Database)
    /// </summary>
    public class InitialEvaluationResultRepository : ServiceLocator<IInitialEvaluationResultRepository, InitialEvaluationResultRepository>, IInitialEvaluationResultRepository
    {
        protected override Func<IInitialEvaluationResultRepository> GetFactory()
        {
            return () => new InitialEvaluationResultRepository();
        }

        /// <summary>
        /// AddInitialEvaluationResult adds a InitialEvaluationResult to the repository
        /// </summary>
        /// <param name="InitialEvaluationResult">The InitialEvaluationResult to add</param>
        /// <returns>The Id of the InitialEvaluationResult</returns>
        public int AddInitialEvaluationResult(InitialEvaluationResult InitialEvaluationResult)
        {
            Requires.NotNull(InitialEvaluationResult);
            Requires.PropertyNotNegative(InitialEvaluationResult, "PortalId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<InitialEvaluationResult>();

                rep.Insert(InitialEvaluationResult);
            }

            return InitialEvaluationResult.InitialEvaluationResultId;
        }

        /// <summary>
        /// DeleteInitialEvaluationResult deletes a InitialEvaluationResult from the repository
        /// </summary>
        /// <param name="InitialEvaluationResult">The InitialEvaluationResult to delete</param>
        public void DeleteInitialEvaluationResult(InitialEvaluationResult InitialEvaluationResult)
        {
            Requires.NotNull(InitialEvaluationResult);
            Requires.PropertyNotNegative(InitialEvaluationResult, "InitialEvaluationResultId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<InitialEvaluationResult>();

                rep.Delete(InitialEvaluationResult);
            }
        }

        public InitialEvaluationResult GetInitialEvaluationResult(int InitialEvaluationResultId)
        {
            Requires.NotNegative("InitialEvaluationResultId", InitialEvaluationResultId);
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<InitialEvaluationResult>();
                var reeval = rep.GetById(InitialEvaluationResultId);
                return reeval;
            }
        }
    

        /// <summary>
        /// This GetInitialEvaluationResults overload retrieves all the InitialEvaluationResults for a portal
        /// </summary>
        /// <remarks>InitialEvaluationResults are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A collection of InitialEvaluationResults</returns>
        public IQueryable<InitialEvaluationResult> GetInitialEvaluationResults(int portalId)
        {
            Requires.NotNegative("portalId", portalId);

            IQueryable<InitialEvaluationResult> InitialEvaluationResults = null;

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<InitialEvaluationResult>();

                InitialEvaluationResults = rep.Get(portalId).AsQueryable();
            }

            return InitialEvaluationResults;
        }

        /// <summary>
        /// This GetInitialEvaluationResults overload retrieves a page of InitialEvaluationResults for a portal
        /// </summary>
        /// <remarks>InitialEvaluationResults are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of InitialEvaluationResults</returns>

        public IPagedList<InitialEvaluationResult> GetInitialEvaluationResults(string searchTerm, int portalId, int pageIndex, int pageSize)
        {
            Requires.NotNegative("portalId", portalId);

            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = "";
            }
            var InitialEvaluationResults = GetInitialEvaluationResults(portalId);


            return new PagedList<InitialEvaluationResult>(InitialEvaluationResults, pageIndex, pageSize);
        }

        /// <summary>
        /// UpdateInitialEvaluationResult updates a InitialEvaluationResult in the repository
        /// </summary>
        /// <param name="InitialEvaluationResult">The InitialEvaluationResult to update</param>
        public void UpdateInitialEvaluationResult(InitialEvaluationResult InitialEvaluationResult)
        {
            Requires.NotNull(InitialEvaluationResult);
            Requires.PropertyNotNegative(InitialEvaluationResult, "InitialEvaluationResultId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<InitialEvaluationResult>();

                rep.Update(InitialEvaluationResult);
            }
        }
    }
}
