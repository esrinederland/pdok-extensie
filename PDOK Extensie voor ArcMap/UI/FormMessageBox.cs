using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pdok4arcgis
{
    public partial class FormMessageBox : Form
    {
        public FormMessageBox()
        {
            InitializeComponent();
            Reset();
        }

        public void Init(string Message, string Details, string Caption)
        {
            txtMessage.Text = Message;
            txtMessage.TabStop = false;

            txtDetails.Text = Details;
            txtDetails.SelectionStart = 0;
            txtDetails.SelectionLength = 0;

            this.Text = Caption;
        }

        private void Reset()
        {
            txtMessage.Text = "";
            txtDetails.Text = "";
        }
    }
}