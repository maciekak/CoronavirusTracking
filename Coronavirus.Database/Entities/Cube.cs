using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coronavirus.Database.Entities
{
    public class Cube
    {
        [Key]
        [Column(Order = 1)]
        public int LatId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int LongId { get; set; }
        [Key]
        [Column(Order = 3)]
        public int TimeId { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
