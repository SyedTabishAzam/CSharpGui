using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;



namespace CSharpGui
{
    public partial class Form3 : Form
    {
        

        
        
        enum RET_CODE { OK=1, FILE_NOT_FOUND=2, BAD_STRUCTURE };
        private RET_CODE success = RET_CODE.FILE_NOT_FOUND;
        
        public Form3()
        {
            InitializeComponent();
            Shown += Form3_Shown;
            FormClosing += Form3_FormClosing;
          
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing )
            {
                Application.Exit();
            }
           
            
            
            
        }

        

        private void Form3_Load(object sender, EventArgs e)
        {
           
            
        }

        

        

        async Task PutTaskDelay(int mili)
        {

            await Task.Delay(mili);
            

        }

        private async void Form3_Shown(object sender, EventArgs e)
        {
            //await PutTaskDelay(); 
            StartSubscriber();
            progressBar1.PerformStep();
            await PutTaskDelay(500);

            progressBar1.PerformStep();
            CheckSubscriberStarted();
            await PutTaskDelay(2000);
            

            label1.Text = Constants.Status.LISTENING;
            progressBar1.PerformStep();
            await PutTaskDelay(1000);

            int counter = 0;
            while((success!= RET_CODE.OK) && (counter<20))
            {

                
                RecurseCheck();
                await PutTaskDelay(1000);
                Console.WriteLine(counter);
                counter++;
            }

            if (success == RET_CODE.OK)
            {
                label1.Text = Constants.Status.LOAD_COMPLETE;
                progressBar1.PerformStep();
                await PutTaskDelay(1000);

                this.FormClosing -= Form3_FormClosing;
                ClearEverything();
                this.Close();

            }
            else if (success == RET_CODE.FILE_NOT_FOUND)
            {
                label1.Text = Constants.Status.LOAD_FAILED;

                
            }
            else if (success == RET_CODE.BAD_STRUCTURE)
            {
                label1.Text = Constants.Status.VALIDATION_FAILED;
            }

            if(!(success==RET_CODE.OK))
            {
                progressBar1.BackColor = Color.Red;
                progressBar1.ForeColor = Color.Red;
                progressBar1.MarqueeAnimationSpeed = 0;
                progressBar1.Value = 0;

                DialogResult result = MessageBox.Show(Constants.Error.LOAD_FAILED, "Failed", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ClearEverything();
                    Form3_Shown(this, null);
                }
                if (result == DialogResult.No)
                {
                    ClearEverything();
                    Application.Exit();
                }
            }

            
           
            
           
            
        }

        private void RecurseCheck()
        {
            
            success = CheckForFile();
             

            
        }

        private void ClearEverything()
        {
            KillSubscriber();
        }

        private async void KillSubscriber()
        {
            Process[] processes = Process.GetProcessesByName(Constants.Filename.SUBSCRIBER_FILE.Substring(0, Constants.Filename.SUBSCRIBER_FILE.Length - 4));
            
            if (processes.Length > 0)
            {
                try
                {

                processes[0].Kill();
                }
                catch(Win32Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            await PutTaskDelay(1000);
        }
        private  void StartSubscriber()
        {
            KillSubscriber();

            
                

            Process proc = null;
            //Do not hardcode
            //string batDir = string.Format(@"D:\");
            proc = new Process();
            //proc.StartInfo.WorkingDirectory = batDir;
            proc.StartInfo.FileName = Constants.Filename.ENV_VAR_FILE;
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
            proc.WaitForExit();

            label1.Text = Constants.Status.SUB_START;

            string cParams = "";
            string pathior = Constants.Filename.RTPS_FILE;
            string pathexe = Constants.Filename.SUBSCRIBER_FILE;
            if (pathior.Contains("ini"))
                cParams = "-DCPSConfigFile " + pathior;
            Process p = new Process();

            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.RedirectStandardOutput = false;
           // p.StartInfo.WorkingDirectory = batDir;
            p.StartInfo.UseShellExecute = true;  // ShellExecute = true not allowed when output is redirected..
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = pathexe;
            p.StartInfo.Arguments = cParams;

            try
            {

                p.Start();

               
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                DialogResult error = MessageBox.Show(Constants.Error.MISSING_SUBSCRIBER, "Error loading subscriber",
    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if(error==DialogResult.OK)
                {
                    Application.Exit();
                }
            }

            
            
        }

        private void CheckSubscriberStarted()
        {
            label1.Text = Constants.Status.CHECKING_PROCESS;
            Process[] processes = Process.GetProcessesByName(Constants.Filename.SUBSCRIBER_FILE.Substring(0, Constants.Filename.SUBSCRIBER_FILE.Length - 4));
            
            if (processes.Length > 0)
            {

            }
            else
            {
                DialogResult error = MessageBox.Show(Constants.Error.FAILED_SUBSCRIBER_START,"Subscriber failed",
    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (error == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }
        public bool GetSuccessLoad()
        {
            return (success == RET_CODE.OK);
        }

        private RET_CODE CheckForFile()
        {



            if (!(File.Exists(Constants.Filename.SCNERIO_DATA_FILE)))
            {
                return RET_CODE.FILE_NOT_FOUND;
            }
            if(!(ValidateData()))
            {
                return RET_CODE.BAD_STRUCTURE;
            }
            return RET_CODE.OK;
            
        }

        private bool ValidateData()
        {
            bool varnamecheck = true;
            bool vartypecheck = true;
            var entitylist = (from line in File.ReadLines(Constants.Filename.SCNERIO_DATA_FILE)
                              let values = line.Split('>')
                              select Tuple.Create(values[0],
                                                  values[1])).ToList();

            foreach (var entity in entitylist)
            {
                




                List<string> variabellist = (entity.Item2).Split(' ').ToList();
    
                foreach (var variables in variabellist)
                {
                    List<string> splitstring = variables.Split(',').ToList();

                    string varname = splitstring[0].Substring(1);
                    string vartype = splitstring[1].TrimEnd(')');

                    varnamecheck = varnamecheck && CheckVarName(varname);
                    vartypecheck = vartypecheck && CheckVarType(vartype);

                    if ((!varnamecheck) || (!vartypecheck))
                        return false;

                }
            }
            return true;
        }

        private bool CheckVarType(string type)
        {
            
            foreach (var v in Constants.AllowedVariables.GetList())
            {
                if (type.Equals(v))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckVarName(string name)
        {
            if(name.Equals(""))
            {
                return false;
            }
            return true;
        }


    }

    
}
