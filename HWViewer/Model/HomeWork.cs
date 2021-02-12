using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWViewer.Model
{
    public class HomeWork
    {
        public string FIO { get; set; }
        public string Task { get; set; }
        public string Decision { get; set; }
        public string Comment { get; set; }

        public HomeWork()
        {
        }

        public HomeWork(string fIO, string task, string decision, string comment)
        {
            FIO = fIO;
            Task = task;
            Decision = decision;
            Comment = comment;
        }

        public override string ToString()
        {
            return $"FIO: {FIO}";
        }
    }
}
