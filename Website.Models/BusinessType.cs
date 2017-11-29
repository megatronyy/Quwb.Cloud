using Lib.Framework.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Website.Models
{
    public class BusinessType : IBaseModel<int>
    {
        [Key]
        [Column("BusinessID")]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string BusinessDes { get; set; }
        public short SortNum { get; set; }
        public short IsShow { get; set; }
        [StringLength(maximumLength: 500)]
        public string Icon { get; set; }
        [StringLength(maximumLength: 500)]
        public string LargeIcon { get; set; }
    }
}
