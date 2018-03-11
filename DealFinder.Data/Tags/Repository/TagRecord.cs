using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DealFinder.Data.Tags.Repository
{
    public class TagRecord
    {
        public TagRecord()
        {
            DealTags = new List<DealTagRecord>();
        }

        [Key]
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public virtual List<DealTagRecord> DealTags { get; set; }
    }
}
    