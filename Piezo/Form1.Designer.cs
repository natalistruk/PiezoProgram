
namespace Piezo
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rotextBox = new System.Windows.Forms.TextBox();
            this.ctextBox = new System.Windows.Forms.TextBox();
            this.etextBox = new System.Windows.Forms.TextBox();
            this.gtextBox = new System.Windows.Forms.TextBox();
            this.ntextBox = new System.Windows.Forms.TextBox();
            this.ltextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.sigmatextBox = new System.Windows.Forms.TextBox();
            this.DtextBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pGraphControl = new ZedGraph.ZedGraphControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.uGraphControl = new ZedGraph.ZedGraphControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(560, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ro(x)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(721, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "c(x)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(894, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "e(x)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1070, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "g(x)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(560, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "N";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(724, 276);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "L";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // rotextBox
            // 
            this.rotextBox.Location = new System.Drawing.Point(564, 114);
            this.rotextBox.Name = "rotextBox";
            this.rotextBox.Size = new System.Drawing.Size(100, 26);
            this.rotextBox.TabIndex = 6;
            this.rotextBox.Text = "4700";
            // 
            // ctextBox
            // 
            this.ctextBox.Location = new System.Drawing.Point(725, 114);
            this.ctextBox.Name = "ctextBox";
            this.ctextBox.Size = new System.Drawing.Size(100, 26);
            this.ctextBox.TabIndex = 7;
            this.ctextBox.Text = "203000000000";
            // 
            // etextBox
            // 
            this.etextBox.Location = new System.Drawing.Point(898, 114);
            this.etextBox.Name = "etextBox";
            this.etextBox.Size = new System.Drawing.Size(100, 26);
            this.etextBox.TabIndex = 8;
            this.etextBox.Text = "-2,53";
            // 
            // gtextBox
            // 
            this.gtextBox.Location = new System.Drawing.Point(1065, 114);
            this.gtextBox.Name = "gtextBox";
            this.gtextBox.Size = new System.Drawing.Size(100, 26);
            this.gtextBox.TabIndex = 9;
            this.gtextBox.Text = "43,6";
            // 
            // ntextBox
            // 
            this.ntextBox.Location = new System.Drawing.Point(564, 313);
            this.ntextBox.Name = "ntextBox";
            this.ntextBox.Size = new System.Drawing.Size(100, 26);
            this.ntextBox.TabIndex = 10;
            this.ntextBox.Text = "256";
            // 
            // ltextBox
            // 
            this.ltextBox.Location = new System.Drawing.Point(725, 313);
            this.ltextBox.Name = "ltextBox";
            this.ltextBox.Size = new System.Drawing.Size(100, 26);
            this.ltextBox.TabIndex = 11;
            this.ltextBox.Text = "0,01";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(446, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(284, 63);
            this.button1.TabIndex = 12;
            this.button1.Text = "Обчислити";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(560, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "sigma";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(721, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "D";
            // 
            // sigmatextBox
            // 
            this.sigmatextBox.Location = new System.Drawing.Point(564, 214);
            this.sigmatextBox.Name = "sigmatextBox";
            this.sigmatextBox.Size = new System.Drawing.Size(100, 26);
            this.sigmatextBox.TabIndex = 15;
            this.sigmatextBox.Text = "5000000";
            // 
            // DtextBox
            // 
            this.DtextBox.Location = new System.Drawing.Point(725, 214);
            this.DtextBox.Name = "DtextBox";
            this.DtextBox.Size = new System.Drawing.Size(100, 26);
            this.DtextBox.TabIndex = 16;
            this.DtextBox.Text = "0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1239, 657);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rotextBox);
            this.tabPage1.Controls.Add(this.gtextBox);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.DtextBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.etextBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.sigmatextBox);
            this.tabPage1.Controls.Add(this.ntextBox);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.ltextBox);
            this.tabPage1.Controls.Add(this.ctextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1231, 624);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Вихідна система";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1231, 624);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Графіки";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(6, 6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1222, 618);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pGraphControl);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1214, 585);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "p(x)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pGraphControl
            // 
            this.pGraphControl.Location = new System.Drawing.Point(19, 8);
            this.pGraphControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pGraphControl.Name = "pGraphControl";
            this.pGraphControl.ScrollGrace = 0D;
            this.pGraphControl.ScrollMaxX = 0D;
            this.pGraphControl.ScrollMaxY = 0D;
            this.pGraphControl.ScrollMaxY2 = 0D;
            this.pGraphControl.ScrollMinX = 0D;
            this.pGraphControl.ScrollMinY = 0D;
            this.pGraphControl.ScrollMinY2 = 0D;
            this.pGraphControl.Size = new System.Drawing.Size(1165, 565);
            this.pGraphControl.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.uGraphControl);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1214, 585);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "u(x)";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // uGraphControl
            // 
            this.uGraphControl.Location = new System.Drawing.Point(7, 8);
            this.uGraphControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uGraphControl.Name = "uGraphControl";
            this.uGraphControl.ScrollGrace = 0D;
            this.uGraphControl.ScrollMaxX = 0D;
            this.uGraphControl.ScrollMaxY = 0D;
            this.uGraphControl.ScrollMaxY2 = 0D;
            this.uGraphControl.ScrollMinX = 0D;
            this.uGraphControl.ScrollMinY = 0D;
            this.uGraphControl.ScrollMinY2 = 0D;
            this.uGraphControl.Size = new System.Drawing.Size(1165, 565);
            this.uGraphControl.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 654);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox rotextBox;
        private System.Windows.Forms.TextBox ctextBox;
        private System.Windows.Forms.TextBox etextBox;
        private System.Windows.Forms.TextBox gtextBox;
        private System.Windows.Forms.TextBox ntextBox;
        private System.Windows.Forms.TextBox ltextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox sigmatextBox;
        private System.Windows.Forms.TextBox DtextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private ZedGraph.ZedGraphControl pGraphControl;
        private ZedGraph.ZedGraphControl uGraphControl;
    }
}

