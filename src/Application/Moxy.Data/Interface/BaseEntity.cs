using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Moxy.Data
{
    public abstract class BaseEntityCreatable<T> : ICreatable
    {

        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }
        [Column("created_at")]
        public virtual DateTime CreatedAt { get; set; }
        [Column("created_by"), MaxLength(100)]
        public virtual string CreatedBy { get; set; }

    }

    public abstract class BaseEntity<T> : ICreatable, IUpdatable, ISoftDeletable
    {
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("created_by"), MaxLength(100)]
        public virtual string CreatedBy { get; set; }
        [Column("updated_at")]
        public virtual DateTime? UpdatedAt { get; set; }
        [Column("updated_by"), MaxLength(100)]
        public virtual string UpdatedBy { get; set; }
        [Column("is_deleted")]
        public virtual bool? IsDeleted { get; set; }
        [Column("deleted_by"), MaxLength(100)]
        public virtual string DeletedBy { get; set; }
        [Column("deleted_at")]
        public virtual DateTime? DeletedAt { get; set; }
    }
}
