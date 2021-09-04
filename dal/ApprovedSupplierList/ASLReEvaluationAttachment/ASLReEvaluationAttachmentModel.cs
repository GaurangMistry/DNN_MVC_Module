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
    [TableName("MMS_ASLReEvaluationAttachments")]
    [PrimaryKey("ASLReEvaluationAttachmentId")]
    [Cacheable("MMS_ASLReEvaluationAttachments", CacheItemPriority.Normal, 20)]
    [Scope("ParentASLId")]
    public class ASLReEvaluationAttachment
    {
        public ASLReEvaluationAttachment()
        {
            ASLReEvaluationAttachmentId = -1;
        }

        public int ASLReEvaluationAttachmentId { get; set; }
        public int PortalId { get; set; }
        public int ParentASLId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FileName { get; set; }
        public byte[] FileBinary { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Extension { get; set; }
        public string MimeType { get; set; }
        
    }

   
}
