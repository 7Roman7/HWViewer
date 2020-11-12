const string TaskHeader = "===Task===";
        const string CodeHeader = "===Code===";
        const string CommentHeader = "===Comment===";
        static void Main(string[] args)
        {
            string version = "", FIO = "", content = "", task = "", code = "", comment = "";
            try
            {
                //constants
                //ну если открывать файл через программу, то наверное сработает
                //path = args[0];
                string path = @"example_3.txt";
                using (StreamReader streamReader = new StreamReader(path))
                {
                    version = streamReader.ReadLine().Replace("version: ", "");
                    //пропуск заголовка ===FIO===, зачем он вообще
                    streamReader.ReadLine();
                    FIO = streamReader.ReadLine();
                    content = streamReader.ReadToEnd();
                    task = content.Substring(content.LastIndexOf(TaskHeader), 
                        content.IndexOf(CodeHeader) - content.LastIndexOf(TaskHeader)).Replace(TaskHeader, "");

                    code = content.Substring(content.LastIndexOf(CodeHeader), 
                        content.IndexOf(CommentHeader) - content.LastIndexOf(CodeHeader)).Replace(CodeHeader, "");

                    comment = content.Substring(content.LastIndexOf(CommentHeader)).Replace(CommentHeader, "");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("version: " + version + "\n");
                Console.WriteLine(FIO + "\n");
                Console.WriteLine(task);
                Console.WriteLine(code);
                Console.WriteLine(comment);
            }
        }