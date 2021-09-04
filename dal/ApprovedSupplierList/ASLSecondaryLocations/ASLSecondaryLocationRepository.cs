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
namespace WebXMS.DAL.ASLApp
{
    /// <summary>
    /// ASLSecondaryLocationRepository provides a concrete implemetation of the IASLSecondaryLocationRepository interface for interacting with the ASLSecondaryLocation Repository(Database)
    /// </summary>
    public class ASLSecondaryLocationRepository : ServiceLocator<IASLSecondaryLocationRepository, ASLSecondaryLocationRepository>, IASLSecondaryLocationRepository
    {
        protected override Func<IASLSecondaryLocationRepository> GetFactory()
        {
            return () => new ASLSecondaryLocationRepository();
        }

        /// <summary>
        /// AddASLSecondaryLocation adds a ASLSecondaryLocation to the repository
        /// </summary>
        /// <param name="ASLSecondaryLocation">The ASLSecondaryLocation to add</param>
        /// <returns>The Id of the ASLSecondaryLocation</returns>
        public int AddASLSecondaryLocation(ASLSecondaryLocation ASLSecondaryLocation)
        {
            Requires.NotNull(ASLSecondaryLocation);
            Requires.PropertyNotNegative(ASLSecondaryLocation, "PortalId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLSecondaryLocation>();

                rep.Insert(ASLSecondaryLocation);
            }

            return ASLSecondaryLocation.ASLSecondaryLocationId;
        }

        /// <summary>
        /// DeleteASLSecondaryLocation deletes a ASLSecondaryLocation from the repository
        /// </summary>
        /// <param name="ASLSecondaryLocation">The ASLSecondaryLocation to delete</param>
        public void DeleteASLSecondaryLocation(ASLSecondaryLocation ASLSecondaryLocation)
        {
            Requires.NotNull(ASLSecondaryLocation);
            Requires.PropertyNotNegative(ASLSecondaryLocation, "ASLSecondaryLocationId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLSecondaryLocation>();

                rep.Delete(ASLSecondaryLocation);
            }
        }

        public ASLSecondaryLocation GetASLSecondaryLocation(int ASLSecondaryLocationId)
        {
            Requires.NotNegative("ASLSecondaryLocationId", ASLSecondaryLocationId);
            ASLSecondaryLocation loc;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLSecondaryLocation>();

                loc = rep.GetById(ASLSecondaryLocationId);
            }
            return loc;
        }

        /// <summary>
        /// This GetLocationsByParentASL overload retrieves all the ASLSecondaryLocations by ParentASL
        /// </summary>
        /// <remarks>ASLSecondaryLocations are cached by ParentASL, so this call will check the cache before going to the Database</remarks>
        /// <param name="ParentASL">ParentASL ID</param>
        /// <returns>A collection of ASLSecondaryLocations</returns>
        public IQueryable<ASLSecondaryLocation> GetLocationsByParentASL(int ParentASLId)
        {
            Requires.NotNegative("ParentASLId", ParentASLId);

            IQueryable<ASLSecondaryLocation> ASLSecondaryLocations = null;

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLSecondaryLocation>();

                ASLSecondaryLocations = rep.Get(ParentASLId).AsQueryable();
            }

            return ASLSecondaryLocations;
        }

        /// <summary>
        /// This GetASLSecondaryLocations overload retrieves a page of ASLSecondaryLocations for a ASL
        /// </summary>
        /// <remarks>ASLSecondaryLocations are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="ASLId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLSecondaryLocations</returns>

        public IPagedList<ASLSecondaryLocation> GetASLSecondaryLocation(string searchTerm, int ParentASLId, int pageIndex, int pageSize)
        {
            Requires.NotNegative("ASLId", ParentASLId);

            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = "";
            }
            var ASLSecondaryLocations = GetLocationsByParentASL(ParentASLId);


            return new PagedList<ASLSecondaryLocation>(ASLSecondaryLocations, pageIndex, pageSize);
        }

        /// <summary>
        /// UpdateASLSecondaryLocation updates a ASLSecondaryLocation in the repository
        /// </summary>
        /// <param name="ASLSecondaryLocation">The ASLSecondaryLocation to update</param>
        public void UpdateASLSecondaryLocation(ASLSecondaryLocation ASLSecondaryLocation)
        {
            Requires.NotNull(ASLSecondaryLocation);
            Requires.PropertyNotNegative(ASLSecondaryLocation, "ASLSecondaryLocationId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLSecondaryLocation>();

                rep.Update(ASLSecondaryLocation);
            }
        }
    }
}
