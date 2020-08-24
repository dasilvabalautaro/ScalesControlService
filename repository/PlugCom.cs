using System;
using System.IO;
using System.IO.Ports;

namespace ScalesControlService.repository
{
    class PlugCom : IDisposable
    {
        #region variables
        SerialPort serialPort;
        private string _portName = "COM1";
        private int _baudRate = 9600;
        private Parity _parity = Parity.None;
        private StopBits _stopBits = StopBits.One;
        private int _dataBits = 8;
        private Handshake _handshake = Handshake.None;
        private bool _rtsEnable = true;
        private bool _dtrEnable = true;
        private bool _discardNull = true;
        public delegate void ResultReadPortDelegate(string result);
        public event ResultReadPortDelegate OnReadPort;
        private string _result;
        public int BaudRate
        {
            get
            {
                return _baudRate;
            }

            set
            {
                _baudRate = value;
                serialPort.BaudRate = _baudRate;
            }
        }

        public Parity Parity
        {
            get
            {
                return _parity;
            }

            set
            {
                _parity = value;
                serialPort.Parity = _parity;
            }
        }

        public StopBits StopBits
        {
            get
            {
                return _stopBits;
            }

            set
            {
                _stopBits = value;
                serialPort.StopBits = _stopBits;
            }
        }

        public int DataBits
        {
            get
            {
                return _dataBits;
            }

            set
            {
                _dataBits = value;
                serialPort.DataBits = _dataBits;
            }
        }

        public Handshake Handshake
        {
            get
            {
                return _handshake;
            }

            set
            {
                _handshake = value;
                serialPort.Handshake = _handshake;
            }
        }

        public bool RtsEnable
        {
            get
            {
                return _rtsEnable;
            }

            set
            {
                _rtsEnable = value;
                serialPort.RtsEnable = _rtsEnable;
            }
        }

        public bool DtrEnable
        {
            get
            {
                return _dtrEnable;
            }

            set
            {
                _dtrEnable = value;
                serialPort.DtrEnable = _dtrEnable;
            }
        }

        public bool DiscardNull
        {
            get
            {
                return _discardNull;
            }

            set
            {
                _discardNull = value;
                serialPort.DiscardNull = _discardNull;
            }
        }

        public string Result
        {
            get
            {
                return _result;
            }

            set
            {
                _result = value;
                if (OnReadPort != null)
                {
                    OnReadPort(_result);
                }
            }
        }

        public string PortName
        {
            get
            {
                return _portName;
            }

            set
            {
                _portName = value;
                serialPort.PortName = _portName;
            }
        }
        #endregion

        #region methods
        public PlugCom()
        {
            serialPort = new SerialPort();
            serialPort.BaudRate = BaudRate;
            serialPort.Parity = Parity;
            serialPort.StopBits = StopBits;
            serialPort.DataBits = DataBits;
            serialPort.Handshake = Handshake;
            serialPort.RtsEnable = RtsEnable;
            serialPort.DtrEnable = DtrEnable;
            serialPort.DiscardNull = DiscardNull;
            serialPort.DataReceived += new
                SerialDataReceivedEventHandler(DataReceivedHandler);

        }

        public void open()
        {
            try
            {

                SerialPortFixer.Execute(PortName);
                serialPort.Open();
            }
            catch (IOException ex)
            {
                throw new ArgumentException(string
                    .Format("Error: {0} Puerto Com", ex.Message));
            }

        }

        public void write(byte[] data)
        {
            try
            {
                serialPort.Write(data, 0, data.Length);
            }
            catch (IOException ex)
            {
                throw new ArgumentException(string
                    .Format("Error Write: {0} Puerto Com", ex.Message));
            }

        }

        public bool isOpen()
        {
            if (serialPort.IsOpen)
            {
                return true;
            }
            return false;
        }

        public void close()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.DtrEnable = false;
                    serialPort.RtsEnable = false;
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                    serialPort.Close();

                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string
                    .Format("Error: {0} Puerto Com", ex.Message));
            }
        }
        private void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            try
            {

                string indata = sp.ReadLine();
                cleanInput(indata);
            }
            catch (TimeoutException te)
            {
                Console.WriteLine(te.Message);
            }
            catch (InvalidOperationException ie)
            {
                Console.WriteLine(ie.Message);
            }
            catch (IOException io)
            {
                Console.WriteLine(io.Message);
            }
            //Console.WriteLine(indata);
        }

        private void cleanInput(string input)
        {

            int k;
            string strValue = string.Empty;
            char[] arrayInput = input.ToCharArray();

            for (int i = 0; i < arrayInput.Length; i++)
            {
                if (int.TryParse(Convert.ToString(arrayInput[i]), out k))
                {
                    strValue += Convert.ToString(k);
                }
                else if (Convert.ToString(arrayInput[i]).Equals("-"))
                {
                    strValue += Convert.ToString(arrayInput[i]);
                }
            }

            Result = strValue;
        }

        public void Dispose()
        {
            serialPort.Dispose();
        }

        ~PlugCom()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                    serialPort.Close();
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion
    }
}
