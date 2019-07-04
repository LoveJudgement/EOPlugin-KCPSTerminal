namespace KCPSTerminal
{
	partial class SettingsControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.labelListeningPort = new System.Windows.Forms.Label();
			this.labelLogPriority = new System.Windows.Forms.Label();
			this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownLogPriority = new System.Windows.Forms.NumericUpDown();
			this.labelToken = new System.Windows.Forms.Label();
			this.textBoxToken = new System.Windows.Forms.TextBox();
			this.checkBoxLogResponse = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLogPriority)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.labelListeningPort, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelLogPriority, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.numericUpDownPort, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.numericUpDownLogPriority, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.labelToken, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.textBoxToken, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.checkBoxLogResponse, 1, 3);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 5;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(150, 150);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// labelListeningPort
			// 
			this.labelListeningPort.AutoSize = true;
			this.labelListeningPort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelListeningPort.Location = new System.Drawing.Point(3, 0);
			this.labelListeningPort.Name = "labelListeningPort";
			this.labelListeningPort.Size = new System.Drawing.Size(71, 26);
			this.labelListeningPort.TabIndex = 0;
			this.labelListeningPort.Text = "Listening Port";
			// 
			// labelLogPriority
			// 
			this.labelLogPriority.AutoSize = true;
			this.labelLogPriority.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelLogPriority.Location = new System.Drawing.Point(3, 26);
			this.labelLogPriority.Name = "labelLogPriority";
			this.labelLogPriority.Size = new System.Drawing.Size(71, 26);
			this.labelLogPriority.TabIndex = 1;
			this.labelLogPriority.Text = "Log Priority";
			// 
			// numericUpDownPort
			// 
			this.numericUpDownPort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDownPort.Location = new System.Drawing.Point(80, 3);
			this.numericUpDownPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.numericUpDownPort.Name = "numericUpDownPort";
			this.numericUpDownPort.Size = new System.Drawing.Size(67, 20);
			this.numericUpDownPort.TabIndex = 2;
			// 
			// numericUpDownLogPriority
			// 
			this.numericUpDownLogPriority.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDownLogPriority.Location = new System.Drawing.Point(80, 29);
			this.numericUpDownLogPriority.Name = "numericUpDownLogPriority";
			this.numericUpDownLogPriority.Size = new System.Drawing.Size(67, 20);
			this.numericUpDownLogPriority.TabIndex = 3;
			// 
			// labelToken
			// 
			this.labelToken.AutoSize = true;
			this.labelToken.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelToken.Location = new System.Drawing.Point(3, 52);
			this.labelToken.Name = "labelToken";
			this.labelToken.Size = new System.Drawing.Size(71, 26);
			this.labelToken.TabIndex = 4;
			this.labelToken.Text = "Token";
			// 
			// textBoxToken
			// 
			this.textBoxToken.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxToken.Location = new System.Drawing.Point(80, 55);
			this.textBoxToken.Name = "textBoxToken";
			this.textBoxToken.Size = new System.Drawing.Size(67, 20);
			this.textBoxToken.TabIndex = 5;
			// 
			// checkBoxLogResponse
			// 
			this.checkBoxLogResponse.AutoSize = true;
			this.checkBoxLogResponse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBoxLogResponse.Location = new System.Drawing.Point(80, 81);
			this.checkBoxLogResponse.Name = "checkBoxLogResponse";
			this.checkBoxLogResponse.Size = new System.Drawing.Size(67, 17);
			this.checkBoxLogResponse.TabIndex = 6;
			this.checkBoxLogResponse.Text = "Include JSON Responses in Log";
			this.checkBoxLogResponse.UseVisualStyleBackColor = true;
			// 
			// SettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "SettingsControl";
			this.Load += new System.EventHandler(this.SettingsControl_Load);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLogPriority)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelListeningPort;
		private System.Windows.Forms.Label labelLogPriority;
		private System.Windows.Forms.NumericUpDown numericUpDownPort;
		private System.Windows.Forms.NumericUpDown numericUpDownLogPriority;
		private System.Windows.Forms.Label labelToken;
		private System.Windows.Forms.TextBox textBoxToken;
		private System.Windows.Forms.CheckBox checkBoxLogResponse;
	}
}
