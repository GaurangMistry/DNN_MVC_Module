// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Caching;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace WebXMS.DAL.DNN.Models
{
    [Serializable]
    [TableName("Lists")]
    [PrimaryKey("EntryID")]
    [Cacheable("Lists", CacheItemPriority.Normal, 20)]
    [Scope("PortalId")]
    public class RegionList
    {
        public RegionList()
        {
            EntryID = -1;
        }

        public int EntryID { get; set; }
        public int PortalId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ListName { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public int ParentId { get; set; }
    }
}
