// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.ASLApp.Models;
namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// IASLSecondaryLocationRepository provides an interface for interacting with the ASLSecondaryLocation Repository(Database)
    /// </summary>
    public interface IASLSecondaryLocationRepository
    {
        /// <summary>
        /// AddASLSecondaryLocation adds a ASLSecondaryLocation to the repository
        /// </summary>
        /// <param name="ASLSecondaryLocation">The ASLSecondaryLocation to add</param>
        /// <returns>The Id of the ASLSecondaryLocation</returns>
        int AddASLSecondaryLocation(ASLSecondaryLocation ASLSecondaryLocation);

        /// <summary>
        /// DeleteASLSecondaryLocation deletes a ASLSecondaryLocation from the repository
        /// </summary>
        /// <param name="ASLSecondaryLocation">The ASLSecondaryLocation to delete</param>
        void DeleteASLSecondaryLocation(ASLSecondaryLocation ASLSecondaryLocation);

        /// <summary>
        /// This GetASLSecondaryLocation  method retrieves a specific ASLSecondaryLocation in a portal
        /// </summary>
        /// <remarks>ASLSecondaryLocations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="ASLSecondaryLocationId">The Id of the ASLSecondaryLocation</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A single of ASLSecondaryLocation</returns>
        ASLSecondaryLocation GetASLSecondaryLocation(int ASLSecondaryLocationId);

        /// <summary>
        /// This GetLocationsByParentASL overload retrieves all the ASLSecondaryLocations by ParentASL
        /// </summary>
        /// <remarks>ASLSecondaryLocations are cached by ParentASL, so this call will check the cache before going to the Database</remarks>
        /// <param name="ParentASL">ParentASL ID</param>
        /// <returns>A collection of ASLSecondaryLocations</returns>
        IQueryable<ASLSecondaryLocation> GetLocationsByParentASL(int ParentASLId);

        /// <summary>
        /// This GetASLSecondaryLocations overload retrieves a page of ASLSecondaryLocations for a ASL
        /// </summary>
        /// <remarks>ASLSecondaryLocations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="ASLId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLSecondaryLocations</returns>
        IPagedList<ASLSecondaryLocation> GetASLSecondaryLocation(string searchTerm, int ParentASLId, int pageIndex, int pageSize);

        /// <summary>
        /// UpdateASLSecondaryLocation updates a ASLSecondaryLocation in the repository
        /// </summary>
        /// <param name="ASLSecondaryLocation">The ASLSecondaryLocation to update</param>
        void UpdateASLSecondaryLocation(ASLSecondaryLocation ASLSecondaryLocation);
    }
}


