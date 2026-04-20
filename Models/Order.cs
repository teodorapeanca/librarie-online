using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarieOnline.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        // RELAȚIE cu OrderItem
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}