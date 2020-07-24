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
            this.TopId = new System.Windows.Forms.TextBox();
            this.Equip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TopId
            // 
            this.TopId.Location = new System.Drawing.Point(50, 54);
            this.TopId.Name = "TopId";
            this.TopId.Size = new System.Drawing.Size(232, 20);
            this.TopId.TabIndex = 0;
            // 
            // Equip
            // 
            this.Equip.Location = new System.Drawing.Point(50, 99);
            this.Equip.Name = "Equip";
            this.Equip.Size = new System.Drawing.Size(94, 58);
            this.Equip.TabIndex = 1;
            this.Equip.Text = "Equip";
            this.Equip.UseVisualStyleBackColor = true;
            this.Equip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Equip_MouseClick);
            // 
            // GuiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Equip);
            this.Controls.Add(this.TopId);
            this.Name = "GuiForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TopId;
        private System.Windows.Forms.Button Equip;
    }
}

