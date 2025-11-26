namespace smart_medication
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtRegId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRegPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRegPwdConfirm = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(88, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "회원가입";
            // 
            // label1 (아이디)
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.Location = new System.Drawing.Point(32, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "아이디";
            // 
            // txtRegId
            // 
            this.txtRegId.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtRegId.Location = new System.Drawing.Point(35, 78);
            this.txtRegId.Name = "txtRegId";
            this.txtRegId.Size = new System.Drawing.Size(220, 25);
            this.txtRegId.TabIndex = 2;
            // 
            // label2 (비밀번호)
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label2.Location = new System.Drawing.Point(32, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "비밀번호";
            // 
            // txtRegPwd
            // 
            this.txtRegPwd.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtRegPwd.Location = new System.Drawing.Point(35, 133);
            this.txtRegPwd.Name = "txtRegPwd";
            this.txtRegPwd.PasswordChar = '●';
            this.txtRegPwd.Size = new System.Drawing.Size(220, 25);
            this.txtRegPwd.TabIndex = 4;
            // 
            // label3 (비밀번호 확인)
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label3.Location = new System.Drawing.Point(32, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "비밀번호 확인";
            // 
            // txtRegPwdConfirm
            // 
            this.txtRegPwdConfirm.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtRegPwdConfirm.Location = new System.Drawing.Point(35, 188);
            this.txtRegPwdConfirm.Name = "txtRegPwdConfirm";
            this.txtRegPwdConfirm.PasswordChar = '●';
            this.txtRegPwdConfirm.Size = new System.Drawing.Size(220, 25);
            this.txtRegPwdConfirm.TabIndex = 6;
            // 
            // btnRegister (가입 버튼)
            // 
            this.btnRegister.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(35, 230);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(220, 40);
            this.btnRegister.TabIndex = 7;
            this.btnRegister.Text = "가 입 하 기";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnCancel (취소 버튼)
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.DimGray;
            this.btnCancel.Location = new System.Drawing.Point(35, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(220, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "취 소";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(290, 340);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.txtRegPwdConfirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRegPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRegId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "회원가입";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRegPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRegPwdConfirm;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
    }
}