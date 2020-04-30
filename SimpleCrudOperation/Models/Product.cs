using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrudOperation.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Column(TypeName="decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int TotalQuantity { get; set; }



    }
}
