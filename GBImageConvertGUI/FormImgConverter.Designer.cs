namespace GBImageConvertGUI
{
    partial class FormImgConverter
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
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNumTiles = new System.Windows.Forms.Label();
            this.btnExportTiles = new System.Windows.Forms.Button();
            this.btnExportMap = new System.Windows.Forms.Button();
            this.comboExportFormat = new System.Windows.Forms.ComboBox();
            this.checkBox_RemoveDupes = new System.Windows.Forms.CheckBox();
            this.picTiles = new GBImageConvertGUI.PictureBoxWithInterpolationMode();
            this.picPreview = new GBImageConvertGUI.PictureBoxWithInterpolationMode();
            this.picOriginal = new GBImageConvertGUI.PictureBoxWithInterpolationMode();
            this.lblDuplicateTiles = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkboxPrependTileCountByte = new System.Windows.Forms.CheckBox();
            this.checkPrependWidthHeight = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenImage.Location = new System.Drawing.Point(12, 12);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(120, 40);
            this.btnOpenImage.TabIndex = 0;
            this.btnOpenImage.Text = "Open Image";
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Original";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Preview";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(292, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Output Tiles";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(102, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "(256x256 max)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(91, 363);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "(256x256 max)";
            // 
            // lblNumTiles
            // 
            this.lblNumTiles.AutoSize = true;
            this.lblNumTiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumTiles.ForeColor = System.Drawing.Color.White;
            this.lblNumTiles.Location = new System.Drawing.Point(817, 35);
            this.lblNumTiles.Name = "lblNumTiles";
            this.lblNumTiles.Size = new System.Drawing.Size(56, 20);
            this.lblNumTiles.TabIndex = 2;
            this.lblNumTiles.Text = "Tiles:";
            // 
            // btnExportTiles
            // 
            this.btnExportTiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportTiles.Location = new System.Drawing.Point(900, 565);
            this.btnExportTiles.Name = "btnExportTiles";
            this.btnExportTiles.Size = new System.Drawing.Size(150, 40);
            this.btnExportTiles.TabIndex = 0;
            this.btnExportTiles.Text = "Export Tiles";
            this.btnExportTiles.UseVisualStyleBackColor = true;
            this.btnExportTiles.Click += new System.EventHandler(this.btnExportTiles_Click);
            // 
            // btnExportMap
            // 
            this.btnExportMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportMap.Location = new System.Drawing.Point(900, 611);
            this.btnExportMap.Name = "btnExportMap";
            this.btnExportMap.Size = new System.Drawing.Size(150, 40);
            this.btnExportMap.TabIndex = 0;
            this.btnExportMap.Text = "Export Map";
            this.btnExportMap.UseVisualStyleBackColor = true;
            this.btnExportMap.Click += new System.EventHandler(this.btnExportMap_Click);
            // 
            // comboExportFormat
            // 
            this.comboExportFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboExportFormat.FormattingEnabled = true;
            this.comboExportFormat.Items.AddRange(new object[] {
            "BINARY .BIN",
            "RGB .ASM",
            "GBDK .C",
            "BITMAP"});
            this.comboExportFormat.Location = new System.Drawing.Point(821, 243);
            this.comboExportFormat.Name = "comboExportFormat";
            this.comboExportFormat.Size = new System.Drawing.Size(208, 28);
            this.comboExportFormat.TabIndex = 3;
            this.comboExportFormat.Text = "Export Format";
            // 
            // checkBox_RemoveDupes
            // 
            this.checkBox_RemoveDupes.AutoSize = true;
            this.checkBox_RemoveDupes.Checked = true;
            this.checkBox_RemoveDupes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_RemoveDupes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_RemoveDupes.ForeColor = System.Drawing.Color.White;
            this.checkBox_RemoveDupes.Location = new System.Drawing.Point(821, 107);
            this.checkBox_RemoveDupes.Name = "checkBox_RemoveDupes";
            this.checkBox_RemoveDupes.Size = new System.Drawing.Size(208, 22);
            this.checkBox_RemoveDupes.TabIndex = 4;
            this.checkBox_RemoveDupes.Text = "Remove Duplicate Tiles";
            this.checkBox_RemoveDupes.UseVisualStyleBackColor = true;
            this.checkBox_RemoveDupes.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // picTiles
            // 
            this.picTiles.BackColor = System.Drawing.Color.Black;
            this.picTiles.Location = new System.Drawing.Point(285, 35);
            this.picTiles.Mode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.picTiles.Name = "picTiles";
            this.picTiles.Size = new System.Drawing.Size(512, 512);
            this.picTiles.TabIndex = 1;
            this.picTiles.TabStop = false;
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.Black;
            this.picPreview.Location = new System.Drawing.Point(12, 386);
            this.picPreview.Mode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(256, 256);
            this.picPreview.TabIndex = 1;
            this.picPreview.TabStop = false;
            // 
            // picOriginal
            // 
            this.picOriginal.BackColor = System.Drawing.Color.Black;
            this.picOriginal.Location = new System.Drawing.Point(12, 95);
            this.picOriginal.Mode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.picOriginal.Name = "picOriginal";
            this.picOriginal.Size = new System.Drawing.Size(256, 256);
            this.picOriginal.TabIndex = 1;
            this.picOriginal.TabStop = false;
            // 
            // lblDuplicateTiles
            // 
            this.lblDuplicateTiles.AutoSize = true;
            this.lblDuplicateTiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuplicateTiles.ForeColor = System.Drawing.Color.White;
            this.lblDuplicateTiles.Location = new System.Drawing.Point(817, 72);
            this.lblDuplicateTiles.Name = "lblDuplicateTiles";
            this.lblDuplicateTiles.Size = new System.Drawing.Size(142, 20);
            this.lblDuplicateTiles.TabIndex = 5;
            this.lblDuplicateTiles.Text = "Duplicate Tiles:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(817, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Export Format:";
            // 
            // checkboxPrependTileCountByte
            // 
            this.checkboxPrependTileCountByte.AutoSize = true;
            this.checkboxPrependTileCountByte.Checked = true;
            this.checkboxPrependTileCountByte.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxPrependTileCountByte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkboxPrependTileCountByte.ForeColor = System.Drawing.Color.White;
            this.checkboxPrependTileCountByte.Location = new System.Drawing.Point(821, 286);
            this.checkboxPrependTileCountByte.Name = "checkboxPrependTileCountByte";
            this.checkboxPrependTileCountByte.Size = new System.Drawing.Size(204, 22);
            this.checkboxPrependTileCountByte.TabIndex = 4;
            this.checkboxPrependTileCountByte.Text = "Prepend Tilecount Byte";
            this.checkboxPrependTileCountByte.UseVisualStyleBackColor = true;
            this.checkboxPrependTileCountByte.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkPrependWidthHeight
            // 
            this.checkPrependWidthHeight.AutoSize = true;
            this.checkPrependWidthHeight.Checked = true;
            this.checkPrependWidthHeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPrependWidthHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkPrependWidthHeight.ForeColor = System.Drawing.Color.White;
            this.checkPrependWidthHeight.Location = new System.Drawing.Point(821, 314);
            this.checkPrependWidthHeight.Name = "checkPrependWidthHeight";
            this.checkPrependWidthHeight.Size = new System.Drawing.Size(230, 22);
            this.checkPrependWidthHeight.TabIndex = 6;
            this.checkPrependWidthHeight.Text = "Prepend Map Width/Height";
            this.checkPrependWidthHeight.UseVisualStyleBackColor = true;
            // 
            // FormImgConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1062, 673);
            this.Controls.Add(this.checkPrependWidthHeight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblDuplicateTiles);
            this.Controls.Add(this.checkboxPrependTileCountByte);
            this.Controls.Add(this.checkBox_RemoveDupes);
            this.Controls.Add(this.comboExportFormat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNumTiles);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picTiles);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.picOriginal);
            this.Controls.Add(this.btnExportMap);
            this.Controls.Add(this.btnExportTiles);
            this.Controls.Add(this.btnOpenImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormImgConverter";
            this.Text = "FormImgConverter";
            ((System.ComponentModel.ISupportInitialize)(this.picTiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenImage;
        private PictureBoxWithInterpolationMode picOriginal;
        private PictureBoxWithInterpolationMode picPreview;
        private PictureBoxWithInterpolationMode picTiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblNumTiles;
        private System.Windows.Forms.Button btnExportTiles;
        private System.Windows.Forms.Button btnExportMap;
        private System.Windows.Forms.ComboBox comboExportFormat;
        private System.Windows.Forms.CheckBox checkBox_RemoveDupes;
        private System.Windows.Forms.Label lblDuplicateTiles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkboxPrependTileCountByte;
        private System.Windows.Forms.CheckBox checkPrependWidthHeight;
    }
}