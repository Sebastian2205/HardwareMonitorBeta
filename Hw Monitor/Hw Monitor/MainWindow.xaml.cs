﻿using System;
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
        double ram_Data = 0.0, disk_Data = 0.0, cpu_Load = 0.0;
        int cpu_Temp = 0, x_time = 0;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

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

            zedgraph_cpu.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_cpu.GraphPane.YAxis.Title.Text = "Auslastung in %";
            zedgraph_cpu.GraphPane.CurveList.Clear();
            list_cpuLoad.Clear();

            zedgraph_ram.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_ram.GraphPane.YAxis.Title.Text = "Durchsatz";
            zedgraph_ram.GraphPane.CurveList.Clear();
            list_ram.Clear();

            zedgraph_disk.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_disk.GraphPane.YAxis.Title.Text = "Durchsatz";
            zedgraph_disk.GraphPane.CurveList.Clear();
            list_disk.Clear();
        
            zedgraph.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph.GraphPane.YAxis.Title.Text = "Durchsatz";
            zedgraph.GraphPane.CurveList.Clear();

            zedgraph_cpuMini.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_cpuMini.GraphPane.YAxis.Title.Text = "Durchsatz";
            zedgraph_cpuMini.GraphPane.CurveList.Clear();

            zedgraph_ramMini.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_ramMini.GraphPane.YAxis.Title.Text = "Durchsatz";
            zedgraph_ramMini.GraphPane.CurveList.Clear();

            zedgraph_diskMini.GraphPane.XAxis.Title.Text = "Zeit";
            zedgraph_diskMini.GraphPane.YAxis.Title.Text = "Durchsatz";
            zedgraph_diskMini.GraphPane.CurveList.Clear();
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            String temp = button.Tag.ToString();
            if (temp == "Cpu")
            {

                textBox_ramInfo.Visibility = Visibility.Hidden;
                textBox_diskInfo.Visibility = Visibility.Hidden;
                textBox_cpuInfo.Visibility = Visibility.Visible;
                Zed_cpu.Visibility = Visibility.Visible;
                Zed_ram.Visibility = Visibility.Hidden;
                Zed.Visibility = Visibility.Hidden;
                Zed_disk.Visibility = Visibility.Hidden;

            }
            if (temp == "Ram")
            {
                textBox_cpuInfo.Visibility = Visibility.Hidden;
                textBox_diskInfo.Visibility = Visibility.Hidden;
                textBox_ramInfo.Visibility = Visibility.Visible;
                Zed_ram.Visibility = Visibility.Visible;
                Zed_cpu.Visibility = Visibility.Hidden;
                Zed.Visibility = Visibility.Hidden;
                Zed_disk.Visibility = Visibility.Hidden;
            }
            if (temp == "Disk")
            {
                textBox_ramInfo.Visibility = Visibility.Hidden;
                textBox_cpuInfo.Visibility = Visibility.Hidden;
                textBox_diskInfo.Visibility = Visibility.Visible;
                Zed_cpu.Visibility = Visibility.Hidden;
                Zed_ram.Visibility = Visibility.Hidden;
                Zed.Visibility = Visibility.Hidden;
                Zed_disk.Visibility = Visibility.Visible;
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            
            String string_cpuLoad = "";
            String string_ramData = "";
            String string_diskLoad = "";
            String string_cpuTemp = "";

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
                            cpu_Temp = (int)sensor.Value.Value;
                            string_cpuTemp += String.Format("{0} Temperature = {1} °C\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                            if (cpu_Temp >= 65)
                            {
                                Button_cpu.Background = Brushes.IndianRed;
                            }
                        }

                        if (sensor.SensorType == SensorType.Load)
                        {
                            cpu_Load = (double)sensor.Value.Value;
                            string_cpuLoad += String.Format("{0} Load = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
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
                            ram_Data = (double)sensor.Value.Value;
                            string_ramData += String.Format("{0} Ram = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
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
                            disk_Data = (double)sensor.Value.Value;
                            string_diskLoad += String.Format("{0} HDD = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");
                        }

                    }
                }

            }
            //Hardware Informationen
            textBox_cpuInfo.Text = string_cpuLoad + "\n" + string_cpuTemp;
            textBox_ramInfo.Text = string_ramData;
            textBox_diskInfo.Text = string_diskLoad;

            //Alle 60s wird der Graph gelöscht und beginnt bei 0
            if (x_time == 60)
            {
                //CPU
                zedgraph_cpu.GraphPane.CurveList.Clear();
                zedgraph_cpuMini.GraphPane.CurveList.Clear();
                list_cpuLoad.Clear();

                //Ram
                zedgraph_ram.GraphPane.CurveList.Clear();
                zedgraph.GraphPane.CurveList.Clear();
                zedgraph_ramMini.GraphPane.CurveList.Clear();
                list_ram.Clear();

                //Disk 
                zedgraph_disk.GraphPane.CurveList.Clear();
                zedgraph_diskMini.GraphPane.CurveList.Clear();
                list_disk.Clear();

                x_time = 0;
            }
            x_time++;

            //X und Y Werte List übergeben
            list_cpuLoad.Add(x_time, cpu_Load);
            list_ram.Add(x_time, ram_Data);
            list_disk.Add(x_time, disk_Data);

            //Graph zeichnen
            LineItem myCurve_cpu = zedgraph_cpu.GraphPane.AddCurve("", list_cpuLoad, System.Drawing.Color.Red, SymbolType.None);
            zedgraph_cpu.AxisChange();
            zedgraph_cpu.Refresh();

            LineItem myCurve_ram = zedgraph_ram.GraphPane.AddCurve("", list_ram, System.Drawing.Color.Blue, SymbolType.None);
            zedgraph_ram.AxisChange();
            zedgraph_ram.Refresh();

            LineItem myCurve_disk = zedgraph_disk.GraphPane.AddCurve("", list_disk, System.Drawing.Color.Green, SymbolType.None);
            zedgraph_disk.AxisChange();
            zedgraph_disk.Refresh();

            LineItem myCurve = zedgraph.GraphPane.AddCurve("", list_cpuLoad, System.Drawing.Color.Red, SymbolType.None);
            LineItem myCurve2 = zedgraph.GraphPane.AddCurve("", list_ram, System.Drawing.Color.Blue, SymbolType.None);
            zedgraph.AxisChange();
            zedgraph.Refresh();

            LineItem myCurve_cpuMini = zedgraph_cpuMini.GraphPane.AddCurve("", list_cpuLoad, System.Drawing.Color.Red, SymbolType.None);
            zedgraph_cpuMini.AxisChange();
            zedgraph_cpuMini.Refresh();

            LineItem myCurve_ramMini = zedgraph_ramMini.GraphPane.AddCurve("", list_ram, System.Drawing.Color.Blue, SymbolType.None);
            zedgraph_ramMini.AxisChange();
            zedgraph_ramMini.Refresh();

            LineItem myCurve_diskMini = zedgraph_diskMini.GraphPane.AddCurve("", list_disk, System.Drawing.Color.Green, SymbolType.None);
            zedgraph_diskMini.AxisChange();
            zedgraph_diskMini.Refresh();
        }


    }
}



