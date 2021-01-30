namespace GBImageConvertGUI
{
    partial class FormMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.PanelNav = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnCollisionMaps = new System.Windows.Forms.Button();
            this.btnBGMaps = new System.Windows.Forms.Button();
            this.btnMetaSprites = new System.Windows.Forms.Button();
            this.btnSpriteSheets = new System.Windows.Forms.Button();
            this.btnMapConverter = new System.Windows.Forms.Button();
            this.btnImgConverter = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel1.Controls.Add(this.PanelNav);
            this.panel1.Controls.Add(this.btnSettings);
            this.panel1.Controls.Add(this.btnCollisionMaps);
            this.panel1.Controls.Add(this.btnBGMaps);
            this.panel1.Controls.Add(this.btnMetaSprites);
            this.panel1.Controls.Add(this.btnSpriteSheets);
            this.panel1.Controls.Add(this.btnMapConverter);
            this.panel1.Controls.Add(this.btnImgConverter);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 673);
            this.panel1.TabIndex = 0;
            // 
            // PanelNav
            // 
            this.PanelNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.PanelNav.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.PanelNav.Location = new System.Drawing.Point(0, 193);
            this.PanelNav.Name = "PanelNav";
            this.PanelNav.Size = new System.Drawing.Size(3, 100);
            this.PanelNav.TabIndex = 2;
            // 
            // btnSettings
            // 
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSettings.Location = new System.Drawing.Point(0, 612);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(200, 45);
            this.btnSettings.TabIndex = 1;
            this.btnSettings.Text = "SETTINGS";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnCollisionMaps
            // 
            this.btnCollisionMaps.FlatAppearance.BorderSize = 0;
            this.btnCollisionMaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCollisionMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollisionMaps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnCollisionMaps.Location = new System.Drawing.Point(0, 467);
            this.btnCollisionMaps.Name = "btnCollisionMaps";
            this.btnCollisionMaps.Size = new System.Drawing.Size(200, 45);
            this.btnCollisionMaps.TabIndex = 1;
            this.btnCollisionMaps.Text = "COLLISION MAPS";
            this.btnCollisionMaps.UseVisualStyleBackColor = true;
            this.btnCollisionMaps.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnBGMaps
            // 
            this.btnBGMaps.FlatAppearance.BorderSize = 0;
            this.btnBGMaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBGMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBGMaps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnBGMaps.Location = new System.Drawing.Point(0, 416);
            this.btnBGMaps.Name = "btnBGMaps";
            this.btnBGMaps.Size = new System.Drawing.Size(200, 45);
            this.btnBGMaps.TabIndex = 1;
            this.btnBGMaps.Text = "BG MAPS";
            this.btnBGMaps.UseVisualStyleBackColor = true;
            this.btnBGMaps.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnMetaSprites
            // 
            this.btnMetaSprites.FlatAppearance.BorderSize = 0;
            this.btnMetaSprites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMetaSprites.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMetaSprites.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnMetaSprites.Location = new System.Drawing.Point(0, 365);
            this.btnMetaSprites.Name = "btnMetaSprites";
            this.btnMetaSprites.Size = new System.Drawing.Size(200, 45);
            this.btnMetaSprites.TabIndex = 1;
            this.btnMetaSprites.Text = "META SPRITES";
            this.btnMetaSprites.UseVisualStyleBackColor = true;
            this.btnMetaSprites.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSpriteSheets
            // 
            this.btnSpriteSheets.FlatAppearance.BorderSize = 0;
            this.btnSpriteSheets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpriteSheets.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpriteSheets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSpriteSheets.Location = new System.Drawing.Point(0, 314);
            this.btnSpriteSheets.Name = "btnSpriteSheets";
            this.btnSpriteSheets.Size = new System.Drawing.Size(200, 45);
            this.btnSpriteSheets.TabIndex = 1;
            this.btnSpriteSheets.Text = "SPRITE SHEETS";
            this.btnSpriteSheets.UseVisualStyleBackColor = true;
            this.btnSpriteSheets.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMapConverter
            // 
            this.btnMapConverter.FlatAppearance.BorderSize = 0;
            this.btnMapConverter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMapConverter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMapConverter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnMapConverter.Location = new System.Drawing.Point(0, 180);
            this.btnMapConverter.Name = "btnMapConverter";
            this.btnMapConverter.Size = new System.Drawing.Size(200, 45);
            this.btnMapConverter.TabIndex = 1;
            this.btnMapConverter.Text = "MAP CONVERTER";
            this.btnMapConverter.UseVisualStyleBackColor = true;
            this.btnMapConverter.Click += new System.EventHandler(this.btnMapConverter_Click);
            // 
            // btnImgConverter
            // 
            this.btnImgConverter.FlatAppearance.BorderSize = 0;
            this.btnImgConverter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImgConverter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImgConverter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnImgConverter.Location = new System.Drawing.Point(0, 129);
            this.btnImgConverter.Name = "btnImgConverter";
            this.btnImgConverter.Size = new System.Drawing.Size(200, 45);
            this.btnImgConverter.TabIndex = 1;
            this.btnImgConverter.Text = "IMG CONVERTER";
            this.btnImgConverter.UseVisualStyleBackColor = true;
            this.btnImgConverter.Click += new System.EventHandler(this.btnImgConverter_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 123);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(156)))), ((int)(((byte)(149)))));
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "GBImageConverter";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GBImageConvertGUI.Properties.Resources.gameboy_icon_8;
            this.pictureBox1.Location = new System.Drawing.Point(55, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 61);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(200, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1062, 673);
            this.pnlMain.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCollisionMaps;
        private System.Windows.Forms.Button btnBGMaps;
        private System.Windows.Forms.Button btnMetaSprites;
        private System.Windows.Forms.Button btnSpriteSheets;
        private System.Windows.Forms.Button btnImgConverter;
        private System.Windows.Forms.Panel PanelNav;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnMapConverter;
    }
}

