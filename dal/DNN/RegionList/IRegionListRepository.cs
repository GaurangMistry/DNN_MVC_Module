// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.DNN.Models;
namespace WebXMS.DAL.DNN
{
    /// <summary>
    /// IRegionListRepository provides an interface for interacting with the RegionList Repository(Database)
    /// </summary>
    public interface IRegionListRepository
    {

        /// <summary>
        /// This GetRegionLists overload retrieves all the RegionLists for a portal
        /// </summary>
        /// <returns>A collection of RegionLists</returns>
        IQueryable<RegionList> GetRegionLists(string countrycode);
        
    }
}


