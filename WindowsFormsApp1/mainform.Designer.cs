namespace WindowsFormsApp1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.FE = new System.Windows.Forms.Button();
            this.CP = new System.Windows.Forms.Button();
            this.RC = new System.Windows.Forms.Button();
            this.FC = new System.Windows.Forms.Button();
            this.ST = new System.Windows.Forms.Button();
            this.ARL = new System.Windows.Forms.Button();
            this.AFL = new System.Windows.Forms.Button();
            this.AFR = new System.Windows.Forms.Button();
            this.ARR = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.DBStatus = new System.Windows.Forms.Label();
            this.GotoManual = new System.Windows.Forms.Button();
            this.dataver = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FE
            // 
            this.FE.Font = new System.Drawing.Font("Consolas", 24F);
            this.FE.Location = new System.Drawing.Point(194, 63);
            this.FE.Name = "FE";
            this.FE.Size = new System.Drawing.Size(178, 120);
            this.FE.TabIndex = 2;
            this.FE.Text = "FE1";
            this.FE.UseVisualStyleBackColor = true;
            this.FE.Click += new System.EventHandler(this.FE_Click);
            // 
            // CP
            // 
            this.CP.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CP.Location = new System.Drawing.Point(10, 63);
            this.CP.Name = "CP";
            this.CP.Size = new System.Drawing.Size(178, 120);
            this.CP.TabIndex = 2;
            this.CP.Text = "CP1";
            this.CP.UseVisualStyleBackColor = true;
            this.CP.Click += new System.EventHandler(this.CP_Click);
            // 
            // RC
            // 
            this.RC.Font = new System.Drawing.Font("Consolas", 24F);
            this.RC.Location = new System.Drawing.Point(378, 63);
            this.RC.Name = "RC";
            this.RC.Size = new System.Drawing.Size(178, 120);
            this.RC.TabIndex = 2;
            this.RC.Text = "RC1";
            this.RC.UseVisualStyleBackColor = true;
            this.RC.Click += new System.EventHandler(this.RC_Click);
            // 
            // FC
            // 
            this.FC.Font = new System.Drawing.Font("Consolas", 24F);
            this.FC.Location = new System.Drawing.Point(10, 189);
            this.FC.Name = "FC";
            this.FC.Size = new System.Drawing.Size(178, 120);
            this.FC.TabIndex = 2;
            this.FC.Text = "FC1";
            this.FC.UseVisualStyleBackColor = true;
            this.FC.Click += new System.EventHandler(this.FC_Click);
            // 
            // ST
            // 
            this.ST.Font = new System.Drawing.Font("Consolas", 24F);
            this.ST.Location = new System.Drawing.Point(10, 315);
            this.ST.Name = "ST";
            this.ST.Size = new System.Drawing.Size(178, 120);
            this.ST.TabIndex = 2;
            this.ST.Text = "ST3";
            this.ST.UseVisualStyleBackColor = true;
            this.ST.Click += new System.EventHandler(this.ST_Click);
            // 
            // ARL
            // 
            this.ARL.Font = new System.Drawing.Font("Consolas", 24F);
            this.ARL.Location = new System.Drawing.Point(194, 315);
            this.ARL.Name = "ARL";
            this.ARL.Size = new System.Drawing.Size(178, 120);
            this.ARL.TabIndex = 2;
            this.ARL.Text = "ARL";
            this.ARL.UseVisualStyleBackColor = true;
            this.ARL.Click += new System.EventHandler(this.ARL_Click);
            // 
            // AFL
            // 
            this.AFL.Font = new System.Drawing.Font("Consolas", 24F);
            this.AFL.Location = new System.Drawing.Point(194, 189);
            this.AFL.Name = "AFL";
            this.AFL.Size = new System.Drawing.Size(178, 120);
            this.AFL.TabIndex = 2;
            this.AFL.Text = "AFL";
            this.AFL.UseVisualStyleBackColor = true;
            this.AFL.Click += new System.EventHandler(this.AFL_Click);
            // 
            // AFR
            // 
            this.AFR.Font = new System.Drawing.Font("Consolas", 24F);
            this.AFR.Location = new System.Drawing.Point(378, 189);
            this.AFR.Name = "AFR";
            this.AFR.Size = new System.Drawing.Size(178, 120);
            this.AFR.TabIndex = 2;
            this.AFR.Text = "AFR";
            this.AFR.UseVisualStyleBackColor = true;
            this.AFR.Click += new System.EventHandler(this.AFR_Click);
            // 
            // ARR
            // 
            this.ARR.Font = new System.Drawing.Font("Consolas", 24F);
            this.ARR.Location = new System.Drawing.Point(379, 315);
            this.ARR.Name = "ARR";
            this.ARR.Size = new System.Drawing.Size(178, 120);
            this.ARR.TabIndex = 2;
            this.ARR.Text = "ARR";
            this.ARR.UseVisualStyleBackColor = true;
            this.ARR.Click += new System.EventHandler(this.ARR_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Exit.Location = new System.Drawing.Point(462, 8);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(94, 49);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "종료";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // DBStatus
            // 
            this.DBStatus.AutoSize = true;
            this.DBStatus.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBStatus.Location = new System.Drawing.Point(12, 8);
            this.DBStatus.Name = "DBStatus";
            this.DBStatus.Padding = new System.Windows.Forms.Padding(5);
            this.DBStatus.Size = new System.Drawing.Size(73, 29);
            this.DBStatus.TabIndex = 6;
            this.DBStatus.Text = "label1";
            // 
            // GotoManual
            // 
            this.GotoManual.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GotoManual.Location = new System.Drawing.Point(293, 8);
            this.GotoManual.Name = "GotoManual";
            this.GotoManual.Size = new System.Drawing.Size(163, 49);
            this.GotoManual.TabIndex = 7;
            this.GotoManual.Text = "IP주소 수동입력";
            this.GotoManual.UseVisualStyleBackColor = true;
            this.GotoManual.Click += new System.EventHandler(this.GotoManual_Click);
            // 
            // dataver
            // 
            this.dataver.AutoSize = true;
            this.dataver.Location = new System.Drawing.Point(11, 45);
            this.dataver.Name = "dataver";
            this.dataver.Size = new System.Drawing.Size(38, 12);
            this.dataver.TabIndex = 8;
            this.dataver.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(567, 445);
            this.Controls.Add(this.dataver);
            this.Controls.Add(this.GotoManual);
            this.Controls.Add(this.DBStatus);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.ST);
            this.Controls.Add(this.RC);
            this.Controls.Add(this.CP);
            this.Controls.Add(this.AFR);
            this.Controls.Add(this.AFL);
            this.Controls.Add(this.ARR);
            this.Controls.Add(this.ARL);
            this.Controls.Add(this.FC);
            this.Controls.Add(this.FE);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "아산공장 PPC IP 변경 프로그램";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FE;
        private System.Windows.Forms.Button CP;
        private System.Windows.Forms.Button RC;
        private System.Windows.Forms.Button FC;
        private System.Windows.Forms.Button ST;
        private System.Windows.Forms.Button ARL;
        private System.Windows.Forms.Button AFL;
        private System.Windows.Forms.Button AFR;
        private System.Windows.Forms.Button ARR;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Label DBStatus;
        private System.Windows.Forms.Button GotoManual;
        private System.Windows.Forms.Label dataver;
    }
}

