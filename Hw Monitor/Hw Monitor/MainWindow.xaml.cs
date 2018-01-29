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
using System.Net.NetworkInformation;
using OpenHardwareMonitor.Hardware;
using ZedGraph;

namespace Hw_Monitor
{

    public partial class MainWindow : Window
    {
        Computer thisComputer = new Computer();
        DispatcherTimer timer = new DispatcherTimer();
        PointPairList list_cpuLoad = new PointPairList();
        PointPairList list_ram = new PointPairList();
        PointPairList list_disk = new PointPairList();
        PointPairList list_network = new PointPairList();
        PerformanceCounter network = new PerformanceCounter();
        double ram_Data = 0.0, disk_Data = 0.0, cpu_Load = 0.0;
        int cpu_Temp = 0, x_time = 0, network_Data = 0;
        String string_Network = "";
      

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            String choose = menuItem.Tag.ToString();
            if(choose == "ansicht")
            {
                Grid_Button.Visibility = Visibility.Visible;
                GridInformation.Visibility = Visibility.Hidden;
                graphVisible();
            }
            if(choose == "information")
            {
                GridInformation.Visibility = Visibility.Visible;
                Grid_Button.Visibility = Visibility.Hidden;
                graphHidden();
            }
        }
        
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

            zedgraph_cpu.GraphPane.Title.Text = "Prozessor";
            zedgraph_cpu.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_cpu.GraphPane.YAxis.Title.Text = "Auslastung in %";
            zedgraph_cpu.GraphPane.CurveList.Clear();
            list_cpuLoad.Clear();

            zedgraph_ram.GraphPane.Title.Text = "Arbeitsspeicher";
            zedgraph_ram.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_ram.GraphPane.YAxis.Title.Text = "Durchsatz";
            zedgraph_ram.GraphPane.CurveList.Clear();
            list_ram.Clear();

            zedgraph_disk.GraphPane.Title.Text = "Datenträger";
            zedgraph_disk.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_disk.GraphPane.YAxis.Title.Text = "Durchsatz";
            zedgraph_disk.GraphPane.CurveList.Clear();
            list_disk.Clear();

            zedgraph_network.GraphPane.Title.Text = "Netzwerkverkehr";
            zedgraph_network.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_network.GraphPane.YAxis.Title.Text = "Bytes/s";
            zedgraph_network.GraphPane.CurveList.Clear();
            list_network.Clear();

            zedgraph_cpuMini.GraphPane.XAxis.Title.Text = "";
            zedgraph_cpuMini.GraphPane.YAxis.Title.Text = "";
            zedgraph_cpuMini.GraphPane.CurveList.Clear();

            zedgraph_ramMini.GraphPane.XAxis.Title.Text = "";
            zedgraph_ramMini.GraphPane.YAxis.Title.Text = "";
            zedgraph_ramMini.GraphPane.CurveList.Clear();

            zedgraph_diskMini.GraphPane.XAxis.Title.Text = "";
            zedgraph_diskMini.GraphPane.YAxis.Title.Text = "";
            zedgraph_diskMini.GraphPane.CurveList.Clear();

            zedgraph_networkMini.GraphPane.XAxis.Title.Text = "";
            zedgraph_networkMini.GraphPane.YAxis.Title.Text = "";
            zedgraph_networkMini.GraphPane.CurveList.Clear();
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            String choose = button.Tag.ToString();

            graphHidden();

