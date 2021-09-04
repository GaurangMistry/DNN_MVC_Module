// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.DNN.Models;
namespace WebXMS.DAL.DNN
{
    /// <summary>
    /// ICountryListRepository provides an interface for interacting with the CountryList Repository(Database)
    /// </summary>
    public interface ICountryListRepository
    {

        /// <summary>
        /// This GetCountryLists overload retrieves all the CountryLists for a portal
        /// </summary>
        /// <returns>A collection of CountryLists</returns>
        IQueryable<CountryList> GetCountryLists();
        
    }
}


