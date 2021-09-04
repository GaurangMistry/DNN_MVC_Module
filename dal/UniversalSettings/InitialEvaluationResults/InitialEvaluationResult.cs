﻿// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Caching;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace WebXMS.DAL.UniversalSettings.Models
{
    [Serializable]
    [TableName("MMS_InitialEvaluationResults")]
    [PrimaryKey("InitialEvaluationResultId")]
    [Cacheable("InitialEvaluationResults", CacheItemPriority.Normal, 20)]
    [Scope("PortalId")]
    public class InitialEvaluationResult
    {
        public InitialEvaluationResult()
        {
            InitialEvaluationResultId = -1;
        }

        public int InitialEvaluationResultId { get; set; }
        public int PortalId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string InitialEvaluationResultName { get; set; }
    }
}