            if (choose == "Cpu")
            {             
                textBox_cpuInfo.Visibility = Visibility.Visible;
                Zed_cpu.Visibility = Visibility.Visible;              
            }
            if (choose == "Ram")
            {               
                textBox_ramInfo.Visibility = Visibility.Visible;
                Zed_ram.Visibility = Visibility.Visible;
            }
            if (choose == "Disk")
            {                
                textBox_diskInfo.Visibility = Visibility.Visible;              
                Zed_disk.Visibility = Visibility.Visible;
            }
            if (choose == "Network")
            {
                textBox_networkInfo.Visibility = Visibility.Visible;
                Zed_network.Visibility = Visibility.Visible;
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {           
            String string_cpuLoad = "";
            String string_ramData = "";
            String string_diskLoad = "";
            String string_cpuTemp = "";
            String string_InfoCpu = "";
            String string_InfoRam = "";
            String string_InfoDisk = "";
            String string_NetworkStatus = "";
            String string_MacAddress = "";
            String string_Username = "";

            string_Username = Environment.UserName;

            foreach (var networkItem in NetworkInterface.GetAllNetworkInterfaces())
            {
                string_Network = networkItem.Description.ToString();
                string_MacAddress = networkItem.GetPhysicalAddress().ToString();
                string_NetworkStatus = networkItem.OperationalStatus.ToString();

                if (string_NetworkStatus == "Down")
                {
                    string_NetworkStatus = "";
                    string_Network = "";
                    string_MacAddress = "";
                }
                else
                {
                    string_Network = string_Network.Replace("(", "[").Replace(")", "]");
                    break;
                }
            }

            network.CategoryName = "Netzwerkschnittstelle";
            network.CounterName = "Gesamtanzahl Bytes/s";
            network.InstanceName = string_Network;
            network_Data = (int)network.NextValue();
            


            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    string_InfoCpu = hardwareItem.Name;
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {                      
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            cpu_Temp = (int)sensor.Value.Value;
                            string_cpuTemp += String.Format("{0} Temperature = {1} °C\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                            cpuTempDanger();
                        }
                        if (sensor.SensorType == SensorType.Load)
                        {
                            cpu_Load = (double)sensor.Value.Value;
                            string_cpuLoad += String.Format("{0} Load = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                        }
                    }
                }
                else if (hardwareItem.HardwareType == HardwareType.RAM)
                {                 
                    hardwareItem.Update();
                    string_InfoRam = hardwareItem.Name;
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        
                        if (sensor.SensorType == SensorType.Data)
                        {
                            ram_Data = (double)sensor.Value.Value;
                            string_ramData += String.Format("{0} Ram = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                        }                      
                    }
                }
                else if (hardwareItem.HardwareType == HardwareType.HDD)
                {
                    hardwareItem.Update();
                    string_InfoDisk = hardwareItem.Name;                 
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {                       
                        if (sensor.SensorType == SensorType.Load)
                        {
                            disk_Data = (double)sensor.Value.Value;
                            string_diskLoad += String.Format("{0} HDD = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                        }

                    }
                }               

            }
            //Hardware Informationen
            textBox_cpuInfo.Text = "Prozessor: " + string_InfoCpu + "\n" + 
                                    string_cpuTemp + "\n" + 
                                    "Prozessortemperatur" + "\n" +                                   
                                    string_cpuLoad;
            textBox_ramInfo.Text = "Arbeitsspeicher: " + string_InfoRam + "\n" + 
                                    string_ramData;
            textBox_diskInfo.Text = "Festplatte: " + "\n" + string_InfoDisk + "\n" + 
                                    string_diskLoad;
            textBox_networkInfo.Text = "Netzwerkarte: " + string_Network + "\n" + 
                                       "Empfangene Bytes/s:   " + network_Data.ToString();
            textBox_networkAllInformation.Text = "Benutzername:    " + string_Username + "\n" +
                                                 "Netzwerkkarte:   " + string_Network + "\n" +
                                                 "Netzwerkstatus:  " + string_NetworkStatus + "\n" +
                                                 "Mac-Adresse      " + string_MacAddress;

            //Alle 60s wird der Graph gelöscht und beginnt bei 0
            if (x_time == 60)
            {
                clearGraph();
            }
            x_time++;

            //X und Y Werte List übergeben
            list_cpuLoad.Add(x_time, cpu_Load);
            list_ram.Add(x_time, ram_Data);
            list_disk.Add(x_time, disk_Data);
            list_network.Add(x_time, network_Data);

            //Graph zeichnen
            DrawGraph();    
        }
        public void graphHidden()
        {
            textBox_ramInfo.Visibility = Visibility.Hidden;
            textBox_cpuInfo.Visibility = Visibility.Hidden;
            textBox_diskInfo.Visibility = Visibility.Hidden;
            textBox_networkInfo.Visibility = Visibility.Hidden;
            Zed_cpu.Visibility = Visibility.Hidden;
            Zed_ram.Visibility = Visibility.Hidden;
            Zed_disk.Visibility = Visibility.Hidden;
            Zed_network.Visibility = Visibility.Hidden;
        }

        public void graphVisible()
        {          
            textBox_cpuInfo.Visibility = Visibility.Visible;         
            Zed_cpu.Visibility = Visibility.Visible;
        }

        public void clearGraph()
        {
            //CPU
            zedgraph_cpu.GraphPane.CurveList.Clear();
            zedgraph_cpuMini.GraphPane.CurveList.Clear();
            list_cpuLoad.Clear();

            //Ram
            zedgraph_ram.GraphPane.CurveList.Clear();
            zedgraph_ramMini.GraphPane.CurveList.Clear();
            list_ram.Clear();

            //Disk 
            zedgraph_disk.GraphPane.CurveList.Clear();
            zedgraph_diskMini.GraphPane.CurveList.Clear();
            list_disk.Clear();

            //Network
            zedgraph_network.GraphPane.CurveList.Clear();
            zedgraph_networkMini.GraphPane.CurveList.Clear();
            list_network.Clear();
           
            x_time = 0;
        }

        public void cpuTempDanger()
        {
            if (cpu_Temp >= 65)
            {
                Button_cpu.Background = Brushes.IndianRed;
            }
            else
            {
                Button_cpu.Background = Brushes.White;
            }
        }

        public void DrawGraph()
        {
            LineItem myCurve_cpu = zedgraph_cpu.GraphPane.AddCurve("", list_cpuLoad, System.Drawing.Color.Blue, SymbolType.None);
            zedgraph_cpu.AxisChange();
            zedgraph_cpu.Refresh();

            LineItem myCurve_ram = zedgraph_ram.GraphPane.AddCurve("", list_ram, System.Drawing.Color.DarkRed, SymbolType.None);
            zedgraph_ram.AxisChange();
            zedgraph_ram.Refresh();

            LineItem myCurve_disk = zedgraph_disk.GraphPane.AddCurve("", list_disk, System.Drawing.Color.Green, SymbolType.None);
            zedgraph_disk.AxisChange();
            zedgraph_disk.Refresh();

            LineItem myCurve_network = zedgraph_network.GraphPane.AddCurve("", list_network, System.Drawing.Color.DarkOrange, SymbolType.None);
            zedgraph_network.AxisChange();
            zedgraph_network.Refresh();

            LineItem myCurve_cpuMini = zedgraph_cpuMini.GraphPane.AddCurve("", list_cpuLoad, System.Drawing.Color.Blue, SymbolType.None);
            zedgraph_cpuMini.AxisChange();
            zedgraph_cpuMini.Refresh();

            LineItem myCurve_ramMini = zedgraph_ramMini.GraphPane.AddCurve("", list_ram, System.Drawing.Color.DarkRed, SymbolType.None);
            zedgraph_ramMini.AxisChange();
            zedgraph_ramMini.Refresh();

            LineItem myCurve_diskMini = zedgraph_diskMini.GraphPane.AddCurve("", list_disk, System.Drawing.Color.Green, SymbolType.None);
            zedgraph_diskMini.AxisChange();
            zedgraph_diskMini.Refresh();

            LineItem myCurve_networkMini = zedgraph_networkMini.GraphPane.AddCurve("", list_network, System.Drawing.Color.DarkOrange, SymbolType.None);
            zedgraph_networkMini.AxisChange();
            zedgraph_networkMini.Refresh();
        }

    }
}



