using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using System.Media;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;
using KeyAuth;
using neutronic_launcher;
using System.ComponentModel.Composition.Primitives;
using static KeyAuth.api;
using DiscordRPC;
using Neutronic_RPC;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsApp2

{
    public partial class Form1 : MaterialForm
    {
        private const string ProcessName = "Growtopia";
        public static api KeyAuthApp = new api( //keyauth login shit change these to your own keyauth things if u want
        name: "",
        ownerid: "",
        secret: "",
        version: "1.0");
        string dllname = "youdllname.dll"; //put the dll in the folder where the loader executable is or make a path for the dll yourself do whatever u want


        [DllImport("kernel32.dll")] //some dll imports
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);



        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple700, Primary.Purple700, Primary.Purple700, Accent.Purple400, TextShade.WHITE); //materialskin.2 things, the ui framework
        }



        private void materialButton1_Click(object sender, EventArgs e) //launch button
        {
            if (materialCheckbox1.Checked) //this will be run if growtopia IS open
            {
                materialButton1.Text = ("Loading Neutronic...");
                Process[] pname = Process.GetProcessesByName("Growtopia");
                var handleprocess2 = OpenProcess(0x0002 | 0x040 | 0x0008 | 0x0020 | 0x0010, false, pname[0].Id);
                var loadlib2 = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                var filename2 = new FileInfo(dllname);
                var allocated2 = VirtualAllocEx(handleprocess2, IntPtr.Zero, (uint)((filename2.FullName.Length + 1) * Marshal.SizeOf(typeof(char))), 0x00001000 | 0x00002000, 4);
                WriteProcessMemory(handleprocess2, allocated2, Encoding.Default.GetBytes(filename2.FullName), (uint)((filename2.FullName.Length + 1) * Marshal.SizeOf(typeof(char))), out _);
                CreateRemoteThread(handleprocess2, IntPtr.Zero, 0, loadlib2, allocated2, 0, IntPtr.Zero);
                materialButton1.Text = ("Neutronic Loaded Press INSERT To Open Menu");
                RPC.SetState("Injected Neutronic", true);
            }
            else if (!materialCheckbox1.Checked) //this will be run if growtopia is NOT open
            {
                materialButton1.Text = ("Loading Neutronic...");
                string growtopiaPath = gtpathbox.Text;
                Process.Start(growtopiaPath);
                Task.Delay(5000);
                Process[] pname = Process.GetProcessesByName("Growtopia");
                var handleprocess2 = OpenProcess(0x0002 | 0x040 | 0x0008 | 0x0020 | 0x0010, false, pname[0].Id);
                var loadlib2 = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                var filename2 = new FileInfo(dllname);
                var allocated2 = VirtualAllocEx(handleprocess2, IntPtr.Zero, (uint)((filename2.FullName.Length + 1) * Marshal.SizeOf(typeof(char))), 0x00001000 | 0x00002000, 4);
                WriteProcessMemory(handleprocess2, allocated2, Encoding.Default.GetBytes(filename2.FullName), (uint)((filename2.FullName.Length + 1) * Marshal.SizeOf(typeof(char))), out _);
                CreateRemoteThread(handleprocess2, IntPtr.Zero, 0, loadlib2, allocated2, 0, IntPtr.Zero);
                materialButton1.Text = ("Neutronic Loaded Press INSERT To Open Menu");
                RPC.SetState("Injected Neutronic", true);
            }

        }

        private void Form1_Load(object sender, EventArgs e) //form loaded 
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string growtopiaPath = Path.Combine(localAppDataPath, @"Growtopia\Growtopia.exe"); //find gt path
            KeyAuthApp.init(); //do not remove this
            materialLabel2.Text = (" Number of keys: " + KeyAuthApp.app_data.numKeys); //keyauth stats doesnt work i think
            materialLabel3.Text = (" Number of users: " + KeyAuthApp.app_data.numUsers);
            materialLabel4.Text = (" Number of online users: " + KeyAuthApp.app_data.numOnlineUsers);
            materialLabel5.Text = (" Application Version: " + KeyAuthApp.app_data.version);
            materialLabel1.Text = ("Only download at Neutronic.xyz, all other websites are fake");
            timer1.Start();
            this.materialLabel2.Font = new Font("Roboto", 50f, FontStyle.Bold);
            this.materialLabel3.Font = new Font("Roboto", 50f, FontStyle.Bold);
            this.materialLabel4.Font = new Font("Roboto", 50f, FontStyle.Bold);
            this.materialLabel5.Font = new Font("Roboto", 50f, FontStyle.Bold);

            RPC.SetState("Neutronic Loader", true); //discord rpc

            materialLabel6.Text = new WebClient().DownloadString("https://bullet.lol/neutronic"); //change this link to what text u want to show in the updates tab, this raw text site is an example

            gtpathbox.Text = (growtopiaPath); //pathbox text it sets it ondefault to the default gt path and yeah there is already choose path thing made

        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcesses()) //anti sniff very easy bypass 
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

        private void tabPage4_Click(object sender, EventArgs e)
        {
               
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void materialSwitch1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void materialButton2_Click(object sender, EventArgs e) //choose gt path
        {
            OpenFileDialog openexe = new OpenFileDialog();

            if (openexe.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gtpathbox.Text = openexe.FileName;
            }
        }
    }
    } 

 
