namespace pdok4arcgis
{
  partial class CswSearchForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CswSearchForm));
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.newCatalogButton = new System.Windows.Forms.Button();
            this.deleteCatalogButton = new System.Windows.Forms.Button();
            this.saveCatalogButton = new System.Windows.Forms.Button();
            this.gbUsage = new System.Windows.Forms.GroupBox();
            this.cbAchtergrond = new System.Windows.Forms.CheckBox();
            this.cbRaadplegen = new System.Windows.Forms.CheckBox();
            this.cbDownload = new System.Windows.Forms.CheckBox();
            this.dgvCswRecords = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrganisation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServiceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetadataVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMapServerURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.helpTabPage = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.configureTabPage = new System.Windows.Forms.TabPage();
            this.catalogComboBox = new System.Windows.Forms.ComboBox();
            this.inCatalogLabel = new System.Windows.Forms.Label();
            this.catalogListBox = new System.Windows.Forms.ListBox();
            this.catalogListLabel = new System.Windows.Forms.Label();
            this.catalogProfileComboBox = new System.Windows.Forms.ComboBox();
            this.catalogUrlTextBox = new System.Windows.Forms.TextBox();
            this.catalogDisplayNameTextBox = new System.Windows.Forms.TextBox();
            this.catalogProfileLabel = new System.Windows.Forms.Label();
            this.catalogDisplayNameLabel = new System.Windows.Forms.Label();
            this.catalogUrlLabel = new System.Windows.Forms.Label();
            this.findTabPage = new System.Windows.Forms.TabPage();
            this.cbRefine = new System.Windows.Forms.CheckBox();
            this.cbSearchTextLO = new System.Windows.Forms.ComboBox();
            this.lblOrganisation = new System.Windows.Forms.Label();
            this.tbOrganisation = new System.Windows.Forms.TextBox();
            this.maxResultsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.resultsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.abstractTextBox = new System.Windows.Forms.TextBox();
            this.abstractLabel = new System.Windows.Forms.Label();
            this.findButton = new System.Windows.Forms.Button();
            this.maximumLabel = new System.Windows.Forms.Label();
            this.searchPhraseTextBox = new System.Windows.Forms.TextBox();
            this.findLabel = new System.Windows.Forms.Label();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuItemAdd2Map = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemViewMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemViewStyledMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemAdd2MapSLD = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.gbUsage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCswRecords)).BeginInit();
            this.helpTabPage.SuspendLayout();
            this.configureTabPage.SuspendLayout();
            this.findTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxResultsNumericUpDown)).BeginInit();
            this.resultsSplitContainer.Panel1.SuspendLayout();
            this.resultsSplitContainer.Panel2.SuspendLayout();
            this.resultsSplitContainer.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // newCatalogButton
            // 
            resources.ApplyResources(this.newCatalogButton, "newCatalogButton");
            this.newCatalogButton.Name = "newCatalogButton";
            this.tooltip.SetToolTip(this.newCatalogButton, resources.GetString("newCatalogButton.ToolTip"));
            this.newCatalogButton.UseVisualStyleBackColor = true;
            this.newCatalogButton.Click += new System.EventHandler(this.NewCatalogButton_Click);
            // 
            // deleteCatalogButton
            // 
            resources.ApplyResources(this.deleteCatalogButton, "deleteCatalogButton");
            this.deleteCatalogButton.Name = "deleteCatalogButton";
            this.tooltip.SetToolTip(this.deleteCatalogButton, resources.GetString("deleteCatalogButton.ToolTip"));
            this.deleteCatalogButton.UseVisualStyleBackColor = true;
            this.deleteCatalogButton.Click += new System.EventHandler(this.DeleteCatalogButton_Click);
            // 
            // saveCatalogButton
            // 
            resources.ApplyResources(this.saveCatalogButton, "saveCatalogButton");
            this.saveCatalogButton.Name = "saveCatalogButton";
            this.tooltip.SetToolTip(this.saveCatalogButton, resources.GetString("saveCatalogButton.ToolTip"));
            this.saveCatalogButton.UseVisualStyleBackColor = true;
            this.saveCatalogButton.Click += new System.EventHandler(this.SaveCatalogButton_Click);
            // 
            // gbUsage
            // 
            resources.ApplyResources(this.gbUsage, "gbUsage");
            this.gbUsage.Controls.Add(this.cbAchtergrond);
            this.gbUsage.Controls.Add(this.cbRaadplegen);
            this.gbUsage.Controls.Add(this.cbDownload);
            this.gbUsage.Name = "gbUsage";
            this.gbUsage.TabStop = false;
            // 
            // cbAchtergrond
            // 
            resources.ApplyResources(this.cbAchtergrond, "cbAchtergrond");
            this.cbAchtergrond.Name = "cbAchtergrond";
            this.cbAchtergrond.UseVisualStyleBackColor = true;
            // 
            // cbRaadplegen
            // 
            resources.ApplyResources(this.cbRaadplegen, "cbRaadplegen");
            this.cbRaadplegen.Name = "cbRaadplegen";
            this.cbRaadplegen.UseVisualStyleBackColor = true;
            // 
            // cbDownload
            // 
            resources.ApplyResources(this.cbDownload, "cbDownload");
            this.cbDownload.Name = "cbDownload";
            this.cbDownload.UseVisualStyleBackColor = true;
            // 
            // dgvCswRecords
            // 
            this.dgvCswRecords.AllowUserToAddRows = false;
            this.dgvCswRecords.AllowUserToDeleteRows = false;
            this.dgvCswRecords.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.dgvCswRecords, "dgvCswRecords");
            this.dgvCswRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCswRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colTitle,
            this.colUsage,
            this.colOrganisation,
            this.colServiceType,
            this.colMetadataVersion,
            this.colMapServerURL});
            this.dgvCswRecords.MultiSelect = false;
            this.dgvCswRecords.Name = "dgvCswRecords";
            this.dgvCswRecords.ReadOnly = true;
            this.dgvCswRecords.RowHeadersVisible = false;
            this.dgvCswRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCswRecords.ShowCellToolTips = false;
            this.tooltip.SetToolTip(this.dgvCswRecords, resources.GetString("dgvCswRecords.ToolTip"));
            this.dgvCswRecords.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCswRecords_CellMouseDown);
            this.dgvCswRecords.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCswRecords_DataBindingComplete);
            this.dgvCswRecords.SelectionChanged += new System.EventHandler(this.dgvCswRecords_SelectionChanged);
            this.dgvCswRecords.Sorted += new System.EventHandler(this.dgvCswRecords_Sorted);
            this.dgvCswRecords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvCswRecords_KeyDown);
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            resources.ApplyResources(this.colID, "colID");
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            // 
            // colTitle
            // 
            this.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colTitle.DataPropertyName = "Title";
            resources.ApplyResources(this.colTitle, "colTitle");
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            // 
            // colUsage
            // 
            this.colUsage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colUsage.DataPropertyName = "Usage";
            resources.ApplyResources(this.colUsage, "colUsage");
            this.colUsage.Name = "colUsage";
            this.colUsage.ReadOnly = true;
            // 
            // colOrganisation
            // 
            this.colOrganisation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colOrganisation.DataPropertyName = "ResponsibleOrganisation";
            resources.ApplyResources(this.colOrganisation, "colOrganisation");
            this.colOrganisation.Name = "colOrganisation";
            this.colOrganisation.ReadOnly = true;
            // 
            // colServiceType
            // 
            this.colServiceType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colServiceType.DataPropertyName = "Protocol";
            resources.ApplyResources(this.colServiceType, "colServiceType");
            this.colServiceType.Name = "colServiceType";
            this.colServiceType.ReadOnly = true;
            // 
            // colMetadataVersion
            // 
            this.colMetadataVersion.DataPropertyName = "MetadataVersion";
            resources.ApplyResources(this.colMetadataVersion, "colMetadataVersion");
            this.colMetadataVersion.Name = "colMetadataVersion";
            this.colMetadataVersion.ReadOnly = true;
            // 
            // colMapServerURL
            // 
            this.colMapServerURL.DataPropertyName = "MapServerURL";
            resources.ApplyResources(this.colMapServerURL, "colMapServerURL");
            this.colMapServerURL.Name = "colMapServerURL";
            this.colMapServerURL.ReadOnly = true;
            // 
            // helpTabPage
            // 
            this.helpTabPage.Controls.Add(this.webBrowser1);
            resources.ApplyResources(this.helpTabPage, "helpTabPage");
            this.helpTabPage.Name = "helpTabPage";
            this.helpTabPage.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            resources.ApplyResources(this.webBrowser1, "webBrowser1");
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // configureTabPage
            // 
            this.configureTabPage.Controls.Add(this.catalogComboBox);
            this.configureTabPage.Controls.Add(this.inCatalogLabel);
            this.configureTabPage.Controls.Add(this.catalogListBox);
            this.configureTabPage.Controls.Add(this.saveCatalogButton);
            this.configureTabPage.Controls.Add(this.deleteCatalogButton);
            this.configureTabPage.Controls.Add(this.newCatalogButton);
            this.configureTabPage.Controls.Add(this.catalogListLabel);
            this.configureTabPage.Controls.Add(this.catalogProfileComboBox);
            this.configureTabPage.Controls.Add(this.catalogUrlTextBox);
            this.configureTabPage.Controls.Add(this.catalogDisplayNameTextBox);
            this.configureTabPage.Controls.Add(this.catalogProfileLabel);
            this.configureTabPage.Controls.Add(this.catalogDisplayNameLabel);
            this.configureTabPage.Controls.Add(this.catalogUrlLabel);
            resources.ApplyResources(this.configureTabPage, "configureTabPage");
            this.configureTabPage.Name = "configureTabPage";
            this.configureTabPage.UseVisualStyleBackColor = true;
            this.configureTabPage.Enter += new System.EventHandler(this.ConfigureTabPage_Enter);
            // 
            // catalogComboBox
            // 
            resources.ApplyResources(this.catalogComboBox, "catalogComboBox");
            this.catalogComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.catalogComboBox.FormattingEnabled = true;
            this.catalogComboBox.Name = "catalogComboBox";
            this.catalogComboBox.Sorted = true;
            // 
            // inCatalogLabel
            // 
            resources.ApplyResources(this.inCatalogLabel, "inCatalogLabel");
            this.inCatalogLabel.Name = "inCatalogLabel";
            // 
            // catalogListBox
            // 
            resources.ApplyResources(this.catalogListBox, "catalogListBox");
            this.catalogListBox.FormattingEnabled = true;
            this.catalogListBox.Name = "catalogListBox";
            this.catalogListBox.Sorted = true;
            this.catalogListBox.SelectedIndexChanged += new System.EventHandler(this.CatalogListBox_SelectedIndexChanged);
            // 
            // catalogListLabel
            // 
            resources.ApplyResources(this.catalogListLabel, "catalogListLabel");
            this.catalogListLabel.Name = "catalogListLabel";
            // 
            // catalogProfileComboBox
            // 
            resources.ApplyResources(this.catalogProfileComboBox, "catalogProfileComboBox");
            this.catalogProfileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.catalogProfileComboBox.FormattingEnabled = true;
            this.catalogProfileComboBox.Name = "catalogProfileComboBox";
            this.catalogProfileComboBox.Sorted = true;
            this.catalogProfileComboBox.SelectedIndexChanged += new System.EventHandler(this.CatalogProfileComboBox_SelectedIndexChanged);
            // 
            // catalogUrlTextBox
            // 
            resources.ApplyResources(this.catalogUrlTextBox, "catalogUrlTextBox");
            this.catalogUrlTextBox.Name = "catalogUrlTextBox";
            this.catalogUrlTextBox.TextChanged += new System.EventHandler(this.CatalogUrlTextBox_TextChanged);
            this.catalogUrlTextBox.MouseHover += new System.EventHandler(this.CatalogUrlTextBox_MouseHover);
            // 
            // catalogDisplayNameTextBox
            // 
            resources.ApplyResources(this.catalogDisplayNameTextBox, "catalogDisplayNameTextBox");
            this.catalogDisplayNameTextBox.Name = "catalogDisplayNameTextBox";
            this.catalogDisplayNameTextBox.TextChanged += new System.EventHandler(this.CatalogDisplayNameTextBox_TextChanged);
            // 
            // catalogProfileLabel
            // 
            resources.ApplyResources(this.catalogProfileLabel, "catalogProfileLabel");
            this.catalogProfileLabel.Name = "catalogProfileLabel";
            // 
            // catalogDisplayNameLabel
            // 
            resources.ApplyResources(this.catalogDisplayNameLabel, "catalogDisplayNameLabel");
            this.catalogDisplayNameLabel.Name = "catalogDisplayNameLabel";
            // 
            // catalogUrlLabel
            // 
            resources.ApplyResources(this.catalogUrlLabel, "catalogUrlLabel");
            this.catalogUrlLabel.Name = "catalogUrlLabel";
            // 
            // findTabPage
            // 
            this.findTabPage.Controls.Add(this.cbRefine);
            this.findTabPage.Controls.Add(this.cbSearchTextLO);
            this.findTabPage.Controls.Add(this.gbUsage);
            this.findTabPage.Controls.Add(this.lblOrganisation);
            this.findTabPage.Controls.Add(this.tbOrganisation);
            this.findTabPage.Controls.Add(this.maxResultsNumericUpDown);
            this.findTabPage.Controls.Add(this.resultsSplitContainer);
            this.findTabPage.Controls.Add(this.findButton);
            this.findTabPage.Controls.Add(this.maximumLabel);
            this.findTabPage.Controls.Add(this.searchPhraseTextBox);
            this.findTabPage.Controls.Add(this.findLabel);
            this.findTabPage.Controls.Add(this.resultsLabel);
            resources.ApplyResources(this.findTabPage, "findTabPage");
            this.findTabPage.Name = "findTabPage";
            this.findTabPage.UseVisualStyleBackColor = true;
            this.findTabPage.Enter += new System.EventHandler(this.FindTabPage_Enter);
            // 
            // cbRefine
            // 
            resources.ApplyResources(this.cbRefine, "cbRefine");
            this.cbRefine.Name = "cbRefine";
            this.cbRefine.UseVisualStyleBackColor = true;
            // 
            // cbSearchTextLO
            // 
            resources.ApplyResources(this.cbSearchTextLO, "cbSearchTextLO");
            this.cbSearchTextLO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchTextLO.FormattingEnabled = true;
            this.cbSearchTextLO.Items.AddRange(new object[] {
            resources.GetString("cbSearchTextLO.Items"),
            resources.GetString("cbSearchTextLO.Items1")});
            this.cbSearchTextLO.Name = "cbSearchTextLO";
            // 
            // lblOrganisation
            // 
            resources.ApplyResources(this.lblOrganisation, "lblOrganisation");
            this.lblOrganisation.Name = "lblOrganisation";
            // 
            // tbOrganisation
            // 
            resources.ApplyResources(this.tbOrganisation, "tbOrganisation");
            this.tbOrganisation.Name = "tbOrganisation";
            // 
            // maxResultsNumericUpDown
            // 
            resources.ApplyResources(this.maxResultsNumericUpDown, "maxResultsNumericUpDown");
            this.maxResultsNumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.maxResultsNumericUpDown.Name = "maxResultsNumericUpDown";
            this.maxResultsNumericUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.maxResultsNumericUpDown.Leave += new System.EventHandler(this.MaxResultsNumericUpDown_Leave);
            // 
            // resultsSplitContainer
            // 
            resources.ApplyResources(this.resultsSplitContainer, "resultsSplitContainer");
            this.resultsSplitContainer.Name = "resultsSplitContainer";
            // 
            // resultsSplitContainer.Panel1
            // 
            this.resultsSplitContainer.Panel1.Controls.Add(this.dgvCswRecords);
            // 
            // resultsSplitContainer.Panel2
            // 
            this.resultsSplitContainer.Panel2.Controls.Add(this.abstractTextBox);
            this.resultsSplitContainer.Panel2.Controls.Add(this.abstractLabel);
            this.resultsSplitContainer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResultsSplitContainer_KeyDown);
            // 
            // abstractTextBox
            // 
            resources.ApplyResources(this.abstractTextBox, "abstractTextBox");
            this.abstractTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.abstractTextBox.Name = "abstractTextBox";
            this.abstractTextBox.ReadOnly = true;
            // 
            // abstractLabel
            // 
            resources.ApplyResources(this.abstractLabel, "abstractLabel");
            this.abstractLabel.Name = "abstractLabel";
            // 
            // findButton
            // 
            resources.ApplyResources(this.findButton, "findButton");
            this.findButton.Name = "findButton";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // maximumLabel
            // 
            resources.ApplyResources(this.maximumLabel, "maximumLabel");
            this.maximumLabel.Name = "maximumLabel";
            // 
            // searchPhraseTextBox
            // 
            resources.ApplyResources(this.searchPhraseTextBox, "searchPhraseTextBox");
            this.searchPhraseTextBox.Name = "searchPhraseTextBox";
            this.searchPhraseTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchPhraseTextBox_KeyDown);
            // 
            // findLabel
            // 
            resources.ApplyResources(this.findLabel, "findLabel");
            this.findLabel.Name = "findLabel";
            // 
            // resultsLabel
            // 
            resources.ApplyResources(this.resultsLabel, "resultsLabel");
            this.resultsLabel.Name = "resultsLabel";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItemAdd2Map,
            this.tsMenuItemViewMetadata,
            this.tsMenuItemViewStyledMetadata,
            this.tsMenuItemAdd2MapSLD});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // tsMenuItemAdd2Map
            // 
            this.tsMenuItemAdd2Map.Name = "tsMenuItemAdd2Map";
            resources.ApplyResources(this.tsMenuItemAdd2Map, "tsMenuItemAdd2Map");
            this.tsMenuItemAdd2Map.Click += new System.EventHandler(this.tsMenuItemAdd2Map_Click);
            // 
            // tsMenuItemViewMetadata
            // 
            this.tsMenuItemViewMetadata.Name = "tsMenuItemViewMetadata";
            resources.ApplyResources(this.tsMenuItemViewMetadata, "tsMenuItemViewMetadata");
            this.tsMenuItemViewMetadata.Click += new System.EventHandler(this.tsMenuItemViewMetadata_Click);
            // 
            // tsMenuItemViewStyledMetadata
            // 
            this.tsMenuItemViewStyledMetadata.Name = "tsMenuItemViewStyledMetadata";
            resources.ApplyResources(this.tsMenuItemViewStyledMetadata, "tsMenuItemViewStyledMetadata");
            this.tsMenuItemViewStyledMetadata.Click += new System.EventHandler(this.tsMenuItemViewStyledMetadata_Click);
            // 
            // tsMenuItemAdd2MapSLD
            // 
            this.tsMenuItemAdd2MapSLD.Name = "tsMenuItemAdd2MapSLD";
            resources.ApplyResources(this.tsMenuItemAdd2MapSLD, "tsMenuItemAdd2MapSLD");
            this.tsMenuItemAdd2MapSLD.Click += new System.EventHandler(this.tsMenuItemAdd2MapSLD_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.findTabPage);
            this.mainTabControl.Controls.Add(this.configureTabPage);
            this.mainTabControl.Controls.Add(this.helpTabPage);
            resources.ApplyResources(this.mainTabControl, "mainTabControl");
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            // 
            // CswSearchForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.mainTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CswSearchForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CswSearchForm_KeyDown);
            this.gbUsage.ResumeLayout(false);
            this.gbUsage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCswRecords)).EndInit();
            this.helpTabPage.ResumeLayout(false);
            this.configureTabPage.ResumeLayout(false);
            this.configureTabPage.PerformLayout();
            this.findTabPage.ResumeLayout(false);
            this.findTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxResultsNumericUpDown)).EndInit();
            this.resultsSplitContainer.Panel1.ResumeLayout(false);
            this.resultsSplitContainer.Panel2.ResumeLayout(false);
            this.resultsSplitContainer.Panel2.PerformLayout();
            this.resultsSplitContainer.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

      private System.Windows.Forms.ToolTip tooltip;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.TabPage helpTabPage;
      private System.Windows.Forms.TabPage configureTabPage;
      private System.Windows.Forms.ListBox catalogListBox;
      private System.Windows.Forms.Button saveCatalogButton;
      private System.Windows.Forms.Button deleteCatalogButton;
      private System.Windows.Forms.Button newCatalogButton;
      private System.Windows.Forms.Label catalogListLabel;
      private System.Windows.Forms.ComboBox catalogProfileComboBox;
      private System.Windows.Forms.TextBox catalogUrlTextBox;
      private System.Windows.Forms.TextBox catalogDisplayNameTextBox;
      private System.Windows.Forms.Label catalogProfileLabel;
      private System.Windows.Forms.Label catalogDisplayNameLabel;
      private System.Windows.Forms.Label catalogUrlLabel;
      private System.Windows.Forms.TabPage findTabPage;
      private System.Windows.Forms.NumericUpDown maxResultsNumericUpDown;
      private System.Windows.Forms.SplitContainer resultsSplitContainer;
      private System.Windows.Forms.DataGridView dgvCswRecords;
      private System.Windows.Forms.TextBox abstractTextBox;
      private System.Windows.Forms.Label abstractLabel;
      private System.Windows.Forms.Button findButton;
      private System.Windows.Forms.Label maximumLabel;
      private System.Windows.Forms.TextBox searchPhraseTextBox;
      private System.Windows.Forms.Label findLabel;
      private System.Windows.Forms.Label resultsLabel;
      private System.Windows.Forms.TabControl mainTabControl;
      private System.Windows.Forms.TextBox tbOrganisation;
      private System.Windows.Forms.Label lblOrganisation;
      private System.Windows.Forms.GroupBox gbUsage;
      private System.Windows.Forms.CheckBox cbAchtergrond;
      private System.Windows.Forms.CheckBox cbRaadplegen;
      private System.Windows.Forms.CheckBox cbDownload;
      private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
      private System.Windows.Forms.ToolStripMenuItem tsMenuItemAdd2Map;
      private System.Windows.Forms.ToolStripMenuItem tsMenuItemViewMetadata;
      private System.Windows.Forms.ToolStripMenuItem tsMenuItemAdd2MapSLD;
      private System.Windows.Forms.ToolStripMenuItem tsMenuItemViewStyledMetadata;
      private System.Windows.Forms.ComboBox cbSearchTextLO;
      private System.Windows.Forms.ComboBox catalogComboBox;
      private System.Windows.Forms.Label inCatalogLabel;
      private System.Windows.Forms.CheckBox cbRefine;
      private System.Windows.Forms.DataGridViewTextBoxColumn colID;
      private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
      private System.Windows.Forms.DataGridViewTextBoxColumn colUsage;
      private System.Windows.Forms.DataGridViewTextBoxColumn colOrganisation;
      private System.Windows.Forms.DataGridViewTextBoxColumn colServiceType;
      private System.Windows.Forms.DataGridViewTextBoxColumn colMetadataVersion;
      private System.Windows.Forms.DataGridViewTextBoxColumn colMapServerURL;
      private System.Windows.Forms.WebBrowser webBrowser1;
  }
}
