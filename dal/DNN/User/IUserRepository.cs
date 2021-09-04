// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Linq;
using DotNetNuke.Collections;
using WebXMS.DAL.DNN.Models;
using System.Collections.Generic;
namespace WebXMS.DAL.DNN
{
    /// <summary>
    /// IUserRepository provides an interface for interacting with the User Repository(Database)
    /// </summary>
    public interface IUserRepository
    {

        /// <summary>
        /// This GetUsers overload retrieves all the Users for a portal
        /// </summary>
        /// <returns>A collection of Users</returns>
        List<User> GetUsers(int portalId);
        
    }
}


