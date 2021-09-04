// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Caching;
using DotNetNuke.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
namespace WebXMS.DAL.ASLApp.Models
{
    [Serializable]
    [TableName("MMS_ASLSecondaryLocations")]
    [PrimaryKey("ASLSecondaryLocationId")]
    [Cacheable("MMS_ASLSecondaryLocations", CacheItemPriority.Normal, 20)]
    [Scope("ParentASLId")]
    public class ASLSecondaryLocation
    {
        public ASLSecondaryLocation()
        {
            ASLSecondaryLocationId = -1;
        }

        public int ASLSecondaryLocationId { get; set; }
        public int PortalId { get; set; }
        public int ParentASLId { get; set; }
        public string SecondaryLocationAddress { get; set; }
        public string SecondaryLocationCity { get; set; }
        public string SecondaryLocationState { get; set; }
        public string SecondaryLocationZip { get; set; }
        public string SecondaryLocationCountry { get; set; }
        public string SecondaryLocationCountryName { get; set; }
        public string SecondaryLocationStateName { get; set; }

    }
}
