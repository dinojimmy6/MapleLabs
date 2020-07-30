namespace WindowsFormsApp1
{
    partial class GuiForm
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
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchResult = new System.Windows.Forms.ListBox();
            this.EquipIcon = new System.Windows.Forms.PictureBox();
            this.Filters = new System.Windows.Forms.Panel();
            this.FilterCape = new System.Windows.Forms.CheckBox();
            this.FilterGloves = new System.Windows.Forms.CheckBox();
            this.FilterShoes = new System.Windows.Forms.CheckBox();
            this.FilterOverall = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.EquipIcon)).BeginInit();
            this.Filters.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(52, 38);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(224, 20);
            this.SearchBox.TabIndex = 2;
            this.SearchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            // 
            // SearchResult
            // 
            this.SearchResult.FormattingEnabled = true;
            this.SearchResult.Location = new System.Drawing.Point(52, 64);
            this.SearchResult.Name = "SearchResult";
            this.SearchResult.Size = new System.Drawing.Size(224, 173);
            this.SearchResult.TabIndex = 3;
            this.SearchResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SearchResult_MouseClick);
            this.SearchResult.DoubleClick += new System.EventHandler(this.SearchResult_DoubleClick);
            // 
            // EquipIcon
            // 
            this.EquipIcon.Location = new System.Drawing.Point(300, 156);
            this.EquipIcon.Name = "EquipIcon";
            this.EquipIcon.Size = new System.Drawing.Size(100, 50);
            this.EquipIcon.TabIndex = 4;
            this.EquipIcon.TabStop = false;
            // 
            // Filters
            // 
            this.Filters.Controls.Add(this.FilterCape);
            this.Filters.Controls.Add(this.FilterGloves);
            this.Filters.Controls.Add(this.FilterShoes);
            this.Filters.Controls.Add(this.FilterOverall);
            this.Filters.Location = new System.Drawing.Point(52, 257);
            this.Filters.Name = "Filters";
            this.Filters.Size = new System.Drawing.Size(361, 171);
            this.Filters.TabIndex = 5;
            // 
            // FilterCape
            // 
            this.FilterCape.AutoSize = true;
            this.FilterCape.Location = new System.Drawing.Point(4, 75);
            this.FilterCape.Name = "FilterCape";
            this.FilterCape.Size = new System.Drawing.Size(51, 17);
            this.FilterCape.TabIndex = 3;
            this.FilterCape.Text = "Cape";
            this.FilterCape.UseVisualStyleBackColor = true;
            this.FilterCape.CheckedChanged += new System.EventHandler(this.FilterCape_CheckedChanged);
            // 
            // FilterGloves
            // 
            this.FilterGloves.AutoSize = true;
            this.FilterGloves.Location = new System.Drawing.Point(4, 51);
            this.FilterGloves.Name = "FilterGloves";
            this.FilterGloves.Size = new System.Drawing.Size(59, 17);
            this.FilterGloves.TabIndex = 2;
            this.FilterGloves.Text = "Gloves";
            this.FilterGloves.UseVisualStyleBackColor = true;
            this.FilterGloves.CheckedChanged += new System.EventHandler(this.FilterGloves_CheckedChanged);
            // 
            // FilterShoes
            // 
            this.FilterShoes.AutoSize = true;
            this.FilterShoes.Location = new System.Drawing.Point(4, 27);
            this.FilterShoes.Name = "FilterShoes";
            this.FilterShoes.Size = new System.Drawing.Size(56, 17);
            this.FilterShoes.TabIndex = 1;
            this.FilterShoes.Text = "Shoes";
            this.FilterShoes.UseVisualStyleBackColor = true;
            this.FilterShoes.CheckedChanged += new System.EventHandler(this.FilterShoes_CheckedChanged);
            // 
            // FilterOverall
            // 
            this.FilterOverall.AutoSize = true;
            this.FilterOverall.Location = new System.Drawing.Point(4, 4);
            this.FilterOverall.Name = "FilterOverall";
            this.FilterOverall.Size = new System.Drawing.Size(59, 17);
            this.FilterOverall.TabIndex = 0;
            this.FilterOverall.Text = "Overall";
            this.FilterOverall.UseVisualStyleBackColor = true;
            this.FilterOverall.CheckedChanged += new System.EventHandler(this.FilterOverall_CheckedChanged);
            // 
            // GuiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Filters);
            this.Controls.Add(this.EquipIcon);
            this.Controls.Add(this.SearchResult);
            this.Controls.Add(this.SearchBox);
            this.Name = "GuiForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.EquipIcon)).EndInit();
            this.Filters.ResumeLayout(false);
            this.Filters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.ListBox SearchResult;
        private System.Windows.Forms.PictureBox EquipIcon;
        private System.Windows.Forms.Panel Filters;
        private System.Windows.Forms.CheckBox FilterCape;
        private System.Windows.Forms.CheckBox FilterGloves;
        private System.Windows.Forms.CheckBox FilterShoes;
        private System.Windows.Forms.CheckBox FilterOverall;
    }
}

