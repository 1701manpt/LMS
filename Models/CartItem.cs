﻿using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Display(Name = "Item")]
        public int ItemId { get; set; }

        [Display(Name = "Cart")]
        public int CartId { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Cost")]
        [Range(0, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(
            @"^(500|[1-9]\d*000)$",
            ErrorMessage = "The field Cost is invalid."
        )]
        public decimal? Cost { get; set; }

        public Cart? Cart { get; set; }
        public Item? Item { get; set; }
    }
}
