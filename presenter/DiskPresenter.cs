using Newtonsoft.Json;
using ScalesControlService.model;
using ScalesControlService.repository;
using System;
using System.IO;

namespace ScalesControlService.presenter
{
    class DiskPresenter
    {
        #region constants
        public const string directoryConfiguration = "configuration";
        public const string file_scale_one = "scale_one.txt";
        public const string file_scale_two = "scale_two.txt";
        public const string file_scale_three = "scale_three.txt";
        public const string file_scale_four = "scale_four.txt";
        public const string lbl_configuration = "scale_configuration";
        public const string lbl_weights = "scale_weights";
        public const string file_weight = "weight.txt";
        public const string directoryWeight = "weight";

        #endregion

        #region variables
        Disk disk = new Disk();
        #endregion

        #region methods

        public DiskPresenter() 
        {
            
        }

        public void WriteToFileLog(string Message)
        {
            disk.WriteToFileLog(Message);
        }

        public void SaveConfigurationPort(string pathFile, Port port)
        {

            try
            {
                string jsonOut = JsonConvert.SerializeObject(port);                
                disk.WriteFile(pathFile, jsonOut);

            }
            catch (System.IO.IOException e)
            {
                WriteToFileLog(e.Message);

            }

        }

        public void SaveWeights(Weight weight)
        {
            try
            {
                string pathDirectory = AppDomain.CurrentDomain
                    .BaseDirectory + "\\" + directoryWeight;
                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                string pathFile = AppDomain.CurrentDomain.BaseDirectory + "\\" +
                directoryWeight + "\\" + file_weight;
                string jsonOut = JsonConvert.SerializeObject(weight);
                disk.WriteFile(pathFile, jsonOut);

            }
            catch (System.IO.IOException e)
            {
                WriteToFileLog(e.Message);

            }
        }

        public Port ReadConfigurationPort(string pathFile)
        {
            Port port = new Port();
            try
            {                
                string content = disk.ReadTextFile(pathFile);
                port = JsonConvert.DeserializeObject<Port>(content);

            }
            catch (System.IO.FileNotFoundException e)
            {

                WriteToFileLog(e.Message);
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                WriteToFileLog(ex.Message);
            }
            return port;
        }

        private void VerifyFilesConfiguration()
        {
            Port port = new Port();
            port.configuration = lbl_configuration;
            ParamsPort paramsPort = new ParamsPort();
            paramsPort.com = "COM1";
            paramsPort.enabled = 0;
            port.Params = paramsPort;
            string pathFile = AppDomain.CurrentDomain.BaseDirectory + "\\" +
                directoryConfiguration + "\\" + file_scale_one;
            
            if (!disk.IsFileExists(pathFile))
            {
                SaveConfigurationPort(pathFile, port);
            }
            pathFile = AppDomain.CurrentDomain.BaseDirectory + "\\" +
                directoryConfiguration + "\\" + file_scale_two;
            if (!disk.IsFileExists(pathFile))
            {
                SaveConfigurationPort(pathFile, port);
            }

            pathFile = AppDomain.CurrentDomain.BaseDirectory + "\\" +
                directoryConfiguration + "\\" + file_scale_three;
            if (!disk.IsFileExists(pathFile))
            {
                SaveConfigurationPort(pathFile, port);
            }

            pathFile = AppDomain.CurrentDomain.BaseDirectory + "\\" +
                directoryConfiguration + "\\" + file_scale_four;
            if (!disk.IsFileExists(pathFile))
            {
                SaveConfigurationPort(pathFile, port);
            }

        }

        public void CreateConfiguration()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + 
                directoryConfiguration;
            disk.CreateWorkDirectory(path);
            VerifyFilesConfiguration();
        }

        #endregion
    }
}
