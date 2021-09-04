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
    /// CompanyLocationRepository provides a concrete implemetation of the ICompanyLocationRepository interface for interacting with the CompanyLocation Repository(Database)
    /// </summary>
    public class CompanyLocationRepository : ServiceLocator<ICompanyLocationRepository, CompanyLocationRepository>, ICompanyLocationRepository
    {
        protected override Func<ICompanyLocationRepository> GetFactory()
        {
            return () => new CompanyLocationRepository();
        }

        /// <summary>
        /// AddCompanyLocation adds a CompanyLocation to the repository
        /// </summary>
        /// <param name="CompanyLocation">The CompanyLocation to add</param>
        /// <returns>The Id of the CompanyLocation</returns>
        public int AddCompanyLocation(CompanyLocation CompanyLocation)
        {
            Requires.NotNull(CompanyLocation);
            Requires.PropertyNotNegative(CompanyLocation, "PortalId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CompanyLocation>();

                rep.Insert(CompanyLocation);
            }

            return CompanyLocation.CompanyLocationId;
        }

        /// <summary>
        /// DeleteCompanyLocation deletes a CompanyLocation from the repository
        /// </summary>
        /// <param name="CompanyLocation">The CompanyLocation to delete</param>
        public void DeleteCompanyLocation(CompanyLocation CompanyLocation)
        {
            Requires.NotNull(CompanyLocation);
            Requires.PropertyNotNegative(CompanyLocation, "CompanyLocationId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CompanyLocation>();

                rep.Delete(CompanyLocation);
            }
        }

        public CompanyLocation GetCompanyLocation(int CompanyLocationId, int portalId)
        {
            Requires.NotNegative("CompanyLocationId", CompanyLocationId);
            Requires.NotNegative("portalId", portalId);

            return GetCompanyLocations(portalId).SingleOrDefault(c => c.CompanyLocationId == CompanyLocationId);
        }

        /// <summary>
        /// This GetCompanyLocations overload retrieves all the CompanyLocations for a portal
        /// </summary>
        /// <remarks>CompanyLocations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A collection of CompanyLocations</returns>
        public IQueryable<CompanyLocation> GetCompanyLocations(int portalId)
        {
            Requires.NotNegative("portalId", portalId);

            IQueryable<CompanyLocation> CompanyLocations = null;

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CompanyLocation>();

                CompanyLocations = rep.Get(portalId).AsQueryable();
            }

            return CompanyLocations;
        }

        /// <summary>
        /// This GetCompanyLocations overload retrieves a page of CompanyLocations for a portal
        /// </summary>
        /// <remarks>CompanyLocations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of CompanyLocations</returns>

        public IPagedList<CompanyLocation> GetCompanyLocations(string searchTerm, int portalId, int pageIndex, int pageSize)
        {
            Requires.NotNegative("portalId", portalId);

            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = "";
            }
            var CompanyLocations = GetCompanyLocations(portalId);


            return new PagedList<CompanyLocation>(CompanyLocations, pageIndex, pageSize);
        }

        /// <summary>
        /// UpdateCompanyLocation updates a CompanyLocation in the repository
        /// </summary>
        /// <param name="CompanyLocation">The CompanyLocation to update</param>
        public void UpdateCompanyLocation(CompanyLocation CompanyLocation)
        {
            Requires.NotNull(CompanyLocation);
            Requires.PropertyNotNegative(CompanyLocation, "CompanyLocationId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CompanyLocation>();

                rep.Update(CompanyLocation);
            }
        }
    }
}
