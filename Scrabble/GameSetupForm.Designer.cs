
namespace Scrabble
{
    partial class GameSetupForm
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
            this.btReturn = new System.Windows.Forms.Button();
            this.rb4P = new System.Windows.Forms.RadioButton();
            this.rb2P = new System.Windows.Forms.RadioButton();
            this.rb3P = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btConfirm = new System.Windows.Forms.Button();
            this.rbOriginalDB = new System.Windows.Forms.RadioButton();
            this.rbCustomDB = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.p1TextBox = new System.Windows.Forms.TextBox();
            this.p3TextBox = new System.Windows.Forms.TextBox();
            this.p4TextBox = new System.Windows.Forms.TextBox();
            this.p2TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbNoLimit = new System.Windows.Forms.RadioButton();
            this.rb60Seconds = new System.Windows.Forms.RadioButton();
            this.rb120Seconds = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDownCustom = new System.Windows.Forms.NumericUpDown();
            this.rbCustomTime = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCustom)).BeginInit();
            this.SuspendLayout();
            // 
            // btReturn
            // 
            this.btReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btReturn.Location = new System.Drawing.Point(418, 374);
            this.btReturn.Name = "btReturn";
            this.btReturn.Size = new System.Drawing.Size(169, 64);
            this.btReturn.TabIndex = 0;
            this.btReturn.Text = "Cancel";
            this.btReturn.UseVisualStyleBackColor = true;
            this.btReturn.Click += new System.EventHandler(this.BtReturn_Click);
            // 
            // rb4P
            // 
            this.rb4P.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rb4P.AutoSize = true;
            this.rb4P.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb4P.Location = new System.Drawing.Point(3, 101);
            this.rb4P.Name = "rb4P";
            this.rb4P.Size = new System.Drawing.Size(183, 43);
            this.rb4P.TabIndex = 1;
            this.rb4P.Text = "4 Players";
            this.rb4P.UseVisualStyleBackColor = true;
            this.rb4P.CheckedChanged += new System.EventHandler(this.NoOfPlayersCheck_CheckedChanged);
            // 
            // rb2P
            // 
            this.rb2P.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rb2P.AutoSize = true;
            this.rb2P.Checked = true;
            this.rb2P.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb2P.Location = new System.Drawing.Point(3, 3);
            this.rb2P.Name = "rb2P";
            this.rb2P.Size = new System.Drawing.Size(183, 43);
            this.rb2P.TabIndex = 2;
            this.rb2P.TabStop = true;
            this.rb2P.Text = "2 Players";
            this.rb2P.UseVisualStyleBackColor = true;
            this.rb2P.CheckedChanged += new System.EventHandler(this.NoOfPlayersCheck_CheckedChanged);
            // 
            // rb3P
            // 
            this.rb3P.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rb3P.AutoSize = true;
            this.rb3P.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb3P.Location = new System.Drawing.Point(3, 52);
            this.rb3P.Name = "rb3P";
            this.rb3P.Size = new System.Drawing.Size(183, 43);
            this.rb3P.TabIndex = 3;
            this.rb3P.Text = "3 Players";
            this.rb3P.UseVisualStyleBackColor = true;
            this.rb3P.CheckedChanged += new System.EventHandler(this.NoOfPlayersCheck_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select number of players:";
            // 
            // btConfirm
            // 
            this.btConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConfirm.Location = new System.Drawing.Point(606, 374);
            this.btConfirm.Name = "btConfirm";
            this.btConfirm.Size = new System.Drawing.Size(169, 64);
            this.btConfirm.TabIndex = 7;
            this.btConfirm.Text = "Confirm";
            this.btConfirm.UseVisualStyleBackColor = true;
            this.btConfirm.Click += new System.EventHandler(this.BtConfirm_Click);
            // 
            // rbOriginalDB
            // 
            this.rbOriginalDB.AutoSize = true;
            this.rbOriginalDB.Checked = true;
            this.rbOriginalDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOriginalDB.Location = new System.Drawing.Point(36, 334);
            this.rbOriginalDB.Name = "rbOriginalDB";
            this.rbOriginalDB.Size = new System.Drawing.Size(314, 43);
            this.rbOriginalDB.TabIndex = 8;
            this.rbOriginalDB.TabStop = true;
            this.rbOriginalDB.Text = "Original dictionary";
            this.rbOriginalDB.UseVisualStyleBackColor = true;
            // 
            // rbCustomDB
            // 
            this.rbCustomDB.AutoSize = true;
            this.rbCustomDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCustomDB.Location = new System.Drawing.Point(36, 381);
            this.rbCustomDB.Name = "rbCustomDB";
            this.rbCustomDB.Size = new System.Drawing.Size(315, 43);
            this.rbCustomDB.TabIndex = 9;
            this.rbCustomDB.Text = "Custom dictionary";
            this.rbCustomDB.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "Select dictionary type:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.05058F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.94942F));
            this.tableLayoutPanel1.Controls.Add(this.p1TextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.p3TextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.p4TextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.p2TextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.rb2P, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rb3P, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rb4P, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(36, 113);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00006F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00007F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00007F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99982F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(461, 196);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // p1TextBox
            // 
            this.p1TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.p1TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p1TextBox.Location = new System.Drawing.Point(192, 5);
            this.p1TextBox.Name = "p1TextBox";
            this.p1TextBox.Size = new System.Drawing.Size(266, 38);
            this.p1TextBox.TabIndex = 13;
            // 
            // p3TextBox
            // 
            this.p3TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.p3TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p3TextBox.Location = new System.Drawing.Point(192, 103);
            this.p3TextBox.Name = "p3TextBox";
            this.p3TextBox.Size = new System.Drawing.Size(266, 38);
            this.p3TextBox.TabIndex = 15;
            this.p3TextBox.Visible = false;
            // 
            // p4TextBox
            // 
            this.p4TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.p4TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p4TextBox.Location = new System.Drawing.Point(192, 152);
            this.p4TextBox.Name = "p4TextBox";
            this.p4TextBox.Size = new System.Drawing.Size(266, 38);
            this.p4TextBox.TabIndex = 16;
            this.p4TextBox.Visible = false;
            // 
            // p2TextBox
            // 
            this.p2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.p2TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p2TextBox.Location = new System.Drawing.Point(192, 54);
            this.p2TextBox.Name = "p2TextBox";
            this.p2TextBox.Size = new System.Drawing.Size(266, 38);
            this.p2TextBox.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(542, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "Select time per turn:";
            // 
            // rbNoLimit
            // 
            this.rbNoLimit.AutoSize = true;
            this.rbNoLimit.Checked = true;
            this.rbNoLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNoLimit.Location = new System.Drawing.Point(6, 3);
            this.rbNoLimit.Name = "rbNoLimit";
            this.rbNoLimit.Size = new System.Drawing.Size(228, 43);
            this.rbNoLimit.TabIndex = 0;
            this.rbNoLimit.TabStop = true;
            this.rbNoLimit.Text = "No time limit";
            this.rbNoLimit.UseVisualStyleBackColor = true;
            this.rbNoLimit.CheckedChanged += new System.EventHandler(this.TimeLimitCheck_CheckedChanged);
            // 
            // rb60Seconds
            // 
            this.rb60Seconds.AutoSize = true;
            this.rb60Seconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb60Seconds.Location = new System.Drawing.Point(6, 46);
            this.rb60Seconds.Name = "rb60Seconds";
            this.rb60Seconds.Size = new System.Drawing.Size(212, 43);
            this.rb60Seconds.TabIndex = 1;
            this.rb60Seconds.Text = "60 seconds";
            this.rb60Seconds.UseVisualStyleBackColor = true;
            this.rb60Seconds.CheckedChanged += new System.EventHandler(this.TimeLimitCheck_CheckedChanged);
            // 
            // rb120Seconds
            // 
            this.rb120Seconds.AutoSize = true;
            this.rb120Seconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb120Seconds.Location = new System.Drawing.Point(6, 95);
            this.rb120Seconds.Name = "rb120Seconds";
            this.rb120Seconds.Size = new System.Drawing.Size(231, 43);
            this.rb120Seconds.TabIndex = 2;
            this.rb120Seconds.Text = "120 seconds";
            this.rb120Seconds.UseVisualStyleBackColor = true;
            this.rb120Seconds.CheckedChanged += new System.EventHandler(this.TimeLimitCheck_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Scrabble.Properties.Resources.Scrabble_logo_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(37, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericUpDownCustom);
            this.panel1.Controls.Add(this.rbCustomTime);
            this.panel1.Controls.Add(this.rbNoLimit);
            this.panel1.Controls.Add(this.rb120Seconds);
            this.panel1.Controls.Add(this.rb60Seconds);
            this.panel1.Location = new System.Drawing.Point(540, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 193);
            this.panel1.TabIndex = 14;
            // 
            // numericUpDownCustom
            // 
            this.numericUpDownCustom.Enabled = false;
            this.numericUpDownCustom.Location = new System.Drawing.Point(168, 165);
            this.numericUpDownCustom.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownCustom.Name = "numericUpDownCustom";
            this.numericUpDownCustom.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownCustom.TabIndex = 4;
            // 
            // rbCustomTime
            // 
            this.rbCustomTime.AutoSize = true;
            this.rbCustomTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCustomTime.Location = new System.Drawing.Point(6, 144);
            this.rbCustomTime.Name = "rbCustomTime";
            this.rbCustomTime.Size = new System.Drawing.Size(156, 43);
            this.rbCustomTime.TabIndex = 3;
            this.rbCustomTime.Text = "Custom";
            this.rbCustomTime.UseVisualStyleBackColor = true;
            // 
            // GameSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbCustomDB);
            this.Controls.Add(this.rbOriginalDB);
            this.Controls.Add(this.btConfirm);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btReturn);
            this.Name = "GameSetupForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCustom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btReturn;
        private System.Windows.Forms.RadioButton rb4P;
        private System.Windows.Forms.RadioButton rb2P;
        private System.Windows.Forms.RadioButton rb3P;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btConfirm;
        private System.Windows.Forms.RadioButton rbOriginalDB;
        private System.Windows.Forms.RadioButton rbCustomDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox p1TextBox;
        private System.Windows.Forms.TextBox p3TextBox;
        private System.Windows.Forms.TextBox p4TextBox;
        private System.Windows.Forms.TextBox p2TextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb120Seconds;
        private System.Windows.Forms.RadioButton rb60Seconds;
        private System.Windows.Forms.RadioButton rbNoLimit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbCustomTime;
        private System.Windows.Forms.NumericUpDown numericUpDownCustom;
    }
}