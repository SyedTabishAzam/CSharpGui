using System;

using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace CSharpGui
{
    

    public partial class Form1 : Form
    {
        private string writePath = "";
        private Timer timer1;
        private List<Entity> entlist = new List<Entity>();
        
        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox2.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox3.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox4.SelectedIndexChanged += new EventHandler(comboBox4_SelectedIndexChanged);
            comboBox5.SelectedIndexChanged += new EventHandler(comboBox4_SelectedIndexChanged);
            comboBox6.SelectedIndexChanged += new EventHandler(comboBox4_SelectedIndexChanged);

            button2.Click += button1_Click;
            button3.Click += button1_Click;
            FormClosing += Form1_FormClosing;
             // Shows Form2
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog(this);

            if(f3.GetSuccessLoad())
            {

                Form2 f2 = new Form2();
                f2.ShowDialog(this);

                CheckFile();
                ChangeEnabled(true);
                PopulateComboBox();

            
            
            

                bool handleControl = f2.getSuccessStart();

                ChangeEnabled(true);
                writePath = f2.GetWritePath();
                DeleteFile();

            }
        }

        private void DeleteFile()
        {
            if (File.Exists(Constants.Filename.SCNERIO_DATA_FILE))
            {
                File.Delete(Constants.Filename.SCNERIO_DATA_FILE);
            }
        }
        private void PopulateComboBox()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach(var entity in entlist)
            {
                comboBox1.Items.Add(entity.name);
                comboBox2.Items.Add(entity.name);
                comboBox3.Items.Add(entity.name);
               
            }

            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            string comboBoxName = (sender as ComboBox).Name;

            string selectedValue = "";
            string selectedVariable = "";
            if (comboBoxName.Equals("comboBox4"))
            {
                selectedValue = comboBox1.SelectedItem as string;
                selectedVariable = comboBox4.SelectedItem as string;
               
            }
            else if (comboBoxName.Equals("comboBox5"))
            {

                selectedValue = comboBox2.SelectedItem as string;
                selectedVariable = comboBox5.SelectedItem as string;
        
                
            }
            else if (comboBoxName.Equals("comboBox6"))
            {
                selectedValue = comboBox3.SelectedItem as string;
                selectedVariable = comboBox6.SelectedItem as string;
                
            }

            foreach (var entity in entlist)
            {

                
                if (selectedValue == entity.name)
                {
                    List<string> varnames = entity.GetVarnames();
                    List<string> vartype = entity.GetVartypes();
                    int count = 0;
                    foreach (var name in varnames)
                    {
                        if(selectedVariable.Equals(name))
                        {
                            if (comboBoxName.Equals("comboBox4"))
                                textBox1.Text = vartype.ElementAt(count);
                            else if (comboBoxName.Equals("comboBox5"))
                                textBox2.Text = vartype.ElementAt(count);
                            else if (comboBoxName.Equals("comboBox6"))
                                textBox3.Text = vartype.ElementAt(count);
                            
                        }
                        count++;
                    }
                }
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comboBoxName = (sender as ComboBox).Name;
            string selectedValue = "";
            if(comboBoxName.Equals("comboBox1"))
            {
                selectedValue = comboBox1.SelectedItem as string;
                comboBox4.Items.Clear();
            }
            else if(comboBoxName.Equals("comboBox2"))
            {
                selectedValue = comboBox2.SelectedItem as string;
                comboBox5.Items.Clear();
            }
            else if(comboBoxName.Equals("comboBox3"))
            {
                selectedValue = comboBox3.SelectedItem as string;
                comboBox6.Items.Clear();
            }

            
            
            foreach (var entity in entlist)
            {
                
                
                if(selectedValue==entity.name)
                {
                    List<string> varnames = entity.GetVarnames();
                    foreach(var name in varnames)
                    {
                       
                        if (comboBoxName.Equals("comboBox1"))
                            comboBox4.Items.Add(name);
                        else if (comboBoxName.Equals("comboBox2"))
                            comboBox5.Items.Add(name);
                        else if (comboBoxName.Equals("comboBox3"))
                            comboBox6.Items.Add(name);
                        
                    }
                }
            }
            
            
        }
        
        private void CheckFile()
        {
            var entitylist = (from line in File.ReadLines(Constants.Filename.SCNERIO_DATA_FILE)
                           let values = line.Split('>')
                           select Tuple.Create(values[0],
                                               values[1])).ToList();
            
            foreach (var entity in entitylist)
            {
                Entity newEntity = new Entity();
                newEntity.name = entity.Item1;
                
   
                

                List<string> variabellist = (entity.Item2).Split(' ').ToList();
                List<string> varnameslst = new List<string>();
                List<string> vartypelst = new List<string>();
                foreach (var variables in variabellist)
                {
                    List<string> splitstring = variables.Split(',').ToList();

                    string varname = splitstring[0].Substring(1);
                    string vartype = splitstring[1].TrimEnd(')');

                    varnameslst.Add(varname);
                    vartypelst.Add(vartype);
                    
                 

                }

                newEntity.SetVarnames(varnameslst);
                newEntity.SetVartypes(vartypelst);

                entlist.Add(newEntity);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string buttonName = (sender as Button).Name;
            string entityName = "";
            string variable = "";
            string type = "";
            string value = "";

            Console.WriteLine(buttonName);
            if(buttonName.Equals("button1"))
            {

                entityName = comboBox1.Text;
                variable = comboBox4.Text;
                type = textBox1.Text;
                value = textBox4.Text;
            }
            else if (buttonName.Equals("button2"))
            {

                entityName = comboBox2.Text;
                variable = comboBox5.Text;
                type = textBox2.Text;
                value = textBox5.Text;
            }
            else if (buttonName.Equals("button3"))
            {

                entityName = comboBox3.Text;
                variable = comboBox6.Text;
                type = textBox3.Text;
                value = textBox6.Text;
            }




            DateTime src = DateTime.Now;
            string time = src.Hour.ToString() + ":" + src.Minute.ToString() + ":" + src.Second.ToString();

            if(ValidateCommand(entityName,variable,type,value))
            {

                richTextBox1.AppendText(DateTime.Now + " " + "Writing Command" + Environment.NewLine);

                string[] command = new string[] { time, entityName,variable, value };


                //this will write wherever publisher is present
                string path = writePath + "\\" + Constants.Filename.COMMAND_FILE;

                SendCommand(command);
                    

                
                if (buttonName.Equals("button1"))
                {
                    button1.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox4.Enabled = false;
                    textBox4.Enabled = false;
                    
                    Acknowledge("1");
                }
                else if (buttonName.Equals("button2"))
                {

                    button2.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox5.Enabled = false;
                    textBox5.Enabled = false;
                    Acknowledge("2");
                }
                else if (buttonName.Equals("button3"))
                {

                    button3.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox6.Enabled = false;
                    textBox6.Enabled = false;
                    Acknowledge("3");
                }
                
             
            }
            else
            {
                richTextBox1.AppendText(GetFormatedTime() + " Error in row : " + buttonName + Environment.NewLine); 
            }
        }

        private void SendCommand(string[] command)
        {
            string path = writePath + "\\" + Constants.Filename.COMMAND_FILE;
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("NEW_COMMANDS");
                foreach (string s in command)
                {
                    sw.Write(s);
                    sw.Write(" ");
                }
                sw.WriteLine("");
            }
        }

        private string GetFormatedTime()
        {
            string dt = "(" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ")";
            return dt;
        }

        private bool ValidateCommand(string entityname,string variable,string type,string value)
        {
            bool success = true;
            if(entityname.Equals(""))
            {
                success = success && false;
                richTextBox1.AppendText(GetFormatedTime() + Constants.Error.ENITITY_EMPTY + Environment.NewLine); 
            }
            if (variable.Equals(""))
            {
                success = success && false;
                richTextBox1.AppendText(GetFormatedTime() + Constants.Error.VARIABLE_EMPTY + Environment.NewLine);
            }
            if (type.Equals(""))
            {
                success = success && false;
                richTextBox1.AppendText(GetFormatedTime() + Constants.Error.UNDEFINED_TYPE + Environment.NewLine);
            }
            if(value.Equals(""))
            {
                success = success && false;
                richTextBox1.AppendText(GetFormatedTime() + Constants.Error.VALUE_EMPTY + Environment.NewLine);
            }

            if(type.Equals(Constants.AllowedVariables.DOUBLE))
            {
                Double j;
                if (Double.TryParse(value,out j))
                {

                }
                else
                {
                    success = success && false;
                    richTextBox1.AppendText(GetFormatedTime() + Constants.Error.TYPE_MISMATCH + Environment.NewLine);
                }
            }

            if (type.Equals(Constants.AllowedVariables.INT))
            {
                int j;
                if (Int32.TryParse(value, out j))
                {

                }
                else
                {
                    success = success && false;
                    richTextBox1.AppendText(GetFormatedTime() + Constants.Error.TYPE_MISMATCH + Environment.NewLine);
                }
            }

            if (type.Equals(Constants.AllowedVariables.BOOL))
            {
                Boolean j;
                if (Boolean.TryParse(value, out j))
                {

                }
                else
                {
                    success = success && false;
                    richTextBox1.AppendText(GetFormatedTime() + Constants.Error.TYPE_MISMATCH + Environment.NewLine);
                }
            }

            if (type.Equals(Constants.AllowedVariables.FLOAT))
            {
                float j;
                if (float.TryParse(value, out j))
                {

                }
                else
                {
                    success = success && false;
                    richTextBox1.AppendText(GetFormatedTime() + Constants.Error.TYPE_MISMATCH + Environment.NewLine);
                }
            }

            return success;


        }

        public void Acknowledge(string box)
        {
            timer1 = new Timer();
            timer1.Tick += (sender, e) => ReadFile(box);
            
            timer1.Interval = 4000; // in miliseconds
            timer1.Start();
            richTextBox1.AppendText(GetFormatedTime() + " Waiting for response from server" + Environment.NewLine);
        }

        public void ReadFile(string box)
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                string path = writePath + "\\" + Constants.Filename.COMMAND_FILE;
                StreamReader sr = new StreamReader(path);

                //Read the first line of text
                line = sr.ReadLine();
                bool clear = false;
                //Continue to read until you reach end of file
                if (line != null)
                {
                    //write the line to console window
                    
                    
                    string[] words = line.Split(' ');
                    
                    if(words[0]=="STATUS")
                    {
                        richTextBox1.AppendText(GetFormatedTime() + " " + line + Environment.NewLine);

                        clear = true;
                        ActivateControl(box);
                        timer1.Stop();
                    }
                    
                    
                    
                    //Read the next line
                    //line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                if(clear)
                {
                    System.IO.File.WriteAllText(path, string.Empty);
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            
            
        }

        public void ActivateControl(string box)
        {
            if (box == "1")
            {
                button1.Enabled = true;
                comboBox1.Enabled = true;
                comboBox4.Enabled = true;
                textBox4.Enabled = true;
                
            }
            if(box=="2")
            {
                button2.Enabled = true;
                comboBox2.Enabled = true;
                comboBox5.Enabled = true;
                textBox5.Enabled = true;
            }
            if(box=="3")
            {
                button3.Enabled = true;
                comboBox3.Enabled = true;
                comboBox6.Enabled = true;
                textBox6.Enabled = true;
            }
            if(box=="21")
            {
                button21.Enabled = true;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearEverything();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
            
        }

        private void ClearEverything()
        {
            KillPublisher();
        }

        private void KillPublisher()
        {

            Process[] processes = Process.GetProcessesByName(Constants.Filename.PUBLISEHR_FILE.Substring(0, Constants.Filename.PUBLISEHR_FILE.Length - 4));
            
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
           
        }

        void ChangeEnabled(bool enabled)
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = enabled;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string[] command = new string[] { "0", "RTC_STOP", "NULL", "NULL"};
            SendCommand(command);
            HandleController("comboBox",false,true);
            HandleController("textBox",false,true);
            button21.Enabled = false;
            button31.Enabled = false;
            
        }

        private void HandleController(string controller,bool enabled, bool clearAllItems)
        {
            List<string> Items = new List<string>();
            var comboBoxes = this.Controls
                  .OfType<ComboBox>()
                  .Where(x => x.Name.StartsWith(controller));
            foreach (var cmbBox in comboBoxes)
            {
                if(clearAllItems)
                    cmbBox.Items.Clear();
                cmbBox.Enabled = enabled;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string[] command = new string[] { "0", "RTC_RESET", "NULL", "NULL" };
            SendCommand(command);

            HandleController("comboBox", false, false);
            HandleController("textBox", false, false);
            button31.Enabled = true;
            button21.Enabled = false;

            Acknowledge("21");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            string[] command = new string[] { "0", "RTC_PLAY", "NULL", "NULL" };
            SendCommand(command);

            HandleController("comboBox", true, false);
            HandleController("textBox", true, false);

            button21.Enabled = true;
            button31.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(Constants.Status.RESTART_APP, "Confirm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] command = new string[] { "0", "RTC_RESTART", "NULL", "NULL" };
                SendCommand(command);
                ClearEverything();
                Application.Restart();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        } 
    }

    public class Entity
    {

        public string name;
        private List<string> varnames;
        private List<string> vartypes;

        public List<string> GetVarnames()
        {
            return varnames;
        }

        public List<string> GetVartypes()
        {
            return vartypes;
        }

        public void SetVarnames(List<string> lst)
        {
            varnames = lst;
        }

        public void SetVartypes(List<string> lst)
        {
            vartypes = lst;
        }
    }

    
}
