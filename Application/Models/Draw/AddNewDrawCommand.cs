﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Draw
{
    public class AddNewDrawCommand
    {
        public DateTime DrawDate { get; set; }
        public int DrawNumber { get; set; }
    }
}
