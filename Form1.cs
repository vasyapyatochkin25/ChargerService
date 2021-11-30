using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace Charger
{
    public partial class Form1 : Form
    {
        String DataIn = "";
        string[] res;
        
        double xAxis;
        //Excel excel = new Excel(@"C:\Users\vasya\Desktop\test.xlsx", 1);
        uint flagDisableChangeTextBox=0;
        public Form1()
        {
            InitializeComponent();
            PortsAvailable();

            //excel.WriteStrToCell(0, 0, "Hello");
            //excel.Save();

            this.chart1.Series[0].Points.Clear();
   
        }
        



        void PortsAvailable()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(ports);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (button1.Enabled == true)
            {
                if (comboBox1.Text!="")
                {
                    if (comboBox2.Text!="")
                    {
                        serialPort1.PortName = comboBox1.Text;
                        serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);

                        serialPort1.Open();
                        groupBox1.Enabled = true;
                        groupBox2.Enabled = true;

                        button1.Enabled = false;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;

                        timer1.Enabled = true;

                    }
                    else
                    {
                        MessageBox.Show(
                                    "Выберите скорость",
                                    "Предупреждение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                else
                {
                    MessageBox.Show(
                                    "Выберите COM порт",
                                    "Предупреждение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Enabled == true)
            {
                timer1.Enabled = false;
                serialPort1.Close();
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;

            }

        }

        private void Changedata(object sender, EventArgs e)
        {
            /*
            // Original array
            Byte[] data = new Byte[] { (byte)DataIn[3], (byte)DataIn[2], (byte)DataIn[1], (byte)DataIn[0] };
            // 42.48047
            // If CPU uses Little Endian, we should reverse the data 
            
            double result = BitConverter.ToSingle(BitConverter.IsLittleEndian ? data.Reverse().ToArray() : data, 0);

            result = Math.Round(result, 2);
            */
            if (flagDisableChangeTextBox==0)
            {
                textBox2.Text = res[6];
                textBox1.Text = res[5];
            }

            label13.Text = res[4];
            label6.Text = res[3]+" A";
            label5.Text = res[2]+" В";
            
            double yAxis = Convert.ToDouble(res[3].Replace(".",","));
            xAxis += 0.2;
            xAxis = Math.Round(xAxis,1);
            
            if (yAxis < 115)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 120;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            if (yAxis < 105)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 110;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            if (yAxis < 95)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 100;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            if (yAxis < 85)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 90;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            if (yAxis < 75)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 80;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            if (yAxis < 65)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 70;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            if (yAxis < 55)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 60;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            if (yAxis < 45)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 50;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }



            if (yAxis < 35)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 40;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }


            if (yAxis < 25)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 30;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            if (yAxis < 15)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 20;
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            }
             
            if (yAxis < 5)
            {
                this.chart1.ChartAreas[0].AxisY.Maximum = 10;
                this.chart1.ChartAreas[0].AxisY.Minimum = -1;
            }


           


            this.chart1.ChartAreas[0].AxisX.Minimum = xAxis - (xAxis * 0.65);
            this.chart1.ChartAreas[0].AxisX.Maximum = (xAxis * 0.35) + xAxis;



            this.chart1.Series[0].Points.AddXY(xAxis, yAxis);
          

            DataIn = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            serialPort1.WriteLine("get");
        }

        private void serialPort1_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            int bufferSize = 0;

            bufferSize = serialPort1.BytesToRead;
            DataIn += serialPort1.ReadExisting();

            int indexIn=DataIn.IndexOf("!"), 
                indexOut= DataIn.IndexOf("\r");

            if (indexIn>-1)
            {
                if (indexOut > -1)
                {

                    res = DataIn.Split(new char[] { '!', ';' });

                    serialPort1.DiscardInBuffer();
                    this.Invoke(new EventHandler(Changedata));
                }
            }
            /*
            if (DataIn.Length >= 4)
            {
  

            }
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (button1.Enabled == false)
            {
                serialPort1.WriteLine("v=" + textBox1.Text.Replace(",", "."));
                serialPort1.WriteLine("i=" + textBox2.Text.Replace(",", "."));
            }

            flagDisableChangeTextBox = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                if (button4.ForeColor==Color.Green)
                {
                    serialPort1.WriteLine("s=start");
                   
                    button4.Text = "СТОП";
                    button4.ForeColor = Color.Red;//red

                }
                else
                {
                    serialPort1.WriteLine("s=stop");
                 
                    button4.Text = "СТАРТ";
                    button4.ForeColor = Color.Green;//green
                }
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {

                serialPort1.WriteLine("Ahreset");
             
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            flagDisableChangeTextBox = 1;
            textBox1.Text = "";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            flagDisableChangeTextBox = 1;
            textBox2.Text = "";
        }
    }
}
