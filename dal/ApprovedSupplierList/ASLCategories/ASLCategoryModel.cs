// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Caching;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace WebXMS.DAL.ASLApp.Models
{
    [Serializable]
    [TableName("MMS_ASLCategories")]
    [PrimaryKey("ASLCategoryId")]
    [Cacheable("ASLCategories", CacheItemPriority.Normal, 20)]
    [Scope("PortalId")]
    public class ASLCategory
    {
        public ASLCategory()
        {
            ASLCategoryId = -1;
        }
        public ASLCategory(string name)
        {
            ASLCategoryId = -1;
            CategoryName = name;
        }

        public int ASLCategoryId { get; set; }
        public int PortalId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string CategoryName { get; set; }
    }
}
