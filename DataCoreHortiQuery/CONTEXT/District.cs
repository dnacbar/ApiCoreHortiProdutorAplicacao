﻿using System;
using System.Collections.Generic;

namespace DataCoreHortiQuery.CONTEXT
{
    public partial class District
    {
        public District()
        {
            Producer = new HashSet<Producer>();
        }

        public Guid IdDistrict { get; set; }
        public string DsName { get; set; }
        public bool? BoActive { get; set; }
        public DateTime DtCreation { get; set; }
        public DateTime DtAtualization { get; set; }

        public virtual ICollection<Producer> Producer { get; set; }
    }
}