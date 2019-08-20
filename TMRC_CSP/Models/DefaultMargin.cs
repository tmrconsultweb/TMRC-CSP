using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    [Table("tbl_DefaultMargin")]
    public class DefaultMargin
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Default Percentage is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Percentage must be numeric")]
        public int DefaultPercentage { get; set; }
        public int Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ResellerId { get; set; }
    }
}