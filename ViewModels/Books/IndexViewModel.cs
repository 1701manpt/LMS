﻿using LMS.Models;
using LMS.Views.Shared.PartialViews;

namespace LMS.ViewModels.Books
{
    public class IndexViewModel
    {
        public List<Book>? Books { get; set; }
        public PaginationPartialViewModel? PaginationPartialViewModel { get; set; }
    }
}
