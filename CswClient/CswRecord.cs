using System;
using System.Collections.Generic;
using System.Text;

namespace pdok4arcgis {
    public class CswRecord {

        private string id;
        private string title;
        private string responsibleOrganisation;
        private string abstractData;
        private BoundingBox boundingBox;
        private string briefMetadata;
        private string summaryMetadata;
        private string fullMetadata;
        private string metadataResourceURL;
        private string mapServerURL;
        private bool isLiveDataOrMap;
        private string serviceName;
        private string serviceType;
        private string metadataversion;
        private string protocol;
        private string usage;


        private String chkStr(String s)
        {
            if (s == null)
            {
                return "";
            }
            else
            {
                return s.Trim();
            }
        }
        #region methods definition
        public void SetServiceProps(DcList references)
        {

            // determine the service url, name and type
            LinkedList<String> schemeVals = references.get(DcList.getScheme(DcList.Scheme.SERVER));
            if (schemeVals.Count > 0)
            {
                mapServerURL = chkStr(schemeVals.First.Value);
            }
            if (mapServerURL == null) mapServerURL = String.Empty; ;

            schemeVals = references.get(DcList.getScheme(DcList.Scheme.SERVICE));
            if (schemeVals.Count > 0)
            {
                serviceName = chkStr(schemeVals.First.Value);
            }

            schemeVals = references.get(DcList.getScheme(DcList.Scheme.SERVICE_TYPE));
            if (schemeVals.Count > 0)
            {
                this.Protocol = (schemeVals.First.Value);
            }
            if ((mapServerURL.Length > 0) && (this.Protocol == null))
            {
                this.Protocol = CswProfile.DeduceProtocolFromURL(mapServerURL);
            }

            // handle the case where an ArcIMS service has been specified with 
            // server/service/this.Protocol parameters
            if ((mapServerURL.Length > 0) &&
                (this.Protocol.Equals("image", StringComparison.CurrentCultureIgnoreCase) ||
                 this.Protocol.Equals("feature") ||
                 this.Protocol.Equals("metadata")))
            {

                if ((serviceName.Length > 0))
                {
                    String esrimap = "servlet/com.esri.esrimap.Esrimap";
                    if (mapServerURL.IndexOf("esrimap") == -1)
                    {
                        if (mapServerURL.IndexOf("?") == -1)
                        {
                            if (!mapServerURL.EndsWith("/")) mapServerURL += "/";
                            mapServerURL = mapServerURL + esrimap + "?ServiceName=" + serviceName;
                        }
                    }
                    else
                    {
                        if (mapServerURL.IndexOf("?") == -1)
                        {
                            mapServerURL = mapServerURL + "?ServiceName=" + serviceName;
                        }
                        else if (mapServerURL.IndexOf("ServiceName=") == -1)
                        {
                            mapServerURL = mapServerURL + "&ServiceName=" + serviceName;
                        }
                    }
                }

                if (this.Protocol.Equals("image"))
                {
                    this.Protocol = "aims";
                }
            }

            // if the resource url has not been directly specified through a "scheme" attribute, 
            // then attempt to pick the best fit for the collection of references
            if (mapServerURL.Length == 0)
            {
                foreach (DcList.Value reference in references)
                {
                    if (reference != null)
                    {
                        String url = reference.getValue();
                        String type = CswProfile.DeduceProtocolFromURL(url);
                        if (type.Length > 0)
                        {
                            mapServerURL = url;
                            this.Protocol = type;
                            break;
                        }
                    }
                }

            }

        }
        #endregion


        #region properties definition

        public string ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        public string ServiceName
        {
            get
            {
                return serviceName;
            }
            set
            {
                serviceName = value;
            }
        }
        public string MetadataVersion
        {
            get
            {
                return metadataversion;
            }
            set
            {
                metadataversion = value;
            }
        }
        public string Protocol
        {
            get
            {
                return protocol;
            }
            set
            {
                protocol = value;
                switch (protocol.ToUpper())
                {
                    case "OGC:WMS":
                    case "WMS":
                        usage = "Raadplegen";
                        break;
                    case "OGC:WFS":
                    case "WFS":
                    case "OGC:WCS":
                    case "WCS":
                        usage = "Analyse/download";
                        break;
                    case "OGC:WMTS":
                    case "WMTS":
                    case "OGC:TMS":
                    case "TMS":
                        usage = "Achtergrond";
                        break;
                    default:
                        usage = "Overig";
                        break;
                }

            }
        }
        public string Usage
        {
            get
            {
                return usage;
            }
        }

        public string ServiceType
        {
            get
            {
                return serviceType;
            }
            set
            {
                serviceType = value;
            }
        }

        public string Title {
            get {
                return title;
            }
            set {
                title = value;
            }
        }

        public string ResponsibleOrganisation
        {
            get
            {
                return responsibleOrganisation;
            }
            set
            {
                responsibleOrganisation = value;
            }
        }

        public string Abstract {
            get {
                return abstractData;
            }
            set {
                abstractData = value;
            }
        }

        public string BriefMetadata {
            get {
                return briefMetadata;
            }
            set {
                briefMetadata = value;
            }
        }

       public BoundingBox BoundingBox {
            get { 
                return boundingBox; 
            }
            set { 
                boundingBox = value; 
            }
        }

        public string SummaryMetadata {
            get {
                return summaryMetadata;
            }
            set {
                summaryMetadata = value;
            }
        }

        public string FullMetadata {
            get {
                return fullMetadata;
            }
            set {
                fullMetadata = value;
            }
        }

        public string MetadataResourceURL {
            get {
                return metadataResourceURL;
            }
            set {
                metadataResourceURL = value;
            }
        }

        public bool IsLiveDataOrMap {
            get {
                return isLiveDataOrMap;
            }
            set {
                isLiveDataOrMap = value;
            }
        }

        public string MapServerURL
        {
            get
            {
                return mapServerURL;
            }
            set
            {
                mapServerURL = value;
            }
        }

#endregion

#region constructor definition

        public CswRecord(string sid) {
            ID = sid;
            boundingBox = new BoundingBox();
        }
        public CswRecord() {
            boundingBox = new BoundingBox();
        }
        public CswRecord(string sid, string stitle, string sabstract) {
            ID = sid;
            Abstract = sabstract;
            Title = stitle;
            boundingBox = new BoundingBox();
        }

#endregion

    }
}
