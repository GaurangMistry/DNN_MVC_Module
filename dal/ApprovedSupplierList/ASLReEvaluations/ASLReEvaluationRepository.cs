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

namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// ASLReEvaluationRepository provides a concrete implemetation of the IASLReEvaluationRepository interface for interacting with the ASLReEvaluation Repository(Database)
    /// </summary>
    public class ASLReEvaluationRepository : ServiceLocator<IASLReEvaluationRepository, ASLReEvaluationRepository>, IASLReEvaluationRepository
    {
        protected override Func<IASLReEvaluationRepository> GetFactory()
        {
            return () => new ASLReEvaluationRepository();
        }

        /// <summary>
        /// AddASLReEvaluation adds a ASLReEvaluation to the repository
        /// </summary>
        /// <param name="ASLReEvaluation">The ASLReEvaluation to add</param>
        /// <returns>The Id of the ASLReEvaluation</returns>
        public int AddASLReEvaluation(ASLReEvaluation ASLReEvaluation)
        {
            Requires.NotNull(ASLReEvaluation);
            Requires.PropertyNotNegative(ASLReEvaluation, "PortalId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluation>();

                rep.Insert(ASLReEvaluation);
            }

            return ASLReEvaluation.ASLReEvaluationId;
        }

        /// <summary>
        /// DeleteASLReEvaluation deletes a ASLReEvaluation from the repository
        /// </summary>
        /// <param name="ASLReEvaluation">The ASLReEvaluation to delete</param>
        public void DeleteASLReEvaluation(ASLReEvaluation ASLReEvaluation)
        {
            Requires.NotNull(ASLReEvaluation);
            Requires.PropertyNotNegative(ASLReEvaluation, "ASLReEvaluationId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluation>();

                rep.Delete(ASLReEvaluation);
            }
        }

        public ASLReEvaluation GetASLReEvaluation(int ASLReEvaluationId)
        {
            Requires.NotNegative("ASLReEvaluationId", ASLReEvaluationId);
            using (var context = DataContext.Instance())
            {
               var rep= context.GetRepository<ASLReEvaluation>();
                var reeval= rep.GetById(ASLReEvaluationId);
                reeval.EvaluationResultName = InitialEvaluationResultRepository.Instance.
                    GetInitialEvaluationResult(reeval.InitialEvaluationResultId.Value).InitialEvaluationResultName ;
                return reeval;
            }
        }
        


        /// <summary>
        /// This GetASLReEvaluations overload retrieves a page of ASLReEvaluations for a portal
        /// </summary>
        /// <remarks>ASLReEvaluations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLReEvaluations</returns>

        public IPagedList<ASLReEvaluation> GetASLReEvaluations(int parentASLId,string searchTerm, int portalId, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = "";
            }
            Requires.NotNegative("parentASLId", parentASLId);
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluation>();
                var reEvals = rep.GetPage(parentASLId, pageIndex, pageSize).AsQueryable();
                foreach (ASLReEvaluation are in reEvals)
                {
                    are.EvaluationResultName = InitialEvaluationResultRepository.Instance.
                    GetInitialEvaluationResult(are.InitialEvaluationResultId.Value).InitialEvaluationResultName;
                }
                return new PagedList<ASLReEvaluation>(reEvals, pageIndex, pageSize);
            }
        }

        /// <summary>
        /// UpdateASLReEvaluation updates a ASLReEvaluation in the repository
        /// </summary>
        /// <param name="ASLReEvaluation">The ASLReEvaluation to update</param>
        public void UpdateASLReEvaluation(ASLReEvaluation ASLReEvaluation)
        {
            Requires.NotNull(ASLReEvaluation);
            Requires.PropertyNotNegative(ASLReEvaluation, "ASLReEvaluationId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluation>();

                rep.Update(ASLReEvaluation);
            }
        }
    }
}
