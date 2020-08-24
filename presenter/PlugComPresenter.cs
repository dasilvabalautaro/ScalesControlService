using ScalesControlService.model;
using ScalesControlService.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalesControlService.presenter
{
    class PlugComPresenter : IDisposable
    {
        #region variables
        private PlugCom plugCom = new PlugCom();
        private DiskPresenter diskPresenter = new DiskPresenter();
        private string fileConfig;
        private int weight = 0;
        private int enabled = 0;
        public int Weight { get => weight; set => weight = value; }
        public string FileConfig { get => fileConfig; set => fileConfig = value; }
        public int Enabled { get => enabled; set => enabled = value; }
        #endregion

        #region methods
        public PlugComPresenter()
        {
            plugCom.OnReadPort += new PlugCom
                .ResultReadPortDelegate(sendResult);
        }

        private void sendResult(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    Weight = Convert.ToInt32(result);
                }
                catch (FormatException fe)
                {

                    diskPresenter.WriteToFileLog(fe.Message);
                }
                catch (OverflowException oe)
                {
                    diskPresenter.WriteToFileLog(oe.Message);
                }
            }
        }

        

        public void LoadConfiguration()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" +
                DiskPresenter.directoryConfiguration + "\\" + FileConfig;
            Port port = diskPresenter.ReadConfigurationPort(path);
            if (!string.IsNullOrEmpty(port.Params.com))
            {
                plugCom.PortName = port.Params.com;
            }
            Enabled = port.Params.enabled;
        }

        public void OpenPort()
        {
            try
            {
                if (!plugCom.isOpen())
                {
                    plugCom.open();
                }


            }
            catch (ArgumentException ie)
            {
                diskPresenter.WriteToFileLog(ie.Message);                
            }

        }

        public void closePort()
        {
            try
            {
                System.Threading.Thread closeDownPort = new System.Threading.Thread(new System.Threading
               .ThreadStart(plugCom.close));
                closeDownPort.Start();
                closeDownPort.Join(2000);
                diskPresenter.WriteToFileLog("END THREAD PORT: " + plugCom.PortName);
            }
            catch (ArgumentException ie)
            {
                diskPresenter.WriteToFileLog(ie.Message);

            }

        }

        public void Dispose()
        {
            plugCom.Dispose();
        }

        
        #endregion
    }
}
