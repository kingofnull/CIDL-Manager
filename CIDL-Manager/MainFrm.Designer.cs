
namespace CIDL_Manager
{
    partial class MainFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.deviceLogTxt = new System.Windows.Forms.TextBox();
            this.cmdTestTxb = new System.Windows.Forms.TextBox();
            this.callLogGrd = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CallerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CallTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callLogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new CIDL_Manager.DataSet();
            this.systemTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.callLogTableAdapter = new CIDL_Manager.DataSetTableAdapters.CallLogTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.callLogGrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.callLogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // deviceLogTxt
            // 
            this.deviceLogTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deviceLogTxt.BackColor = System.Drawing.Color.Black;
            this.deviceLogTxt.ForeColor = System.Drawing.Color.LimeGreen;
            this.deviceLogTxt.Location = new System.Drawing.Point(0, 246);
            this.deviceLogTxt.Multiline = true;
            this.deviceLogTxt.Name = "deviceLogTxt";
            this.deviceLogTxt.ReadOnly = true;
            this.deviceLogTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.deviceLogTxt.Size = new System.Drawing.Size(764, 154);
            this.deviceLogTxt.TabIndex = 1;
            // 
            // cmdTestTxb
            // 
            this.cmdTestTxb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTestTxb.Location = new System.Drawing.Point(3, 222);
            this.cmdTestTxb.Name = "cmdTestTxb";
            this.cmdTestTxb.Size = new System.Drawing.Size(756, 20);
            this.cmdTestTxb.TabIndex = 2;
            this.cmdTestTxb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmdTestTxb_KeyDown);
            // 
            // callLogGrd
            // 
            this.callLogGrd.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.callLogGrd.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.callLogGrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.callLogGrd.AutoGenerateColumns = false;
            this.callLogGrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.callLogGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.callLogGrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.CallerName,
            this.PhoneNumber,
            this.CallTime});
            this.callLogGrd.DataSource = this.callLogBindingSource;
            this.callLogGrd.Location = new System.Drawing.Point(0, 3);
            this.callLogGrd.Name = "callLogGrd";
            this.callLogGrd.ReadOnly = true;
            this.callLogGrd.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.callLogGrd.RowHeadersVisible = false;
            this.callLogGrd.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.callLogGrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.callLogGrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.callLogGrd.Size = new System.Drawing.Size(764, 214);
            this.callLogGrd.TabIndex = 3;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.FillWeight = 17.70302F;
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // CallerName
            // 
            this.CallerName.DataPropertyName = "CallerName";
            this.CallerName.FillWeight = 216.1779F;
            this.CallerName.HeaderText = "CallerName";
            this.CallerName.Name = "CallerName";
            this.CallerName.ReadOnly = true;
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.DataPropertyName = "PhoneNumber";
            this.PhoneNumber.FillWeight = 81.21828F;
            this.PhoneNumber.HeaderText = "PhoneNumber";
            this.PhoneNumber.Name = "PhoneNumber";
            this.PhoneNumber.ReadOnly = true;
            // 
            // CallTime
            // 
            this.CallTime.DataPropertyName = "CallTime";
            this.CallTime.FillWeight = 84.90083F;
            this.CallTime.HeaderText = "CallTime";
            this.CallTime.Name = "CallTime";
            this.CallTime.ReadOnly = true;
            // 
            // callLogBindingSource
            // 
            this.callLogBindingSource.DataMember = "CallLog";
            this.callLogBindingSource.DataSource = this.dataSet;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // systemTrayIcon
            // 
            this.systemTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTrayIcon.Icon")));
            // 
            // callLogTableAdapter
            // 
            this.callLogTableAdapter.ClearBeforeFill = true;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 400);
            this.Controls.Add(this.callLogGrd);
            this.Controls.Add(this.cmdTestTxb);
            this.Controls.Add(this.deviceLogTxt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrm";
            this.Text = "CID Logger Manager(v.23.10.12)";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.callLogGrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.callLogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox deviceLogTxt;
        private System.Windows.Forms.TextBox cmdTestTxb;
        private System.Windows.Forms.DataGridView callLogGrd;
        public System.Windows.Forms.NotifyIcon systemTrayIcon;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource callLogBindingSource;
        private DataSetTableAdapters.CallLogTableAdapter callLogTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CallerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhoneNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CallTime;
    }
}

