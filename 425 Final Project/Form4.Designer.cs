
namespace _425_Final_Project
{
    partial class VoteForm
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
            this.voteBtn = new System.Windows.Forms.Button();
            this.questionLbl = new System.Windows.Forms.Label();
            this.vote1Txt = new System.Windows.Forms.TextBox();
            this.voteList = new System.Windows.Forms.ListBox();
            this.vote2Txt = new System.Windows.Forms.TextBox();
            this.vote3Txt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.alertLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // voteBtn
            // 
            this.voteBtn.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voteBtn.Location = new System.Drawing.Point(187, 346);
            this.voteBtn.Name = "voteBtn";
            this.voteBtn.Size = new System.Drawing.Size(163, 31);
            this.voteBtn.TabIndex = 22;
            this.voteBtn.Text = "Send in final votes.";
            this.voteBtn.UseVisualStyleBackColor = true;
            this.voteBtn.Click += new System.EventHandler(this.voteBtn_Click);
            // 
            // questionLbl
            // 
            this.questionLbl.AutoSize = true;
            this.questionLbl.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLbl.Location = new System.Drawing.Point(12, 22);
            this.questionLbl.Name = "questionLbl";
            this.questionLbl.Size = new System.Drawing.Size(513, 21);
            this.questionLbl.TabIndex = 21;
            this.questionLbl.Text = "Final vote is needed. Host interviews with the following students:";
            // 
            // vote1Txt
            // 
            this.vote1Txt.Location = new System.Drawing.Point(63, 305);
            this.vote1Txt.Name = "vote1Txt";
            this.vote1Txt.Size = new System.Drawing.Size(100, 20);
            this.vote1Txt.TabIndex = 23;
            // 
            // voteList
            // 
            this.voteList.FormattingEnabled = true;
            this.voteList.Location = new System.Drawing.Point(63, 46);
            this.voteList.Name = "voteList";
            this.voteList.Size = new System.Drawing.Size(408, 56);
            this.voteList.TabIndex = 24;
            // 
            // vote2Txt
            // 
            this.vote2Txt.Location = new System.Drawing.Point(217, 306);
            this.vote2Txt.Name = "vote2Txt";
            this.vote2Txt.Size = new System.Drawing.Size(100, 20);
            this.vote2Txt.TabIndex = 25;
            // 
            // vote3Txt
            // 
            this.vote3Txt.Location = new System.Drawing.Point(371, 306);
            this.vote3Txt.Name = "vote3Txt";
            this.vote3Txt.Size = new System.Drawing.Size(100, 20);
            this.vote3Txt.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(113, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 21);
            this.label1.TabIndex = 27;
            this.label1.Text = "...and submit their student IDs here:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 21);
            this.label2.TabIndex = 28;
            this.label2.Text = "Vote #1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(234, 282);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 21);
            this.label3.TabIndex = 29;
            this.label3.Text = "Vote #2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(387, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 21);
            this.label4.TabIndex = 30;
            this.label4.Text = "Vote #3:";
            // 
            // alertLbl
            // 
            this.alertLbl.AutoSize = true;
            this.alertLbl.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alertLbl.Location = new System.Drawing.Point(262, 168);
            this.alertLbl.Name = "alertLbl";
            this.alertLbl.Size = new System.Drawing.Size(20, 21);
            this.alertLbl.TabIndex = 31;
            this.alertLbl.Text = "  ";
            // 
            // VoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(533, 389);
            this.Controls.Add(this.alertLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vote3Txt);
            this.Controls.Add(this.vote2Txt);
            this.Controls.Add(this.voteList);
            this.Controls.Add(this.vote1Txt);
            this.Controls.Add(this.voteBtn);
            this.Controls.Add(this.questionLbl);
            this.Name = "VoteForm";
            this.Text = "Voting Needed";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button voteBtn;
        private System.Windows.Forms.Label questionLbl;
        private System.Windows.Forms.TextBox vote1Txt;
        private System.Windows.Forms.ListBox voteList;
        private System.Windows.Forms.TextBox vote2Txt;
        private System.Windows.Forms.TextBox vote3Txt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label alertLbl;
    }
}