
namespace Scrabble
{
    partial class MainMenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            this.btNewGame = new System.Windows.Forms.Button();
            this.btLoadGame = new System.Windows.Forms.Button();
            this.btStats = new System.Windows.Forms.Button();
            this.btNewWords = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btNewGame
            // 
            this.btNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNewGame.Location = new System.Drawing.Point(37, 103);
            this.btNewGame.Name = "btNewGame";
            this.btNewGame.Size = new System.Drawing.Size(318, 58);
            this.btNewGame.TabIndex = 1;
            this.btNewGame.Text = "New Game";
            this.btNewGame.UseVisualStyleBackColor = true;
            this.btNewGame.Click += new System.EventHandler(this.btNewGame_Click);
            // 
            // btLoadGame
            // 
            this.btLoadGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLoadGame.Location = new System.Drawing.Point(37, 167);
            this.btLoadGame.Name = "btLoadGame";
            this.btLoadGame.Size = new System.Drawing.Size(318, 58);
            this.btLoadGame.TabIndex = 2;
            this.btLoadGame.Text = "Load Game";
            this.btLoadGame.UseVisualStyleBackColor = true;
            this.btLoadGame.Click += new System.EventHandler(this.btLoadGame_Click);
            // 
            // btStats
            // 
            this.btStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStats.Location = new System.Drawing.Point(37, 231);
            this.btStats.Name = "btStats";
            this.btStats.Size = new System.Drawing.Size(318, 58);
            this.btStats.TabIndex = 3;
            this.btStats.Text = "Statistics";
            this.btStats.UseVisualStyleBackColor = true;
            this.btStats.Click += new System.EventHandler(this.btStats_Click);
            // 
            // btNewWords
            // 
            this.btNewWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNewWords.Location = new System.Drawing.Point(37, 295);
            this.btNewWords.Name = "btNewWords";
            this.btNewWords.Size = new System.Drawing.Size(318, 58);
            this.btNewWords.TabIndex = 4;
            this.btNewWords.Text = "Add New Words";
            this.btNewWords.UseVisualStyleBackColor = true;
            this.btNewWords.Click += new System.EventHandler(this.btNewWords_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(387, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(374, 380);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(384, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "How to play";
            // 
            // btExit
            // 
            this.btExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExit.Location = new System.Drawing.Point(37, 363);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(318, 58);
            this.btExit.TabIndex = 8;
            this.btExit.Text = "Exit Game";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Scrabble.Properties.Resources.Scrabble_logo_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(37, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btNewWords);
            this.Controls.Add(this.btStats);
            this.Controls.Add(this.btLoadGame);
            this.Controls.Add(this.btNewGame);
            this.Name = "MainMenuForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btNewGame;
        private System.Windows.Forms.Button btLoadGame;
        private System.Windows.Forms.Button btStats;
        private System.Windows.Forms.Button btNewWords;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btExit;
    }
}