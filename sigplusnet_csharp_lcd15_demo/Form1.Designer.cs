namespace sigplusnet_csharp_lcd15_demo
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmdStart = new System.Windows.Forms.Button();
            this.sigPlusNET1 = new Topaz.SigPlusNET();
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(344, 104);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(75, 23);
            this.cmdStart.TabIndex = 0;
            this.cmdStart.Text = "Start Demo";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // sigPlusNET1
            // 
            this.sigPlusNET1.BackColor = System.Drawing.Color.White;
            this.sigPlusNET1.ForeColor = System.Drawing.Color.Black;
            this.sigPlusNET1.Location = new System.Drawing.Point(12, 12);
            this.sigPlusNET1.Name = "sigPlusNET1";
            this.sigPlusNET1.Size = new System.Drawing.Size(326, 114);
            this.sigPlusNET1.TabIndex = 1;
            this.sigPlusNET1.Text = "sigPlusNET1";
            this.sigPlusNET1.PenDown += new System.EventHandler(this.sigPlusNET1_PenDown);
            this.sigPlusNET1.PenUp += new System.EventHandler(this.sigPlusNET1_PenUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 138);
            this.Controls.Add(this.sigPlusNET1);
            this.Controls.Add(this.cmdStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "sigPlusNET C# LCD 1x5 Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private Topaz.SigPlusNET sigPlusNET1;
    }
}

