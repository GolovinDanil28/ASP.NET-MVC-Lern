﻿using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
