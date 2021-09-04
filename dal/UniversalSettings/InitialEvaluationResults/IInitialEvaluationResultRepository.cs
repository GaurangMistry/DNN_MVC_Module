// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.UniversalSettings.Models;
namespace WebXMS.DAL.UniversalSettings
{
    /// <summary>
    /// IInitialEvaluationResultRepository provides an interface for interacting with the InitialEvaluationResult Repository(Database)
    /// </summary>
    public interface IInitialEvaluationResultRepository
    {
        /// <summary>
        /// AddInitialEvaluationResult adds a InitialEvaluationResult to the repository
        /// </summary>
        /// <param name="InitialEvaluationResult">The InitialEvaluationResult to add</param>
        /// <returns>The Id of the InitialEvaluationResult</returns>
        int AddInitialEvaluationResult(InitialEvaluationResult InitialEvaluationResult);

        /// <summary>
        /// DeleteInitialEvaluationResult deletes a InitialEvaluationResult from the repository
        /// </summary>
        /// <param name="InitialEvaluationResult">The InitialEvaluationResult to delete</param>
        void DeleteInitialEvaluationResult(InitialEvaluationResult InitialEvaluationResult);

        /// <summary>
        /// This GetInitialEvaluationResult  method retrieves a specific InitialEvaluationResult in a portal
        /// </summary>
        /// <remarks>InitialEvaluationResults are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="InitialEvaluationResultId">The Id of the InitialEvaluationResult</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A single of InitialEvaluationResult</returns>
        InitialEvaluationResult GetInitialEvaluationResult(int InitialEvaluationResultId);

        /// <summary>
        /// This GetInitialEvaluationResults overload retrieves all the InitialEvaluationResults for a portal
        /// </summary>
        /// <remarks>InitialEvaluationResults are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A collection of InitialEvaluationResults</returns>
        IQueryable<InitialEvaluationResult> GetInitialEvaluationResults(int portalId);

        /// <summary>
        /// This GetInitialEvaluationResults overload retrieves a page of InitialEvaluationResults for a portal
        /// </summary>
        /// <remarks>InitialEvaluationResults are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of InitialEvaluationResults</returns>
        IPagedList<InitialEvaluationResult> GetInitialEvaluationResults(string searchTerm, int portalId, int pageIndex, int pageSize);

        /// <summary>
        /// UpdateInitialEvaluationResult updates a InitialEvaluationResult in the repository
        /// </summary>
        /// <param name="InitialEvaluationResult">The InitialEvaluationResult to update</param>
        void UpdateInitialEvaluationResult(InitialEvaluationResult InitialEvaluationResult);
    }
}


