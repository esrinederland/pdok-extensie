using System;
using System.Collections.Generic;
using System.Text;

namespace pdok4arcgis
{
    /// <summary>
    /// CswSearchResponse class.
    /// </summary>
    /// <remarks>
    /// CswSearchResponse class is used to store the response for CSW search request.
    /// </remarks>
    public class CswSearchResponse
    {
        private string _responseXML;
        private CswRecords _records;

        /// <summary>
        /// Constructor
        /// </summary>
        public CswSearchResponse()
        {
            _responseXML = "";
            _records = new CswRecords();
        }

        /// <summary>
        /// Response XML
        /// </summary>
        public string ResponseXML
        {
            get { return _responseXML; }
            protected internal set { _responseXML = value; }
        }

        /// <summary>
        /// CSW Records returned
        /// </summary>
        public CswRecords Records
        {
            get { return _records; }
            protected internal set { _records = value; }
        }
    }
}
