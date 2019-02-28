namespace SMS_Shkolla_Manager
{
    partial class NgarkoSkedare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NgarkoSkedare));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnDil = new DevComponents.DotNetBar.ButtonX();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.progressBar = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.dsTelefon = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsTelefon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.Controls.Add(this.btnDil);
            this.panelEx1.Controls.Add(this.btnOk);
            this.panelEx1.Controls.Add(this.progressBar);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(574, 125);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "panelEx1";
            this.panelEx1.ThemeAware = true;
            // 
            // btnDil
            // 
            this.btnDil.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDil.Location = new System.Drawing.Point(475, 78);
            this.btnDil.Name = "btnDil";
            this.btnDil.Size = new System.Drawing.Size(77, 23);
            this.btnDil.TabIndex = 11;
            this.btnDil.Text = "Mbyll";
            this.btnDil.Click += new System.EventHandler(this.btnDil_Click);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(23, 78);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(23, 35);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(529, 23);
            this.progressBar.TabIndex = 0;
            this.progressBar.Text = "progressBarX1";
            // 
            // dsTelefon
            // 
            this.dsTelefon.DataSetName = "NewDataSet";
            this.dsTelefon.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn10});
            this.dataTable1.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "AMZA",
                        "CIKLI"}, true)});
            this.dataTable1.PrimaryKey = new System.Data.DataColumn[] {
        this.dataColumn4,
        this.dataColumn5};
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn4
            // 
            this.dataColumn4.AllowDBNull = false;
            this.dataColumn4.ColumnName = "AMZA";
            // 
            // dataColumn5
            // 
            this.dataColumn5.AllowDBNull = false;
            this.dataColumn5.ColumnName = "CIKLI";
            this.dataColumn5.DataType = typeof(bool);
            // 
            // dataColumn10
            // 
            this.dataColumn10.ColumnName = "TELEFON";
            // 
            // NgarkoSkedare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 125);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(580, 157);
            this.MinimumSize = new System.Drawing.Size(580, 157);
            this.Name = "NgarkoSkedare";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMS Shkolla Manager - Ngarkimi i Skedarëve";
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsTelefon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBar;
        private DevComponents.DotNetBar.ButtonX btnDil;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn10;
        public System.Data.DataSet dsTelefon;
    }
}