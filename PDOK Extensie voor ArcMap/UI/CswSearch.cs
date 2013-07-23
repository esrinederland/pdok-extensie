using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;

using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Resources;

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geoprocessing;

// using ESRI.ArcGIS.Geodatabase;
using Microsoft.Win32;
using ESRI.ArcGIS.Geodatabase;
using System.Net;
using Ionic.Zip;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
//using BruTileArcGIS;
using System.Globalization;



namespace pdok4arcgis
{
    /// <summary>
    /// CSW search control as a dockable window.
    /// </summary>
    [ComVisible(true)]
    [Guid("AB9C4357-4932-4543-B37A-A08EF55EAFC7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("pdok4arcmap.CswSearch")]
    public partial class CswSearchForm : Form, IDockableWindowDef
    {
        #region Private Variable(s)
        private IApplication m_application;

        private CswManager _cswManager = new CswManager();
        private CswProfiles _cswProfiles;
        private CswCatalogs _cswCatalogs;
        private bool _inited = false;
        private string _mapServerUrl = null;
        private bool _isWriteLogs = false;
        private string _logFilePath = "";

        // Search Tab Variables
        private CswSearchRequest _searchRequest;
      //  private string _xsltPrepareMetadataFilePath;
     //   private XslCompiledTransform _xsltPrepare;
     //   private string _xsltMetadataToHtmlFullFilePath;
    //    private XslCompiledTransform _xsltFull;
        private string styledRecordResponse = null;

        // Configure Tab Variables
        private ArrayList _catalogList;
        private ArrayList _profileList;
        private bool _newClicked = false;
        private bool _isCatalogListDirty = false;
      

        // Help Tab Variables
        private string _helpFilePath="";
        private bool _isHelpFileLoaded = false;
        #endregion

        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);
            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxDockableWindows.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxDockableWindows.Unregister(regKey);

        }

        #endregion
        #endregion

        #region Constructor/Destructor Function(s)

        /// <summary>
        /// Constructor for CswSearch control
        /// </summary>
        public CswSearchForm()
        {
            InitializeComponent();

            try
            {
              //  _xsltMetadataToHtmlFullFilePath = System.IO.Path.Combine(Utils.GetSpecialFolderPath(SpecialFolder.ConfigurationFiles), "metadata_to_html_full.xsl");
              //  _xsltPrepareMetadataFilePath = System.IO.Path.Combine(Utils.GetSpecialFolderPath(SpecialFolder.ConfigurationFiles), "xml_prepare.xslt");
                _helpFilePath = System.IO.Path.Combine(Utils.GetSpecialFolderPath(SpecialFolder.Help), "help.htm");
                this.webBrowser1.Url = new Uri(String.Format("file:///{0}", _helpFilePath));

                String logFilePath = Utils.GetSpecialFolderPath(SpecialFolder.LogFiles);
                if (logFilePath != null && logFilePath.Trim().Length > 0)
                {
                    _isWriteLogs = true;
                    _logFilePath = logFilePath + "//PDOK4ArcMap.log";
                    Utils.setLogFile(_logFilePath);
                }

                InitMyComponents();
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(StringResources.LoadFindServicesFailed + "\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Init components for "Find Services" dockable window
        /// </summary>
        private void InitMyComponents()
        {
            if (!_inited)
            {
                // version info
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //    productBuildNoLabel.Text = "(" + StringResources.BuildNo + " " + fvi.ProductVersion + ")";

                // note: To save loading time, help Tab is not loaded.
                _inited = (LoadSearchTab() && LoadConfigTab());
                if (_inited && catalogComboBox.Items.Count > 0) catalogComboBox.SelectedIndex = 0;
                if (_inited) cbSearchTextLO.SelectedIndex = 0;
                //if (_inited) cbCriteriaLO.SelectedIndex = 0;
            }
        }
         
        #endregion

        #region IDockableWindowDef Members

        string IDockableWindowDef.Caption
        {
            get
            {
                return StringResources.ApplicationCaption;
            }
        }

        int IDockableWindowDef.ChildHWND
        {
            get { return this.Handle.ToInt32(); }
        }

        string IDockableWindowDef.Name
        {
            get
            {
                return this.Name;
            }
        }

        void IDockableWindowDef.OnCreate(object hook)
        {
            m_application = hook as IApplication;
        }

        void IDockableWindowDef.OnDestroy()
        {
            //TODO: Release resources and call dispose of any ActiveX control initialized
            m_application = null;
        }

        object IDockableWindowDef.UserData
        {
            get { return null; }
        }

        #endregion

        #region Search Tab Function(s)

        /// <summary>
        /// if there is a change on catalog list, we should refresh 
        /// the csw catalog dropdown control on Search tab as well
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void FindTabPage_Enter(object sender, EventArgs e)
        {
            if (_inited && _isCatalogListDirty)
            {
                catalogComboBox.DataSource = catalogListBox.DataSource;
                catalogComboBox.DisplayMember = catalogListBox.DisplayMember;
                catalogComboBox.ValueMember = catalogListBox.ValueMember;

                _isCatalogListDirty = false;
            }

        }

        /// <summary>
        /// Search CSW catalog with the criteria defined by user
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void FindButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            try
            {

                // Todo: add more validation. only minimum validation at this point.
                if (catalogComboBox.SelectedIndex == -1)
                {
                    ShowErrorMessageBox(StringResources.PleaseSelectACswCatalog);
                    return;
                }

                CswCatalog catalog = (CswCatalog)catalogComboBox.SelectedItem;
                if (catalog == null) { throw new NullReferenceException(StringResources.CswCatalogIsNull); }

                // reset GUI for search results
                ResetSearchResultsGUI();
       

                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                if (!catalog.IsConnected())
                {
                    string errMsg = "";
                    try { catalog.Connect(_isWriteLogs); 
                    }
                    catch (Exception ex) { errMsg = ex.Message; }
                    if (!catalog.IsConnected())
                    {
                         sb.AppendLine("Failed to connect to Catalog");
                        ShowErrorMessageBox (StringResources.ConnectToCatalogFailed + "\r\n" + errMsg);
                        return;
                    }
                }

                // todo: need paging maganism. update SearchCriteria.StartPoistion

                // generate search criteria
                CswSearchCriteria searchCriteria = new CswSearchCriteria();
                searchCriteria.SearchText = searchPhraseTextBox.Text;
                searchCriteria.SearchTextLogicalOperator = (CswSearchCriteria.LogicalOperator)cbSearchTextLO.SelectedIndex;
                searchCriteria.FilterLogicalOperator = CswSearchCriteria.LogicalOperator.And;
                searchCriteria.Organisation = tbOrganisation.Text;
                if (cbDownload.Checked) searchCriteria.TypesOfUse.Add(cbDownload.Text);
                if (cbRaadplegen.Checked) searchCriteria.TypesOfUse.Add(cbRaadplegen.Text);
                if (cbAchtergrond.Checked) searchCriteria.TypesOfUse.Add(cbAchtergrond.Text);
                searchCriteria.StartPosition = 1;
                searchCriteria.MaxRecords = (int)maxResultsNumericUpDown.Value;
                //searchCriteria.LiveDataAndMapOnly = (liveDataAndMapsOnlyCheckBox.Checked);
                searchCriteria.RefinePreviousFilter = ((cbRefine.Checked) && (cbRefine.Enabled));

                //if (useCurrentExtentCheckBox.Checked) 
                //{
                //    try { searchCriteria.Envelope = CurrentMapViewExtent(); }
                //    catch (Exception ex)
                //    {
                //        String errMsg = StringResources.GetCurrentExtentFailed + "\r\n" +
                //                            ex.Message + "\r\n" + "\r\n" +
                //                            StringResources.UncheckCurrentExtentAndTryAgain;

                //        sb.AppendLine(errMsg);
                //        ShowErrorMessageBox(errMsg);
                //        return;
                //    }
                //}
                //else
                //{
                    searchCriteria.Envelope = null;
                //}

                // search
                if (_searchRequest == null) { _searchRequest = new CswSearchRequest(_isWriteLogs); }
                _searchRequest.Catalog = catalog;
                _searchRequest.Criteria = searchCriteria;
                _searchRequest.Search();
                ShowSearchResults(sb);
            }
            catch (Exception ex)
            {
                sb.AppendLine(ex.Message);
                ShowErrorMessageBox (ex.Message);
            }
            finally
            {
                if (_isWriteLogs)
                    Utils.writeFile(sb.ToString());
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private void ShowSearchResults(StringBuilder sb)
        {

            CswSearchResponse response = _searchRequest.Response();
            // show search results
            ArrayList alRecords = CswObjectsToArrayList(response.Records);
            if (response.Records.Count > 0)
            {
                dgvCswRecords.AutoGenerateColumns = false;
                SortableBindingList<CswRecord> cswrecords = CswObjectsToSortableBindingList(response.Records);
                dgvCswRecords.DataSource = cswrecords;
            }
            else
            {
                cbRefine.Checked = cbRefine.Enabled = false;
                sb.AppendLine(StringResources.NoRecordsFound);
                ShowErrorMessageBox(StringResources.NoRecordsFound);
            }
            //NumberOfRecordsMatched only set when implemented by ...GetRecords_response.xslt
            if (response.Records.NumberOfRecordsMatched != null)
            {
                if (alRecords.Count < response.Records.NumberOfRecordsMatched)
                {
                    resultsLabel.Text = String.Format("{0} ({1}/{2}) - {3}", StringResources.SearchResultsLabelText, alRecords.Count, response.Records.NumberOfRecordsMatched, StringResources.SearchResultsLabelNotAllDisplayed);
                    resultsLabel.Font = new Font(resultsLabel.Font, FontStyle.Bold);
                }
                else
                {
                    resultsLabel.Font = new Font(resultsLabel.Font, FontStyle.Regular);
                    resultsLabel.Text = String.Format("{0} ({1}/{2})", StringResources.SearchResultsLabelText, alRecords.Count, response.Records.NumberOfRecordsMatched);
                }
            }
            else
            {
                resultsLabel.Font = new Font(resultsLabel.Font, FontStyle.Regular);
                resultsLabel.Text = String.Format("{0} ({1})", StringResources.SearchResultsLabelText, alRecords.Count);
            }
        }

        /// <summary>
        /// Get current view extent (in geographical coordinate system). 
        /// </summary>
        /// <remarks>
        /// If error occurred, exception would be thrown.
        /// </remarks>
        /// <returns>view extent as an envelope object</returns>
        private pdok4arcgis.Envelope CurrentMapViewExtent()
        {
            pdok4arcgis.Envelope envCurrentViewExent;

            IMxDocument mxDoc = (IMxDocument)m_application.Document;
            IMap map = mxDoc.FocusMap;
            IActiveView activeView = (IActiveView)map;
            IEnvelope extent = activeView.Extent;
            if (extent == null) return null;

            ISpatialReference CurrentMapSpatialReference = extent.SpatialReference;
            if (CurrentMapSpatialReference == null) throw new Exception(StringResources.SpatialReferenceNotDefined);
            if (CurrentMapSpatialReference is IUnknownCoordinateSystem)
            {
                // unknown cooridnate system
                throw new Exception(StringResources.UnknownCoordinateSystem);
            }
            else if (CurrentMapSpatialReference is IGeographicCoordinateSystem)
            {
                // already in geographical coordinate system, reuse coordinate values
                envCurrentViewExent = new pdok4arcgis.Envelope(extent.XMin, extent.YMin, extent.XMax, extent.YMax);
            }
            else if (CurrentMapSpatialReference is IProjectedCoordinateSystem)
            {
                // project to geographical coordinate system
                ISpatialReferenceFactory srFactory = new SpatialReferenceEnvironmentClass();
                IGeographicCoordinateSystem gcs = srFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_NAD1983);
                gcs.SetFalseOriginAndUnits(-180, -90, 1000000);
                extent.Project(gcs);
                envCurrentViewExent = new pdok4arcgis.Envelope(extent.XMin, extent.YMin, extent.XMax, extent.YMax);
            }
            else
            {
                ShowErrorMessageBox(StringResources.UnsupportedCoordinateSystem);
                envCurrentViewExent = null;
            }

            return envCurrentViewExent;
        }

        /// <summary>
        /// Fire up "search" after user press "Enter" key in the textbox
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void SearchPhraseTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                FindButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Refresh GUI when a new catalog is selected 
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void CatalogComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // reset GUI for search result
                ResetSearchResultsGUI();

                // update GUI for search criteria
                if (catalogComboBox.SelectedIndex > -1)
                {
                    CswCatalog catalog = (CswCatalog)catalogComboBox.SelectedItem;
                    if (catalog == null) { throw new NullReferenceException(StringResources.CswCatalogIsNull); }

                    // update UI
                    //useCurrentExtentCheckBox.Enabled = catalog.Profile.SupportSpatialQuery;
                    //if (!catalog.Profile.SupportSpatialQuery) useCurrentExtentCheckBox.Checked = false;
                    //liveDataAndMapsOnlyCheckBox.Enabled = catalog.Profile.SupportContentTypeQuery;
                    //if (!catalog.Profile.SupportContentTypeQuery) liveDataAndMapsOnlyCheckBox.Checked = false;
                    tsMenuItemViewStyledMetadata.Enabled = !String.IsNullOrEmpty(catalog.Profile.XsltStyledResponse);
                }
                else
                {
                    // no catalog was selected
                    //useCurrentExtentCheckBox.Checked = false;
                    //useCurrentExtentCheckBox.Enabled = false;
                    //liveDataAndMapsOnlyCheckBox.Checked = false;
                    //liveDataAndMapsOnlyCheckBox.Enabled = false;
                    tsMenuItemViewStyledMetadata.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message);
            }
        }

        /// <summary>
        /// Reset the maximum number of results to integer value
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void MaxResultsNumericUpDown_Leave(object sender, EventArgs e)
        {
            // reset the Maximum recs to integer value
            maxResultsNumericUpDown.Value = (int)maxResultsNumericUpDown.Value;
        }


        /// <summary>
        /// Event handler for "Tool Strip" mouse enter event.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        /* 
           YZ 05/21/2007: don't remove. it's used to get around the issue related to 
           toolstrip not raising button clicked event when 
           inheriting CswSearch from Form instead of UserControl
         */
        private void ToolstripButton_MouseEnter(object sender, EventArgs e)
        {
            m_application.Visible = true;
        }

        /// <summary>
        /// Load components for Search Tab
        /// </summary>
        /// <returns>true if successful; false if error occurred. Exception shall be raised if there was any.</returns>
        private bool LoadSearchTab()
        {
            ResetSearchResultsGUI();

            // load CSW Profiles
            try
            {
                _cswProfiles = _cswManager.loadProfile();
                if (_cswProfiles == null) { throw new NullReferenceException(); }
            }
            catch (Exception ex)
            {
                throw new Exception(StringResources.LoadProfilesFailed + "\r\n" + ex.Message, ex);
            }

            // load CSW Catalogs
            try
            {
                _cswCatalogs = _cswManager.loadCatalog();
                if (_cswCatalogs == null) { throw new NullReferenceException(); }

                _catalogList = CswObjectsToArrayList(_cswCatalogs);
                catalogComboBox.BeginUpdate();
                catalogComboBox.DataSource = _catalogList;
                catalogComboBox.DisplayMember = "Name";
                catalogComboBox.ValueMember = "ID";
                catalogComboBox.SelectedIndex = -1;
                catalogComboBox.EndUpdate();
            }
            catch (Exception ex)
            {
                throw new Exception(StringResources.LoadCatalogsFailed + "\r\n" + ex.Message, ex);
            }

            return true;
        }

        /// <summary>
        /// Reset GUI for search results, including listbox, search result label, buttons, abstracts, etc.
        /// </summary>
        private void ResetSearchResultsGUI()
        {
            resultsLabel.Text= StringResources.SearchResultsLabelText;
            resultsLabel.Font = new Font(resultsLabel.Font, FontStyle.Regular);
            resultsLabel.Refresh();
            dgvCswRecords.DataSource = null;

            abstractTextBox.Text = "";

            // GUI update for buttons
            tsMenuItemAdd2Map.Enabled = false;
            tsMenuItemViewMetadata.Enabled = false;
            tsMenuItemAdd2MapSLD.Enabled = false;
            
        }

        /// <summary>
        /// Retrieve metadata for the selected record from server, then display it in metadata viwer. 
        /// If failed to retrieve metadata froms erver, display a detailed message including HTTP response from server.
        /// </summary>
        private void tsMenuItemViewMetadata_Click(object sender, EventArgs e)
        {
            ViewMetadata(false);
        }

        private void ViewMetadata(bool bApplyTransform)
        {

            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                CswRecord record = (CswRecord)dgvCswRecords.CurrentRow.DataBoundItem;

                // retrieve metadata
                XmlDocument xmlDoc = RetrieveSelectedMetadataFromCatalog(bApplyTransform);
                if (xmlDoc == null && styledRecordResponse == null) return;

                string tmpFilePath = "";
                if (xmlDoc != null && styledRecordResponse == null)
                {
                    // display metadata in XML format
                    tmpFilePath = GenerateTempFilename("Meta", "xml");
                    XmlWriter xmlWriter = new XmlTextWriter(tmpFilePath, Encoding.UTF8);
                    XmlNode binaryNode = xmlDoc.GetElementsByTagName("Binary")[0];
                    if (binaryNode != null)
                    {
                        XmlNode enclosureNode = xmlDoc.GetElementsByTagName("Enclosure")[0];
                        if (enclosureNode != null)
                            binaryNode.RemoveChild(enclosureNode);
                    }

                    String outputStr = xmlDoc.InnerXml.Replace("utf-16", "utf-8");
                    xmlWriter.WriteRaw(outputStr);
                    xmlWriter.Close();
                }
                else if (xmlDoc == null && styledRecordResponse != null
                    && styledRecordResponse.Trim().Length > 0)
                {
                    // display metadata in XML format
                    tmpFilePath = GenerateTempFilename("Meta", "html");
                    FileInfo fileInfo = new FileInfo(tmpFilePath);
                    System.IO.FileStream fileStream = fileInfo.Create();
                    StreamWriter sr = new StreamWriter(fileStream);
                    sr.Write(styledRecordResponse);
                    sr.Close();
                    fileStream.Close();
                    styledRecordResponse = null;
                }

                // pop up a metadata viwer displaying the metadata as HTML
                FormViewMetadata frmViewMetadata = new FormViewMetadata();
                frmViewMetadata.FormClosed += new FormClosedEventHandler(RemoveTempFileAfterMetadataViewerClosed);
                frmViewMetadata.MetadataTitle = record.Title;
                // frmViewMetadata.WindowState = FormWindowState.Maximized;
                frmViewMetadata.Navigate(tmpFilePath);
                frmViewMetadata.Show();
                frmViewMetadata.Activate();

                // note: temp file will be deleted when metadata viwer closes. 
                //       see "RemoveTempFileAfterMetadataViewerClosed()"
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message);

            }
            finally
            {
                styledRecordResponse = null;
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Event handler for Metadata Viwer form closed event. 
        /// To delete temporary metadata file after the viewer is closed.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void RemoveTempFileAfterMetadataViewerClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                FormViewMetadata frmViewMetadata = (FormViewMetadata)(sender);
                if (frmViewMetadata != null) { System.IO.File.Delete(frmViewMetadata.MetadataFilePath); }
            }
            catch
            {
                // ignore, if error occurrs when deleting temp file
            }
        }

