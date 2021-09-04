// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.ASLApp.Models;
namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// IASLReEvaluationAttachmentRepository provides an interface for interacting with the ASLReEvaluationAttachment Repository(Database)
    /// </summary>
    public interface IASLReEvaluationAttachmentRepository
    {
        /// <summary>
        /// AddASLReEvaluationAttachment adds a ASLReEvaluationAttachment to the repository
        /// </summary>
        /// <param name="ASLReEvaluationAttachment">The ASLReEvaluationAttachment to add</param>
        /// <returns>The Id of the ASLReEvaluationAttachment</returns>
        int AddASLReEvaluationAttachment(ASLReEvaluationAttachment ASLReEvaluationAttachment);

        /// <summary>
        /// DeleteASLReEvaluationAttachment deletes a ASLReEvaluationAttachment from the repository
        /// </summary>
        /// <param name="ASLReEvaluationAttachment">The ASLReEvaluationAttachment to delete</param>
        void DeleteASLReEvaluationAttachment(ASLReEvaluationAttachment ASLReEvaluationAttachment);

        /// <summary>
        /// This GetASLReEvaluationAttachment  method retrieves a specific ASLReEvaluationAttachment in a portal
        /// </summary>
        /// <remarks>ASLReEvaluationAttachments are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="ASLReEvaluationAttachmentId">The Id of the ASLReEvaluationAttachment</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A single of ASLReEvaluationAttachment</returns>
        ASLReEvaluationAttachment GetASLReEvaluationAttachment(int ASLReEvaluationAttachmentId);

        /// <summary>
        /// This GetAttachmentsByParentASL overload retrieves all the ASLReEvaluationAttachments by ParentASL
        /// </summary>
        /// <remarks>ASLReEvaluationAttachments are cached by ParentASL, so this call will check the cache before going to the Database</remarks>
        /// <param name="ParentASL">ParentASL ID</param>
        /// <returns>A collection of ASLReEvaluationAttachments</returns>
        IQueryable<ASLReEvaluationAttachment> GetAttachmentsByParentASLReEvaluation(int ParentASLId);

        /// <summary>
        /// This GetASLReEvaluationAttachments overload retrieves a page of ASLReEvaluationAttachments for a ASL
        /// </summary>
        /// <remarks>ASLReEvaluationAttachments are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="ASLId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLReEvaluationAttachments</returns>
        IPagedList<ASLReEvaluationAttachment> GetASLReEvaluationAttachment(string searchTerm, int ParentASLId, int pageIndex, int pageSize);

        /// <summary>
        /// UpdateASLReEvaluationAttachment updates a ASLReEvaluationAttachment in the repository
        /// </summary>
        /// <param name="ASLReEvaluationAttachment">The ASLReEvaluationAttachment to update</param>
        void UpdateASLReEvaluationAttachment(ASLReEvaluationAttachment ASLReEvaluationAttachment);
    }
}


