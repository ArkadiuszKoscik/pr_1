namespace bazadanych
{
    partial class Form1
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
            this.PokazAutaButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PokazAutaButton
            // 
            this.PokazAutaButton.Location = new System.Drawing.Point(12, 104);
            this.PokazAutaButton.Name = "PokazAutaButton";
            this.PokazAutaButton.Size = new System.Drawing.Size(124, 40);
            this.PokazAutaButton.TabIndex = 2;
            this.PokazAutaButton.Text = "Pokaż auta";
            this.PokazAutaButton.UseVisualStyleBackColor = true;
            this.PokazAutaButton.Click += new System.EventHandler(this.PokazAutaButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 156);
            this.Controls.Add(this.PokazAutaButton);
            this.Name = "Form1";
            this.Text = "Warsztat";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button PokazAutaButton;
    }
}

