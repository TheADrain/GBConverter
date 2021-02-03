namespace GBImageConvertGUI
{
    partial class FormMapConverter
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
            this.btnExportMap = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.picTilesetPreview = new GBImageConvertGUI.PictureBoxWithInterpolationMode();
            this.picMapPreview = new GBImageConvertGUI.PictureBoxWithInterpolationMode();
            this.label1 = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.checkPrependWidthHeight = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkboxPrependTileCountByte = new System.Windows.Forms.CheckBox();
            this.comboExportFormat = new System.Windows.Forms.ComboBox();
            this.checkDuplicateReplacement = new System.Windows.Forms.CheckBox();
            this.btn_importJSON = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picTilesetPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMapPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportMap
            // 
            this.btnExportMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportMap.Location = new System.Drawing.Point(869, 563);
            this.btnExportMap.Name = "btnExportMap";
            this.btnExportMap.Size = new System.Drawing.Size(150, 40);
            this.btnExportMap.TabIndex = 1;
            this.btnExportMap.Text = "Export Map";
            this.btnExportMap.UseVisualStyleBackColor = true;
            this.btnExportMap.Click += new System.EventHandler(this.btnExportMap_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tileset Preview";
            // 
            // picTilesetPreview
            // 
            this.picTilesetPreview.BackColor = System.Drawing.Color.Black;
            this.picTilesetPreview.Location = new System.Drawing.Point(12, 35);
            this.picTilesetPreview.Mode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.picTilesetPreview.Name = "picTilesetPreview";
            this.picTilesetPreview.Size = new System.Drawing.Size(282, 269);
            this.picTilesetPreview.TabIndex = 3;
            this.picTilesetPreview.TabStop = false;
            // 
            // picMapPreview
            // 
            this.picMapPreview.BackColor = System.Drawing.Color.Black;
            this.picMapPreview.Location = new System.Drawing.Point(310, 35);
            this.picMapPreview.Mode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.picMapPreview.Name = "picMapPreview";
            this.picMapPreview.Size = new System.Drawing.Size(709, 495);
            this.picMapPreview.TabIndex = 5;
            this.picMapPreview.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(503, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tilemap Preview";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.White;
            this.lblError.Location = new System.Drawing.Point(323, 45);
            this.lblError.MaximumSize = new System.Drawing.Size(400, 35);
            this.lblError.MinimumSize = new System.Drawing.Size(400, 35);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(400, 35);
            this.lblError.TabIndex = 7;
            this.lblError.Text = "ERRORLABEL";
            // 
            // checkPrependWidthHeight
            // 
            this.checkPrependWidthHeight.AutoSize = true;
            this.checkPrependWidthHeight.Checked = true;
            this.checkPrependWidthHeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPrependWidthHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkPrependWidthHeight.ForeColor = System.Drawing.Color.White;
            this.checkPrependWidthHeight.Location = new System.Drawing.Point(401, 592);
            this.checkPrependWidthHeight.Name = "checkPrependWidthHeight";
            this.checkPrependWidthHeight.Size = new System.Drawing.Size(230, 22);
            this.checkPrependWidthHeight.TabIndex = 11;
            this.checkPrependWidthHeight.Text = "Prepend Map Width/Height";
            this.checkPrependWidthHeight.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(641, 563);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Export Format:";
            // 
            // checkboxPrependTileCountByte
            // 
            this.checkboxPrependTileCountByte.AutoSize = true;
            this.checkboxPrependTileCountByte.Checked = true;
            this.checkboxPrependTileCountByte.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxPrependTileCountByte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkboxPrependTileCountByte.ForeColor = System.Drawing.Color.White;
            this.checkboxPrependTileCountByte.Location = new System.Drawing.Point(401, 564);
            this.checkboxPrependTileCountByte.Name = "checkboxPrependTileCountByte";
            this.checkboxPrependTileCountByte.Size = new System.Drawing.Size(204, 22);
            this.checkboxPrependTileCountByte.TabIndex = 9;
            this.checkboxPrependTileCountByte.Text = "Prepend Tilecount Byte";
            this.checkboxPrependTileCountByte.UseVisualStyleBackColor = true;
            // 
            // comboExportFormat
            // 
            this.comboExportFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboExportFormat.FormattingEnabled = true;
            this.comboExportFormat.Items.AddRange(new object[] {
            "BINARY .BIN",
            "RGB .ASM",
            "GBDK .C"});
            this.comboExportFormat.Location = new System.Drawing.Point(645, 586);
            this.comboExportFormat.Name = "comboExportFormat";
            this.comboExportFormat.Size = new System.Drawing.Size(208, 28);
            this.comboExportFormat.TabIndex = 8;
            this.comboExportFormat.Text = "Export Format";
            // 
            // checkDuplicateReplacement
            // 
            this.checkDuplicateReplacement.AutoSize = true;
            this.checkDuplicateReplacement.Checked = true;
            this.checkDuplicateReplacement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDuplicateReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkDuplicateReplacement.ForeColor = System.Drawing.Color.White;
            this.checkDuplicateReplacement.Location = new System.Drawing.Point(401, 536);
            this.checkDuplicateReplacement.Name = "checkDuplicateReplacement";
            this.checkDuplicateReplacement.Size = new System.Drawing.Size(285, 22);
            this.checkDuplicateReplacement.TabIndex = 12;
            this.checkDuplicateReplacement.Text = "Do Tileset Duplicate Replacement";
            this.checkDuplicateReplacement.UseVisualStyleBackColor = true;
            this.checkDuplicateReplacement.CheckedChanged += new System.EventHandler(this.checkDuplicateReplacement_CheckedChanged);
            // 
            // btn_importJSON
            // 
            this.btn_importJSON.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_importJSON.Location = new System.Drawing.Point(12, 584);
            this.btn_importJSON.Name = "btn_importJSON";
            this.btn_importJSON.Size = new System.Drawing.Size(220, 30);
            this.btn_importJSON.TabIndex = 13;
            this.btn_importJSON.Text = "Import Map JSON";
            this.btn_importJSON.UseVisualStyleBackColor = true;
            this.btn_importJSON.Click += new System.EventHandler(this.btn_importJSON_Click);
            // 
            // FormMapConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1044, 626);
            this.Controls.Add(this.btn_importJSON);
            this.Controls.Add(this.checkDuplicateReplacement);
            this.Controls.Add(this.checkPrependWidthHeight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkboxPrependTileCountByte);
            this.Controls.Add(this.comboExportFormat);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picMapPreview);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picTilesetPreview);
            this.Controls.Add(this.btnExportMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMapConverter";
            this.Text = "FormMapConverter";
            ((System.ComponentModel.ISupportInitialize)(this.picTilesetPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMapPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExportMap;
        private System.Windows.Forms.Label label4;
        private PictureBoxWithInterpolationMode picTilesetPreview;
        private PictureBoxWithInterpolationMode picMapPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.CheckBox checkPrependWidthHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkboxPrependTileCountByte;
        private System.Windows.Forms.ComboBox comboExportFormat;
        private System.Windows.Forms.CheckBox checkDuplicateReplacement;
        private System.Windows.Forms.Button btn_importJSON;
    }
}