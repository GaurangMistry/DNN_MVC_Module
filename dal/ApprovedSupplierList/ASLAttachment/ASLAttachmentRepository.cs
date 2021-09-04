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
    /// ASLAttachmentRepository provides a concrete implemetation of the IASLAttachmentRepository interface for interacting with the ASLAttachment Repository(Database)
    /// </summary>
    public class ASLAttachmentRepository : ServiceLocator<IASLAttachmentRepository, ASLAttachmentRepository>, IASLAttachmentRepository
    {
        protected override Func<IASLAttachmentRepository> GetFactory()
        {
            return () => new ASLAttachmentRepository();
        }

        /// <summary>
        /// AddASLAttachment adds a ASLAttachment to the repository
        /// </summary>
        /// <param name="ASLAttachment">The ASLAttachment to add</param>
        /// <returns>The Id of the ASLAttachment</returns>
        public int AddASLAttachment(ASLAttachment ASLAttachment)
        {
            Requires.NotNull(ASLAttachment);
            Requires.PropertyNotNegative(ASLAttachment, "PortalId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLAttachment>();

                rep.Insert(ASLAttachment);
            }

            return ASLAttachment.ASLAttachmentId;
        }

        /// <summary>
        /// DeleteASLAttachment deletes a ASLAttachment from the repository
        /// </summary>
        /// <param name="ASLAttachment">The ASLAttachment to delete</param>
        public void DeleteASLAttachment(ASLAttachment ASLAttachment)
        {
            Requires.NotNull(ASLAttachment);
            Requires.PropertyNotNegative(ASLAttachment, "ASLAttachmentId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLAttachment>();

                rep.Delete(ASLAttachment);
            }
        }

        public ASLAttachment GetASLAttachment(int ASLAttachmentId)
        {
            Requires.NotNegative("ASLAttachmentId", ASLAttachmentId);
            ASLAttachment loc;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLAttachment>();

                loc = rep.GetById(ASLAttachmentId);
            }
            return loc;
        }

        /// <summary>
        /// This GetAttachmentsByParentASL overload retrieves all the ASLAttachments by ParentASL
        /// </summary>
        /// <remarks>ASLAttachments are cached by ParentASL, so this call will check the cache before going to the Database</remarks>
        /// <param name="ParentASL">ParentASL ID</param>
        /// <returns>A collection of ASLAttachments</returns>
        public IQueryable<ASLAttachment> GetAttachmentsByParentASL(int ParentASLId)
        {
            Requires.NotNegative("ParentASLId", ParentASLId);

            IQueryable<ASLAttachment> ASLAttachments = null;

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLAttachment>();

                ASLAttachments = rep.Get(ParentASLId).AsQueryable();
            }

            return ASLAttachments;
        }

        /// <summary>
        /// This GetASLAttachments overload retrieves a page of ASLAttachments for a ASL
        /// </summary>
        /// <remarks>ASLAttachments are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="ASLId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLAttachments</returns>

        public IPagedList<ASLAttachment> GetASLAttachment(string searchTerm, int ParentASLId, int pageIndex, int pageSize)
        {
            Requires.NotNegative("ASLId", ParentASLId);

            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = "";
            }
            var ASLAttachments = GetAttachmentsByParentASL(ParentASLId);


            return new PagedList<ASLAttachment>(ASLAttachments, pageIndex, pageSize);
        }

        /// <summary>
        /// UpdateASLAttachment updates a ASLAttachment in the repository
        /// </summary>
        /// <param name="ASLAttachment">The ASLAttachment to update</param>
        public void UpdateASLAttachment(ASLAttachment ASLAttachment)
        {
            Requires.NotNull(ASLAttachment);
            Requires.PropertyNotNegative(ASLAttachment, "ASLAttachmentId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLAttachment>();

                rep.Update(ASLAttachment);
            }
        }
    }
}
