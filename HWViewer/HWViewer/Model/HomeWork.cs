using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWViewer.Model
{
    class HomeWork
    {
        public string FIO { get; set; }
        public string Task { get; set; }
        public string Code { get; set; }
        public string Comment { get; set; }


        public override string ToString()
        {
            return $"FIO: {FIO}";
        }
    }
}
