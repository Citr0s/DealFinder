using System;
using System.ComponentModel.DataAnnotations;

namespace DealFinder.Data.Tags.Repository
{
    public class TagRecord
    {
        [Key]
        public Guid Identifier { get; set; }
        public string Name { get; set; }
    }
}