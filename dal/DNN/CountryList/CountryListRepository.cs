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
    /// CountryListRepository provides a concrete implemetation of the ICountryListRepository interface for interacting with the CountryList Repository(Database)
    /// </summary>
    public class CountryListRepository : ServiceLocator<ICountryListRepository, CountryListRepository>, ICountryListRepository
    {
        protected override Func<ICountryListRepository> GetFactory()
        {
            return () => new CountryListRepository();
        }

        /// <summary>
        /// This GetCountryLists overload retrieves all the CountryLists for a portal
        /// </summary>
        /// <remarks>CountryLists are cached by portal, so this call will check the cache before going to the Database</remarks>

        /// <returns>A collection of CountryLists</returns>
        public IQueryable<CountryList> GetCountryLists()
        {
            IQueryable<CountryList> CountryLists = null;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CountryList>();

                CountryLists = rep.Get().Where(c=>c.ListName=="Country").AsQueryable();
            }
            return CountryLists;
        }
        
    }
}
