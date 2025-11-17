using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NIBAUTH.Domain.Entities.Base
{
    public class DefaultTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        public Guid? CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        public Guid? UpdatedById { get; set; }
        public User? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
