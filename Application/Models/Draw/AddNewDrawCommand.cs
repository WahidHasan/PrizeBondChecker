﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Draw
{
    public class AddNewDrawCommand
    {
        public DateTime DrawDate { get; set; }
        public int DrawNumber { get; set; }
        public IFormFile File { get; set; }
    }
}
