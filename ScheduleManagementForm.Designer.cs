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
            this.lblCurrentStock = new System.Windows.Forms.Label();
            this.groupBoxTime = new System.Windows.Forms.GroupBox();
            this.dtpDinner = new System.Windows.Forms.DateTimePicker();
            this.dtpLunch = new System.Windows.Forms.DateTimePicker();
            this.dtpMorning = new System.Windows.Forms.DateTimePicker();
            this.lblUnit3 = new System.Windows.Forms.Label();
            this.lblUnit2 = new System.Windows.Forms.Label();
            this.lblUnit1 = new System.Windows.Forms.Label();
            this.numDinner = new System.Windows.Forms.NumericUpDown();
            this.numLunch = new System.Windows.Forms.NumericUpDown();
            this.numMorning = new System.Windows.Forms.NumericUpDown();
            this.chkDinner = new System.Windows.Forms.CheckBox();
            this.chkLunch = new System.Windows.Forms.CheckBox();
            this.chkMorning = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvSchedules = new System.Windows.Forms.DataGridView();
            this.groupBoxTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLunch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMorning)).BeginInit();
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
            // lblCurrentStock
            // 
            this.lblCurrentStock.AutoSize = true;
            this.lblCurrentStock.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblCurrentStock.ForeColor = System.Drawing.Color.SlateGray;
            this.lblCurrentStock.Location = new System.Drawing.Point(110, 85);
            this.lblCurrentStock.Name = "lblCurrentStock";
            this.lblCurrentStock.Size = new System.Drawing.Size(60, 15);
            this.lblCurrentStock.TabIndex = 10;
            this.lblCurrentStock.Text = "현재 재고: -";
            // 
            // groupBoxTime
            // 
            this.groupBoxTime.Controls.Add(this.dtpDinner);
            this.groupBoxTime.Controls.Add(this.dtpLunch);
            this.groupBoxTime.Controls.Add(this.dtpMorning);
            this.groupBoxTime.Controls.Add(this.lblUnit3);
            this.groupBoxTime.Controls.Add(this.lblUnit2);
            this.groupBoxTime.Controls.Add(this.lblUnit1);
            this.groupBoxTime.Controls.Add(this.numDinner);
            this.groupBoxTime.Controls.Add(this.numLunch);
            this.groupBoxTime.Controls.Add(this.numMorning);
            this.groupBoxTime.Controls.Add(this.chkDinner);
            this.groupBoxTime.Controls.Add(this.chkLunch);
            this.groupBoxTime.Controls.Add(this.chkMorning);
            this.groupBoxTime.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.groupBoxTime.Location = new System.Drawing.Point(25, 110);
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.Size = new System.Drawing.Size(320, 140); // 너비를 245 -> 320으로 늘림
            this.groupBoxTime.TabIndex = 3;
            this.groupBoxTime.TabStop = false;
            this.groupBoxTime.Text = "복용 시간 및 개수";
            // 
            // dtpDinner
            // 
            this.dtpDinner.Format = System.Windows.Forms.DateTimePickerFormat.Custom; // [수정] Custom으로 변경
            this.dtpDinner.CustomFormat = "HH:mm"; // [추가] 24시간 형식 (예: 19:00)
            this.dtpDinner.Location = new System.Drawing.Point(80, 100);
            this.dtpDinner.Name = "dtpDinner";
            this.dtpDinner.ShowUpDown = true;
            this.dtpDinner.Size = new System.Drawing.Size(95, 25);
            this.dtpDinner.TabIndex = 11;
            // 
            // dtpLunch
            // 
            this.dtpLunch.Format = System.Windows.Forms.DateTimePickerFormat.Custom; // [수정] Custom으로 변경
            this.dtpLunch.CustomFormat = "HH:mm"; // [추가] 24시간 형식 (예: 13:00)
            this.dtpLunch.Location = new System.Drawing.Point(80, 65);
            this.dtpLunch.Name = "dtpLunch";
            this.dtpLunch.ShowUpDown = true;
            this.dtpLunch.Size = new System.Drawing.Size(95, 25);
            this.dtpLunch.TabIndex = 10;
            // 
            // dtpMorning
            // 
            this.dtpMorning.Format = System.Windows.Forms.DateTimePickerFormat.Custom; // [수정] Custom으로 변경
            this.dtpMorning.CustomFormat = "HH:mm"; // [추가] 24시간 형식 (예: 08:00)
            this.dtpMorning.Location = new System.Drawing.Point(80, 30);
            this.dtpMorning.Name = "dtpMorning";
            this.dtpMorning.ShowUpDown = true;
            this.dtpMorning.Size = new System.Drawing.Size(95, 25);
            this.dtpMorning.TabIndex = 9;
            // 
            // lblUnit3
            // 
            this.lblUnit3.AutoSize = true;
            this.lblUnit3.Location = new System.Drawing.Point(250, 102);
            this.lblUnit3.Name = "lblUnit3";
            this.lblUnit3.Size = new System.Drawing.Size(23, 19);
            this.lblUnit3.TabIndex = 8;
            this.lblUnit3.Text = "정";
            // 
            // lblUnit2
            // 
            this.lblUnit2.AutoSize = true;
            this.lblUnit2.Location = new System.Drawing.Point(250, 67);
            this.lblUnit2.Name = "lblUnit2";
            this.lblUnit2.Size = new System.Drawing.Size(23, 19);
            this.lblUnit2.TabIndex = 7;
            this.lblUnit2.Text = "정";
            // 
            // lblUnit1
            // 
            this.lblUnit1.AutoSize = true;
            this.lblUnit1.Location = new System.Drawing.Point(250, 32);
            this.lblUnit1.Name = "lblUnit1";
            this.lblUnit1.Size = new System.Drawing.Size(23, 19);
            this.lblUnit1.TabIndex = 6;
            this.lblUnit1.Text = "정";
            // 
            // numDinner
            // 
            this.numDinner.Location = new System.Drawing.Point(185, 100);
            this.numDinner.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numDinner.Name = "numDinner";
            this.numDinner.Size = new System.Drawing.Size(60, 25);
            this.numDinner.TabIndex = 5;
            this.numDinner.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numLunch
            // 
            this.numLunch.Location = new System.Drawing.Point(185, 65);
            this.numLunch.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numLunch.Name = "numLunch";
            this.numLunch.Size = new System.Drawing.Size(60, 25);
            this.numLunch.TabIndex = 4;
            this.numLunch.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numMorning
            // 
            this.numMorning.Location = new System.Drawing.Point(185, 30);
            this.numMorning.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMorning.Name = "numMorning";
            this.numMorning.Size = new System.Drawing.Size(60, 25);
            this.numMorning.TabIndex = 3;
            this.numMorning.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // chkDinner
            // 
            this.chkDinner.AutoSize = true;
            this.chkDinner.Checked = true;
            this.chkDinner.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDinner.Location = new System.Drawing.Point(15, 100);
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
            this.chkLunch.Location = new System.Drawing.Point(15, 65);
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
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(360, 57);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 193);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "등록";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Orange;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(435, 57);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 193);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "수정";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.IndianRed;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(510, 57);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 193);
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
            this.dgvSchedules.Location = new System.Drawing.Point(25, 270);
            this.dgvSchedules.Name = "dgvSchedules";
            this.dgvSchedules.ReadOnly = true;
            this.dgvSchedules.RowTemplate.Height = 23;
            this.dgvSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedules.Size = new System.Drawing.Size(555, 170); // 너비 늘림
            this.dgvSchedules.TabIndex = 8;
            this.dgvSchedules.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedules_CellClick);
            // 
            // ScheduleManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(605, 461); // 폼 너비 늘림
            this.Controls.Add(this.dgvSchedules);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblCurrentStock);
            this.Controls.Add(this.groupBoxTime);
            this.Controls.Add(this.cboMeds);
            this.Controls.Add(this.lblMed);
            this.Controls.Add(this.lblTitle);
            this.Name = "ScheduleManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "복용 시간 관리";
            this.groupBoxTime.ResumeLayout(false);
            this.groupBoxTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLunch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMorning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMed;
        private System.Windows.Forms.ComboBox cboMeds;
        private System.Windows.Forms.Label lblCurrentStock;
        private System.Windows.Forms.GroupBox groupBoxTime;
        private System.Windows.Forms.CheckBox chkDinner;
        private System.Windows.Forms.CheckBox chkLunch;
        private System.Windows.Forms.CheckBox chkMorning;
        private System.Windows.Forms.DateTimePicker dtpMorning; // 추가
        private System.Windows.Forms.DateTimePicker dtpLunch;   // 추가
        private System.Windows.Forms.DateTimePicker dtpDinner;  // 추가
        private System.Windows.Forms.NumericUpDown numDinner;
        private System.Windows.Forms.NumericUpDown numLunch;
        private System.Windows.Forms.NumericUpDown numMorning;
        private System.Windows.Forms.Label lblUnit3;
        private System.Windows.Forms.Label lblUnit2;
        private System.Windows.Forms.Label lblUnit1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvSchedules;
    }
}