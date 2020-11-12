using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SaveFIO.Model
{
    class FileWorker
    {
        public Homework Check(string pathOrFilename)
        {
            var hw = new Homework();

            try
            {
                using (StreamReader sr = new StreamReader(pathOrFilename, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        hw.FIO = line;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return hw;
        }

        public Homework RegisterFIO(string pathOrFilename, string fio)
        {
            var hw = new Homework();
            hw.FIO = fio;
            try
            {
                using (StreamWriter sw = new StreamWriter(pathOrFilename, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(fio);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return hw;
        }




    }
}
