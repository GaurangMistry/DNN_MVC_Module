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
    [TableName("MMS_ASLAttachments")]
    [PrimaryKey("ASLAttachmentId")]
    [Cacheable("MMS_ASLAttachments", CacheItemPriority.Normal, 20)]
    [Scope("ParentASLId")]
    public class ASLAttachment
    {
        public ASLAttachment()
        {
            ASLAttachmentId = -1;
        }

        public int ASLAttachmentId { get; set; }
        public int PortalId { get; set; }
        public int ParentASLId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FileName { get; set; }
        public byte[] FileBinary { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Extension { get; set; }
        public string MimeType { get; set; }
        
    }

    public class PostFile
    {
        public string name { get; set; }
        public string type { get; set; }
        public long size { get; set; }
        public string data { get; set; }
    }
}
