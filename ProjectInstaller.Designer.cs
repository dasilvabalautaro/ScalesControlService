namespace ScalesControlService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstallerScalesControl = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstallerScalesControl = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstallerScalesControl
            // 
            this.serviceProcessInstallerScalesControl.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstallerScalesControl.Password = null;
            this.serviceProcessInstallerScalesControl.Username = null;
            // 
            // serviceInstallerScalesControl
            // 
            this.serviceInstallerScalesControl.Description = "Control of Scales";
            this.serviceInstallerScalesControl.DisplayName = "ScalesControlService";
            this.serviceInstallerScalesControl.ServiceName = "ScalesControlService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstallerScalesControl,
            this.serviceInstallerScalesControl});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerScalesControl;
        private System.ServiceProcess.ServiceInstaller serviceInstallerScalesControl;
    }
}