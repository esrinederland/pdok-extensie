using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pdok4arcgis
{
    public partial class FormViewMetadata : Form
    {
        private string _caption="";     
        private string _filePath = "";

        public string MetadataFilePath
        {
            get { return _filePath; }
        }

        public FormViewMetadata()
        {
            InitializeComponent();
        }

        public void Navigate(string urlString)
        {
            _filePath = urlString;
            Uri tmpUri = new Uri(urlString);
            webBrowserViewer.Navigate(tmpUri);
        }

        public string MetadataTitle
        {
            set { this.Text = value + " - " + _caption; }
        }

        private void FormViewMetadata_Load(object sender, EventArgs e)
        {
            _caption = this.Text;   // "Metadata Viewer";
        }
    }
}