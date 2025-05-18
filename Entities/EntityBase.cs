using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base_API.Entities
{
    public abstract class EntityBase
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Id { get; set; } = 0;

        [Required]
        [Column("inserted_at")]
        public DateTime InsertedAt { get; private set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        public void SetInsertedNow()
        {
            this.InsertedAt = DateTime.Now;
        }

        public void SetUpdatedNow()
        {
            this.UpdatedAt = DateTime.Now;
        }
    }
}
