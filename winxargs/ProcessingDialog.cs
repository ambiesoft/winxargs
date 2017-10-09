using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Ambiesoft;

namespace winxargs
{
    public partial class ProcessingDialog : Form
    {
        readonly ProcessStartInfo _psi;
        delegate void VSDeletgate(string data);

        public ProcessingDialog(ProcessStartInfo psi)
        {
            _psi = psi;
            InitializeComponent();
        }

        void setTitle(string title)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new VSDeletgate(setTitle), title);
                return;
            }
            this.Text = string.Format("{0} | {1}", title, Application.ProductName);
        }
        Process _process;
        private void ProcessingDialog_Load(object sender, EventArgs e)
        {
            _psi.CreateNoWindow = true;
            _psi.UseShellExecute = false;

            _psi.RedirectStandardOutput = true;
            _psi.RedirectStandardError = true;


            try
            {
                _process = new Process();
                _process.StartInfo = _psi;

                _process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
                _process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);
                _process.EnableRaisingEvents = true;
                _process.Exited += new EventHandler(_process_Exited);
                _process.Start();
                _process.BeginOutputReadLine();
                _process.BeginErrorReadLine();
                setTitle(Properties.Resources.S_PROCESS_RUNNING);
            }
            catch (Exception ex)
            {
                AmbLib.Alert(this, ex);
            }
        }


        void _process_Exited(object sender, EventArgs e)
        {
            setTitle(Properties.Resources.S_PROCESS_COMPLETE);
        }

        void AppendOut(string data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new VSDeletgate(AppendOut), data);
                return;
            }
            txtOut.AppendText(data);
            txtOut.AppendText("\r\n");
        }
        void AppendErr(string data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new VSDeletgate(AppendErr), data);
                return;
            }
            txtErr.AppendText(data);
            txtErr.AppendText("\r\n");
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                AppendOut(e.Data);
            }
        }
        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                AppendErr(e.Data);
            }
        }


    }
}
