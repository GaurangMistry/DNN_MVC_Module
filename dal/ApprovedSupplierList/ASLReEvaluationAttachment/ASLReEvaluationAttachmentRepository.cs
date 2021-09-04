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
    /// ASLReEvaluationAttachmentRepository provides a concrete implemetation of the IASLReEvaluationAttachmentRepository interface for interacting with the ASLReEvaluationAttachment Repository(Database)
    /// </summary>
    public class ASLReEvaluationAttachmentRepository : ServiceLocator<IASLReEvaluationAttachmentRepository, ASLReEvaluationAttachmentRepository>, IASLReEvaluationAttachmentRepository
    {
        protected override Func<IASLReEvaluationAttachmentRepository> GetFactory()
        {
            return () => new ASLReEvaluationAttachmentRepository();
        }

        /// <summary>
        /// AddASLReEvaluationAttachment adds a ASLReEvaluationAttachment to the repository
        /// </summary>
        /// <param name="ASLReEvaluationAttachment">The ASLReEvaluationAttachment to add</param>
        /// <returns>The Id of the ASLReEvaluationAttachment</returns>
        public int AddASLReEvaluationAttachment(ASLReEvaluationAttachment ASLReEvaluationAttachment)
        {
            Requires.NotNull(ASLReEvaluationAttachment);
            Requires.PropertyNotNegative(ASLReEvaluationAttachment, "PortalId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluationAttachment>();

                rep.Insert(ASLReEvaluationAttachment);
            }

            return ASLReEvaluationAttachment.ASLReEvaluationAttachmentId;
        }

        /// <summary>
        /// DeleteASLReEvaluationAttachment deletes a ASLReEvaluationAttachment from the repository
        /// </summary>
        /// <param name="ASLReEvaluationAttachment">The ASLReEvaluationAttachment to delete</param>
        public void DeleteASLReEvaluationAttachment(ASLReEvaluationAttachment ASLReEvaluationAttachment)
        {
            Requires.NotNull(ASLReEvaluationAttachment);
            Requires.PropertyNotNegative(ASLReEvaluationAttachment, "ASLReEvaluationAttachmentId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluationAttachment>();

                rep.Delete(ASLReEvaluationAttachment);
            }
        }

        public ASLReEvaluationAttachment GetASLReEvaluationAttachment(int ASLReEvaluationAttachmentId)
        {
            Requires.NotNegative("ASLReEvaluationAttachmentId", ASLReEvaluationAttachmentId);
            ASLReEvaluationAttachment loc;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluationAttachment>();

                loc = rep.GetById(ASLReEvaluationAttachmentId);
            }
            return loc;
        }

        /// <summary>
        /// This GetAttachmentsByParentASL overload retrieves all the ASLReEvaluationAttachments by ParentASL
        /// </summary>
        /// <remarks>ASLReEvaluationAttachments are cached by ParentASL, so this call will check the cache before going to the Database</remarks>
        /// <param name="ParentASL">ParentASL ID</param>
        /// <returns>A collection of ASLReEvaluationAttachments</returns>
        public IQueryable<ASLReEvaluationAttachment> GetAttachmentsByParentASLReEvaluation(int ParentASLId)
        {
            Requires.NotNegative("ParentASLId", ParentASLId);

            IQueryable<ASLReEvaluationAttachment> ASLReEvaluationAttachments = null;

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluationAttachment>();

                ASLReEvaluationAttachments = rep.Get(ParentASLId).AsQueryable();
            }

            return ASLReEvaluationAttachments;
        }

        /// <summary>
        /// This GetASLReEvaluationAttachments overload retrieves a page of ASLReEvaluationAttachments for a ASL
        /// </summary>
        /// <remarks>ASLReEvaluationAttachments are cached by portal, so this call will check the cache before going to the Database</remarks>
        /// <param name="searchTerm">The term to search for</param>
        /// <param name="ASLId">The Id of the portal</param>
        /// <param name="pageIndex">The page Index to fetch - this is 0 based so the first page is when pageIndex = 0</param>
        /// <param name="pageSize">The size of the page to fetch from the database</param>
        /// <returns>A paged collection of ASLReEvaluationAttachments</returns>

        public IPagedList<ASLReEvaluationAttachment> GetASLReEvaluationAttachment(string searchTerm, int ParentASLReEvaluationId, int pageIndex, int pageSize)
        {
            Requires.NotNegative("ParentASLReEvaluationId", ParentASLReEvaluationId);

            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = "";
            }
            var ASLReEvaluationAttachments = GetAttachmentsByParentASLReEvaluation(ParentASLReEvaluationId);


            return new PagedList<ASLReEvaluationAttachment>(ASLReEvaluationAttachments, pageIndex, pageSize);
        }

        /// <summary>
        /// UpdateASLReEvaluationAttachment updates a ASLReEvaluationAttachment in the repository
        /// </summary>
        /// <param name="ASLReEvaluationAttachment">The ASLReEvaluationAttachment to update</param>
        public void UpdateASLReEvaluationAttachment(ASLReEvaluationAttachment ASLReEvaluationAttachment)
        {
            Requires.NotNull(ASLReEvaluationAttachment);
            Requires.PropertyNotNegative(ASLReEvaluationAttachment, "ASLReEvaluationAttachmentId");

            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<ASLReEvaluationAttachment>();

                rep.Update(ASLReEvaluationAttachment);
            }
        }
    }
}
