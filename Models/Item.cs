﻿using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    // Model Item chung cho Book, DVD, Magazine
    public abstract class Item
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Type")]
        public ItemType Type { get; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Price")]
        [Range(0.01, double.MaxValue)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal Price { get; set; }

        [Display(Name = "Borrowed Items")]

        public List<BorrowedItem>? BorrowedItems { get; set; }
        public List<BorrowedItemTemp>? BorrowedItemTemps { get; set; }
    }
}