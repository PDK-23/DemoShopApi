﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShopApi.Application.DTOs
{
    public class CreateProductDto
    {
        public string name {  get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }
}
