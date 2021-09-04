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
using WebXMS.DAL.DNN.Models;
using WebXMS.DAL.DNN;
namespace WebXMS.DAL.DNN
{
    /// <summary>
    /// RegionListRepository provides a concrete implemetation of the IRegionListRepository interface for interacting with the RegionList Repository(Database)
    /// </summary>
    public class RegionListRepository : ServiceLocator<IRegionListRepository, RegionListRepository>, IRegionListRepository
    {
        protected override Func<IRegionListRepository> GetFactory()
        {
            return () => new RegionListRepository();
        }

        /// <summary>
        /// This GetRegionLists overload retrieves all the RegionLists for a portal
        /// </summary>
        /// <remarks>RegionLists are cached by portal, so this call will check the cache before going to the Database</remarks>

        /// <returns>A collection of RegionLists</returns>
        public IQueryable<RegionList> GetRegionLists(string CountryCode)
        {
         
            IQueryable<RegionList> RegionLists = null;
            using (var context = DataContext.Instance())
            {
                
                var rep = context.GetRepository<RegionList>();
                var country = rep.Get().Where(c => c.Value == CountryCode).FirstOrDefault();

                RegionLists = rep.Get().Where(c=>c.ListName=="Region" && c.ParentId == country.EntryID).AsQueryable();
            }
            return RegionLists;
        }
        
    }
}
