namespace MIPSEmulator
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
            this.userCodeTextBox = new System.Windows.Forms.RichTextBox();
            this.UserCodeLabel = new System.Windows.Forms.Label();
            this.reg_GridView = new System.Windows.Forms.DataGridView();
            this.Register = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pr_GridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PipelineRegistersLabel = new System.Windows.Forms.Label();
            this.RegistersLabel = new System.Windows.Forms.Label();
            this.PCLabel = new System.Windows.Forms.Label();
            this.pcTextBox = new System.Windows.Forms.TextBox();
            this.initButton = new System.Windows.Forms.Button();
            this.runCycleButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.reg_GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pr_GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // userCodeTextBox
            // 
            this.userCodeTextBox.Location = new System.Drawing.Point(12, 49);
            this.userCodeTextBox.Name = "userCodeTextBox";
            this.userCodeTextBox.Size = new System.Drawing.Size(282, 331);
            this.userCodeTextBox.TabIndex = 0;
            this.userCodeTextBox.Text = "";
            // 
            // UserCodeLabel
            // 
            this.UserCodeLabel.AutoSize = true;
            this.UserCodeLabel.Location = new System.Drawing.Point(13, 30);
            this.UserCodeLabel.Name = "UserCodeLabel";
            this.UserCodeLabel.Size = new System.Drawing.Size(57, 13);
            this.UserCodeLabel.TabIndex = 1;
            this.UserCodeLabel.Text = "User Code";
            // 
            // reg_GridView
            // 
            this.reg_GridView.AllowUserToAddRows = false;
            this.reg_GridView.AllowUserToDeleteRows = false;
            this.reg_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reg_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Register,
            this.Value});
            this.reg_GridView.Location = new System.Drawing.Point(313, 28);
            this.reg_GridView.Name = "reg_GridView";
            this.reg_GridView.ReadOnly = true;
            this.reg_GridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.reg_GridView.Size = new System.Drawing.Size(244, 362);
            this.reg_GridView.TabIndex = 2;
            // 
            // Register
            // 
            this.Register.HeaderText = "Register";
            this.Register.Name = "Register";
            this.Register.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 101;
            // 
            // pr_GridView
            // 
            this.pr_GridView.AllowUserToAddRows = false;
            this.pr_GridView.AllowUserToDeleteRows = false;
            this.pr_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pr_GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.pr_GridView.Location = new System.Drawing.Point(577, 28);
            this.pr_GridView.Name = "pr_GridView";
            this.pr_GridView.ReadOnly = true;
            this.pr_GridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pr_GridView.Size = new System.Drawing.Size(244, 362);
            this.pr_GridView.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Register";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // PipelineRegistersLabel
            // 
            this.PipelineRegistersLabel.AutoSize = true;
            this.PipelineRegistersLabel.Location = new System.Drawing.Point(574, 9);
            this.PipelineRegistersLabel.Name = "PipelineRegistersLabel";
            this.PipelineRegistersLabel.Size = new System.Drawing.Size(91, 13);
            this.PipelineRegistersLabel.TabIndex = 4;
            this.PipelineRegistersLabel.Text = "Pipeline Registers";
            // 
            // RegistersLabel
            // 
            this.RegistersLabel.AutoSize = true;
            this.RegistersLabel.Location = new System.Drawing.Point(310, 9);
            this.RegistersLabel.Name = "RegistersLabel";
            this.RegistersLabel.Size = new System.Drawing.Size(80, 13);
            this.RegistersLabel.TabIndex = 5;
            this.RegistersLabel.Text = "MIPS Registers";
            // 
            // PCLabel
            // 
            this.PCLabel.AutoSize = true;
            this.PCLabel.Location = new System.Drawing.Point(9, 406);
            this.PCLabel.Name = "PCLabel";
            this.PCLabel.Size = new System.Drawing.Size(21, 13);
            this.PCLabel.TabIndex = 6;
            this.PCLabel.Text = "PC";
            // 
            // pcTextBox
            // 
            this.pcTextBox.Location = new System.Drawing.Point(36, 406);
            this.pcTextBox.Name = "pcTextBox";
            this.pcTextBox.Size = new System.Drawing.Size(119, 20);
            this.pcTextBox.TabIndex = 7;
            this.pcTextBox.Text = "1000";
            // 
            // initButton
            // 
            this.initButton.Location = new System.Drawing.Point(170, 396);
            this.initButton.Name = "initButton";
            this.initButton.Size = new System.Drawing.Size(124, 39);
            this.initButton.TabIndex = 8;
            this.initButton.Text = "Initialise";
            this.initButton.UseVisualStyleBackColor = true;
            this.initButton.Click += new System.EventHandler(this.initButton_Click);
            // 
            // runCycleButton
            // 
            this.runCycleButton.Location = new System.Drawing.Point(313, 396);
            this.runCycleButton.Name = "runCycleButton";
            this.runCycleButton.Size = new System.Drawing.Size(128, 39);
            this.runCycleButton.TabIndex = 9;
            this.runCycleButton.Text = "Run 1 Cycle";
            this.runCycleButton.UseVisualStyleBackColor = true;
            this.runCycleButton.Click += new System.EventHandler(this.runCycleButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 454);
            this.Controls.Add(this.runCycleButton);
            this.Controls.Add(this.initButton);
            this.Controls.Add(this.pcTextBox);
            this.Controls.Add(this.PCLabel);
            this.Controls.Add(this.RegistersLabel);
            this.Controls.Add(this.PipelineRegistersLabel);
            this.Controls.Add(this.pr_GridView);
            this.Controls.Add(this.reg_GridView);
            this.Controls.Add(this.UserCodeLabel);
            this.Controls.Add(this.userCodeTextBox);
            this.Name = "MainForm";
            this.Text = "MIPS Emulator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reg_GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pr_GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox userCodeTextBox;
        private System.Windows.Forms.Label UserCodeLabel;
        private System.Windows.Forms.DataGridView reg_GridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Register;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridView pr_GridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label PipelineRegistersLabel;
        private System.Windows.Forms.Label RegistersLabel;
        private System.Windows.Forms.Label PCLabel;
        private System.Windows.Forms.TextBox pcTextBox;
        private System.Windows.Forms.Button initButton;
        private System.Windows.Forms.Button runCycleButton;
    }
}

