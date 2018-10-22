using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Moxy.EntityFramework.Interface
{
    public abstract class BaseEntity : ICreatable, IUpdatable, ISoftDeletable
    {
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("created_by"), MaxLength(100)]
        public string CreatedBy { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [Column("updated_by")]
        public string UpdatedBy { get; set; }
        [Column("is_deleted")]
        public bool? IsDeleted { get; set; }
        [Column("deleted_by")]
        public string DeletedBy { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
