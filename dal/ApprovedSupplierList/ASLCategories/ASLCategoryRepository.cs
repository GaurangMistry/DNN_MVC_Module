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
    /// ASLCategoryRepository provides a concrete implemetation of the IASLCategoryRepository interface for interacting with the ASLCategory Repository(Database)
    /// </summary>
    public class ASLCategoryRepository : ServiceLocator<IASLCategoryRepository, ASLCategoryRepository>, IASLCategoryRepository
    {
        protected override Func<IASLCategoryRepository> GetFactory()
        {
            return () => new ASLCategoryRepository();
        }

        /// <summary>
        /// AddASLCategory adds a ASLCategory to the repository
        /// </summary>
        /// <param name="ASLCategory">The ASLCategory to add</param>
        /// <returns>The Id of the ASLCategory</returns>
        public int AddASLCategory(ASLCategory ASLCategory)
        {
            Requires.NotNull(ASLCategory);
            Requires.PropertyNotNegative(ASLCategory, "PortalId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLCategory>();

                rep.Insert(ASLCategory);
            }

            return ASLCategory.ASLCategoryId;
        }

        /// <summary>
        /// DeleteASLCategory deletes a ASLCategory from the repository
        /// </summary>
        /// <param name="ASLCategory">The ASLCategory to delete</param>
        public void DeleteASLCategory(ASLCategory ASLCategory)
        {
            Requires.NotNull(ASLCategory);
            Requires.PropertyNotNegative(ASLCategory, "ASLCategoryId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLCategory>();

                rep.Delete(ASLCategory);
            }
        }

        public ASLCategory GetASLCategory(int ASLCategoryId, int portalId)
        {
            Requires.NotNegative("ASLCategoryId", ASLCategoryId);
            Requires.NotNegative("portalId", portalId);

            return GetASLCategory(portalId).SingleOrDefault(c => c.ASLCategoryId == ASLCategoryId);
        }

        /// <summary>
        /// This GetASLCategorys overload retrieves all the ASLCategorys for a portal
        /// </summary>
        /// <remarks>ASLCategorys are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="portalId">The Id of the portal</param>
        /// <returns>A collection of ASLCategorys</returns>
        public IQueryable<ASLCategory> GetASLCategory(int portalId)
        {
            Requires.NotNegative("portalId", portalId);

            IQueryable<ASLCategory> ASLCategorys = null;

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLCategory>();

                ASLCategorys = rep.Get(portalId).AsQueryable();
            }

            return ASLCategorys;
        }

        /// <summary>
        /// This GetASLCategorys overload retrieves a page of ASLCategorys for a portal
        /// </summary>
        /// <remarks>ASLCategorys are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="portalId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLCategorys</returns>

        public IPagedList<ASLCategory> GetASLCategory(string searchTerm, int portalId, int pageIndex, int pageSize)
        {
            Requires.NotNegative("portalId", portalId);

            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = "";
            }
            var ASLCategorys = GetASLCategory(portalId);


            return new PagedList<ASLCategory>(ASLCategorys, pageIndex, pageSize);
        }

        /// <summary>
        /// UpdateASLCategory updates a ASLCategory in the repository
        /// </summary>
        /// <param name="ASLCategory">The ASLCategory to update</param>
        public void UpdateASLCategory(ASLCategory ASLCategory)
        {
            Requires.NotNull(ASLCategory);
            Requires.PropertyNotNegative(ASLCategory, "ASLCategoryId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLCategory>();

                rep.Update(ASLCategory);
            }
        }
    }
}
