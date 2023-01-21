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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("tvnUseEntryListColummnWidth");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("tvnSearchForm", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("tvnExpired");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("tvnLastMod");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("tvnLargeEntries");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("tvnDupPw");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("tvnSimPwPairs");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("tvnSimPwCluster");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("tvnPwQuality");
            this.tcTabs = new System.Windows.Forms.TabControl();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.gPWDisplay = new System.Windows.Forms.GroupBox();
            this.rbPWDisplayEntryList = new System.Windows.Forms.RadioButton();
            this.rbPWDisplayNever = new System.Windows.Forms.RadioButton();
            this.rbPWDisplayAlways = new System.Windows.Forms.RadioButton();
            this.gSearches = new System.Windows.Forms.GroupBox();
            this.tvHookedSearches = new System.Windows.Forms.TreeView();
            this.cbMultiDBSearchInfoSearchFormActive = new System.Windows.Forms.CheckBox();
            this.cbMultiDBSearchInfoSingleSearchActive = new System.Windows.Forms.CheckBox();
            this.tpHelp = new System.Windows.Forms.TabPage();
            this.tbDesc = new System.Windows.Forms.TextBox();
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
            this.tcTabs.Size = new System.Drawing.Size(1169, 715);
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
            this.tpOptions.Size = new System.Drawing.Size(1149, 657);
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
            this.gPWDisplay.Location = new System.Drawing.Point(18, 458);
            this.gPWDisplay.Name = "gPWDisplay";
            this.gPWDisplay.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.gPWDisplay.Size = new System.Drawing.Size(1113, 171);
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
            this.rbPWDisplayEntryList.Size = new System.Drawing.Size(1077, 36);
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
            this.rbPWDisplayNever.Size = new System.Drawing.Size(1077, 36);
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
            this.rbPWDisplayAlways.Size = new System.Drawing.Size(1077, 36);
            this.rbPWDisplayAlways.TabIndex = 0;
            this.rbPWDisplayAlways.TabStop = true;
            this.rbPWDisplayAlways.Text = "Always";
            this.rbPWDisplayAlways.UseVisualStyleBackColor = true;
            // 
            // gSearches
            // 
            this.gSearches.Controls.Add(this.tvHookedSearches);
            this.gSearches.Controls.Add(this.cbMultiDBSearchInfoSearchFormActive);
            this.gSearches.Controls.Add(this.cbMultiDBSearchInfoSingleSearchActive);
            this.gSearches.Dock = System.Windows.Forms.DockStyle.Top;
            this.gSearches.Location = new System.Drawing.Point(18, 15);
            this.gSearches.Name = "gSearches";
            this.gSearches.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.gSearches.Size = new System.Drawing.Size(1113, 443);
            this.gSearches.TabIndex = 0;
            this.gSearches.TabStop = false;
            // 
            // tvHookedSearches
            // 
            this.tvHookedSearches.CheckBoxes = true;
            this.tvHookedSearches.Dock = System.Windows.Forms.DockStyle.Top;
            this.tvHookedSearches.FullRowSelect = true;
            this.tvHookedSearches.Indent = 38;
            this.tvHookedSearches.Location = new System.Drawing.Point(18, 46);
            this.tvHookedSearches.Name = "tvHookedSearches";
            treeNode1.Name = "tvnUseEntryListColummnWidth";
            treeNode1.Text = "tvnUseEntryListColummnWidth";
            treeNode2.Name = "tvnSearchForm";
            treeNode2.Text = "tvnSearchForm";
            treeNode3.Name = "tvnExpired";
            treeNode3.Text = "tvnExpired";
            treeNode4.Name = "tvnLastMod";
            treeNode4.Text = "tvnLastMod";
            treeNode5.Name = "tvnLargeEntries";
            treeNode5.Text = "tvnLargeEntries";
            treeNode6.Name = "tvnDupPw";
            treeNode6.Text = "tvnDupPw";
            treeNode7.Name = "tvnSimPwPairs";
            treeNode7.Text = "tvnSimPwPairs";
            treeNode8.Name = "tvnSimPwCluster";
            treeNode8.Text = "tvnSimPwCluster";
            treeNode9.Name = "tvnPwQuality";
            treeNode9.Text = "tvnPwQuality";
            this.tvHookedSearches.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            this.tvHookedSearches.Size = new System.Drawing.Size(1077, 288);
            this.tvHookedSearches.TabIndex = 112;
            this.tvHookedSearches.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvHookedSearches_BeforeChecked);
            // 
            // cbMultiDBSearchInfoSearchFormActive
            // 
            this.cbMultiDBSearchInfoSearchFormActive.AutoSize = true;
            this.cbMultiDBSearchInfoSearchFormActive.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbMultiDBSearchInfoSearchFormActive.Location = new System.Drawing.Point(18, 356);
            this.cbMultiDBSearchInfoSearchFormActive.Margin = new System.Windows.Forms.Padding(5);
            this.cbMultiDBSearchInfoSearchFormActive.Name = "cbMultiDBSearchInfoSearchFormActive";
            this.cbMultiDBSearchInfoSearchFormActive.Size = new System.Drawing.Size(1077, 36);
            this.cbMultiDBSearchInfoSearchFormActive.TabIndex = 109;
            this.cbMultiDBSearchInfoSearchFormActive.Text = "cbMultiDBSearchInfoSearchFormActive";
            this.cbMultiDBSearchInfoSearchFormActive.UseVisualStyleBackColor = true;
            // 
            // cbMultiDBSearchInfoSingleSearchActive
            // 
            this.cbMultiDBSearchInfoSingleSearchActive.AutoSize = true;
            this.cbMultiDBSearchInfoSingleSearchActive.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbMultiDBSearchInfoSingleSearchActive.Location = new System.Drawing.Point(18, 392);
            this.cbMultiDBSearchInfoSingleSearchActive.Margin = new System.Windows.Forms.Padding(5);
            this.cbMultiDBSearchInfoSingleSearchActive.Name = "cbMultiDBSearchInfoSingleSearchActive";
            this.cbMultiDBSearchInfoSingleSearchActive.Size = new System.Drawing.Size(1077, 36);
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
            this.tpHelp.Size = new System.Drawing.Size(1149, 657);
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
            this.tbDesc.Size = new System.Drawing.Size(1113, 627);
            this.tbDesc.TabIndex = 6;
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
            this.Size = new System.Drawing.Size(1205, 745);
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
		internal System.Windows.Forms.CheckBox cbMultiDBSearchInfoSingleSearchActive;
		internal System.Windows.Forms.CheckBox cbMultiDBSearchInfoSearchFormActive;
		private System.Windows.Forms.RadioButton rbPWDisplayAlways;
		private System.Windows.Forms.RadioButton rbPWDisplayNever;
		private System.Windows.Forms.RadioButton rbPWDisplayEntryList;
        private System.Windows.Forms.TreeView tvHookedSearches;
    }
}
