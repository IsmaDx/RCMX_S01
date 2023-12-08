using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace S1rcmxAPI.Models.ResponseModel
{
    public class PostLoggerModel
    { 
        public string UUID { get; set; }
        public DateTime FecOps { get; set; }
        public string SerFol { get; set; }
        public int NumFol { get; set; }
    }
}