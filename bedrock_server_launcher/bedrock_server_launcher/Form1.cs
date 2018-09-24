using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace bedrock_server_launcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "请点击启动\r\n";
            WebServer ws = new WebServer(SendResponse, "http://localhost:2333/");
            ws.Run();
        }

        private string revString = "";
        public string SendResponse(HttpListenerRequest request)
        {
            revString = "";
            string cmd = WebUtility.UrlDecode(request.RawUrl).Substring(1);
            sendCmd(cmd);
            AppendText("收到api命令：" + cmd + "\r\n");
            System.Threading.Thread.Sleep(500);
            return string.Format(revString, DateTime.Now);
        }

        bool isopen = false;
        Process _process = new Process();
        ProcessStartInfo psi = new ProcessStartInfo();
        private void button1_Click(object sender, EventArgs e)
        {
            //启动 cmd.exe
            psi.FileName = "cmd.exe";
            psi.Arguments = "/c @ECHO OFF &&" + textBox2.Text;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true;
            _process.StartInfo = psi;
            //定义接收消息的 Handler
            _process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
            _process.Start();
            //开始接收
            _process.BeginOutputReadLine();
            isopen = true;
        }

        private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Data) == false)
            {
                revString += e.Data + "\r\n";
                this.AppendText(e.Data + "\r\n");
            }
        }

        private void sendCmd(string cmd)
        {
            if(isopen)
                _process.StandardInput.WriteLine(cmd);
        }

        #region 解决多线程下控件访问的问题  

        public delegate void AppendTextCallback(string text);
        public void AppendText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                this.textBox1.Invoke(d, text);
            }
            else
            {
                this.textBox1.AppendText(text);
            }
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            sendCmd(textBox3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sendCmd("stop");
            isopen = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendCmd("stop");
            MessageBox.Show("退出后会自动关闭服务器");
            Application.Exit();
        }
    }
}
