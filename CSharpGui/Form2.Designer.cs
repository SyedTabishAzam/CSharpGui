namespace CSharpGui
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Location = new System.Drawing.Point(26, 39);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(418, 124);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configuration";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(337, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "IOR  or INI File";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox2
            // 
            this.textBox2.AutoCompleteCustomSource.AddRange(new string[] {
            "D:\\GUIApplication\\CSharpGui\\CSharpGui\\bin\\Debug\\rtps.ini"});
            this.textBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(86, 78);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(225, 20);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "D:\\GUIApplication\\CSharpGui\\CSharpGui\\bin\\Debug\\rtps.ini";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(337, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Publisher";
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "D:\\GUIApplication\\CSharpGui\\CSharpGui\\bin\\Debug\\CommandPublisher.exe"});
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(86, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(225, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "D:\\GUIApplication\\CSharpGui\\CSharpGui\\bin\\Debug\\CommandPublisher.exe";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(337, 187);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Select";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Executable (*.exe)|*.exe";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "IOR File (*.ior)|*.ior|INI File (*.ini)|*.ini";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 245);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox4);
            this.Name = "Form2";
            this.Text = "Initialize";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}