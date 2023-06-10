using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace stm32_tek_veri_okuma
{
    public partial class Form1 : Form
    {
        string[] portlar = SerialPort.GetPortNames();
        string[] baudrate_hizlar = { "4800", "9600", "57600", "115200" };
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;
            }
            foreach (string baudrate in baudrate_hizlar)
            {
                comboBox2.Items.Add(baudrate);
                comboBox2.SelectedIndex = 0;
            }
            label1.Text = "BAGLANTI KAPALI";
            label1.ForeColor = Color.Red;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
            serialPort1.Open();
            label2.Text = "Baglanti Açık";
            
        }
        private void button_durdur_Click(object sender, EventArgs e)
        {
         
            if(serialPort1.IsOpen==true)
            {
                serialPort1.Close();
                label1.Text = "Bağlanti Kapalı";
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(serialPort1.IsOpen==true)
            {
                serialPort1.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            serialPort1.Write("1");
            button3.BackColor = Color.Green;
            label1.Text = "LED YANDI";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Write("0");
            label1.Text = "LED SÖNDÜ";
            button3.BackColor = Color.White;
        }
    }
}
