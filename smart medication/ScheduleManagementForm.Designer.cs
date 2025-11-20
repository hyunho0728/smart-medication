namespace smart_medication
{
    partial class ScheduleManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMed = new System.Windows.Forms.Label();
            this.cboMeds = new System.Windows.Forms.ComboBox();
            this.groupBoxTime = new System.Windows.Forms.GroupBox();
            this.chkDinner = new System.Windows.Forms.CheckBox();
            this.chkLunch = new System.Windows.Forms.CheckBox();
            this.chkMorning = new System.Windows.Forms.CheckBox();
            this.lblOneTimeDosage = new System.Windows.Forms.Label();
            this.numOneTimeDosage = new System.Windows.Forms.NumericUpDown();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvSchedules = new System.Windows.Forms.DataGridView();
            this.groupBoxTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOneTimeDosage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(140, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "복용 시간 관리";
            // 
            // lblMed
            // 
            this.lblMed.AutoSize = true;
            this.lblMed.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblMed.Location = new System.Drawing.Point(25, 60);
            this.lblMed.Name = "lblMed";
            this.lblMed.Size = new System.Drawing.Size(70, 19);
            this.lblMed.TabIndex = 1;
            this.lblMed.Text = "약품 선택:";
            // 
            // cboMeds
            // 
            this.cboMeds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMeds.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.cboMeds.FormattingEnabled = true;
            this.cboMeds.Location = new System.Drawing.Point(110, 57);
            this.cboMeds.Name = "cboMeds";
            this.cboMeds.Size = new System.Drawing.Size(160, 25);
            this.cboMeds.TabIndex = 2;
            // 
            // groupBoxTime (시간 선택 그룹)
            // 
            this.groupBoxTime.Controls.Add(this.chkDinner);
            this.groupBoxTime.Controls.Add(this.chkLunch);
            this.groupBoxTime.Controls.Add(this.chkMorning);
            this.groupBoxTime.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.groupBoxTime.Location = new System.Drawing.Point(25, 100);
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.Size = new System.Drawing.Size(245, 70);
            this.groupBoxTime.TabIndex = 3;
            this.groupBoxTime.TabStop = false;
            this.groupBoxTime.Text = "복용 빈도 (체크하세요)";
            // 
            // chkDinner
            // 
            this.chkDinner.AutoSize = true;
            this.chkDinner.Checked = true;
            this.chkDinner.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDinner.Location = new System.Drawing.Point(160, 30);
            this.chkDinner.Name = "chkDinner";
            this.chkDinner.Size = new System.Drawing.Size(56, 23);
            this.chkDinner.TabIndex = 2;
            this.chkDinner.Text = "저녁";
            this.chkDinner.UseVisualStyleBackColor = true;
            // 
            // chkLunch
            // 
            this.chkLunch.AutoSize = true;
            this.chkLunch.Checked = true;
            this.chkLunch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLunch.Location = new System.Drawing.Point(85, 30);
            this.chkLunch.Name = "chkLunch";
            this.chkLunch.Size = new System.Drawing.Size(56, 23);
            this.chkLunch.TabIndex = 1;
            this.chkLunch.Text = "점심";
            this.chkLunch.UseVisualStyleBackColor = true;
            // 
            // chkMorning
            // 
            this.chkMorning.AutoSize = true;
            this.chkMorning.Checked = true;
            this.chkMorning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMorning.Location = new System.Drawing.Point(15, 30);
            this.chkMorning.Name = "chkMorning";
            this.chkMorning.Size = new System.Drawing.Size(56, 23);
            this.chkMorning.TabIndex = 0;
            this.chkMorning.Text = "아침";
            this.chkMorning.UseVisualStyleBackColor = true;
            // 
            // lblOneTimeDosage
            // 
            this.lblOneTimeDosage.AutoSize = true;
            this.lblOneTimeDosage.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblOneTimeDosage.Location = new System.Drawing.Point(25, 190);
            this.lblOneTimeDosage.Name = "lblOneTimeDosage";
            this.lblOneTimeDosage.Size = new System.Drawing.Size(84, 19);
            this.lblOneTimeDosage.TabIndex = 4;
            this.lblOneTimeDosage.Text = "1회 복용량:";
            // 
            // numOneTimeDosage
            // 
            this.numOneTimeDosage.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.numOneTimeDosage.Location = new System.Drawing.Point(115, 188);
            this.numOneTimeDosage.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numOneTimeDosage.Name = "numOneTimeDosage";
            this.numOneTimeDosage.Size = new System.Drawing.Size(155, 25);
            this.numOneTimeDosage.TabIndex = 5;
            this.numOneTimeDosage.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(290, 57);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 156);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "일괄 등록";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.IndianRed;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(390, 57);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 156);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "선택 삭제";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // dgvSchedules
            // 
            this.dgvSchedules.AllowUserToAddRows = false;
            this.dgvSchedules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedules.BackgroundColor = System.Drawing.Color.White;
            this.dgvSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedules.Location = new System.Drawing.Point(25, 230);
            this.dgvSchedules.Name = "dgvSchedules";
            this.dgvSchedules.ReadOnly = true;
            this.dgvSchedules.RowTemplate.Height = 23;
            this.dgvSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedules.Size = new System.Drawing.Size(455, 210);
            this.dgvSchedules.TabIndex = 8;
            // 
            // ScheduleManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(510, 461);
            this.Controls.Add(this.dgvSchedules);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.numOneTimeDosage);
            this.Controls.Add(this.lblOneTimeDosage);
            this.Controls.Add(this.groupBoxTime);
            this.Controls.Add(this.cboMeds);
            this.Controls.Add(this.lblMed);
            this.Controls.Add(this.lblTitle);
            this.Name = "ScheduleManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "복용 시간 관리";
            this.groupBoxTime.ResumeLayout(false);
            this.groupBoxTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOneTimeDosage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMed;
        private System.Windows.Forms.ComboBox cboMeds;
        private System.Windows.Forms.GroupBox groupBoxTime;
        private System.Windows.Forms.CheckBox chkDinner;
        private System.Windows.Forms.CheckBox chkLunch;
        private System.Windows.Forms.CheckBox chkMorning;
        private System.Windows.Forms.Label lblOneTimeDosage;
        private System.Windows.Forms.NumericUpDown numOneTimeDosage;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvSchedules;
    }
}