using ScalesControlService.presenter;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;


namespace ScalesControlService
{
    public partial class ScalesControlService : ServiceBase
    {
        #region variables
        ManagerScalesPresenter managerScalesPresenter = new ManagerScalesPresenter();
        #endregion

        public ScalesControlService()
        {
            InitializeComponent();
            eventLogServices = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("ScalesControlSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "ScalesControlSource", "ScalesControlLog");
            }
            eventLogServices.Source = "ScalesControlSource";
            eventLogServices.Log = "ScalesControlLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLogServices.WriteEntry("In OnStart.");
            managerScalesPresenter.WriteToFileLog("Service is started at " + DateTime.Now);
            managerScalesPresenter.CreateConfiguration();
            managerScalesPresenter.LoadConfiguration();
            managerScalesPresenter.OpenPorts();
            Timer timer = new Timer();
            timer.Interval = 1000; // 60 seconds -> 60000 miliseconds - 1 second
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLogServices.WriteEntry("Capture weight: " + DateTime.Now, 
                EventLogEntryType.Information);
            managerScalesPresenter.WriteWeights();

        }

        protected override void OnStop()
        {

            eventLogServices.WriteEntry("In OnStop.");
            managerScalesPresenter.WriteToFileLog("Service is stopped at " + DateTime.Now);
            managerScalesPresenter.ClosePorts();
            
        }


    }
}
