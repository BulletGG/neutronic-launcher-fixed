﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Diagnostics;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WindowsFormsApp2;
using DiscordRPC;

namespace neutronic_launcher
{
    public partial class Form2 : MaterialForm  //this is login form
    {
        public Form2()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple700, Primary.Purple700, Primary.Purple700, Accent.Purple400, TextShade.WHITE);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcesses())  //anti sniff very easy bypass 
            {
                bool flag =
                    process.ProcessName.Contains("smsniff") || process.ProcessName.Contains("HttpAnalyzer") || process.ProcessName.Contains("dnSpy") ||
                    process.ProcessName.Contains("IDA") || process.ProcessName.Contains("Olly") || process.ProcessName.Contains("Dumper") ||
                    process.ProcessName.Contains("Reflector") || process.ProcessName.Contains("Wireshark") || process.ProcessName.Contains("WPE") ||
                    process.ProcessName.Contains("HTTP Debugger Pro") || process.ProcessName.Contains("The Wireshark Network Analyzer") || process.ProcessName.Contains("WinDbg") ||
                    process.ProcessName.Contains("Colasoft Capsa") || process.ProcessName.Contains("OllyDbg") || process.ProcessName.Contains("WPE PRO") ||
                    process.ProcessName.Contains("Microsoft Network Monitor") || process.ProcessName.Contains("Fiddler") || process.ProcessName.Contains("SmartSniff") ||
                    process.ProcessName.Contains("Immunity Debugger") || process.ProcessName.Contains("Process Explorer") || process.ProcessName.Contains("PE Tools") ||
                    process.ProcessName.Contains("AQtime") || process.ProcessName.Contains("DS-5 Debug") || process.ProcessName.Contains("Dbxtool") ||
                    process.ProcessName.Contains("Topaz") || process.ProcessName.Contains("FusionDebug") || process.ProcessName.Contains("NetBeans") ||
                    process.ProcessName.Contains("Rational Purify") || process.ProcessName.Contains(".NET Reflector") || process.ProcessName.Contains("Cheat Engine") ||
                    process.ProcessName.Contains("Sigma Engine") || process.ProcessName.Contains("Fixed Engine") || process.ProcessName.Contains("Hack Engine");
                    process.ProcessName.Contains("Process Hacker");


                if (flag)
                {
                    Environment.Exit(0);

                }
            }
            Thread.Sleep(150);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            timer1.Start();

        }

        private void materialButton1_Click(object sender, EventArgs e) //login
        {

                Form1 main = new Form1();
                main.Show();
                this.Hide();
                MessageBox.Show("Logging In succesfull");
            
        }

        private void materialButton2_Click(object sender, EventArgs e) //register
        {

                Form1 main = new Form1();
                main.Show();
                this.Hide();
                MessageBox.Show("Registering Succesful, remember your password");
            
        }

        private void materialButton3_Click(object sender, EventArgs e) //register with key only
        {

                Form1 main = new Form1();
                main.Show();
                this.Hide();
                MessageBox.Show("Registering Succesful, your username and password are auto set to your key, so save it");
            
        }
    }
}
