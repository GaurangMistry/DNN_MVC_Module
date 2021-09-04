// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.ASLApp.Models;
namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// IASLAttachmentRepository provides an interface for interacting with the ASLAttachment Repository(Database)
    /// </summary>
    public interface IASLAttachmentRepository
    {
        /// <summary>
        /// AddASLAttachment adds a ASLAttachment to the repository
        /// </summary>
        /// <param name="ASLAttachment">The ASLAttachment to add</param>
        /// <returns>The Id of the ASLAttachment</returns>
        int AddASLAttachment(ASLAttachment ASLAttachment);

        /// <summary>
        /// DeleteASLAttachment deletes a ASLAttachment from the repository
        /// </summary>
        /// <param name="ASLAttachment">The ASLAttachment to delete</param>
        void DeleteASLAttachment(ASLAttachment ASLAttachment);

        /// <summary>
        /// This GetASLAttachment  method retrieves a specific ASLAttachment in a portal
        /// </summary>
        /// <remarks>ASLAttachments are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="ASLAttachmentId">The Id of the ASLAttachment</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A single of ASLAttachment</returns>
        ASLAttachment GetASLAttachment(int ASLAttachmentId);

        /// <summary>
        /// This GetAttachmentsByParentASL overload retrieves all the ASLAttachments by ParentASL
        /// </summary>
        /// <remarks>ASLAttachments are cached by ParentASL, so this call will check the cache before going to the Database</remarks>
        /// <param name="ParentASL">ParentASL ID</param>
        /// <returns>A collection of ASLAttachments</returns>
        IQueryable<ASLAttachment> GetAttachmentsByParentASL(int ParentASLId);

        /// <summary>
        /// This GetASLAttachments overload retrieves a page of ASLAttachments for a ASL
        /// </summary>
        /// <remarks>ASLAttachments are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="ASLId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLAttachments</returns>
        IPagedList<ASLAttachment> GetASLAttachment(string searchTerm, int ParentASLId, int pageIndex, int pageSize);

        /// <summary>
        /// UpdateASLAttachment updates a ASLAttachment in the repository
        /// </summary>
        /// <param name="ASLAttachment">The ASLAttachment to update</param>
        void UpdateASLAttachment(ASLAttachment ASLAttachment);
    }
}


