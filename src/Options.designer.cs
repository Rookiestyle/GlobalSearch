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
            this.gPWDisplay = new System.Windows.Forms.GroupBox();
            this.rbPWDisplayEntryList = new System.Windows.Forms.RadioButton();
            this.rbPWDisplayNever = new System.Windows.Forms.RadioButton();
            this.rbPWDisplayAlways = new System.Windows.Forms.RadioButton();
            this.gSearches = new System.Windows.Forms.GroupBox();
            this.cbSearchPwQuality = new System.Windows.Forms.CheckBox();
            this.cbSearchPwCluster = new System.Windows.Forms.CheckBox();
            this.cbSearchPwPairs = new System.Windows.Forms.CheckBox();
            this.cbSearchDupPw = new System.Windows.Forms.CheckBox();
            this.cbSearchLarge = new System.Windows.Forms.CheckBox();
            this.cbSearchLastMod = new System.Windows.Forms.CheckBox();
            this.cbUseEntryListColumnWidths = new System.Windows.Forms.CheckBox();
            this.cbSearchForm = new System.Windows.Forms.CheckBox();
            this.cbMultiDBSearchInfoSearchFormActive = new System.Windows.Forms.CheckBox();
            this.cbMultiDBSearchInfoSingleSearchActive = new System.Windows.Forms.CheckBox();
            this.tpHelp = new System.Windows.Forms.TabPage();
            this.tbDesc = new System.Windows.Forms.TextBox();
            this.cbSearchAllExpired = new System.Windows.Forms.CheckBox();
            this.tcTabs.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.gPWDisplay.SuspendLayout();
            this.gSearches.SuspendLayout();
            this.tpHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTabs
            // 
            this.tcTabs.Controls.Add(this.tpOptions);
            this.tcTabs.Controls.Add(this.tpHelp);
            this.tcTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTabs.Location = new System.Drawing.Point(18, 15);
            this.tcTabs.Margin = new System.Windows.Forms.Padding(5);
            this.tcTabs.Name = "tcTabs";
            this.tcTabs.SelectedIndex = 0;
            this.tcTabs.Size = new System.Drawing.Size(718, 795);
            this.tcTabs.TabIndex = 0;
            // 
            // tpOptions
            // 
            this.tpOptions.BackColor = System.Drawing.Color.Transparent;
            this.tpOptions.Controls.Add(this.gPWDisplay);
            this.tpOptions.Controls.Add(this.gSearches);
            this.tpOptions.Location = new System.Drawing.Point(10, 48);
            this.tpOptions.Margin = new System.Windows.Forms.Padding(5);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.tpOptions.Size = new System.Drawing.Size(698, 737);
            this.tpOptions.TabIndex = 4;
            this.tpOptions.Text = "Settings";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // gPWDisplay
            // 
            this.gPWDisplay.Controls.Add(this.rbPWDisplayEntryList);
            this.gPWDisplay.Controls.Add(this.rbPWDisplayNever);
            this.gPWDisplay.Controls.Add(this.rbPWDisplayAlways);
            this.gPWDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            this.gPWDisplay.Location = new System.Drawing.Point(18, 506);
            this.gPWDisplay.Name = "gPWDisplay";
            this.gPWDisplay.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.gPWDisplay.Size = new System.Drawing.Size(662, 171);
            this.gPWDisplay.TabIndex = 1;
            this.gPWDisplay.TabStop = false;
            this.gPWDisplay.Text = "gPWDisplay";
            // 
            // rbPWDisplayEntryList
            // 
            this.rbPWDisplayEntryList.AutoSize = true;
            this.rbPWDisplayEntryList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPWDisplayEntryList.Location = new System.Drawing.Point(18, 118);
            this.rbPWDisplayEntryList.Name = "rbPWDisplayEntryList";
            this.rbPWDisplayEntryList.Size = new System.Drawing.Size(626, 36);
            this.rbPWDisplayEntryList.TabIndex = 2;
            this.rbPWDisplayEntryList.TabStop = true;
            this.rbPWDisplayEntryList.Text = "Entry List";
            this.rbPWDisplayEntryList.UseVisualStyleBackColor = true;
            // 
            // rbPWDisplayNever
            // 
            this.rbPWDisplayNever.AutoSize = true;
            this.rbPWDisplayNever.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPWDisplayNever.Location = new System.Drawing.Point(18, 82);
            this.rbPWDisplayNever.Name = "rbPWDisplayNever";
            this.rbPWDisplayNever.Size = new System.Drawing.Size(626, 36);
            this.rbPWDisplayNever.TabIndex = 1;
            this.rbPWDisplayNever.TabStop = true;
            this.rbPWDisplayNever.Text = "Never";
            this.rbPWDisplayNever.UseVisualStyleBackColor = true;
            // 
            // rbPWDisplayAlways
            // 
            this.rbPWDisplayAlways.AutoSize = true;
            this.rbPWDisplayAlways.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPWDisplayAlways.Location = new System.Drawing.Point(18, 46);
            this.rbPWDisplayAlways.Name = "rbPWDisplayAlways";
            this.rbPWDisplayAlways.Size = new System.Drawing.Size(626, 36);
            this.rbPWDisplayAlways.TabIndex = 0;
            this.rbPWDisplayAlways.TabStop = true;
            this.rbPWDisplayAlways.Text = "Always";
            this.rbPWDisplayAlways.UseVisualStyleBackColor = true;
            // 
            // gSearches
            // 
            this.gSearches.Controls.Add(this.cbSearchPwQuality);
            this.gSearches.Controls.Add(this.cbSearchPwCluster);
            this.gSearches.Controls.Add(this.cbSearchPwPairs);
            this.gSearches.Controls.Add(this.cbSearchDupPw);
            this.gSearches.Controls.Add(this.cbSearchLarge);
            this.gSearches.Controls.Add(this.cbSearchLastMod);
            this.gSearches.Controls.Add(this.cbUseEntryListColumnWidths);
            this.gSearches.Controls.Add(this.cbSearchForm);
            this.gSearches.Controls.Add(this.cbMultiDBSearchInfoSearchFormActive);
            this.gSearches.Controls.Add(this.cbMultiDBSearchInfoSingleSearchActive);
            this.gSearches.Dock = System.Windows.Forms.DockStyle.Top;
            this.gSearches.Location = new System.Drawing.Point(18, 15);
            this.gSearches.Name = "gSearches";
            this.gSearches.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.gSearches.Size = new System.Drawing.Size(662, 491);
            this.gSearches.TabIndex = 0;
            this.gSearches.TabStop = false;
            // 
            // cbSearchPwQuality
            // 
            this.cbSearchPwQuality.AutoSize = true;
            this.cbSearchPwQuality.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSearchPwQuality.Location = new System.Drawing.Point(18, 298);
            this.cbSearchPwQuality.Margin = new System.Windows.Forms.Padding(5);
            this.cbSearchPwQuality.Name = "cbSearchPwQuality";
            this.cbSearchPwQuality.Size = new System.Drawing.Size(626, 36);
            this.cbSearchPwQuality.TabIndex = 107;
            this.cbSearchPwQuality.Text = "PwQuality";
            this.cbSearchPwQuality.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbSearchPwQuality.UseVisualStyleBackColor = true;
            // 
            // cbSearchPwCluster
            // 
            this.cbSearchPwCluster.AutoSize = true;
            this.cbSearchPwCluster.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSearchPwCluster.Location = new System.Drawing.Point(18, 262);
            this.cbSearchPwCluster.Margin = new System.Windows.Forms.Padding(5);
            this.cbSearchPwCluster.Name = "cbSearchPwCluster";
            this.cbSearchPwCluster.Size = new System.Drawing.Size(626, 36);
            this.cbSearchPwCluster.TabIndex = 106;
            this.cbSearchPwCluster.Text = "SearchPwCluster";
            this.cbSearchPwCluster.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbSearchPwCluster.UseVisualStyleBackColor = true;
            // 
            // cbSearchPwPairs
            // 
            this.cbSearchPwPairs.AutoSize = true;
            this.cbSearchPwPairs.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSearchPwPairs.Location = new System.Drawing.Point(18, 226);
            this.cbSearchPwPairs.Margin = new System.Windows.Forms.Padding(5);
            this.cbSearchPwPairs.Name = "cbSearchPwPairs";
            this.cbSearchPwPairs.Size = new System.Drawing.Size(626, 36);
            this.cbSearchPwPairs.TabIndex = 105;
            this.cbSearchPwPairs.Text = "SearchPwPairs";
            this.cbSearchPwPairs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbSearchPwPairs.UseVisualStyleBackColor = true;
            // 
            // cbSearchDupPw
            // 
            this.cbSearchDupPw.AutoSize = true;
            this.cbSearchDupPw.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSearchDupPw.Location = new System.Drawing.Point(18, 190);
            this.cbSearchDupPw.Margin = new System.Windows.Forms.Padding(5);
            this.cbSearchDupPw.Name = "cbSearchDupPw";
            this.cbSearchDupPw.Size = new System.Drawing.Size(626, 36);
            this.cbSearchDupPw.TabIndex = 104;
            this.cbSearchDupPw.Text = "SearchDupPw";
            this.cbSearchDupPw.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbSearchDupPw.UseVisualStyleBackColor = true;
            // 
            // cbSearchLarge
            // 
            this.cbSearchLarge.AutoSize = true;
            this.cbSearchLarge.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSearchLarge.Location = new System.Drawing.Point(18, 154);
            this.cbSearchLarge.Margin = new System.Windows.Forms.Padding(5);
            this.cbSearchLarge.Name = "cbSearchLarge";
            this.cbSearchLarge.Size = new System.Drawing.Size(626, 36);
            this.cbSearchLarge.TabIndex = 103;
            this.cbSearchLarge.Text = "SearchLarge";
            this.cbSearchLarge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbSearchLarge.UseVisualStyleBackColor = true;
            // 
            // cbSearchLastMod
            // 
            this.cbSearchLastMod.AutoSize = true;
            this.cbSearchLastMod.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSearchLastMod.Location = new System.Drawing.Point(18, 118);
            this.cbSearchLastMod.Margin = new System.Windows.Forms.Padding(5);
            this.cbSearchLastMod.Name = "cbSearchLastMod";
            this.cbSearchLastMod.Size = new System.Drawing.Size(626, 36);
            this.cbSearchLastMod.TabIndex = 102;
            this.cbSearchLastMod.Text = "SearchLastMod";
            this.cbSearchLastMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbSearchLastMod.UseVisualStyleBackColor = true;
            // 
            // cbUseEntryListColumnWidths
            // 
            this.cbUseEntryListColumnWidths.AutoSize = true;
            this.cbUseEntryListColumnWidths.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbUseEntryListColumnWidths.Location = new System.Drawing.Point(18, 82);
            this.cbUseEntryListColumnWidths.Margin = new System.Windows.Forms.Padding(5);
            this.cbUseEntryListColumnWidths.Name = "cbUseEntryListColumnWidths";
            this.cbUseEntryListColumnWidths.Padding = new System.Windows.Forms.Padding(36, 0, 0, 0);
            this.cbUseEntryListColumnWidths.Size = new System.Drawing.Size(626, 36);
            this.cbUseEntryListColumnWidths.TabIndex = 111;
            this.cbUseEntryListColumnWidths.Text = "UseEntryListColumnWidths";
            this.cbUseEntryListColumnWidths.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbUseEntryListColumnWidths.UseVisualStyleBackColor = true;
            // 
            // cbSearchForm
            // 
            this.cbSearchForm.AutoSize = true;
            this.cbSearchForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSearchForm.Location = new System.Drawing.Point(18, 46);
            this.cbSearchForm.Margin = new System.Windows.Forms.Padding(5);
            this.cbSearchForm.Name = "cbSearchForm";
            this.cbSearchForm.Size = new System.Drawing.Size(626, 36);
            this.cbSearchForm.TabIndex = 101;
            this.cbSearchForm.Text = "SearchForm";
            this.cbSearchForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbSearchForm.UseVisualStyleBackColor = true;
            // 
            // cbMultiDBSearchInfoSearchFormActive
            // 
            this.cbMultiDBSearchInfoSearchFormActive.AutoSize = true;
            this.cbMultiDBSearchInfoSearchFormActive.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbMultiDBSearchInfoSearchFormActive.Location = new System.Drawing.Point(18, 404);
            this.cbMultiDBSearchInfoSearchFormActive.Margin = new System.Windows.Forms.Padding(5);
            this.cbMultiDBSearchInfoSearchFormActive.Name = "cbMultiDBSearchInfoSearchFormActive";
            this.cbMultiDBSearchInfoSearchFormActive.Size = new System.Drawing.Size(626, 36);
            this.cbMultiDBSearchInfoSearchFormActive.TabIndex = 109;
            this.cbMultiDBSearchInfoSearchFormActive.Text = "cbMultiDBSearchInfoSearchFormActive";
            this.cbMultiDBSearchInfoSearchFormActive.UseVisualStyleBackColor = true;
            // 
            // cbMultiDBSearchInfoSingleSearchActive
            // 
            this.cbMultiDBSearchInfoSingleSearchActive.AutoSize = true;
            this.cbMultiDBSearchInfoSingleSearchActive.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbMultiDBSearchInfoSingleSearchActive.Location = new System.Drawing.Point(18, 440);
            this.cbMultiDBSearchInfoSingleSearchActive.Margin = new System.Windows.Forms.Padding(5);
            this.cbMultiDBSearchInfoSingleSearchActive.Name = "cbMultiDBSearchInfoSingleSearchActive";
            this.cbMultiDBSearchInfoSingleSearchActive.Size = new System.Drawing.Size(626, 36);
            this.cbMultiDBSearchInfoSingleSearchActive.TabIndex = 110;
            this.cbMultiDBSearchInfoSingleSearchActive.Text = "cbMultiDBSearchInfoSingleSearchActive";
            this.cbMultiDBSearchInfoSingleSearchActive.UseVisualStyleBackColor = true;
            // 
            // tpHelp
            // 
            this.tpHelp.BackColor = System.Drawing.Color.Transparent;
            this.tpHelp.Controls.Add(this.tbDesc);
            this.tpHelp.Location = new System.Drawing.Point(10, 48);
            this.tpHelp.Margin = new System.Windows.Forms.Padding(5);
            this.tpHelp.Name = "tpHelp";
            this.tpHelp.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.tpHelp.Size = new System.Drawing.Size(698, 737);
            this.tpHelp.TabIndex = 5;
            this.tpHelp.Text = "Desc";
            this.tpHelp.UseVisualStyleBackColor = true;
            // 
            // tbDesc
            // 
            this.tbDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDesc.Location = new System.Drawing.Point(18, 15);
            this.tbDesc.Margin = new System.Windows.Forms.Padding(5);
            this.tbDesc.Multiline = true;
            this.tbDesc.Name = "tbDesc";
            this.tbDesc.ReadOnly = true;
            this.tbDesc.Size = new System.Drawing.Size(662, 707);
            this.tbDesc.TabIndex = 6;
            // 
            // cbSearchAllExpired
            // 
            this.cbSearchAllExpired.AutoSize = true;
            this.cbSearchAllExpired.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbSearchAllExpired.Location = new System.Drawing.Point(18, 298);
            this.cbSearchAllExpired.Margin = new System.Windows.Forms.Padding(5);
            this.cbSearchAllExpired.Name = "cbSearchAllExpired";
            this.cbSearchAllExpired.Size = new System.Drawing.Size(626, 36);
            this.cbSearchAllExpired.TabIndex = 108;
            this.cbSearchAllExpired.Text = "All expired";
            this.cbSearchAllExpired.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbSearchAllExpired.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tcTabs);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Options";
            this.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.Size = new System.Drawing.Size(754, 825);
            this.tcTabs.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
            this.gPWDisplay.ResumeLayout(false);
            this.gPWDisplay.PerformLayout();
            this.gSearches.ResumeLayout(false);
            this.gSearches.PerformLayout();
            this.tpHelp.ResumeLayout(false);
            this.tpHelp.PerformLayout();
            this.ResumeLayout(false);

		}

		private System.Windows.Forms.TabControl tcTabs;
		private System.Windows.Forms.TabPage tpHelp;
		private System.Windows.Forms.TextBox tbDesc;
		private System.Windows.Forms.TabPage tpOptions;
		private System.Windows.Forms.GroupBox gPWDisplay;
		private System.Windows.Forms.GroupBox gSearches;
		internal System.Windows.Forms.CheckBox cbSearchForm;
		internal System.Windows.Forms.CheckBox cbSearchLastMod;
		internal System.Windows.Forms.CheckBox cbSearchLarge;
		internal System.Windows.Forms.CheckBox cbSearchDupPw;
		internal System.Windows.Forms.CheckBox cbSearchPwPairs;
		internal System.Windows.Forms.CheckBox cbSearchPwCluster;
		internal System.Windows.Forms.CheckBox cbSearchPwQuality;
		internal System.Windows.Forms.CheckBox cbMultiDBSearchInfoSingleSearchActive;
		internal System.Windows.Forms.CheckBox cbMultiDBSearchInfoSearchFormActive;
		internal System.Windows.Forms.CheckBox cbSearchAllExpired;
		private System.Windows.Forms.RadioButton rbPWDisplayAlways;
		private System.Windows.Forms.RadioButton rbPWDisplayNever;
		private System.Windows.Forms.RadioButton rbPWDisplayEntryList;
        internal System.Windows.Forms.CheckBox cbUseEntryListColumnWidths;
    }
}
