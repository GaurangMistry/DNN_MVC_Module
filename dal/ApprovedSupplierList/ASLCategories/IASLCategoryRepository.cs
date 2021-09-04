// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.ASLApp.Models;
namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// IASLCategoryRepository provides an interface for interacting with the ASLCategory Repository(Database)
    /// </summary>
    public interface IASLCategoryRepository
    {
        /// <summary>
        /// AddASLCategory adds a ASLCategory to the repository
        /// </summary>
        /// <param name="ASLCategory">The ASLCategory to add</param>
        /// <returns>The Id of the ASLCategory</returns>
        int AddASLCategory(ASLCategory ASLCategory);

        /// <summary>
        /// DeleteASLCategory deletes a ASLCategory from the repository
        /// </summary>
        /// <param name="ASLCategory">The ASLCategory to delete</param>
        void DeleteASLCategory(ASLCategory ASLCategory);

        /// <summary>
        /// This GetASLCategory  method retrieves a specific ASLCategory in a portal
        /// </summary>
        /// <remarks>ASLCategorys are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="ASLCategoryId">The Id of the ASLCategory</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A single of ASLCategory</returns>
        ASLCategory GetASLCategory(int ASLCategoryId, int portalId);

        /// <summary>
        /// This GetASLCategorys overload retrieves all the ASLCategorys for a portal
        /// </summary>
        /// <remarks>ASLCategorys are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A collection of ASLCategorys</returns>
        IQueryable<ASLCategory> GetASLCategory(int portalId);

        /// <summary>
        /// This GetASLCategorys overload retrieves a page of ASLCategorys for a portal
        /// </summary>
        /// <remarks>ASLCategorys are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLCategorys</returns>
        IPagedList<ASLCategory> GetASLCategory(string searchTerm, int portalId, int pageIndex, int pageSize);

        /// <summary>
        /// UpdateASLCategory updates a ASLCategory in the repository
        /// </summary>
        /// <param name="ASLCategory">The ASLCategory to update</param>
        void UpdateASLCategory(ASLCategory ASLCategory);
    }
}


