// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.UniversalSettings.Models;
namespace WebXMS.DAL.UniversalSettings
{
    /// <summary>
    /// ICompanyLocationRepository provides an interface for interacting with the CompanyLocation Repository(Database)
    /// </summary>
    public interface ICompanyLocationRepository
    {
        /// <summary>
        /// AddCompanyLocation adds a CompanyLocation to the repository
        /// </summary>
        /// <param name="CompanyLocation">The CompanyLocation to add</param>
        /// <returns>The Id of the CompanyLocation</returns>
        int AddCompanyLocation(CompanyLocation CompanyLocation);

        /// <summary>
        /// DeleteCompanyLocation deletes a CompanyLocation from the repository
        /// </summary>
        /// <param name="CompanyLocation">The CompanyLocation to delete</param>
        void DeleteCompanyLocation(CompanyLocation CompanyLocation);

        /// <summary>
        /// This GetCompanyLocation  method retrieves a specific CompanyLocation in a portal
        /// </summary>
        /// <remarks>CompanyLocations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="CompanyLocationId">The Id of the CompanyLocation</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A single of CompanyLocation</returns>
        CompanyLocation GetCompanyLocation(int CompanyLocationId, int portalId);

        /// <summary>
        /// This GetCompanyLocations overload retrieves all the CompanyLocations for a portal
        /// </summary>
        /// <remarks>CompanyLocations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A collection of CompanyLocations</returns>
        IQueryable<CompanyLocation> GetCompanyLocations(int portalId);

        /// <summary>
        /// This GetCompanyLocations overload retrieves a page of CompanyLocations for a portal
        /// </summary>
        /// <remarks>CompanyLocations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of CompanyLocations</returns>
        IPagedList<CompanyLocation> GetCompanyLocations(string searchTerm, int portalId, int pageIndex, int pageSize);

        /// <summary>
        /// UpdateCompanyLocation updates a CompanyLocation in the repository
        /// </summary>
        /// <param name="CompanyLocation">The CompanyLocation to update</param>
        void UpdateCompanyLocation(CompanyLocation CompanyLocation);
    }
}


