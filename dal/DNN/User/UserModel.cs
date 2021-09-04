// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Caching;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace WebXMS.DAL.DNN.Models
{
    [Serializable]
    [TableName("Users")]
    [PrimaryKey("UserID")]
    [Cacheable("Users", CacheItemPriority.Normal, 20)]
    [Scope("PortalId")]
    public class User
    {
        public User()
        {
            UserID = -1;
        }

        public int UserID { get; set; }
        public int PortalId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
