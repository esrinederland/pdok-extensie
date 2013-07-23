namespace pdok4arcgis
{
    partial class FormViewMetadata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormViewMetadata));
            this.webBrowserViewer = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserViewer
            // 
            resources.ApplyResources(this.webBrowserViewer, "webBrowserViewer");
            this.webBrowserViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserViewer.Name = "webBrowserViewer";
            // 
            // FormViewMetadata
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowserViewer);
            this.Name = "FormViewMetadata";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.FormViewMetadata_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserViewer;
    }
}