// Copyright (c) DNN Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Caching;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace WebXMS.DAL.ASLApp.Models
{
    [Serializable]
    [TableName("MMS_ASLReEvaluations")]
    [PrimaryKey("ASLReEvaluationId")]
    [Cacheable("ASLReEvaluations", CacheItemPriority.Normal, 20)]
    [Scope("ParentASLId")]
    public class ASLReEvaluation
    {
        public ASLReEvaluation()
        {
            ASLReEvaluationId = -1;
        }
        public ASLReEvaluation(string name)
        {
            ASLReEvaluationId = -1;
        }

        [Required(AllowEmptyStrings = false)]
        public int ASLReEvaluationId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int PortalId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int ParentASLId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public DateTime? EvaluationDate { get; set; }
        [Required(AllowEmptyStrings = false)]
        public DateTime? NextReEvaluationDate { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string EvaluationCriteria { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int? InitialEvaluationResultId { get; set; }
        public int? EvaluationBy { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string EvaluationComments { get; set; }

        [IgnoreColumn]
        public string EvaluationResultName { get; set; }
    }
}
