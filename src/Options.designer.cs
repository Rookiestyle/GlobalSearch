namespace GlobalSearch
{
	partial class Options
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.tcTabs = new System.Windows.Forms.TabControl();
			this.tpOptions = new System.Windows.Forms.TabPage();
			this.cbMultiDBSearchInfoSearchFormActive = new System.Windows.Forms.CheckBox();
			this.cbMultiDBSearchInfoSingleSearchActive = new System.Windows.Forms.CheckBox();
			this.cbSearchPwQuality = new System.Windows.Forms.CheckBox();
			this.cbSearchPwCluster = new System.Windows.Forms.CheckBox();
			this.cbSearchPwPairs = new System.Windows.Forms.CheckBox();
			this.cbSearchDupPw = new System.Windows.Forms.CheckBox();
			this.cbSearchLarge = new System.Windows.Forms.CheckBox();
			this.cbSearchLastMod = new System.Windows.Forms.CheckBox();
			this.cbSearchForm = new System.Windows.Forms.CheckBox();
			this.tpHelp = new System.Windows.Forms.TabPage();
			this.tbDesc = new System.Windows.Forms.TextBox();
			this.tcTabs.SuspendLayout();
			this.tpOptions.SuspendLayout();
			this.tpHelp.SuspendLayout();
			this.SuspendLayout();
			// 
			// tcTabs
			// 
			this.tcTabs.Controls.Add(this.tpOptions);
			this.tcTabs.Controls.Add(this.tpHelp);
			this.tcTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcTabs.Location = new System.Drawing.Point(10, 10);
			this.tcTabs.Name = "tcTabs";
			this.tcTabs.SelectedIndex = 0;
			this.tcTabs.Size = new System.Drawing.Size(404, 512);
			this.tcTabs.TabIndex = 0;
			// 
			// tpOptions
			// 
			this.tpOptions.BackColor = System.Drawing.Color.Transparent;
			this.tpOptions.Controls.Add(this.cbMultiDBSearchInfoSearchFormActive);
			this.tpOptions.Controls.Add(this.cbMultiDBSearchInfoSingleSearchActive);
			this.tpOptions.Controls.Add(this.cbSearchPwQuality);
			this.tpOptions.Controls.Add(this.cbSearchPwCluster);
			this.tpOptions.Controls.Add(this.cbSearchPwPairs);
			this.tpOptions.Controls.Add(this.cbSearchDupPw);
			this.tpOptions.Controls.Add(this.cbSearchLarge);
			this.tpOptions.Controls.Add(this.cbSearchLastMod);
			this.tpOptions.Controls.Add(this.cbSearchForm);
			this.tpOptions.Location = new System.Drawing.Point(4, 29);
			this.tpOptions.Name = "tpOptions";
			this.tpOptions.Padding = new System.Windows.Forms.Padding(10);
			this.tpOptions.Size = new System.Drawing.Size(396, 479);
			this.tpOptions.TabIndex = 4;
			this.tpOptions.Text = "Settings";
			this.tpOptions.UseVisualStyleBackColor = true;
			// 
			// cbMultiDBSearchInfoSearchFormActive
			// 
			this.cbMultiDBSearchInfoSearchFormActive.AutoSize = true;
			this.cbMultiDBSearchInfoSearchFormActive.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.cbMultiDBSearchInfoSearchFormActive.Location = new System.Drawing.Point(10, 421);
			this.cbMultiDBSearchInfoSearchFormActive.Name = "cbMultiDBSearchInfoSearchFormActive";
			this.cbMultiDBSearchInfoSearchFormActive.Size = new System.Drawing.Size(376, 24);
			this.cbMultiDBSearchInfoSearchFormActive.TabIndex = 7;
			this.cbMultiDBSearchInfoSearchFormActive.Text = "cbMultiDBSearchInfoSearchFormActive";
			this.cbMultiDBSearchInfoSearchFormActive.UseVisualStyleBackColor = true;
			// 
			// cbMultiDBSearchInfoSingleSearchActive
			// 
			this.cbMultiDBSearchInfoSingleSearchActive.AutoSize = true;
			this.cbMultiDBSearchInfoSingleSearchActive.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.cbMultiDBSearchInfoSingleSearchActive.Location = new System.Drawing.Point(10, 445);
			this.cbMultiDBSearchInfoSingleSearchActive.Name = "cbMultiDBSearchInfoSingleSearchActive";
			this.cbMultiDBSearchInfoSingleSearchActive.Size = new System.Drawing.Size(376, 24);
			this.cbMultiDBSearchInfoSingleSearchActive.TabIndex = 8;
			this.cbMultiDBSearchInfoSingleSearchActive.Text = "cbMultiDBSearchInfoSingleSearchActive";
			this.cbMultiDBSearchInfoSingleSearchActive.UseVisualStyleBackColor = true;
			// 
			// cbSearchPwQuality
			// 
			this.cbSearchPwQuality.AutoSize = true;
			this.cbSearchPwQuality.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSearchPwQuality.Location = new System.Drawing.Point(10, 154);
			this.cbSearchPwQuality.Name = "cbSearchPwQuality";
			this.cbSearchPwQuality.Size = new System.Drawing.Size(376, 24);
			this.cbSearchPwQuality.TabIndex = 6;
			this.cbSearchPwQuality.Text = "PwQuality";
			this.cbSearchPwQuality.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cbSearchPwQuality.UseVisualStyleBackColor = true;
			// 
			// cbSearchPwCluster
			// 
			this.cbSearchPwCluster.AutoSize = true;
			this.cbSearchPwCluster.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSearchPwCluster.Location = new System.Drawing.Point(10, 130);
			this.cbSearchPwCluster.Name = "cbSearchPwCluster";
			this.cbSearchPwCluster.Size = new System.Drawing.Size(376, 24);
			this.cbSearchPwCluster.TabIndex = 5;
			this.cbSearchPwCluster.Text = "SearchPwCluster";
			this.cbSearchPwCluster.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cbSearchPwCluster.UseVisualStyleBackColor = true;
			// 
			// cbSearchPwPairs
			// 
			this.cbSearchPwPairs.AutoSize = true;
			this.cbSearchPwPairs.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSearchPwPairs.Location = new System.Drawing.Point(10, 106);
			this.cbSearchPwPairs.Name = "cbSearchPwPairs";
			this.cbSearchPwPairs.Size = new System.Drawing.Size(376, 24);
			this.cbSearchPwPairs.TabIndex = 4;
			this.cbSearchPwPairs.Text = "SearchPwPairs";
			this.cbSearchPwPairs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cbSearchPwPairs.UseVisualStyleBackColor = true;
			// 
			// cbSearchDupPw
			// 
			this.cbSearchDupPw.AutoSize = true;
			this.cbSearchDupPw.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSearchDupPw.Location = new System.Drawing.Point(10, 82);
			this.cbSearchDupPw.Name = "cbSearchDupPw";
			this.cbSearchDupPw.Size = new System.Drawing.Size(376, 24);
			this.cbSearchDupPw.TabIndex = 3;
			this.cbSearchDupPw.Text = "SearchDupPw";
			this.cbSearchDupPw.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cbSearchDupPw.UseVisualStyleBackColor = true;
			// 
			// cbSearchLarge
			// 
			this.cbSearchLarge.AutoSize = true;
			this.cbSearchLarge.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSearchLarge.Location = new System.Drawing.Point(10, 58);
			this.cbSearchLarge.Name = "cbSearchLarge";
			this.cbSearchLarge.Size = new System.Drawing.Size(376, 24);
			this.cbSearchLarge.TabIndex = 2;
			this.cbSearchLarge.Text = "SearchLarge";
			this.cbSearchLarge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cbSearchLarge.UseVisualStyleBackColor = true;
			// 
			// cbSearchLastMod
			// 
			this.cbSearchLastMod.AutoSize = true;
			this.cbSearchLastMod.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSearchLastMod.Location = new System.Drawing.Point(10, 34);
			this.cbSearchLastMod.Name = "cbSearchLastMod";
			this.cbSearchLastMod.Size = new System.Drawing.Size(376, 24);
			this.cbSearchLastMod.TabIndex = 81;
			this.cbSearchLastMod.Text = "SearchLastMod";
			this.cbSearchLastMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cbSearchLastMod.UseVisualStyleBackColor = true;
			// 
			// cbSearchForm
			// 
			this.cbSearchForm.AutoSize = true;
			this.cbSearchForm.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbSearchForm.Location = new System.Drawing.Point(10, 10);
			this.cbSearchForm.Name = "cbSearchForm";
			this.cbSearchForm.Size = new System.Drawing.Size(376, 24);
			this.cbSearchForm.TabIndex = 0;
			this.cbSearchForm.Text = "SearchForm";
			this.cbSearchForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cbSearchForm.UseVisualStyleBackColor = true;
			// 
			// tpHelp
			// 
			this.tpHelp.BackColor = System.Drawing.Color.Transparent;
			this.tpHelp.Controls.Add(this.tbDesc);
			this.tpHelp.Location = new System.Drawing.Point(4, 29);
			this.tpHelp.Name = "tpHelp";
			this.tpHelp.Padding = new System.Windows.Forms.Padding(10);
			this.tpHelp.Size = new System.Drawing.Size(396, 479);
			this.tpHelp.TabIndex = 5;
			this.tpHelp.Text = "Desc";
			this.tpHelp.UseVisualStyleBackColor = true;
			// 
			// tbDesc
			// 
			this.tbDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbDesc.Location = new System.Drawing.Point(10, 10);
			this.tbDesc.Multiline = true;
			this.tbDesc.Name = "tbDesc";
			this.tbDesc.ReadOnly = true;
			this.tbDesc.Size = new System.Drawing.Size(376, 459);
			this.tbDesc.TabIndex = 6;
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.tcTabs);
			this.Name = "Options";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Size = new System.Drawing.Size(424, 532);
			this.tcTabs.ResumeLayout(false);
			this.tpOptions.ResumeLayout(false);
			this.tpOptions.PerformLayout();
			this.tpHelp.ResumeLayout(false);
			this.tpHelp.PerformLayout();
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.TabControl tcTabs;
		private System.Windows.Forms.TabPage tpOptions;
		private System.Windows.Forms.TabPage tpHelp;
		internal System.Windows.Forms.CheckBox cbSearchDupPw;
		internal System.Windows.Forms.CheckBox cbSearchForm;
		internal System.Windows.Forms.CheckBox cbSearchPwQuality;
		internal System.Windows.Forms.CheckBox cbSearchPwCluster;
		internal System.Windows.Forms.CheckBox cbSearchPwPairs;
		private System.Windows.Forms.TextBox tbDesc;
		internal System.Windows.Forms.CheckBox cbSearchLarge;
		internal System.Windows.Forms.CheckBox cbSearchLastMod;
		internal System.Windows.Forms.CheckBox cbMultiDBSearchInfoSingleSearchActive;
		internal System.Windows.Forms.CheckBox cbMultiDBSearchInfoSearchFormActive;
	}
}
