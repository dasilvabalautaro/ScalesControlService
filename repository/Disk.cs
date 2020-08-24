using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ScalesControlService.repository
{
    class Disk
    {
        #region methods
        public Disk() { }

        public string ReadTextFile(string strPath)
        {
            StreamReader objStream;
            string strReturn = string.Empty;

            try
            {
                if (File.Exists(strPath))
                {
                    objStream = new StreamReader(strPath, Encoding.Default);
                    strReturn = objStream.ReadToEnd();
                    objStream.Close();
                }

            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new FileNotFoundException("Read File", e);

            }
            return strReturn;
        }

        public void FileDelete(string strPath)
        {
            try
            {
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new FileNotFoundException("Delete File", e);

            }
        }

        public bool IsFileExists(string nameFile)
        {
            return File.Exists(nameFile);
        }

        public void CreateWorkDirectory(string nameDirectory)
        {
            
            if (!Directory.Exists(nameDirectory))
            {
                Directory.CreateDirectory(nameDirectory);
            }
        }

        public void WriteFile(string strPath, string content)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(strPath);

                streamWriter.WriteLine(content);
                streamWriter.Dispose();
            }
            catch (System.IO.IOException e)
            {
                throw new FileNotFoundException("Write File", e);

            }
        }

        public void WriteFileOfFiles(string strPath, string[] content)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(strPath);
                for (int i = 0; i < content.Count(); i++)
                {
                    streamWriter.WriteLine(content[i]);
                }

                streamWriter.Dispose();
            }
            catch (System.IO.IOException e)
            {
                throw new FileNotFoundException("Write File", e);

            }
        }

        public void WriteToFileLog(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + 
                "\\Logs\\ServiceLog_" + 
                DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        #endregion
    }
}
