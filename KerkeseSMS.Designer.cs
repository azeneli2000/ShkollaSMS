namespace SMS_Shkolla_Manager
{
    partial class KerkeseSMS
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnDil = new DevComponents.DotNetBar.ButtonX();
            this.btnDergo = new DevComponents.DotNetBar.ButtonX();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.numSMS = new System.Windows.Forms.NumericUpDown();
            this.txtKoment = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtMarresi = new System.Windows.Forms.TextBox();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Controls.Add(this.btnDil);
            this.panelEx1.Controls.Add(this.btnDergo);
            this.panelEx1.Controls.Add(this.uiGroupBox2);
            this.panelEx1.Controls.Add(this.uiGroupBox1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(520, 467);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.ThemeAware = true;
            // 
            // btnDil
            // 
            this.btnDil.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDil.Location = new System.Drawing.Point(415, 421);
            this.btnDil.Name = "btnDil";
            this.btnDil.Size = new System.Drawing.Size(84, 30);
            this.btnDil.TabIndex = 7;
            this.btnDil.Text = "Dil";
            this.btnDil.Click += new System.EventHandler(this.btnDil_Click);
            // 
            // btnDergo
            // 
            this.btnDergo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDergo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDergo.Location = new System.Drawing.Point(9, 421);
            this.btnDergo.Name = "btnDergo";
            this.btnDergo.Size = new System.Drawing.Size(84, 30);
            this.btnDergo.TabIndex = 6;
            this.btnDergo.Text = "Dërgo";
            this.btnDergo.Click += new System.EventHandler(this.btnDergo_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.numSMS);
            this.uiGroupBox2.Controls.Add(this.txtKoment);
            this.uiGroupBox2.Controls.Add(this.label3);
            this.uiGroupBox2.Controls.Add(this.label4);
            this.uiGroupBox2.Location = new System.Drawing.Point(9, 68);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(490, 338);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // numSMS
            // 
            this.numSMS.Location = new System.Drawing.Point(105, 24);
            this.numSMS.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSMS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSMS.Name = "numSMS";
            this.numSMS.Size = new System.Drawing.Size(120, 20);
            this.numSMS.TabIndex = 4;
            this.numSMS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtKoment
            // 
            this.txtKoment.Location = new System.Drawing.Point(105, 61);
            this.txtKoment.Multiline = true;
            this.txtKoment.Name = "txtKoment";
            this.txtKoment.Size = new System.Drawing.Size(360, 262);
            this.txtKoment.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Koment shtesë";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Nr mesazhesh";
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Për";
            // 
            // txtMarresi
            // 
            this.txtMarresi.BackColor = System.Drawing.Color.AliceBlue;
            this.txtMarresi.Location = new System.Drawing.Point(105, 21);
            this.txtMarresi.Name = "txtMarresi";
            this.txtMarresi.ReadOnly = true;
            this.txtMarresi.Size = new System.Drawing.Size(360, 20);
            this.txtMarresi.TabIndex = 3;
            this.txtMarresi.TabStop = false;
            this.txtMarresi.Text = "info@visioninfosolution.com";
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtMarresi);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Location = new System.Drawing.Point(9, 3);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(490, 59);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // KerkeseSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 467);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KerkeseSMS";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kerkese për blerje SMS";
            this.Load += new System.EventHandler(this.KerkeseSMS_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.TextBox txtKoment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSMS;
        private DevComponents.DotNetBar.ButtonX btnDergo;
        private DevComponents.DotNetBar.ButtonX btnDil;
        private System.Windows.Forms.ErrorProvider error;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.TextBox txtMarresi;
        private System.Windows.Forms.Label label2;
    }
}