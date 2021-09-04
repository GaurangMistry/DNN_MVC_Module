// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.ASLApp.Models;
using WebXMS.DAL.WebXMS.Models;
namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// IASLRepository provides an interface for interacting with the ASL Repository(Database)
    /// </summary>
    public interface IASLRepository
    {
        /// <summary>
        /// AddASL adds a ASL to the repository
        /// </summary>
        /// <param name="ASL">The ASL to add</param>
        /// <returns>The Id of the ASL</returns>
        int AddASL(ASL ASL);

        /// <summary>
        /// DeleteASL deletes a ASL from the repository
        /// </summary>
        /// <param name="ASL">The ASL to delete</param>
        void DeleteASL(ASL ASL);

        /// <summary>
        /// This GetASL  method retrieves a specific ASL in a portal
        /// </summary>
        /// <remarks>ASLs are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="ASLId">The Id of the ASL</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A single of ASL</returns>
        ASL GetASL(int ASLId);


        /// <summary>
        /// This GetASLs overload retrieves a page of ASLs for a portal
        /// </summary>
        /// <remarks>ASLs are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLs</returns>
        XMSPagedList<ASL> GetASL(string searchTerm, int portalId, int pageIndex, int pageSize);

        //Search Method accepts an asl object as filter
        XMSPagedList<ASL> GetASL(ASL asl);

        //Get Chart for Supplier total by categories
        XMSChartModel GetChartSupplierByCategory(int portalId, string catName = "");
        
        //Get Chart for Supplier total by Status
        XMSChartModel GetChartSupplierByStatus(int portalId, string catName = "");
        XMSChartModel GetChartByReEvaluationDate(int portalId, string catName = "");
        /// <summary>
        /// UpdateASL updates a ASL in the repository
        /// </summary>
        /// <param name="ASL">The ASL to update</param>
        void UpdateASL(ASL ASL);
    }
}


