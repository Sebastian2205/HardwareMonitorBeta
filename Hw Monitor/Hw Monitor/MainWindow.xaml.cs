using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using OpenHardwareMonitor.Hardware;

namespace Hw_Monitor
{

    public partial class MainWindow : Window
    {
        Computer thisComputer = new Computer();
       
        
        DispatcherTimer timer = new DispatcherTimer();
        PerformanceCounter pCPU = new PerformanceCounter("Prozessor", "Prozessorzeit (%)", "_Total");
       
       
        

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();

            thisComputer.CPUEnabled = true;
            thisComputer.Open();
            thisComputer.RAMEnabled = true;
            thisComputer.HDDEnabled = true;
   
          

        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            String temp = btn.Content.ToString();
            if(temp == "Cpu")
            {
                textBox_ramInfo.Visibility = Visibility.Hidden;
                textBox_diskInfo.Visibility = Visibility.Hidden;
                textBox_cpuInfo.Visibility = Visibility.Visible;
                
            }
            if(temp == "Ram")
            {
                textBox_cpuInfo.Visibility = Visibility.Hidden;
                textBox_diskInfo.Visibility = Visibility.Hidden;
                textBox_ramInfo.Visibility = Visibility.Visible;  
            }
            if(temp == "Disk")
            {
                textBox_ramInfo.Visibility = Visibility.Hidden;
                textBox_cpuInfo.Visibility = Visibility.Hidden;
                textBox_diskInfo.Visibility = Visibility.Visible;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            double pCpu = pCPU.NextValue();





            String temp = "";
            String temp2 = ""; 
            String temp3 = "";
            String usage = "";
            
            

            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        

                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            int tmp = (int)sensor.Value.Value;
                            usage += String.Format("{0} Temperature = {1} °C\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                           
                            if (tmp >= 65)
                            {
                                label_cpu.Background = Brushes.IndianRed;
                            }
                        }

                        if (sensor.SensorType == SensorType.Load)
                        {
                            temp += String.Format("{0} Load = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                        }

                    }
                }
               
            }

            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.RAM)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();

                    foreach (var sensor in hardwareItem.Sensors)
                    {


                        if (sensor.SensorType == SensorType.Data)
                        {
                            temp2 += String.Format("{0} Ram = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                        }



                    }
                }

            }


            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.HDD)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();

                    foreach (var sensor in hardwareItem.Sensors)
                    {


                        if (sensor.SensorType == SensorType.Load)
                        {
                            temp3 += String.Format("{0} HDD = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                        }



                    }
                }

            }




            textBox_cpuInfo.Text = temp + "\n" + usage;
            textBox_ramInfo.Text = temp2;
            textBox_diskInfo.Text = temp3;
           
          
        }



    }
}



