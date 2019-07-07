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
			this.labelPort = new System.Windows.Forms.Label();
			this.labelLogPriority = new System.Windows.Forms.Label();
			this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownLogPriority = new System.Windows.Forms.NumericUpDown();
			this.labelToken = new System.Windows.Forms.Label();
			this.textBoxToken = new System.Windows.Forms.TextBox();
			this.checkBoxLogResponse = new System.Windows.Forms.CheckBox();
			this.numericUpDownCompressionLevel = new System.Windows.Forms.NumericUpDown();
			this.labelCompressionLevel = new System.Windows.Forms.Label();
			this.labelMouseEventMode = new System.Windows.Forms.Label();
			this.comboBoxMouseEventMode = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDownPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDownLogPriority)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDownCompressionLevel)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(
				new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.labelPort, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelLogPriority, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.numericUpDownPort, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.numericUpDownLogPriority, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.labelToken, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.textBoxToken, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.checkBoxLogResponse, 1, 4);
			this.tableLayoutPanel.Controls.Add(this.numericUpDownCompressionLevel, 2, 2);
			this.tableLayoutPanel.Controls.Add(this.labelCompressionLevel, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.labelMouseEventMode, 0, 5);
			this.tableLayoutPanel.Controls.Add(this.comboBoxMouseEventMode, 1, 5);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 7;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(
				new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(350, 346);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// labelPort
			// 
			this.labelPort.AutoSize = true;
			this.labelPort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelPort.Location = new System.Drawing.Point(4, 0);
			this.labelPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelPort.Name = "labelPort";
			this.labelPort.Size = new System.Drawing.Size(226, 29);
			this.labelPort.TabIndex = 0;
			this.labelPort.Text = "Listening Port";
			// 
			// labelLogPriority
			// 
			this.labelLogPriority.AutoSize = true;
			this.labelLogPriority.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelLogPriority.Location = new System.Drawing.Point(4, 87);
			this.labelLogPriority.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelLogPriority.Name = "labelLogPriority";
			this.labelLogPriority.Size = new System.Drawing.Size(226, 29);
			this.labelLogPriority.TabIndex = 6;
			this.labelLogPriority.Text = "Log Priority";
			// 
			// numericUpDownPort
			// 
			this.numericUpDownPort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDownPort.Location = new System.Drawing.Point(238, 3);
			this.numericUpDownPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericUpDownPort.Maximum = new decimal(new int[] {65535, 0, 0, 0});
			this.numericUpDownPort.Name = "numericUpDownPort";
			this.numericUpDownPort.Size = new System.Drawing.Size(108, 23);
			this.numericUpDownPort.TabIndex = 1;
			// 
			// numericUpDownLogPriority
			// 
			this.numericUpDownLogPriority.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDownLogPriority.Location = new System.Drawing.Point(238, 90);
			this.numericUpDownLogPriority.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericUpDownLogPriority.Name = "numericUpDownLogPriority";
			this.numericUpDownLogPriority.Size = new System.Drawing.Size(108, 23);
			this.numericUpDownLogPriority.TabIndex = 7;
			// 
			// labelToken
			// 
			this.labelToken.AutoSize = true;
			this.labelToken.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelToken.Location = new System.Drawing.Point(4, 29);
			this.labelToken.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelToken.Name = "labelToken";
			this.labelToken.Size = new System.Drawing.Size(226, 29);
			this.labelToken.TabIndex = 2;
			this.labelToken.Text = "Token";
			// 
			// textBoxToken
			// 
			this.textBoxToken.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxToken.Location = new System.Drawing.Point(238, 32);
			this.textBoxToken.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.textBoxToken.Name = "textBoxToken";
			this.textBoxToken.Size = new System.Drawing.Size(108, 23);
			this.textBoxToken.TabIndex = 3;
			// 
			// checkBoxLogResponse
			// 
			this.checkBoxLogResponse.AutoSize = true;
			this.checkBoxLogResponse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBoxLogResponse.Location = new System.Drawing.Point(238, 119);
			this.checkBoxLogResponse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.checkBoxLogResponse.Name = "checkBoxLogResponse";
			this.checkBoxLogResponse.Size = new System.Drawing.Size(108, 19);
			this.checkBoxLogResponse.TabIndex = 8;
			this.checkBoxLogResponse.Text = "Include JSON Responses in Log";
			this.checkBoxLogResponse.UseVisualStyleBackColor = true;
			// 
			// numericUpDownCompressionLevel
			// 
			this.numericUpDownCompressionLevel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDownCompressionLevel.Location = new System.Drawing.Point(238, 61);
			this.numericUpDownCompressionLevel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.numericUpDownCompressionLevel.Name = "numericUpDownCompressionLevel";
			this.numericUpDownCompressionLevel.Size = new System.Drawing.Size(108, 23);
			this.numericUpDownCompressionLevel.TabIndex = 5;
			// 
			// labelCompressionLevel
			// 
			this.labelCompressionLevel.AutoSize = true;
			this.labelCompressionLevel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelCompressionLevel.Location = new System.Drawing.Point(4, 58);
			this.labelCompressionLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelCompressionLevel.Name = "labelCompressionLevel";
			this.labelCompressionLevel.Size = new System.Drawing.Size(226, 29);
			this.labelCompressionLevel.TabIndex = 4;
			this.labelCompressionLevel.Text = "JPEG Compresion Level (0 to use PNG)";
			// 
			// labelMouseEventMode
			// 
			this.labelMouseEventMode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelMouseEventMode.Location = new System.Drawing.Point(4, 141);
			this.labelMouseEventMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelMouseEventMode.Name = "labelMouseEventMode";
			this.labelMouseEventMode.Size = new System.Drawing.Size(226, 31);
			this.labelMouseEventMode.TabIndex = 9;
			this.labelMouseEventMode.Text = "Mouse Event Mode";
			// 
			// comboBoxMouseEventMode
			// 
			this.comboBoxMouseEventMode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxMouseEventMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMouseEventMode.FormattingEnabled = true;
			this.comboBoxMouseEventMode.Location = new System.Drawing.Point(238, 144);
			this.comboBoxMouseEventMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.comboBoxMouseEventMode.Name = "comboBoxMouseEventMode";
			this.comboBoxMouseEventMode.Size = new System.Drawing.Size(108, 23);
			this.comboBoxMouseEventMode.TabIndex = 10;
			// 
			// SettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "SettingsControl";
			this.Size = new System.Drawing.Size(350, 346);
			this.Load += new System.EventHandler(this.SettingsControl_Load);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDownPort)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDownLogPriority)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDownCompressionLevel)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelPort;
		private System.Windows.Forms.Label labelLogPriority;
		private System.Windows.Forms.NumericUpDown numericUpDownPort;
		private System.Windows.Forms.NumericUpDown numericUpDownLogPriority;
		private System.Windows.Forms.Label labelToken;
		private System.Windows.Forms.TextBox textBoxToken;
		private System.Windows.Forms.CheckBox checkBoxLogResponse;
		private System.Windows.Forms.NumericUpDown numericUpDownCompressionLevel;
		private System.Windows.Forms.Label labelMouseEventMode;
		private System.Windows.Forms.ComboBox comboBoxMouseEventMode;
		private System.Windows.Forms.Label labelCompressionLevel;
	}
}
