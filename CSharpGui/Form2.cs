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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CSharpGui
{
    public partial class Form2 : Form
    {
        private int browseCount = 0;
        private bool successStart = false;
        private string writePath = Directory.GetCurrentDirectory();
        public Form2()
        {

            InitializeComponent();
            FormClosing += Form2_FormClosing;
            
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        
        }

        public bool getSuccessStart()
        {
            return successStart;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pathexe = textBox1.Text;
            string pathior = textBox2.Text;
            
            if (IsValidPath(pathexe) && IsValidPath(pathior))
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                string cParams = "";
                if (pathior.Contains("ior"))
                    cParams = "-DCPSInfoRepo file://" + pathior;
                else if (pathior.Contains("ini"))
                    cParams = "-DCPSConfigFile " + pathior; 
                
                Process p = new Process();
                p.StartInfo.RedirectStandardError = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.UseShellExecute = false;  // ShellExecute = true not allowed when output is redirected..
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.FileName = pathexe;

                int pos = pathexe.LastIndexOf("/") + 1;

                string procname = pathexe.Substring(pos, pathexe.Length - pos);
                KillPublisher(procname);
                System.Threading.Thread.Sleep(1000);
                p.StartInfo.Arguments = cParams;
                p.Start();
                System.Threading.Thread.Sleep(1000);
                successStart = true;
                
                writePath = Path.GetDirectoryName(pathexe);
                System.IO.File.WriteAllText(writePath + "//" + Constants.Filename.COMMAND_FILE, string.Empty);
                this.FormClosing -= Form2_FormClosing; 
                this.Close();
                
                
            }
            
        }

        private void KillPublisher(string procname)
        {
            Process[] processes = Process.GetProcessesByName(procname.Substring(0, procname.Length - 4));

            if (processes.Length > 0)
            {
                try
                {

                    processes[0].Kill();
                }
                catch (Win32Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public string GetWritePath()
        {
            return writePath;
        }

        private bool IsValidPath(string path)
        {
            //Regex driveCheck = new Regex(@"^[a-zA-Z]:\\$");
            //if (!driveCheck.IsMatch(path.Substring(0, 3))) return false;
            //string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
            //strTheseAreInvalidFileNameChars += @":/?*" + "\"";
            //Regex containsABadCharacter = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
            //if (containsABadCharacter.IsMatch(path.Substring(3, path.Length - 3)))
            if(File.Exists(path))
                return true;

           
            return false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                textBox1.Clear();
                string file = openFileDialog1.FileName;
                try
                {
                    string directoryPath = Path.GetDirectoryName(file);
                    textBox1.Text = file;
                    textBox1.Enabled = false;
                    
                }
                catch (IOException)
                {
                }
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog2.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                textBox2.Clear();
                string file = openFileDialog2.FileName;
                try
                {
                    string directoryPath = Path.GetDirectoryName(file);
                    textBox2.Text = file;
                    textBox2.Enabled = false;

                }
                catch (IOException)
                {
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        
    }
}
