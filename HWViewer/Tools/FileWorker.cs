using HWViewer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWViewer.Tools
{
    static class FileWorker
    {
        const string delimiter = "===";

        const string Header_FIO = delimiter + "FIO" + delimiter + "\r\n";
        const string Header_Task = "\r\n" + delimiter + "Task" + delimiter + "\r\n";
        const string Header_Decision = "\r\n" + delimiter + "Decision" + delimiter + "\r\n";
        const string Header_Comment = "\r\n" + delimiter + "Comment" + delimiter + "\r\n";

        public static HomeWork Open(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                return ConvertToHomeWork(streamReader.ReadToEnd());
            }
        }


        public static HomeWork ConvertToHomeWork(string content)
        {
            var hw = new HomeWork();

            try
            {
                hw.FIO = ExtractText(content, Header_FIO, Header_Task);
                hw.Task = ExtractText(content, Header_Task, Header_Decision);
                hw.Decision = ExtractText(content, Header_Decision, Header_Comment);
                hw.Comment = ExtractText(content, Header_Comment);
            }
            catch
            {
                throw new FileFormatException();
            }
            return hw;
        }

        public static void Save(string path, HomeWork hw)
        {
            using (var streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(Header_FIO);
                streamWriter.Write(hw.FIO);

                streamWriter.Write(Header_Task);
                streamWriter.Write(hw.Task);

                streamWriter.Write(Header_Decision);
                streamWriter.Write(hw.Decision);

                streamWriter.Write(Header_Comment);
                streamWriter.Write(hw.Comment);
            }
        }

        /// <summary>
        /// Вытащить текст, что идёт после определённого заголовка и до определённого
        /// </summary>
        /// <param name="content"></param>
        /// <param name="startAfter"></param>
        /// <param name="endBefore"></param>
        /// <returns></returns>
        private static string ExtractText(string content, string startAfter, string endBefore)
        {
            var taskStart = content.IndexOf(startAfter) + startAfter.Length;
            var taskEnd = content.IndexOf(endBefore);
            var length = taskEnd - taskStart;
            if (length > 0)
                return content.Substring(taskStart, length);
            else
                return string.Empty;
        }

        /// <summary>
        /// Вытащить текст, что идёт после определённого заголовка
        /// идти до конца
        /// </summary>
        /// <param name="content"></param>
        /// <param name="startAfter"></param>
        /// <returns></returns>
        private static string ExtractText(string content, string startAfter)
        {
            var taskStart = content.IndexOf(startAfter) + startAfter.Length;
            return content.Substring(taskStart);
        }

        /// <summary>
        /// Вытащить всё содержимое файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string OpenRaw(string path)
        {
            return File.ReadAllText(path);
        }




    }

    class FileFormatException : Exception
    {

        public FileFormatException()
            : base("File has wrong format")
        {
        }
    }
}
