// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.ASLApp.Models;
namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// IASLReEvaluationRepository provides an interface for interacting with the ASLReEvaluation Repository(Database)
    /// </summary>
    public interface IASLReEvaluationRepository
    {
        /// <summary>
        /// AddASLReEvaluation adds a ASLReEvaluation to the repository
        /// </summary>
        /// <param name="ASLReEvaluation">The ASLReEvaluation to add</param>
        /// <returns>The Id of the ASLReEvaluation</returns>
        int AddASLReEvaluation(ASLReEvaluation ASLReEvaluation);

        /// <summary>
        /// DeleteASLReEvaluation deletes a ASLReEvaluation from the repository
        /// </summary>
        /// <param name="ASLReEvaluation">The ASLReEvaluation to delete</param>
        void DeleteASLReEvaluation(ASLReEvaluation ASLReEvaluation);

        /// <summary>
        /// This GetASLReEvaluation  method retrieves a specific ASLReEvaluation in a portal
        /// </summary>
        /// <remarks>ASLReEvaluations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="ASLReEvaluationId">The Id of the ASLReEvaluation</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A single of ASLReEvaluation</returns>
        ASLReEvaluation GetASLReEvaluation(int ASLReEvaluationId);

    
        /// <summary>
        /// This GetASLReEvaluations overload retrieves a page of ASLReEvaluations for a portal
        /// </summary>
        /// <remarks>ASLReEvaluations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLReEvaluations</returns>
        IPagedList<ASLReEvaluation> GetASLReEvaluations(int parentASLId,string searchTerm, int portalId, int pageIndex, int pageSize);

        /// <summary>
        /// UpdateASLReEvaluation updates a ASLReEvaluation in the repository
        /// </summary>
        /// <param name="ASLReEvaluation">The ASLReEvaluation to update</param>
        void UpdateASLReEvaluation(ASLReEvaluation ASLReEvaluation);
    }
}


