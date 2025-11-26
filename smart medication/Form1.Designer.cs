namespace smart_medication
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblTodayMedicineCount = new System.Windows.Forms.Label();
            this.lblTimeInfo = new System.Windows.Forms.Label();
            this.lblWelcom = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.buttonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnMedicineRegi = new System.Windows.Forms.Button();
            this.btnScheduleMan = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.mainContentWrapper = new System.Windows.Forms.Panel();
            this.borderPanel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.dgvMedicineList = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelActionButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTakeMorning = new System.Windows.Forms.Button();
            this.btnTakeLunch = new System.Windows.Forms.Button();
            this.btnTakeDinner = new System.Windows.Forms.Button();
            this.btnTakeAll = new System.Windows.Forms.Button();
            this.lblMedicationSummary = new System.Windows.Forms.Label();
            this.lblTodayMedicationTitle = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.topPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.buttonLayout.SuspendLayout();
            this.mainContentWrapper.SuspendLayout();
            this.borderPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicineList)).BeginInit();
            this.panelActionButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(222)))), ((int)(((byte)(237)))));
            this.topPanel.Controls.Add(this.lblTodayMedicineCount);
            this.topPanel.Controls.Add(this.lblTimeInfo);
            this.topPanel.Controls.Add(this.lblWelcom);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(800, 80);
            this.topPanel.TabIndex = 0;
            // 
            // lblTodayMedicineCount
            // 
            this.lblTodayMedicineCount.AutoSize = true;
            this.lblTodayMedicineCount.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTodayMedicineCount.Location = new System.Drawing.Point(676, 44);
            this.lblTodayMedicineCount.Name = "lblTodayMedicineCount";
            this.lblTodayMedicineCount.Size = new System.Drawing.Size(121, 17);
            this.lblTodayMedicineCount.TabIndex = 2;
            this.lblTodayMedicineCount.Text = "오늘 복용할 약 0개";
            // 
            // lblTimeInfo
            // 
            this.lblTimeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeInfo.AutoSize = true;
            this.lblTimeInfo.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblTimeInfo.Location = new System.Drawing.Point(660, 25);
            this.lblTimeInfo.Name = "lblTimeInfo";
            this.lblTimeInfo.Size = new System.Drawing.Size(137, 19);
            this.lblTimeInfo.TabIndex = 1;
            this.lblTimeInfo.Text = "현재 시간 : 00:00:00";
            this.lblTimeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWelcom
            // 
            this.lblWelcom.AutoSize = true;
            this.lblWelcom.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.lblWelcom.Location = new System.Drawing.Point(30, 25);
            this.lblWelcom.Name = "lblWelcom";
            this.lblWelcom.Size = new System.Drawing.Size(174, 25);
            this.lblWelcom.TabIndex = 0;
            this.lblWelcom.Text = "안녕하세요 user님!";
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.White;
            this.bottomPanel.Controls.Add(this.buttonLayout);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 500);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(30, 10, 30, 20);
            this.bottomPanel.Size = new System.Drawing.Size(800, 100);
            this.bottomPanel.TabIndex = 1;
            // 
            // buttonLayout
            // 
            this.buttonLayout.ColumnCount = 3;
            this.buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.buttonLayout.Controls.Add(this.btnMedicineRegi, 0, 0);
            this.buttonLayout.Controls.Add(this.btnScheduleMan, 1, 0);
            this.buttonLayout.Controls.Add(this.btnLogout, 2, 0);
            this.buttonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLayout.Location = new System.Drawing.Point(30, 10);
            this.buttonLayout.Name = "buttonLayout";
            this.buttonLayout.RowCount = 1;
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonLayout.Size = new System.Drawing.Size(740, 70);
            this.buttonLayout.TabIndex = 0;
            // 
            // btnMedicineRegi
            // 
            this.btnMedicineRegi.BackColor = System.Drawing.Color.SteelBlue;
            this.btnMedicineRegi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMedicineRegi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMedicineRegi.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMedicineRegi.ForeColor = System.Drawing.Color.White;
            this.btnMedicineRegi.Location = new System.Drawing.Point(5, 5);
            this.btnMedicineRegi.Margin = new System.Windows.Forms.Padding(5);
            this.btnMedicineRegi.Name = "btnMedicineRegi";
            this.btnMedicineRegi.Size = new System.Drawing.Size(236, 60);
            this.btnMedicineRegi.TabIndex = 0;
            this.btnMedicineRegi.Text = "약품 관리";
            this.btnMedicineRegi.UseVisualStyleBackColor = false;
            this.btnMedicineRegi.Click += new System.EventHandler(this.btnMedicineRegi_Click);
            // 
            // btnScheduleMan
            // 
            this.btnScheduleMan.BackColor = System.Drawing.Color.SteelBlue;
            this.btnScheduleMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScheduleMan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScheduleMan.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnScheduleMan.ForeColor = System.Drawing.Color.White;
            this.btnScheduleMan.Location = new System.Drawing.Point(251, 5);
            this.btnScheduleMan.Margin = new System.Windows.Forms.Padding(5);
            this.btnScheduleMan.Name = "btnScheduleMan";
            this.btnScheduleMan.Size = new System.Drawing.Size(236, 60);
            this.btnScheduleMan.TabIndex = 2;
            this.btnScheduleMan.Text = "복용 시간 관리";
            this.btnScheduleMan.UseVisualStyleBackColor = false;
            this.btnScheduleMan.Click += new System.EventHandler(this.btnScheduleMan_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.IndianRed;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(497, 5);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(238, 60);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "로그아웃";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // mainContentWrapper
            // 
            this.mainContentWrapper.Controls.Add(this.borderPanel);
            this.mainContentWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentWrapper.Location = new System.Drawing.Point(0, 80);
            this.mainContentWrapper.Name = "mainContentWrapper";
            this.mainContentWrapper.Padding = new System.Windows.Forms.Padding(40);
            this.mainContentWrapper.Size = new System.Drawing.Size(800, 420);
            this.mainContentWrapper.TabIndex = 2;
            // 
            // borderPanel
            // 
            this.borderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(180)))), ((int)(((byte)(150)))));
            this.borderPanel.Controls.Add(this.contentPanel);
            this.borderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel.Location = new System.Drawing.Point(40, 40);
            this.borderPanel.Name = "borderPanel";
            this.borderPanel.Padding = new System.Windows.Forms.Padding(5);
            this.borderPanel.Size = new System.Drawing.Size(720, 340);
            this.borderPanel.TabIndex = 0;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Controls.Add(this.dgvMedicineList);
            this.contentPanel.Controls.Add(this.panelActionButtons);
            this.contentPanel.Controls.Add(this.lblMedicationSummary);
            this.contentPanel.Controls.Add(this.lblTodayMedicationTitle);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(5, 5);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.Size = new System.Drawing.Size(710, 330);
            this.contentPanel.TabIndex = 0;
            // 
            // dgvMedicineList
            // 
            this.dgvMedicineList.AllowUserToAddRows = false;
            this.dgvMedicineList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedicineList.BackgroundColor = System.Drawing.Color.White;
            this.dgvMedicineList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMedicineList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(230)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMedicineList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMedicineList.ColumnHeadersHeight = 40;
            this.dgvMedicineList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colName,
            this.colCheck,
            this.colNote,
            this.colRemain});
            this.dgvMedicineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMedicineList.EnableHeadersVisualStyles = false;
            this.dgvMedicineList.Location = new System.Drawing.Point(20, 123);
            this.dgvMedicineList.Name = "dgvMedicineList";
            this.dgvMedicineList.ReadOnly = true;
            this.dgvMedicineList.RowHeadersVisible = false;
            this.dgvMedicineList.RowTemplate.Height = 35;
            this.dgvMedicineList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicineList.Size = new System.Drawing.Size(670, 147);
            this.dgvMedicineList.TabIndex = 2;
            // 
            // colTime
            // 
            this.colTime.HeaderText = "시간";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.HeaderText = "약품";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "복용 여부";
            this.colCheck.Name = "colCheck";
            this.colCheck.ReadOnly = true;
            // 
            // colNote
            // 
            this.colNote.HeaderText = "비고";
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            // 
            // colRemain
            // 
            this.colRemain.HeaderText = "남은 약";
            this.colRemain.Name = "colRemain";
            this.colRemain.ReadOnly = true;
            // 
            // panelActionButtons
            // 
            this.panelActionButtons.AutoSize = true;
            this.panelActionButtons.Controls.Add(this.btnTakeMorning);
            this.panelActionButtons.Controls.Add(this.btnTakeLunch);
            this.panelActionButtons.Controls.Add(this.btnTakeDinner);
            this.panelActionButtons.Controls.Add(this.btnTakeAll);
            this.panelActionButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActionButtons.Location = new System.Drawing.Point(20, 72);
            this.panelActionButtons.Name = "panelActionButtons";
            this.panelActionButtons.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelActionButtons.Size = new System.Drawing.Size(670, 51);
            this.panelActionButtons.TabIndex = 3;
            // 
            // btnTakeMorning
            // 
            this.btnTakeMorning.BackColor = System.Drawing.Color.Orange;
            this.btnTakeMorning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeMorning.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btnTakeMorning.ForeColor = System.Drawing.Color.White;
            this.btnTakeMorning.Location = new System.Drawing.Point(3, 3);
            this.btnTakeMorning.Name = "btnTakeMorning";
            this.btnTakeMorning.Size = new System.Drawing.Size(100, 35);
            this.btnTakeMorning.TabIndex = 0;
            this.btnTakeMorning.Text = "아침약 복용";
            this.btnTakeMorning.UseVisualStyleBackColor = false;
            this.btnTakeMorning.Click += new System.EventHandler(this.btnTakeMorning_Click);
            // 
            // btnTakeLunch
            // 
            this.btnTakeLunch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnTakeLunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeLunch.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btnTakeLunch.ForeColor = System.Drawing.Color.White;
            this.btnTakeLunch.Location = new System.Drawing.Point(109, 3);
            this.btnTakeLunch.Name = "btnTakeLunch";
            this.btnTakeLunch.Size = new System.Drawing.Size(100, 35);
            this.btnTakeLunch.TabIndex = 1;
            this.btnTakeLunch.Text = "점심약 복용";
            this.btnTakeLunch.UseVisualStyleBackColor = false;
            this.btnTakeLunch.Click += new System.EventHandler(this.btnTakeLunch_Click);
            // 
            // btnTakeDinner
            // 
            this.btnTakeDinner.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnTakeDinner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeDinner.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btnTakeDinner.ForeColor = System.Drawing.Color.White;
            this.btnTakeDinner.Location = new System.Drawing.Point(215, 3);
            this.btnTakeDinner.Name = "btnTakeDinner";
            this.btnTakeDinner.Size = new System.Drawing.Size(100, 35);
            this.btnTakeDinner.TabIndex = 2;
            this.btnTakeDinner.Text = "저녁약 복용";
            this.btnTakeDinner.UseVisualStyleBackColor = false;
            this.btnTakeDinner.Click += new System.EventHandler(this.btnTakeDinner_Click);
            // 
            // btnTakeAll
            // 
            this.btnTakeAll.BackColor = System.Drawing.Color.DimGray;
            this.btnTakeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeAll.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btnTakeAll.ForeColor = System.Drawing.Color.White;
            this.btnTakeAll.Location = new System.Drawing.Point(321, 3);
            this.btnTakeAll.Name = "btnTakeAll";
            this.btnTakeAll.Size = new System.Drawing.Size(130, 35);
            this.btnTakeAll.TabIndex = 3;
            this.btnTakeAll.Text = "모든 약 일괄 복용";
            this.btnTakeAll.UseVisualStyleBackColor = false;
            this.btnTakeAll.Click += new System.EventHandler(this.btnTakeAll_Click);
            // 
            // lblMedicationSummary
            // 
            this.lblMedicationSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMedicationSummary.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.lblMedicationSummary.Location = new System.Drawing.Point(20, 270);
            this.lblMedicationSummary.Name = "lblMedicationSummary";
            this.lblMedicationSummary.Size = new System.Drawing.Size(670, 40);
            this.lblMedicationSummary.TabIndex = 1;
            this.lblMedicationSummary.Text = "오늘 복용해야하는 약이 0가지 있어요";
            this.lblMedicationSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTodayMedicationTitle
            // 
            this.lblTodayMedicationTitle.AutoSize = true;
            this.lblTodayMedicationTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTodayMedicationTitle.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.lblTodayMedicationTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTodayMedicationTitle.Name = "lblTodayMedicationTitle";
            this.lblTodayMedicationTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblTodayMedicationTitle.Size = new System.Drawing.Size(174, 52);
            this.lblTodayMedicationTitle.TabIndex = 0;
            this.lblTodayMedicationTitle.Text = "오늘의 복용 약";
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.mainContentWrapper);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Pill Manager";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.buttonLayout.ResumeLayout(false);
            this.mainContentWrapper.ResumeLayout(false);
            this.borderPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicineList)).EndInit();
            this.panelActionButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblWelcom;
        private System.Windows.Forms.Label lblTimeInfo;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.TableLayoutPanel buttonLayout;
        private System.Windows.Forms.Button btnMedicineRegi;
        private System.Windows.Forms.Button btnLogout; // [변경] 변수명 변경
        private System.Windows.Forms.Button btnScheduleMan;
        private System.Windows.Forms.Panel mainContentWrapper;
        private System.Windows.Forms.Panel borderPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Label lblTodayMedicationTitle;
        private System.Windows.Forms.Label lblMedicationSummary;
        private System.Windows.Forms.DataGridView dgvMedicineList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemain;
        private System.Windows.Forms.Label lblTodayMedicineCount;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.FlowLayoutPanel panelActionButtons;
        private System.Windows.Forms.Button btnTakeMorning;
        private System.Windows.Forms.Button btnTakeLunch;
        private System.Windows.Forms.Button btnTakeDinner;
        private System.Windows.Forms.Button btnTakeAll;
    }
}