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
    [TableName("MMS_ASLs")]
    [PrimaryKey("ASLId")]
    [Cacheable("MMS_ASLs", CacheItemPriority.Normal, 20)]
    [Scope("PortalId")]
    public class ASL
    {
        public ASL()
        {
            ASLId = -1;
        }

        public int ASLId { get; set; }
        public int PortalId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string SupplierName { get; set; }
        public int? ASLCategoryId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ScopeOfService { get; set; }
        public string SuppliedCompanyLocations { get; set; }
        [Required]
        public bool? IsActive { get; set; } = null;
        [Required(AllowEmptyStrings = false)]
        public string MainLocationAddress { get; set; }
        public string CriticalProductService { get; set; }

        public string NonCriticalProductService { get; set; }
        public string MainLocationCity { get; set; }
        public string MainLocationState { get; set; }
        public string MainLocationZip { get; set; }
        public string MainLocationCountry { get; set; }
        public string MainLocationCountryName { get; set; }
        public string MainLocationStateName { get; set; }
        public string SecondaryLocationAddress { get; set; }
        public string SecondaryLocationCity { get; set; }
        public string SecondaryLocationState { get; set; }
        public string SecondaryLocationZip { get; set; }
        public string SecondaryLocationCountry { get; set; }
        public string SecondaryLocationCountryName { get; set; }
        public string SecondaryLocationStateName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ContactName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ContactEmail { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ContactPhone { get; set; }
        [Required(AllowEmptyStrings = false)]
        public DateTime? InitialEvaluationDate { get; set; } = null;
        [Required(AllowEmptyStrings = false)]
        public bool? InitialEvaluationIsGrandfathered { get; set; } = null;
        [Required(AllowEmptyStrings = false)]
        public string InitialEvaluationCriteria { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int? InitialEvaluationResultId { get; set; }
        public int? InitialEvaluationBy { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string InitialEvaluationComments { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int? ReevaluationInterval { get; set; }

        [IgnoreColumn]
        public string CategoryName{ get; set; }
        [IgnoreColumn]
        public string InitialEvaluationResultName { get; set; }
        [IgnoreColumn]
        public DateTime NextReEvaluationDate { get; set; }

        [IgnoreColumn]
        public string[] upload { get; set; }
        [IgnoreColumn]
        public int[] VMSuppliedCompanyLocations { get; set; }
        [IgnoreColumn]
        public List<string> ErrorFields { get; set; } = new List<string>();
        [IgnoreColumn]
        public string  SecondaryLocations { get; set; }
        [IgnoreColumn]
        public List<ASLReEvaluation> ReEvaluations { get; set; }
        [IgnoreColumn]
        public string Status { get; set; }
        [IgnoreColumn]
        public DateTime? LastReEvaluationDate { get; set; }
        [IgnoreColumn]
        public int? LastReEvaluationBy { get; set; }
        [IgnoreColumn]
        public DateTime? FilterInitialEvalFrom { get; set; }
        [IgnoreColumn]
        public DateTime? FilterInitialEvalTo { get; set; }
        [IgnoreColumn]
        public DateTime? FilterReEvaluationFrom { get; set; }
        [IgnoreColumn]
        public DateTime? FilterReEvaluationTo { get; set; }
        [IgnoreColumn]
        public int? FilterReEvaluationBy { get; set; }
        [IgnoreColumn]
        public string FilterSortBy { get; set; }
        [IgnoreColumn]
        public string FilterSortDirection { get; set; }
        [IgnoreColumn]
        public int? FilterPageSize { get; set; }
        [IgnoreColumn]
        public int? FilterPageIndex { get; set; }

    }
}