        /// <summary>
        /// Normalize a string for filename.
        /// </summary>
        /// <param name="filename">File name string to be normalized</param>
        /// <returns>Normalized file name string</returns>
        private string NormalizeFilename(string filename)
        {
            // Get a list of invalid file characters.
            char[] invalidFilenameChars = System.IO.Path.GetInvalidFileNameChars();

            // replace invalid characters with ' ' char
            for (int i = 0; i < invalidFilenameChars.GetLength(0); i++)
            {
                filename = filename.Replace(invalidFilenameChars[i], ' ');
            }
            return filename;
        }

        /// <summary>
        /// Retrieve metadta for the selected record from server. 
        /// Then add the live data or maps that the metadta describes to ArcMap as a layer.
        /// </summary>
        private void AddToMap_Clicked()
        {
            AddSelectedRecordToMap();
        }
        private void AddSelectedRecordToMap()
        {
            AddSelectedRecordToMap(String.Empty);
        }
        private void AddSelectedRecordToMap(string sldForWMS)
        {
            ////DvD 20120522: Started refactoring... but abandonned due to expected impact
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                CswRecord selectedRecord = (CswRecord)dgvCswRecords.CurrentRow.DataBoundItem;

                if (selectedRecord == null) throw new NullReferenceException(StringResources.CswRecordIsNull);

                if (selectedRecord.MapServerURL == null || selectedRecord.MapServerURL.Trim().Length == 0)
                {
                    // retrieve metadata
                    //DvD 20120522: duplicate code: RetrieveSelectedMetadataFromCatalog
                    RetrieveAddToMapInfoFromCatalog();
                    //Update the record with info from the GetRecordByID search request (in case the GetRecords_Response did not parse the url)
                    //(this could be done much neater ... but will result in serious refactoring which has not been accounted for currently 20120522 DvD)
                    selectedRecord.MapServerURL = _searchRequest.GetMapServerUrl();
                }
                if (selectedRecord.MapServerURL == null || selectedRecord.MapServerURL.Trim().Length == 0)
                {
                    ShowErrorMessageBox(StringResources.NoUrlInMetadata);
                    return;
                }
                AddMapServiceLayer(selectedRecord,sldForWMS);
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }
        private void AddMapServiceLayer(CswRecord cswrecord, string sldForWMS)
        {
            MapServiceInfo msinfo = new MapServiceInfo();
            msinfo.Server = cswrecord.MapServerURL;
            msinfo.Service = cswrecord.ServiceName;
            msinfo.ServiceType = cswrecord.Protocol;
            CswProfile.ParseServiceInfoFromUrl(msinfo, cswrecord.MapServerURL, cswrecord.Protocol);
            switch (cswrecord.Protocol.ToUpper())
            {
                case "WMS":
                case "OGC:WMS":
                    AddLayerWMS(msinfo, true,sldForWMS);
                    break;
                case "WCS":
                case "OGC:WCS":
                    AddLayerWCS(cswrecord.MapServerURL);
                    break;
                case "OGC:WMTS"://"TMS":
                case "UKST":
                    AddLayerWMTS(cswrecord);//AddLayerTMS(cswrecord);
                    break;
                case "WFS":
                case "OGC:WFS":
                    AddLayerWFS(cswrecord);
                    break;
                case "AGS":
                    string url = String.Empty;
                    url = cswrecord.MapServerURL;
                    if (url.ToLower().Contains("arcgis/rest"))
                    {
                        url = url + "?f=lyr";
                        CswClient client = new CswClient();
                        AddAGSService(client.SubmitHttpRequest("DOWNLOAD", url, ""));
                    }
                    else
                    {
                        AddAGSService(url);
                    }
                    break;
                case "AIMS":
                    AddLayerArcIMS(msinfo);
                    break;
                default: 
                    MessageBox.Show(String.Format("Adding data of type {0} is not implemented yet.",cswrecord.Protocol.ToUpper()));
                    break;
            }
        }
        private void AddLayerTMS(CswRecord cswrecord)
        {
        }
        private void AddLayerWMTS(CswRecord cswrecord)
        {
            //string url = @"http://gisdemo.agro.nl/wmts/PDOK_wmts_met_bbox_groot.xml";
            //string url = "http://acceptatie." + cswrecord.MapServerURL.Substring(7, cswrecord.MapServerURL.Length - 7);
            string url = cswrecord.MapServerURL;
            int lastIndex = cswrecord.MapServerURL.LastIndexOf('/') + 1;
            int length = cswrecord.MapServerURL.IndexOf('?') - lastIndex;
            string layerTitle = cswrecord.MapServerURL.Substring(lastIndex, length);

            try
            {
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("URL", url);

                IMxDocument mxDoc = (IMxDocument)m_application.Document;
                IMap map = (IMap)mxDoc.FocusMap;
                IActiveView activeView = (IActiveView)map;

                IWMTSConnectionFactory wmtsconnFact = new WMTSConnectionFactoryClass();
                IWMTSConnection wmtsconn = wmtsconnFact.Open(propertySet, 0, null);
                IWMTSServiceDescription wmtsServiceDesc = (IWMTSServiceDescription)wmtsconn;
                
                //Get WMTSConnectionName
                IWMTSConnectionName wmtsConnectionName = (IWMTSConnectionName)wmtsconn.FullName;

                for (int i = 0; i < wmtsServiceDesc.LayerDescriptionCount; i++)
                {
                    IWMTSLayerDescription ld = wmtsServiceDesc.get_LayerDescription(i);

                    IPropertySet layerPropertiesSet = wmtsConnectionName.ConnectionProperties;
                    //Set the layer name and assign this to the WMTSConnectionName
                    layerPropertiesSet.SetProperty("LAYERNAME", ld.Identifier);
                    wmtsConnectionName.ConnectionProperties = layerPropertiesSet;

                    if (ld.Title == layerTitle)//cswrecord.Title)
                    {
                        bool connected = false;
                        try
                        {
                            IWMTSLayer wmtslayer = new WMTSLayerClass();
                            
                            //Pass in the Name with layerIdentifier.
                            connected = wmtslayer.Connect((IName)wmtsConnectionName);

                            map.AddLayer((ILayer)wmtslayer);
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessageBox(StringResources.ConnectToMapServiceFailed + "\r\n" + "url:" + url + "\r\n" + ex.Message);
                            connected = false;
                        }
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                //TODO: wmts string resource aanmaken?
                ShowErrorMessageBox(StringResources.AddWmsLayerFailed + "\r\n" + ex.Message);
            }
        }
        private void AddLayerWFS(CswRecord cswrecord)
        {
            string urlEndpoint = cswrecord.MapServerURL;
            
            string getFeatureUrl = BuildGetFeatureRequest(urlEndpoint);

            string wfsDownloadFolder = MakeValidFileName(urlEndpoint);
            //string downloadFileName = System.IO.Path.Combine(Utils.GetSpecialFolderPath(SpecialFolder.Temp), "PDOK");
            //downloadFileName = System.IO.Path.Combine(downloadFileName, wfsDownloadFolder);
            //System.IO.Directory.CreateDirectory(downloadFileName);
            //downloadFileName = System.IO.Path.Combine(downloadFileName, "shapes.zip");

            //Replaced the above by explicit user interaction to specify filename (requested by RWS because they expect most users dont have the right to create a file in the temp folder (?)
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Zip file|*.zip";
            saveFileDialog1.DefaultExt = "zip";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Title = "WFS zipped shape opslaan als";
            saveFileDialog1.FileName = wfsDownloadFolder + ".zip";
            DialogResult dlgResult = saveFileDialog1.ShowDialog();
            if (String.IsNullOrEmpty(saveFileDialog1.FileName)||dlgResult == System.Windows.Forms.DialogResult.Cancel) return;
            string downloadFileName = saveFileDialog1.FileName;

            try
            {
                if (System.IO.File.Exists(downloadFileName))
                    System.IO.File.Delete(downloadFileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "File delete failed");
            }

            if (!DownloadShapeZip(getFeatureUrl, downloadFileName)) return;
            string extractedFilesFolder = downloadFileName.Replace(".zip", "");
            Unzip(downloadFileName,extractedFilesFolder);
            AddShapesFromDirToFocusMap(extractedFilesFolder, cswrecord.Title);
        }

        private string BuildGetFeatureRequest(string url)
        {
            String[] s = url.Trim().Split('?');
            //hardcode the version 1.0.0 (and thus assume it is also implemented by higher version wfs services) so we know what the response looks like
            url = s[0] + "?request=GetCapabilities&service=WFS&version=1.0.0";
            CswClient client = new CswClient();
            String response = client.SubmitHttpRequest("GET", url, "");

            XmlDocument xmlDocument = new XmlDocument();
            try { xmlDocument.LoadXml(response); }
            catch (XmlException xmlEx)
            { }
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("wfs", "http://www.opengis.net/wfs");
            XmlNode shapeZip = xmlDocument.SelectSingleNode(@"/wfs:WFS_Capabilities/wfs:Capability/wfs:Request/wfs:GetFeature/wfs:ResultFormat/wfs:SHAPE-ZIP",nsmgr);
            if (shapeZip == null) throw new Exception("This WFS service does not support getting features in the required SHAPE-ZIP ResultFormat.");
            XmlNodeList featureTypeNames = xmlDocument.SelectNodes(@"/wfs:WFS_Capabilities/wfs:FeatureTypeList/wfs:FeatureType/wfs:Name",nsmgr);

            if (featureTypeNames != null && featureTypeNames.Count > 0)
            {
                //Convert to array in order to Join
                string[] typenames = new string[featureTypeNames.Count];
                for(int i = 0;i<featureTypeNames.Count;i++)
                {
                    typenames[i]= featureTypeNames[i].InnerText;
                }
                //1.1.0 because of 5 param BBOX
                string getFeatureUrl = s[0] + @"?SERVICE=WFS&VERSION=1.1.0&REQUEST=GetFeature&TYPENAME=" + String.Join(",",typenames) + "&outputFormat=shape-zip";
                //'SERVICE=WFS&VERSION=1.0.0&REQUEST=GetFeature&TYPENAME='+layers+'&BBOX='+xmin+","+ymin+","+xmax+","+ymax+'&outputFormat=shape-zip')  
                //getFeatureUrl = getFeatureUrl + @"&BBOX=0,0,350000,650000,EPSG:28992";
                getFeatureUrl = String.Format("{0}&{1}", getFeatureUrl, ComposeBBOXkeyValuePair()); 
                return getFeatureUrl;
            }
            else
            {
                throw new Exception("This WFS service does not return any featuretypes.");
            }
        }
        private string ComposeBBOXkeyValuePair()
        {
            IMxDocument mxDoc = (IMxDocument)m_application.Document;
            IMap map = mxDoc.FocusMap;
            IActiveView activeView = (IActiveView)map;
            IEnvelope extent = activeView.Extent;

            ISpatialReference CurrentMapSpatialReference = extent.SpatialReference;
            if (CurrentMapSpatialReference == null) throw new Exception(StringResources.SpatialReferenceNotDefined);
            int wkid = CurrentMapSpatialReference.FactoryCode;
            return String.Format("BBOX={0},{1},{2},{3},EPSG:{4}",
                extent.XMin.ToString(CultureInfo.InvariantCulture),
                extent.YMin.ToString(CultureInfo.InvariantCulture),
                extent.XMax.ToString(CultureInfo.InvariantCulture),
                extent.YMax.ToString(CultureInfo.InvariantCulture), wkid);
        }
        private static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }

        private bool DownloadShapeZip(string url,string downloadFileName)
        {
            //Not clear why I should use cswc.SubmitHttpRequest("DOWNLOAD"....) as is the case in AddWCSLayer....
            // Create an instance of WebClient
            WebClient client = new WebClient();

            //// Hookup DownloadFileCompleted Event
            //client.DownloadFileCompleted +=
            //new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            string urlHits = url + @"&resultType=hits";
            Stream responseStream = client.OpenRead(urlHits);
            StreamReader reader = new StreamReader(responseStream);
            String response = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);
            XmlElement featureCollection = xmlDocument.DocumentElement;
            string numberOfFeatures = featureCollection.Attributes["numberOfFeatures"].InnerText;
            if (String.IsNullOrEmpty(numberOfFeatures) || numberOfFeatures == "0")
            {
                MessageBox.Show("No features to download for the current extent.", "No features",MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return false;
            }
            DialogResult dialogResult = MessageBox.Show(String.Format(@"You are about to download {0} features for the current extent.{1}(Server-side restrictions on the maximum number of features may apply.{1}Generally, this is the case when the maximum number of features equals a round number.){1}{1}Are you sure?", numberOfFeatures,Environment.NewLine), "Confirm download", MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.No) return false;
            
            client.DownloadFile(new Uri(url), downloadFileName);
            client.Dispose();
            return true;
                      
        }

        private void Unzip(string filename, string extractedFilesFolder)
        {

            try
            {
                if (System.IO.Directory.Exists(extractedFilesFolder))
                    System.IO.Directory.Delete(extractedFilesFolder,true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "File delete failed");
            }
            using (ZipFile zip = ZipFile.Read(filename))
            {
                // This call to ExtractAll() assumes:
                //   - none of the entries are password-protected.
                //   - want to extract all entries to current working directory
                //   - none of the files in the zip already exist in the directory;
                //     if they do, the method will throw.
                //zip.FlattenFoldersOnExtract = true;
                zip.TempFileFolder = Utils.GetSpecialFolderPath(SpecialFolder.Temp);
                zip.ExtractAll(extractedFilesFolder);
            }
        }


         void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
         {

         }

        private void AddLayerWCS(string url)
        {
            // MapServiceInfo msi = new MapServiceInfo();
            String[] s = url.Trim().Split('?');

            url = s[0] + "?request=GetCapabilities&service=WCS";
            CswClient client = new CswClient();
            String response = client.SubmitHttpRequest("GET", url, "");

            XmlDocument xmlDocument = new XmlDocument();
            try { xmlDocument.LoadXml(response); }
            catch (XmlException xmlEx)
            { }

            XmlNodeList contentMetadata = xmlDocument.GetElementsByTagName("wcs:Contents");//ContentMetadata

            if (contentMetadata != null && contentMetadata.Count > 0)
            {
                XmlNodeList coverageList = contentMetadata.Item(0).ChildNodes;

                foreach (XmlNode coverage in coverageList)
                {

                    XmlNodeList nodes = coverage.ChildNodes;

                    foreach (XmlNode node in nodes)
                    {
                        if (node.Name.ToLower().Equals("ows:title"))//name
                        {
                            url = s[0] + "?request=GetCoverage&service=WCS&format=GeoTIFF&coverage=" + node.InnerText;

                            try
                            {
                                String filePath = client.SubmitHttpRequest("DOWNLOAD", url, "");
                                AddAGSService(filePath);

                            }
                            catch (Exception e)
                            {
                                ShowErrorMessageBox("WCS service with no GeoTiff interface");
                                return;
                            }
                        }
                    }

                }

            }
            else
            {
                /*  contentMetadata = xmlDocument.GetElementsByTagName("CoverageSummary");

                  if (contentMetadata != null && contentMetadata.Count > 0)
                  {
                      XmlNodeList coverageList = contentMetadata.Item(0).ChildNodes;

                      foreach (XmlNode coverage in coverageList)
                      {

                          if (coverage.Name.ToLower().Equals("identifier"))
                          {
                              url = s[0] + "?request=GetCoverage&service=WCS&format=GeoTIFF&coverage=" + coverage.InnerText;

                              try
                              {
                                  String filePath = client.SubmitHttpRequest("DOWNLOAD", url, "");
                                  AddAGSService(filePath);

                              }
                              catch (Exception e)
                              {
                                  ShowErrorMessageBox("WCS service with no GeoTiff interface");
                                  return;
                              }
                          }
                      }

                  }*/

            }
        }

        /// <summary>
        /// Retrieves the selected metadata (in search result listbox) from CSW catalog. Exception shall be thrown.
        /// </summary>
        /// <remarks>
        /// Called in View Metadata, Download Metadata, and Add to Map
        /// </remarks>
        /// <returns>A XMLDocument object if metadata was retrieved successfully. Null if otherwise.</returns>
        private XmlDocument RetrieveSelectedMetadataFromCatalog(bool bApplyTransform)
        {
            try
            {
                // validate
                if (catalogComboBox.SelectedIndex == -1) { throw new Exception(StringResources.NoCatalogSelected); }
                if (dgvCswRecords.SelectedRows.Count == 0) { throw new Exception(StringResources.NoSearchResultSelected); }
                CswCatalog catalog = (CswCatalog)catalogComboBox.SelectedItem;
                if (catalog == null) { throw new NullReferenceException(StringResources.CswCatalogIsNull); }
                CswRecord record = (CswRecord)dgvCswRecords.CurrentRow.DataBoundItem;
                if (record == null) throw new NullReferenceException(StringResources.CswRecordIsNull);

                // connect to catalog if needed
                if (!catalog.IsConnected())
                {
                    string errMsg = "";
                    try { catalog.Connect(_isWriteLogs); }
                    catch (Exception ex) { errMsg = ex.Message; }

                    // exit if still not connected
                    if (!catalog.IsConnected())
                    {
                        ShowErrorMessageBox(StringResources.ConnectToCatalogFailed + "\r\n" + errMsg);
                        return null;
                    }
                }

                bool isTransformed = false;

                // retrieve metadata doc by its ID
                if (_searchRequest == null) { _searchRequest = new CswSearchRequest(_isWriteLogs); }
                _searchRequest.Catalog = catalog;
                try {
                    isTransformed = _searchRequest.GetMetadataByID(record.ID, bApplyTransform);                
                }
                catch (Exception ex)
                {
                    ShowDetailedErrorMessageBox(StringResources.RetrieveMetadataFromCatalogFailed, _searchRequest.Response().ResponseXML);
                    System.Diagnostics.Trace.WriteLine(StringResources.RetrieveMetadataFromCatalogFailed);
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                    System.Diagnostics.Trace.WriteLine(_searchRequest.Response().ResponseXML);
                    return null;
                }

               
                    CswSearchResponse response = _searchRequest.Response();
                    CswRecord recordMetadata = response.Records[0];
                    if (recordMetadata.FullMetadata.Length == 0) { throw new Exception(StringResources.EmptyMetadata); }

                    if (!isTransformed)
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        try { xmlDoc.LoadXml(recordMetadata.FullMetadata); }
                        catch (XmlException xmlEx)
                        {
                            ShowDetailedErrorMessageBox(StringResources.LoadMetadataFailed + "\r\n" + xmlEx.Message,
                                                        recordMetadata.FullMetadata);
                            return null;
                        }
                        return xmlDoc;
                    }
                    else
                    {
                        styledRecordResponse = recordMetadata.FullMetadata;
                        return null;
                    }

                    
              
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(StringResources.RetrieveMetadataFromCatalogFailed + "\r\n" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retrieves the selected metadata (in search result listbox) from CSW catalog. Exception shall be thrown.
        /// </summary>
        /// <remarks>
        /// Called in View Metadata, Download Metadata, and Add to Map
        /// </remarks>
        /// <returns>A XMLDocument object if metadata was retrieved successfully. Null if otherwise.</returns>
        private void RetrieveAddToMapInfoFromCatalog()
        {
            try
            {
                // validate
                if (catalogComboBox.SelectedIndex == -1) { throw new Exception(StringResources.NoCatalogSelected); }
                if (dgvCswRecords.SelectedRows.Count == 0) { throw new Exception(StringResources.NoSearchResultSelected); }
                CswCatalog catalog = (CswCatalog)catalogComboBox.SelectedItem;
                if (catalog == null) { throw new NullReferenceException(StringResources.CswCatalogIsNull); }
                CswRecord record = (CswRecord)dgvCswRecords.CurrentRow.DataBoundItem;
                if (record == null) throw new NullReferenceException(StringResources.CswRecordIsNull);

                // connect to catalog if needed
                if (!catalog.IsConnected())
                {
                    string errMsg = "";
                    try { catalog.Connect(_isWriteLogs); }
                    catch (Exception ex) { errMsg = ex.Message; }

                    // exit if still not connected
                    if (!catalog.IsConnected())
                    {
                        ShowErrorMessageBox(StringResources.ConnectToCatalogFailed + "\r\n" + errMsg);
                    }
                }

                // retrieve metadata doc by its ID
                if (_searchRequest == null) { _searchRequest = new CswSearchRequest(_isWriteLogs); }
                _searchRequest.Catalog = catalog;
                try
                {
                    _searchRequest.GetAddToMapInfoByID(record.ID);

                }
                catch (Exception ex)
                {
                    ShowDetailedErrorMessageBox(StringResources.RetrieveMetadataFromCatalogFailed, _searchRequest.Response().ResponseXML);
                    System.Diagnostics.Trace.WriteLine(StringResources.RetrieveMetadataFromCatalogFailed);
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                    System.Diagnostics.Trace.WriteLine(_searchRequest.Response().ResponseXML);
                 
                }
             
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(StringResources.RetrieveMetadataFromCatalogFailed + "\r\n" + ex.Message);
               
            }
        }

        /// <summary>
        /// prepare metadata. Metadata xml is transformed to an intermediate format. 
        /// Map service information shall be generated and put in <Esri></Esri> tags.
        /// </summary>
        /// <param name="xmlDoc">Metadata XML document</param>
        /// <returns>Transformed XML document</returns>
       /* private XmlDocument PrepareMetadata(XmlDocument xmlDoc)
        {
            try
            {
                if (xmlDoc == null) { throw new ArgumentNullException(); }

                //load the Xsl if necessary
                if (_xsltPrepare == null)
                {
                    _xsltPrepare = new XslCompiledTransform();
                    try { _xsltPrepare.Load(_xsltPrepareMetadataFilePath); }
                    catch (Exception ex)
                    {
                        ShowErrorMessageBox(StringResources.LoadMetadataPreparationStylesheetFailed + "\r\n" + ex.Message);
                        return null;
                    }
                }

                // todo: clean metadata xml. remove namespaces (to be consistant with the behavior on Portal) (?) 

                // transform
                StringWriter strWriter = new StringWriter();
                _xsltPrepare.Transform(new XmlNodeReader(xmlDoc), null, (TextWriter)strWriter);
                strWriter.Close();

                XmlDocument newXmlDoc = new XmlDocument();
                newXmlDoc.LoadXml(strWriter.ToString());

                return newXmlDoc;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(StringResources.PrepareMetadataFailed + "\r\n" + ex.Message);
                return null;
            }
        }*/

        /// <summary>
        /// Parse out service information (such as service type, server name, service name, etc) from metadta document
        /// </summary>
        /// <param name="xmlDoc">xml metadata doc to be parsed</param>
        /// <param name="msi">A MapServiceInfo object as output</param>
        private MapServiceInfo ParseServiceInfoFromMetadata(XmlDocument xmlDoc)
        {
            // note: some required node may missing if it isn't a metadata for liveData or map
            try
            {
                if (xmlDoc == null) { throw new ArgumentNullException(); }

                MapServiceInfo msi = new MapServiceInfo();

                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                xmlNamespaceManager.AddNamespace("cat", "http://www.esri.com/metadata/csw/");
                xmlNamespaceManager.AddNamespace("csw", "http://www.opengis.net/cat/csw");
                xmlNamespaceManager.AddNamespace("gmd", "http://www.isotc211.org/2005/gmd");

                XmlNode nodeMetadata = xmlDoc.SelectSingleNode("//metadata|//cat:metadata|//csw:metadata|//gmd:MD_Metadata", xmlNamespaceManager);
                if (nodeMetadata == null) { throw new Exception(StringResources.MetadataNodeMissing); }

                // parse out service information
                XmlNode nodeEsri = nodeMetadata.SelectSingleNode("Esri");
                if (nodeEsri == null) throw new Exception(StringResources.EsriNodeMissing);

                // server
                XmlNode node = nodeEsri.SelectSingleNode("Server");
                if (node == null) throw new Exception(StringResources.ServerNodeMissing);
                msi.Server = node.InnerText;

                // service
                node = nodeEsri.SelectSingleNode("Service");
                if (node != null) { msi.Service = node.InnerText; }

                // service type
                node = nodeEsri.SelectSingleNode("ServiceType");
                if (node != null) { msi.ServiceType = node.InnerText; }

                // service param
                node = nodeEsri.SelectSingleNode("ServiceParam");
                if (node != null) { msi.ServiceParam = node.InnerText; }

                // issecured
                node = nodeEsri.SelectSingleNode("issecured");
                if (node != null) { msi.IsSecured = (node.InnerText.Equals("True", StringComparison.OrdinalIgnoreCase)); }

                return msi;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(StringResources.MapServiceInfoNotAvailable + "\r\n" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Add WCS map service layer to map
        /// </summary>
        /// <param name="msi">Map service information</param>
        private void AddLayerWCS(pdok4arcgis.MapServiceInfo msi, Boolean fromServerUrl)
        {
            if (msi == null) { throw new ArgumentNullException(); }

            try
            {
                string _mapServerUrl = AppendQuestionOrAmpersandToUrlString(msi.Server);
                // append serviceParam to server url
                // todo: does msi.ServiceParam have a leading "?" or "&"?
                if (msi.ServiceParam.Length > 0 && !fromServerUrl)
                {
                    _mapServerUrl = _mapServerUrl + msi.ServiceParam;
                    _mapServerUrl = AppendQuestionOrAmpersandToUrlString(_mapServerUrl);
                }

                /*
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("URL", url);

                IMxDocument mxDoc = (IMxDocument)m_application.Document;
                IMap map = (IMap)mxDoc.FocusMap;
                IActiveView activeView = (IActiveView)map;

                IWCSLayer wcsLayer = new WCSLayerClass();
                

       //         IWCSCoverageDescription
         //       IWCSGroupLayer wcsGroupLayer = (IWCSGroupLayer)new WCSMapLayerClass();

                IWCSConnectionName wcsConnectionName = new WCSConnectionName();
                wcsConnectionName.ConnectionProperties = propertySet;

                // connect to wms service
                IDataLayer dataLayer;
                bool connected = false;
                try
                {
                    dataLayer = (IDataLayer) wcsLayer;
                    IName connName = (IName) wcsConnectionName;
                    connected = dataLayer.Connect(connName);
                }
                catch (Exception ex)
                {
                    ShowErrorMessageBox(StringResources.ConnectToMapServiceFailed + "\r\n" + ex.Message);
                    connected = false;
                }
                if (!connected) return;


                // get service description out of the layer. the service description contains 
                // information about the wms categories and layers supported by the service
             //   IWCSServiceDescription wcsServiceDesc = wcsLayer.WMSServiceDescription;

                IWCSCoverageDescription wcsCoverageDesc;
                ILayer newLayer;
                ILayer layer;
                IWCSLayer newWcsLayer;
         //     IWCSGroupLayer newWmsGroupLayer;          
            
                // add to focus map
             //   map.AddLayer(layer);

                return;*/

                 // MapServiceInfo msi = new MapServiceInfo();
                        String[] s = _mapServerUrl.Trim().Split('?');

                        _mapServerUrl = s[0] + "?request=GetCapabilities&service=WCS";
                        CswClient client = new CswClient();
                        String response = client.SubmitHttpRequest("GET", _mapServerUrl, "");

                         XmlDocument xmlDocument = new XmlDocument();
                         try { xmlDocument.LoadXml(response); }
                         catch (XmlException xmlEx)
                         { }

                         XmlNodeList contentMetadata = xmlDocument.GetElementsByTagName("ContentMetadata");

                         if (contentMetadata != null && contentMetadata.Count > 0)
                         {
                             XmlNodeList coverageList = contentMetadata.Item(0).ChildNodes;

                             foreach (XmlNode coverage in coverageList) {
                                  
                                 XmlNodeList nodes = coverage.ChildNodes;

                                 foreach(XmlNode node in nodes)
                                 {
                                     if (node.Name.ToLower().Equals("name"))
                                     {
                                         _mapServerUrl = s[0] + "?request=GetCoverage&service=WCS&format=GeoTIFF&coverage=" + node.InnerText;

                                         try{
                                            String filePath  = client.SubmitHttpRequest("DOWNLOAD", _mapServerUrl, "");
                                            AddAGSService(filePath);

                                         } catch(Exception e){
                                             ShowErrorMessageBox("WCS service with no GeoTiff interface");
                                             return;
                                         }                                
                                     }
                                 }

                             }

                         }                                            

                        //msi.Server = s[0];
                       // AddLayerWCS(msi, true);
                    }
            
            catch (Exception ex)
            {
              //  ShowErrorMessageBox(StringResources.AddWcsLayerFailed + "\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Add WMS map service layer to map
        /// </summary>
        /// <param name="msi">Map service information</param>
        private void AddLayerWMS(pdok4arcgis.MapServiceInfo msi, Boolean fromServerUrl, string sld)
        {
            if (msi == null) { throw new ArgumentNullException(); }

            try
            {
                string url = AppendQuestionOrAmpersandToUrlString(msi.Server);
                // append serviceParam to server url
                // todo: does msi.ServiceParam have a leading "?" or "&"?
                if (msi.ServiceParam.Length > 0 && !fromServerUrl)
                {
                    url = url + msi.ServiceParam;
                    url = AppendQuestionOrAmpersandToUrlString(url);
                }
                if (!String.IsNullOrEmpty(sld))
                {
                    url = String.Format("{0}SLD={1}&", url, sld);
                }
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("URL", url);

                IMxDocument mxDoc = (IMxDocument)m_application.Document;
                IMap map = (IMap)mxDoc.FocusMap;
                IActiveView activeView = (IActiveView)map;
                IWMSGroupLayer wmsGroupLayer = (IWMSGroupLayer)new WMSMapLayerClass();
                IWMSConnectionName wmsConnectionName = new WMSConnectionName();
                wmsConnectionName.ConnectionProperties = propertySet;

                // connect to wms service
                IDataLayer dataLayer;
                bool connected = false;
                try
                {
                    dataLayer = (IDataLayer)wmsGroupLayer;
                    IName connName = (IName)wmsConnectionName;
                    connected = dataLayer.Connect(connName);
                }
                catch (Exception ex) 
                {
                    ShowErrorMessageBox(StringResources.ConnectToMapServiceFailed + "\r\n" + "url:" + url +"\r\n" + ex.Message);
                    connected = false;
                }
                if (!connected) return;

                ILayer layer = (ILayer)wmsGroupLayer;
                if (wmsGroupLayer.Count == 0)
                { // Old code that was present for 9.3 but causes issues for 10.1, as the layers have already been added at this point. The documentation for IWMSMapLayer still states that this is necessary to get the layers (but the behaviour changed , probably in ArcGIS 10.0)
                    // get service description out of the layer. the service description contains 
                    // inforamtion about the wms categories and layers supported by the service
                    IWMSServiceDescription wmsServiceDesc = wmsGroupLayer.WMSServiceDescription;
                    IWMSLayerDescription wmsLayerDesc;
                    ILayer newLayer;
                    IWMSLayer newWmsLayer;
                    IWMSGroupLayer newWmsGroupLayer;
                    for (int i = 0; i < wmsServiceDesc.LayerDescriptionCount; i++)
                    {
                        newLayer = null;

                        wmsLayerDesc = wmsServiceDesc.get_LayerDescription(i);
                        if (wmsLayerDesc.LayerDescriptionCount == 0)
                        {
                            // wms layer
                            newWmsLayer = wmsGroupLayer.CreateWMSLayer(wmsLayerDesc);
                            newLayer = (ILayer)newWmsLayer;
                            if (newLayer == null) { throw new Exception(StringResources.CreateWmsLayerFailed); };
                        }
                        else
                        {
                            // wms group layer
                            newWmsGroupLayer = wmsGroupLayer.CreateWMSGroupLayers(wmsLayerDesc);
                            newLayer = (ILayer)newWmsGroupLayer;
                            if (newLayer == null) { throw new Exception(StringResources.CreateWmsGroupLayerFailed); }
                        }

                        // add newly created layer
                        wmsGroupLayer.InsertLayer(newLayer, 0);
                    }

                    // configure the layer before adding it to the map
                    layer.Name = wmsServiceDesc.WMSTitle;
                }
                ExpandLayer(layer, true);
                SetLayerVisibility(layer, true);

                // add to focus map
                map.AddLayer(layer);

                return;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox (StringResources.AddWmsLayerFailed + "\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Add ArcIMS map service layer to map
        /// </summary>
        /// <param name="msi">Map service information</param>
        private void AddLayerArcIMS(pdok4arcgis.MapServiceInfo msi)
        {
            if (msi == null) { throw new ArgumentNullException(); }
            try
            {
                IMxDocument mxDoc = (IMxDocument)m_application.Document;
                IMap map = (IMap)mxDoc.FocusMap;
                IActiveView activeView = (IActiveView)map;

                bool isAlreadyInMap = false;
            /*    for (int i = 0; i < map.LayerCount; i++)
                {
                    // note: minimum checking. to make sure only unique services are added.
                    if (msi.Service.Equals(map.get_Layer(i).Name, StringComparison.OrdinalIgnoreCase))
                    {
                        isAlreadyInMap = true;
                        break;
                    }
                }*/

                if (isAlreadyInMap)
                {
                    ShowErrorMessageBox (StringResources.MapServiceLayerAlreadyExistInMap);
                    return;
                }
                else
                {
                    //IIMSConnection imsConnection;
                    IIMSMapLayer imsMapLayer;
                    
                    // create service description based on provided map service info
                    IIMSServiceDescription imsServiceDesc = new IMSServiceNameClass();
                    imsServiceDesc.URL = msi.Server;
                    imsServiceDesc.Name = msi.Service;
                    imsServiceDesc.ServiceType = acServiceType.acMapService;
                    // note: add as image map service
                    // todo: research on the relationship between MapServiceInfo.ServiceType and IIMSServiceDescription
                    #region acServiceType info
                    /*
                acMapService 0 An image map service. 
                acFeatureService 1 A feature stream service. 
                acMetadataService 2 A metadata service. 
                acGlobeService 3 A globe service. 
                acWMSService 4 A WMS service. 
                acUnknownService 5 An unknown service. 
                */
                    #endregion

                    // connect to service
                    try
                    {
                        imsMapLayer = new IMSMapLayerClass();
                        imsMapLayer.ConnectToService(imsServiceDesc);
                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        // note: when a site request usr/pwd to connect, user may choose "cancel". Then, imsMapLayer.ConnectToService() 
                        //       would then throw an System.Runtime.InteropServices.COMException. Catch it but no need to display 
                        //       any error message.
                        System.Diagnostics.Trace.WriteLine(ex.Message, "NoConnectionToService");
                        return;
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessageBox(StringResources.ConnectToMapServiceFailed + "\r\n" + ex.Message);
                        return;
                    }

                    ILayer layer = (ILayer)imsMapLayer;
                    ExpandLayer(layer, true);
                    SetLayerVisibility(layer, true);

                    // Add the layer
                    mxDoc.AddLayer(imsMapLayer);

                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(StringResources.AddArcimsLayerFailed + "\r\n" + ex.Message);
            }
        }

        private void  AddAGSService(string fileName){
            try
            {
                IMxDocument mxDoc = (IMxDocument)m_application.Document;
                IMap map = (IMap)mxDoc.FocusMap;
                IActiveView activeView = (IActiveView)map;
                
                bool isAlreadyInMap = false;
              /*  for (int i = 0; i < map.LayerCount; i++)
                {
                    // note: minimum checking. to make sure only unique services are added.
                    if (fileName.Contains(map.get_Layer(i).Name))
                    {
                        isAlreadyInMap = true;
                        break;
                    }
                }*/

                if (isAlreadyInMap)
                {
                    ShowErrorMessageBox(StringResources.MapServiceLayerAlreadyExistInMap);
                    return;
                }
                else
                {             
                    if (fileName.ToLower().Contains("http") && !fileName.ToLower().Contains("arcgis/rest"))
                    {
                        if(fileName.EndsWith("MapServer"))
                            fileName = fileName.Remove(fileName.LastIndexOf("MapServer"));

                        String[] s = fileName.ToLower().Split(new String[]{"/services"}, StringSplitOptions.RemoveEmptyEntries);

                        IPropertySet propertySet = new PropertySetClass();
                        propertySet.SetProperty("URL", s[0] + "/services"); // fileName

                       IMapServer mapServer = null;  
  
                        IAGSServerConnectionFactory pAGSServerConFactory =  new AGSServerConnectionFactory();
                        IAGSServerConnection agsCon = pAGSServerConFactory.Open(propertySet,0); 
                        IAGSEnumServerObjectName pAGSSObjs = agsCon.ServerObjectNames;
                        IAGSServerObjectName pAGSSObj = pAGSSObjs.Next();
   
                       while (pAGSSObj != null) {
                        if(pAGSSObj.Type=="MapServer" && pAGSSObj.Name.ToLower() == s[1].TrimStart('/').TrimEnd('/')){               
                            break;
                        }
                        pAGSSObj = pAGSSObjs.Next();
                       }


                        IName pName =  (IName) pAGSSObj;  
                        IAGSServerObject pAGSO = (IAGSServerObject) pName.Open();
                        mapServer = (IMapServer) pAGSO;


                        IPropertySet prop = new PropertySetClass();
                        prop.SetProperty("URL", fileName);
                        prop.SetProperty("Name",pAGSSObj.Name);


                        IMapServerLayer layer = new MapServerLayerClass();
                        layer.ServerConnect(pAGSSObj,mapServer.get_MapName(0));


                        mxDoc.AddLayer((ILayer)layer);

                    }
                    else
                    {
                       
                        IGxFile pGxFile;
                       
                        if (fileName.ToLower().EndsWith(".tif"))
                        {
                            IRasterLayer pGxLayer = (IRasterLayer) new RasterLayer();
                            pGxLayer.CreateFromFilePath(fileName);                          
                            if (pGxLayer.Valid)
                            {
                                map.AddLayer((ILayer) pGxLayer);
                            }
                        }
                        else
                        {
                            IGxLayer pGxLayer = new GxLayer();
                            pGxFile = (GxFile)pGxLayer;
                            pGxFile.Path = fileName;
                        
                            if (pGxLayer.Layer != null)
                            {
                                map.AddLayer(pGxLayer.Layer);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(StringResources.AddArcGISLayerFailed + "\r\n" + ex.Message);
            }
        }

        #endregion

        #region Configure Tab Function(s)
        /// <summary>
        /// Highlight current catalog in the list when Configure Tab is activated.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void ConfigureTabPage_Enter(object sender, EventArgs e)
        {
            if (_inited)
            {
                // in sync with the current csw catalog on Search Tab
                catalogListBox.SelectedItem = catalogComboBox.SelectedItem;
            }
        }

        /// <summary>
        /// Load components for Config Tab
        /// </summary>
        /// <returns>true if successful; false if error occurred. Exception shall be raised if there was any.</returns>
        private bool LoadConfigTab()
        {
            try
            {
                // populate profiles
                _profileList = CswObjectsToArrayList(_cswProfiles);
                catalogProfileComboBox.BeginUpdate();
                catalogProfileComboBox.DataSource = _profileList;
                catalogProfileComboBox.DisplayMember = "Name";
                catalogProfileComboBox.ValueMember = "ID";
                catalogProfileComboBox.SelectedIndex = -1;
                catalogProfileComboBox.EndUpdate();

                AddData();
                UpdateCatalogListLabel();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(StringResources.LoadConfigTabFailed  + "\r\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Update catalog list label
        /// </summary>
        private void UpdateCatalogListLabel()
        {
            int count = catalogListBox.Items.Count;
            if (count > 0)
            {
                catalogListLabel.Text = StringResources.CatalogListLabelText + " (" + catalogListBox.Items.Count.ToString() + ")";
            }
            else
            {
                catalogListLabel.Text = StringResources.CatalogListLabelText;
            }
        }

        /// <summary>
        /// event handling for catalog listbox selection changed event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void CatalogListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (catalogListBox.SelectedIndex < 0)
                {
                    catalogProfileComboBox.Enabled = true;
                    deleteCatalogButton.Enabled = false;
                    saveCatalogButton.Enabled = false;
                    return;
                }
                else
                {
                    CswCatalog catalog = (CswCatalog)catalogListBox.SelectedItem;
                    catalogProfileComboBox.SelectedItem = catalog.Profile;
                    catalogUrlTextBox.Text = catalog.URL;
                    catalogDisplayNameTextBox.Text = catalog.Name;

                    catalogProfileComboBox.Enabled = true;
                    deleteCatalogButton.Enabled = true;
                    saveCatalogButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox (ex.Message);
            }
        }

        /// <summary>
        /// delete the selected catalog
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void DeleteCatalogButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (catalogListBox.SelectedItem == null)
                {
                    ShowErrorMessageBox(StringResources.SelectAnItemToDelete);
                }
                else
                {
                    CswCatalog catalog = (CswCatalog)catalogListBox.SelectedItem;
                    if (MessageBox.Show(StringResources.DeleteConfirmation, StringResources.ConfirmDelete, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _catalogList.Remove(catalog);
                        catalogListBox.Update();
                        _cswManager.deleteCatalog(catalog);
                        ClearData();
                        AddData();
                    }
                }
                _isCatalogListDirty = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message);
            }
            finally
            {
                UpdateCatalogListLabel();
            }
        }

        /// <summary>
        /// Add a new catalog
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void NewCatalogButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearData();
                
                _newClicked = true;
                deleteCatalogButton.Enabled = false;
                saveCatalogButton.Enabled = false;
                catalogProfileComboBox.SelectedItem = catalogProfileComboBox.Items[0];

                catalogUrlTextBox.Focus();
                catalogListBox.SelectedIndex = -1;
                _isCatalogListDirty = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message);
            }
        }

        /// <summary>
        /// Save/Update a catalog
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void SaveCatalogButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_newClicked == true)
                {
                    CswProfile profile = catalogProfileComboBox.SelectedItem as CswProfile;

                    string url = "";
                    url = catalogUrlTextBox.Text.Trim();
                    string name = "";
                    name = catalogDisplayNameTextBox.Text.Trim();
                    if (url.Length == 0)
                    {
                        ShowErrorMessageBox(StringResources.UrlIsEmpty);
                    }
                    else
                    {
                        CswCatalog catalog = new CswCatalog(url, name, profile);
                        catalog.resetConnection();
                        _cswManager.addCatalog(catalog);
                        ClearData();
                        AddData();
                        catalogListBox.SelectedIndex = catalogListBox.Items.IndexOf(catalog);
                        _newClicked = false;
                        MessageBox.Show(StringResources.CatalogAddedSuccessfully, StringResources.Success, MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else if (catalogListBox.SelectedItem == null)
                {
                    MessageBox.Show(StringResources.SelectAnItemToUpdate);
                }
                else
                {
                    CswCatalog catalog = (CswCatalog)catalogListBox.SelectedItem;
                    int index = catalogListBox.SelectedIndex;
                    CswProfile profile = catalogProfileComboBox.SelectedItem as CswProfile;
                    _cswManager.updateCatalog(catalog, catalogDisplayNameTextBox.Text, catalogUrlTextBox.Text, profile);
                    catalog.resetConnection();
                    ClearData();
                    AddData();
                    catalogListBox.SelectedIndex = index;
                    MessageBox.Show(StringResources.CatalogSavedSuccessfully, StringResources.Success, MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                _isCatalogListDirty = true;

            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message);
            }
            finally
            {
                UpdateCatalogListLabel();
            }
        }

        /// <summary>
        /// Display pop up tooltip for catalog URL when mouse hovers the textbox
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void CatalogUrlTextBox_MouseHover(object sender, EventArgs e)
        {
            if (catalogUrlTextBox.Text.Length > 0)
            {
                tooltip.InitialDelay = 1000;
                tooltip.ReshowDelay = 1000;
                tooltip.IsBalloon = false;
                tooltip.ToolTipTitle = "";
                tooltip.SetToolTip(catalogUrlTextBox, catalogUrlTextBox.Text);
            }
        }

        /// <summary>
        /// adding data to the catalog listbox 
        /// </summary>
        private void AddData()
        {
            _catalogList = CswObjectsToArrayList(_cswCatalogs);
            _catalogList.Sort();

            catalogListBox.BeginUpdate();
            catalogListBox.DataSource = _catalogList;
            catalogListBox.DisplayMember = "Name";
            catalogListBox.ValueMember = "ID";
            catalogListBox.SelectedIndex = _cswCatalogs.Count - 1;
            catalogListBox.EndUpdate();
        }

        /// <summary>
        /// Clearing the display and url text boxes
        /// </summary>
        private void ClearData()
        {
            catalogDisplayNameTextBox.Text = "";
            catalogUrlTextBox.Text = "";
        }

        /// <summary>
        /// Function on change of txtDisplayName  text changed event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void CatalogDisplayNameTextBox_TextChanged(object sender, EventArgs e)
        {
            saveCatalogButton.Enabled = (catalogUrlTextBox.Text.Length > 0);
        }

        /// <summary>
        /// Function on change of txtURL  text changed event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void CatalogUrlTextBox_TextChanged(object sender, EventArgs e)
        {
            saveCatalogButton.Enabled = (catalogUrlTextBox.Text.Length > 0);
        }

        /// <summary>
        /// Function on change of combProfile text changed event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void CatalogProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveCatalogButton.Enabled = (catalogUrlTextBox.Text.Length > 0);
        }

        #endregion

        #region Help Tab Function(s)
        /// <summary>
        /// Load help files when Help Tab is activated the first time
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
      /*  private void HelpTabPage_Enter(object sender, EventArgs e)
        {
            if (!_isHelpFileLoaded) LoadHelpTab();
        }*/

        /// <summary>
        /// Load components for Help Tab. 
        /// </summary>
        /// <returns>true if successful; false if error occurred. Exception shall be raised if there was any.</returns>
       /* private bool LoadHelpTab()
        {
            if (!_isHelpFileLoaded)
            {
                try
                {
                    // load help html file
                    viewHelpWebBrowser.Url = new Uri(_helpFilePath);
                    _isHelpFileLoaded = true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessageBox(StringResources.LoadHelpFilesFailed + "\r\n" + ex.Message);
                }
            }
            return _isHelpFileLoaded;
        }*/

        #endregion

        #region About Tab Function(s)
        /// <summary>
        /// Event handling when "About GPT" hyperlink is clicked. 
        /// Pop up a default browser and navigate to the URL specified in the label.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
      /*  private void WebsiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                websiteLinkLabel.Links[0].Visited = true;
                System.Diagnostics.Process.Start(websiteLinkLabel.Text);
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox(ex.Message);
            }
        }*/
        #endregion

        #region Tab Key handling Function(s)

        /// <summary>
        /// Intercept "Tab" key and step focus to the next control
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void CswSearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // todo: enhance "GetNextTabStopControl()"
                Control ctrl = Utils.GetNextTabStopControl(this);
                if (ctrl != null) ctrl.Focus();
            }
        }

        /// <summary>
        /// Step focus to search result listbox when user press "Tab" key on the split container
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void ResultsSplitContainer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                dgvCswRecords.Focus();
            }
        }

        #endregion

        #region Utils

        /// <summary>
        /// convert cswobjects to arraylist 
        /// </summary>
        /// <remarks>
        /// will phase out once CswObjects implements IList interface.
        /// </remarks>
        /// <param name="cswObjs">CSWObjects to be converted</param>
        /// <returns>ArrayList that contains a list of CSW objects.</returns>
        private ArrayList CswObjectsToArrayList(CswObjects cswObjs)
        {
            ArrayList alCswObjects = new ArrayList();
            for (int i = 0; i < cswObjs.Count; i++)
            {
                alCswObjects.Add(cswObjs[i]);
            }
            return alCswObjects;
        }
        private SortableBindingList<CswRecord> CswObjectsToSortableBindingList(CswObjects cswObjs)
        {
                SortableBindingList<CswRecord> csws = new SortableBindingList<CswRecord>();
                for (int i = 0; i < cswObjs.Count; i++)
                {
                    csws.Add((CswRecord)cswObjs[i]);
                }
                return csws;
        }

        /// <summary>
        /// Generate temporary filename with specified prefix and surfix. It uses current windows user's temp folder by default.
        /// </summary>
        /// <param name="prefix">Prefix for the temporary file name</param>
        /// <param name="surfix">Surfix for the temporary file name</param>
        /// <returns>Full path to the temporary file</returns>
        private string GenerateTempFilename(string prefix, string surfix)
        {
            // todo: use System.IO.Path.GetRandomFileName() to accomodate prefix;
            //       it will avoid the issue of .tmp file being generated by system
            string tempFilename = System.IO.Path.GetTempFileName();
            try { System.IO.File.Delete(tempFilename); }
            catch { }
            tempFilename = System.IO.Path.ChangeExtension(tempFilename, surfix);

            return tempFilename;
        }

        /// <summary>
        /// Append question mark or ampersand to a url string
        /// </summary>
        /// <param name="urlString">source URL string</param>
        /// <returns>output URL string</returns>
        private string AppendQuestionOrAmpersandToUrlString(string urlString)
        {
            urlString = urlString.Trim();
            string finalChar = urlString.Substring(urlString.Length - 1, 1);    // final char
            if (!finalChar.Equals("?") && !finalChar.Equals("&"))
            {
                if (urlString.LastIndexOf("=") > -1) { urlString = urlString + "&"; }
                else { urlString = urlString + "?"; }
            }
            return urlString;
        }
        private string AppendSldToUrlString(string urlString, string sld)
        {
            urlString = urlString.Trim();
            string finalChar = urlString.Substring(urlString.Length - 1, 1);    // final char
            if (!finalChar.Equals("?") && !finalChar.Equals("&"))
            {
                if (urlString.LastIndexOf("=") > -1) { urlString = urlString + "&"; }
                else { urlString = urlString + "?"; }
            }
            return urlString;
        }

        /// <summary>
        /// Display an error message dialog with the provided message string, with default caption, button and icon
        /// </summary>
        /// <param name="ErrorMessage">Error message to be displayed</param>
        private void ShowErrorMessageBox(string ErrorMessage)
        {
            MessageBox.Show(ErrorMessage, StringResources.ErrorMessageDialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Display a warning message dialog with the provided message string, with default caption, button and icon
        /// </summary>
        /// <param name="WarningMessage">Warning message to be displayed</param>
        private void ShowWarningMessageBox(string WarningMessage)
        {
            MessageBox.Show(WarningMessage, StringResources.WarningMessageDialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Display a error messagebox with details.
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="detailedMessage">details</param>
        private void ShowDetailedErrorMessageBox(string message, string details)
        {
            FormMessageBox frmMessageBox = new FormMessageBox();
            frmMessageBox.Init(message, details, StringResources.ErrorMessageDialogCaption);
            frmMessageBox.ShowDialog(this);
        }

        /// <summary>
        /// Set layer visibility, including its sub layers if applicable.
        /// </summary>
        /// <param name="layer">Layer</param>
        /// <param name="visible">Indicates if the layer is visible</param>
        private void SetLayerVisibility(ILayer layer, bool visible)
        {
            if (layer == null) return;

            layer.Visible = visible;

            if (layer is ICompositeLayer)
            {
                ICompositeLayer compositeLayer = (ICompositeLayer)layer;
                for (int i = 0; i < compositeLayer.Count; i++)
                {
                    SetLayerVisibility(compositeLayer.get_Layer(i), visible);
                }
            }
        }

        /// <summary>
        /// collapse layer or not, including its sub layers if applicable.
        /// </summary>
        /// <param name="layer">Layer</param>
        /// <param name="visible">Indicates if the layer is expanded in the TOC.</param>
        private void ExpandLayer(ILayer layer, bool expanded)
        {
            if (layer == null) return;

            if (layer is ICompositeLayer2)
            {
                ICompositeLayer2 compositeLayer = (ICompositeLayer2)layer;
                compositeLayer.Expanded = true;
                for (int i = 0; i < compositeLayer.Count; i++)
                {
                    ExpandLayer(compositeLayer.get_Layer(i), expanded);
                }
            }
        }

        #endregion

       /// <summary>
        /// Event handler for link labeled event.
        /// </summary>
        /// <param name="param1">The sender object</param>
        /// <param name="param1">The event arguments</param>
        private void linkLblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "http://webhelp.esri.com/geoportal_extension/9.3.1/ext_csw_clnts.htm");
        }

        private void linkLblAbt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "http://webhelp.esri.com/geoportal_extension/9.3.1/welcome_to_gpt.htm#about_gpt");
        }

        private void AddShapesFromDirToFocusMap(string dir, string grouplayername)
        {
            // Start tool for download from WFS service
            try
            {
                IWorkspaceFactory workspaceFactoryShape = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspaceFactoryShape.OpenFromFile(dir, 0);

                IGroupLayer groupLayer = new GroupLayerClass();
                groupLayer.Name = grouplayername;
                IEnumDatasetName enumDatasetName = ((IWorkspace)featureWorkspace).get_DatasetNames(esriDatasetType.esriDTFeatureClass);
                IDatasetName featureDatasetName = (IDatasetName)enumDatasetName.Next();
                while (featureDatasetName != null)
                {
                    IFeatureLayer featureLayer = new FeatureLayerClass();
                    featureLayer.FeatureClass = featureWorkspace.OpenFeatureClass(featureDatasetName.Name);
                    featureLayer.Name = featureDatasetName.Name;
                    groupLayer.Add(featureLayer);
                    featureDatasetName = (IDatasetName)enumDatasetName.Next();
                }
                IMxDocument mxDoc = (IMxDocument)m_application.Document;
                IMap map = (IMap)mxDoc.FocusMap;
                IActiveView activeView = (IActiveView)map;
                map.AddLayer(groupLayer);
                mxDoc.UpdateContents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Update GUI when a new record (or none) is selected in the search results listbox
        /// </summary>
        /// <param name="sender">event sender</param>
        ///// <param name="e">event args</param>
        private void dgvCswRecords_SelectionChanged(object sender, EventArgs e)
        {
            CswRecord record= (CswRecord)dgvCswRecords.CurrentRow.DataBoundItem;
           
            //Copied from listbox-eventhandler

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            CswCatalog catalog = (CswCatalog)catalogComboBox.SelectedItem;

            if (catalog == null) { throw new NullReferenceException(StringResources.CswCatalogIsNull); }
            // update GUI
            UpdateGUI();

            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void UpdateGUI()
        {
            CswRecord record = (CswRecord)dgvCswRecords.CurrentRow.DataBoundItem;
            if (record == null)
            {
                // no record selected
                abstractTextBox.Text = "";

                // GUI update for buttons
                tsMenuItemAdd2Map.Enabled = false;
                tsMenuItemViewMetadata.Enabled = false;
                tsMenuItemAdd2MapSLD.Enabled = false;

            }
            else
            {
                abstractTextBox.Text = record.Abstract;
                tsMenuItemAdd2Map.Enabled = record.IsLiveDataOrMap;
                tsMenuItemViewMetadata.Enabled = true;

                tsMenuItemAdd2MapSLD.Enabled = (record.Protocol!=null)&&(record.Protocol.Equals("OGC:WMS", StringComparison.CurrentCultureIgnoreCase) || record.Protocol.Equals("WMS", StringComparison.CurrentCultureIgnoreCase));
            }
        }

        /// <summary>
        /// Step focus to Abstract when user press "Tab" key on the search result datagridview
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void dgvCswRecords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                abstractTextBox.Focus();
            }
        }

      

        private void dgvCswRecords_Sorted(object sender, EventArgs e)
        {
            //Nicer would be to maintain currently selected item: after some web searching it appears that this is not easy and therefore left it for the moment
            //(http://social.msdn.microsoft.com/forums/en-US/winformsdatacontrols/thread/01f937af-d0d0-4de5-8919-088e88c5af77/)
            
            UpdateGUI();
        }

        private void tsMenuItemAdd2Map_Click(object sender, EventArgs e)
        {
            AddSelectedRecordToMap();
        }

        private void dgvCswRecords_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dgvCswRecords.CurrentCell = dgvCswRecords.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvCswRecords.ContextMenuStrip = contextMenuStrip1;
                UpdateGUI();
            }
            else
            {
                dgvCswRecords.ContextMenuStrip = null;
            }

        }

        private void tsMenuItemAdd2MapSLD_Click(object sender, EventArgs e)
        {

            string sld = Interaction.InputBox("Specificeer een voor de WMS bereikbaar pad (URL) naar een SLD:" + Environment.NewLine + Environment.NewLine +
                "Een Styled Layer Descriptor (SLD) is een XML schema gespecificeerd door het Open Geospatial Consortium (OGC) om de weergave van kaartlagen (zowel raster als vector) te beschrijven." + Environment.NewLine
            , "Styled Layer Descriptor","");
            AddSelectedRecordToMap(sld);
        }

        private void tsMenuItemViewStyledMetadata_Click(object sender, EventArgs e)
        {
            ViewMetadata(true);
        }

        private void dgvCswRecords_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
             cbRefine.Enabled = (dgvCswRecords.Rows.Count > 0);
            //Avoid that the checkbox is checked when it is re-enabled (for different search criteria)
             if (!cbRefine.Enabled) cbRefine.Checked = false;
        }

        System.Diagnostics.Process p = new System.Diagnostics.Process();
        private void richTextBox1_LinkClicked(object sender,
        System.Windows.Forms.LinkClickedEventArgs e)
        {
            // Call Process.Start method to open a browser
            // with link text as URL.
            p = System.Diagnostics.Process.Start("IExplore.exe", e.LinkText);
        }







        

    }
}
