﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public virtual int id { get; set; }
        public string? createdBy { get; set; }
        public DateTime? created { get; set; }
        public string? modifiedBy { get; set; }

        public DateTime? modifiedAt { get; set; }
    }
}
