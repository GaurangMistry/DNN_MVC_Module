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
using System.Data.SqlClient;
namespace WebXMS.DAL.DNN
{
    /// <summary>
    /// UserRepository provides a concrete implemetation of the IUserRepository interface for interacting with the User Repository(Database)
    /// </summary>
    public class UserRepository : ServiceLocator<IUserRepository, UserRepository>, IUserRepository
    {
        protected override Func<IUserRepository> GetFactory()
        {
            return () => new UserRepository();
        }

        /// <summary>
        /// This GetUsers overload retrieves all the Users for a portal
        /// </summary>
        /// <remarks>Users are cached by portal, so this call will check the cache before going to the Database</remarks>

        /// <returns>A collection of Users</returns>
        public List<User> GetUsers(int portalId)
        {
            List<User> Users = null;
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<User>();
     
                Users = (List<User>)context.ExecuteQuery<User>(System.Data.CommandType.Text , "Select * from Users inner join UserPortals on UserPortals.UserId = Users.UserID where UserPortals.PortalID=" + portalId,null);
            
            }
            return Users;
        }
        
    }
}
