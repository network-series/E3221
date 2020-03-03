using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;


namespace 实验二
{
    public partial class Form1 : Form
    {
        SerialPort spSend=new SerialPort("COM1");
        SerialPort spReceive=new SerialPort("COM2");
        delegate void HandleInterfaceUpdateDelegate(string text);
        HandleInterfaceUpdateDelegate interfaceUpdateHandle;
        
        // 显示端口并提示用户输入端口
        public void SetPortName(string defaultPortName)
        {
            string portName;
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
            portName = comboBox1.Text;

            if (portName == null || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
        }
        // 显示波特率值并提示用户输入值
        public static int SetPortBaudRate(int defaultPortBaudRate)
        {
            string baudRate;
            baudRate = Console.ReadLine();
            if (baudRate == null)
            {
                baudRate = defaultPortBaudRate.ToString();
            }
            return int.Parse(baudRate);
        }

        // 显示端口奇偶校验值并提示用户输入值
        public void SetPortParity(Parity defaultPortParity)
        {
            string parity;
            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                comboBox2.Items.Add(s);
            }
            parity = comboBox2.Text;

            if (parity == null)
            {
                parity = defaultPortParity.ToString();
            }
        }
        // 显示数据位值并提示用户输入值
        public static int SetPortDataBits(int defaultPortDataBits)
        {
            string dataBits;
            dataBits = Console.ReadLine();
            if (dataBits == null)
            {
                dataBits = defaultPortDataBits.ToString();
            }
            return int.Parse(dataBits.ToUpperInvariant());
        }
        // 显示停止位值并提示用户输入值
        public void SetPortStopBits(StopBits defaultPortStopBits)
        {
            string stopBits;
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                comboBox3.Items.Add(s);
            }
            stopBits = comboBox2.Text;

            if (stopBits == null)
            {
                stopBits = defaultPortStopBits.ToString();
            }
        }
        public static Handshake SetPortHandshake(Handshake defaultPortHandshake)
        {
            string handshake;

            Console.WriteLine("Available Handshake options:");
            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Handshake value (Default: {0}):", defaultPortHandshake.ToString());
            handshake = Console.ReadLine();

            if (handshake == null)
            {
                handshake = defaultPortHandshake.ToString();
            }

            return (Handshake)Enum.Parse(typeof(Handshake), handshake, true);
        }
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void UpdateTextBox(string text)
        {
            listBox2.Items.Add(DateTime.Now.ToString() + " " + text);
        }
        public void spReceive_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] readBuffer = new byte[spReceive.ReadBufferSize];
            spReceive.Read(readBuffer, 0, readBuffer.Length); 
            this.Invoke(interfaceUpdateHandle, new string[] { Encoding.ASCII.GetString(readBuffer) });
        }

            private void Form1_Load(object sender, EventArgs e)
        {
            SetPortName("");
            SetPortStopBits(StopBits.None);
            SetPortParity(Parity.None);
            InitClient();
        }

        public void InitClient()
        {
            interfaceUpdateHandle = new HandleInterfaceUpdateDelegate(UpdateTextBox);  //实例化委托对象   
            spSend.Open();  //SerialPort对象在程序结束前必须关闭，在此说明
            spReceive.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(spReceive_DataReceived);
            spReceive.ReceivedBytesThreshold = 1;
            spReceive.Open();
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(DateTime.Now.ToString()+ " "+textBox6.Text);
            spSend.WriteLine(textBox6.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = SetPortBaudRate(int.Parse(textBox1.Text)).ToString();
            textBox2.Text = SetPortDataBits(int.Parse(textBox2.Text)).ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
