using ScalesControlService.model;

namespace ScalesControlService.presenter
{
    class ManagerScalesPresenter
    {
        #region variables
        private DiskPresenter diskPresenter = new DiskPresenter();
        private PlugComPresenter scaleOne = new PlugComPresenter();
        private PlugComPresenter scaleTwo = new PlugComPresenter();
        private PlugComPresenter scaleThree = new PlugComPresenter();
        private PlugComPresenter scaleFour = new PlugComPresenter();
        #endregion

        #region methods
        public ManagerScalesPresenter() 
        {
            scaleOne.FileConfig = DiskPresenter.file_scale_one;
            scaleTwo.FileConfig = DiskPresenter.file_scale_two;
            scaleThree.FileConfig = DiskPresenter.file_scale_three;
            scaleFour.FileConfig = DiskPresenter.file_scale_four;
        }

        ~ManagerScalesPresenter()
        {
            scaleOne.Dispose();
            scaleTwo.Dispose();
            scaleThree.Dispose();
            scaleFour.Dispose();
        }
        public void LoadConfiguration()
        {
            scaleOne.LoadConfiguration();
            scaleTwo.LoadConfiguration();
            scaleThree.LoadConfiguration();
            scaleFour.LoadConfiguration();
        }

        public void OpenPorts()
        {
            if (scaleOne.Enabled == 1)
            {
                scaleOne.OpenPort();
            }
            else
            {
                diskPresenter.WriteToFileLog("Disabled Scale 1");
            }

            if (scaleTwo.Enabled == 1)
            {
                scaleTwo.OpenPort();
            }
            else
            {
                diskPresenter.WriteToFileLog("Disabled Scale 2");
            }
            if (scaleThree.Enabled == 1)
            {
                scaleThree.OpenPort();
            }
            else
            {
                diskPresenter.WriteToFileLog("Disabled Scale 3");
            }
            if (scaleFour.Enabled == 1)
            {
                scaleFour.OpenPort();
            }
            else
            {
                diskPresenter.WriteToFileLog("Disabled Scale 4");
            }
        }

        public void ClosePorts()
        {
            scaleOne.closePort();
            scaleTwo.closePort();
            scaleThree.closePort();
            scaleFour.closePort();
        }

        public void WriteWeights()
        {
            int w1 = scaleOne.Weight;
            int w2 = scaleTwo.Weight;
            int w3 = scaleThree.Weight;
            int w4 = scaleFour.Weight;
            int totalWeight = w1 + w2 + w3 + w4;

            Weight weight = new Weight();
            weight.configuration = DiskPresenter.lbl_weights;
            ParamsWeight paramsWeight = new ParamsWeight();
            paramsWeight.scale_one = w1;
            paramsWeight.scale_two = w2;
            paramsWeight.scale_three = w3;
            paramsWeight.scale_four = w4;
            paramsWeight.total = totalWeight;
            weight.Params = paramsWeight;
            diskPresenter.SaveWeights(weight);
            
        }

        public void WriteToFileLog(string Message)
        {
            diskPresenter.WriteToFileLog(Message);
        }

        public void CreateConfiguration()
        {
            diskPresenter.CreateConfiguration();
        }

        #endregion
    }
}
