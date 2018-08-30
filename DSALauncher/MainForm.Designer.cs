namespace DSALauncher
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.input = new System.Windows.Forms.ComboBox();
            this.hideDropDownLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // input
            // 
            this.input.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.input.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input.DropDownHeight = 500;
            this.input.DropDownWidth = 800;
            this.input.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input.FormattingEnabled = true;
            this.input.IntegralHeight = false;
            this.input.Location = new System.Drawing.Point(0, 0);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(532, 47);
            this.input.TabIndex = 0;
            this.input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_KeyUp);
            this.input.MouseDown += new System.Windows.Forms.MouseEventHandler(this.input_MouseDown);
            // 
            // hideDropDownLabel
            // 
            this.hideDropDownLabel.BackColor = System.Drawing.Color.White;
            this.hideDropDownLabel.Location = new System.Drawing.Point(517, 8);
            this.hideDropDownLabel.Name = "hideDropDownLabel";
            this.hideDropDownLabel.Size = new System.Drawing.Size(10, 30);
            this.hideDropDownLabel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(532, 44);
            this.ControlBox = false;
            this.Controls.Add(this.hideDropDownLabel);
            this.Controls.Add(this.input);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox input;
        private System.Windows.Forms.Label hideDropDownLabel;
    }
}

