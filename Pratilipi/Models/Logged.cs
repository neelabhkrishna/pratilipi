namespace Pratilipi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Logged")]
    public partial class Logged
    {
        public int id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        public int? Storyid { get; set; }

        [Column(TypeName = "date")]
        public DateTime? viewdate { get; set; }

        public virtual Story Story { get; set; }

        public virtual User User { get; set; }
    }
}
