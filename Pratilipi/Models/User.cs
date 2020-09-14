namespace Pratilipi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Loggeds = new HashSet<Logged>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [StringLength(50)]
        [Required]
        public string username { get; set; }

        [StringLength(50)]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [StringLength(50)]
        public string lastlogin { get; set; }
        [EmailAddress]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Logged> Loggeds { get; set; }
    }
}
