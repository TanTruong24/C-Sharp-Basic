﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Decimal Price { get; set; }

        public int RemainQuantity { get; set; }
    }
}
