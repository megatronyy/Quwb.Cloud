using Lib.Framework.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Website.Models
{
    public class TutorClassType : IBaseModel<int>
    {
        [Key]
        [Column("TutorClassTypeId")]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string TutorClassTypeName { get; set; }
        public bool Active { get; set; } = true;
        [StringLength(maximumLength: 200)]
        public string Remark { get; set; }
        public int TutorClassCount { get; set; }
    }
}
