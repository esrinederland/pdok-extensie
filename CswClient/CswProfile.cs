using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace pdok4arcgis {
    public class CswProfile {

        private string id;
        private string name;
        private string cswnamespace;
        private string kvp;
        private string description;
        private string requestxslt;
        private string responsexslt;
        private string metadataxslt;
        private string xsltStyledResponse;
        private bool filter_livedatamap;
        private bool filter_extentsearch;
        private bool filter_spatialboundary;   
        private XslCompiledTransform requestxsltobj;
        private XslCompiledTransform responsexsltobj;
        private XslCompiledTransform metadataxsltobj;
        private XslCompiledTransform displayResponseXsltObj;
        private XmlDocument getRecordsRequest;

#region "Properties"
        public XmlDocument LastUntransformedGetRecordsRequest {
            get {
                return getRecordsRequest;
            }
        }

        public string ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        public string Name {
            set {
                name = value;
            }
            get {
                return name;
            }
        }

       public string CswNamespace {
            get {
                return cswnamespace;
            }
            set {
                cswnamespace = value;
            }
        }

        public string Description {
            set {
                description = value;
            }
            get {
                return description;
            }
        }
        public string XsltStyledResponse
        {
            set
            {
                xsltStyledResponse = value;
            }
            get
            {
                return xsltStyledResponse;
            }
        }

        public bool SupportContentTypeQuery {
            set {
                filter_livedatamap = value;
            }
            get {
                return filter_livedatamap;
            }
        }

        public bool SupportSpatialQuery {
            set {
                filter_extentsearch = value;
            }
            get {
                return filter_extentsearch;
            }
        }

        public bool SupportSpatialBoundary {
            get { 
                return filter_spatialboundary; 
            }
            set { 
                filter_spatialboundary = value; 
            }
        }


       

#endregion

# region "ConstructorDefinition"

public CswProfile(string sid,string sname,string sdescription)
        {
            ID = sid;
            Name = sname;
            Description = sdescription;
        }

        public CswProfile(string sid, string sname, string sdescription, string skvp, string srequestxslt, string sresponsexslt, string smetadataxslt, string sdisplayResponseXslt, bool livedatamap, bool extentsearch, bool spatialboundary)
        {
            ID = sid;
            Name = sname;
            CswNamespace = CswProfiles.DEFAULT_CSW_NAMESPACE;
            Description = sdescription;
            kvp = skvp;
            requestxslt = srequestxslt;
            responsexslt = sresponsexslt;
            metadataxslt = smetadataxslt;
            xsltStyledResponse = sdisplayResponseXslt;
            filter_livedatamap = livedatamap;
            filter_extentsearch = extentsearch;
            filter_spatialboundary = spatialboundary;
            //enable document() support
            XsltSettings settings = new XsltSettings(true, true);
            XmlUrlResolver xmlurlresolver = new XmlUrlResolver();
            responsexsltobj = new XslCompiledTransform();
            responsexsltobj.Load(responsexslt, settings, xmlurlresolver);
            requestxsltobj = new XslCompiledTransform();
            requestxsltobj.Load(requestxslt, settings, xmlurlresolver);
            if (metadataxslt.Length > 0) {
                metadataxsltobj = new XslCompiledTransform();
                //enable document() support
                metadataxsltobj.Load(metadataxslt, settings, xmlurlresolver);
            }

            if (xsltStyledResponse.Length > 0)
            {
                 displayResponseXsltObj = new XslCompiledTransform();
                //enable document() support
                 displayResponseXsltObj.Load(xsltStyledResponse, settings, xmlurlresolver);
            }
            
        }

public CswProfile(string sid, string sname, string scswnamespace, string sdescription, string skvp, string srequestxslt, string sresponsexslt, string smetadataxslt,  string sdisplayResponseXslt, bool livedatamap, bool extentsearch,bool spatialboundary) {
            ID = sid;
            Name = sname;
            CswNamespace = scswnamespace;
            Description = sdescription;
            kvp = skvp;
            requestxslt = srequestxslt;
            responsexslt = sresponsexslt;
            metadataxslt = smetadataxslt;
            xsltStyledResponse = sdisplayResponseXslt;
            filter_livedatamap = livedatamap;
            filter_extentsearch = extentsearch;
            filter_spatialboundary = spatialboundary;
             //enable document() support
            XsltSettings settings = new XsltSettings(true, true);
            XmlUrlResolver xmlurlresolver = new XmlUrlResolver();
            responsexsltobj = new XslCompiledTransform();
            responsexsltobj.Load(responsexslt, settings, xmlurlresolver);
            requestxsltobj = new XslCompiledTransform();
            requestxsltobj.Load(requestxslt, settings, xmlurlresolver);
            if (metadataxslt.Length > 0) {
                metadataxsltobj = new XslCompiledTransform();               
                metadataxsltobj.Load(metadataxslt, settings, xmlurlresolver);
             
            }

            if (xsltStyledResponse.Length > 0)
            {
                displayResponseXsltObj = new XslCompiledTransform();
                //enable document() support
                displayResponseXsltObj.Load(xsltStyledResponse, settings, xmlurlresolver);
            }

        }

#endregion


        # region "PublicFunctions"

        /// <summary>
        /// Parse a CSW response. 
        /// </summary>
        /// <remarks>
        /// The CSW response is parsed and the records collection is populated
        /// with the result.The reponse is parsed based on the response xslt.
        /// </remarks>
        /// <param name="param1">The string response</param>
        /// <param name="param2">The recordlist which needs to be populated</param>
        public void ReadCSWGetRecordsResponse(string responsestring, CswRecords recordslist) {
         
            try {
                TextReader textreader = new StringReader(responsestring);
                XmlTextReader xmltextreader = new XmlTextReader(textreader);
                //load the Xml doc
                XPathDocument xPathDoc = new XPathDocument(xmltextreader);
                if (responsexsltobj == null) {
                    responsexsltobj = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings(true, true);
                    responsexsltobj.Load(responsexslt,settings, new XmlUrlResolver() );
                } 
                //create the output stream
                StringWriter writer = new StringWriter();
                //do the actual transform of Xml
                responsexsltobj.Transform(xPathDoc, null, writer);
                writer.Close();
                //populate CswRecords
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(writer.ToString());
                recordslist.SetNumberOfRecordsMatched(doc); 
                XmlNodeList xmlnodes = doc.GetElementsByTagName("Record");
                foreach (XmlNode xmlnode in xmlnodes)
                {
                    CswRecord record = new CswRecord();
                    record.ID = xmlnode.SelectSingleNode ("ID").InnerText;
                    record.Title = xmlnode.SelectSingleNode("Title").InnerText;
                    record.ResponsibleOrganisation = xmlnode.SelectSingleNode("ReponsibleOrganisation").InnerText;
                    record.MetadataVersion = xmlnode.SelectSingleNode("MetadataVersion").InnerText;
                    record.Abstract = xmlnode.SelectSingleNode("Abstract").InnerText;
                    String lowercorner = "";
                    if(this.SupportSpatialBoundary){
                        lowercorner = xmlnode.SelectSingleNode("LowerCorner").InnerText;
                    }
                    String uppercorner = "";
                    if (this.SupportSpatialBoundary){
                        uppercorner = xmlnode.SelectSingleNode("UpperCorner").InnerText;
                    }
                    if ((lowercorner.Length>0 && uppercorner.Length>0 )) {
                    /*  record.BoundingBox.Maxx = Double.Parse(lowercorner.Substring(0, lowercorner.IndexOf(' ')));
                        record.BoundingBox.Miny = Double.Parse(lowercorner.Substring(lowercorner.IndexOf(' ') + 1));
                        record.BoundingBox.Minx = Double.Parse(uppercorner.Substring(0, uppercorner.IndexOf(' ')));
                        record.BoundingBox.Maxy = Double.Parse(uppercorner.Substring(uppercorner.IndexOf(' ') + 1));*/
                        Boolean parseFlag  = false;
                        CultureInfo cultureInfo = new CultureInfo("en-us");
                        double pareseResult = 0.0;
                        parseFlag = Double.TryParse(lowercorner.Substring(0, lowercorner.IndexOf(' ')), NumberStyles.Number, cultureInfo, out pareseResult);
                        record.BoundingBox.Minx = pareseResult;
                        parseFlag = Double.TryParse(lowercorner.Substring(lowercorner.IndexOf(' ') + 1), NumberStyles.Number, cultureInfo, out pareseResult);
                        record.BoundingBox.Miny = pareseResult;
                        parseFlag = Double.TryParse(uppercorner.Substring(0, uppercorner.IndexOf(' ')), NumberStyles.Number, cultureInfo, out pareseResult);
                        record.BoundingBox.Maxx = pareseResult;
                        parseFlag = Double.TryParse(uppercorner.Substring(uppercorner.IndexOf(' ') + 1), NumberStyles.Number, cultureInfo, out pareseResult);
                        record.BoundingBox.Maxy = pareseResult;
                        if (parseFlag == false)
                        {
                            throw new Exception("Number format error");
                        }

                    }
                    else {
                        record.BoundingBox.Maxx = 500.00;
                        record.BoundingBox.Miny = 500.00;
                        record.BoundingBox.Minx = 500.00;
                        record.BoundingBox.Maxy = 500.00;
                    }
                    XmlNode node = xmlnode.SelectSingleNode("Type");
                    if (node != null)
                    {
                        record.IsLiveDataOrMap = node.InnerText.Equals("liveData", StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        record.IsLiveDataOrMap = false;
                    }


                    XmlNode referencesNode = xmlnode.SelectSingleNode("References");
                    if (referencesNode != null)
                    {
                        String references = referencesNode.InnerText;
                        DcList list = new DcList();
                        list.add(references);
                        record.SetServiceProps(list);

                    }
                    else
                    {
                        record.MapServerURL = String.Empty;
                    }
                    
                    recordslist.AddRecord(record.ID, record);
                }
            }
            catch (Exception e) {
                throw e;
            }
            //return recordslist;

        }





        public static void ParseServiceInfoFromUrl(MapServiceInfo msinfo, String mapServerUrl, String serviceType)
        {

            msinfo.Service = "Generic " + serviceType + " Service Name";
            String[] urlParts = mapServerUrl.Trim().Split('?');
            if (urlParts.Length > 0)
                msinfo.Server = urlParts[0];
            else
                msinfo.Server = mapServerUrl;

            String[] s = msinfo.Server.Split(new String[] { "/servlet/com.esri.esrimap.Esrimap" }, StringSplitOptions.RemoveEmptyEntries);
            msinfo.Server = s[0];

            if (urlParts.Length > 1)
            {
                String[] urlParams = urlParts[1].Trim().Split('&');

                foreach (String param in urlParams)
                {
                    String paramPrefix = param.ToLower();
                    if (paramPrefix.StartsWith("service=") || paramPrefix.StartsWith("servicename="))
                    {
                        msinfo.Service = param.Trim().Split('=')[1];
                    }
                }

            }

        }     

        /// <summary>
        /// Generate a CSW request string. 
        /// </summary>
        /// <remarks>
        /// The CSW request string is built.
        /// The request is string is build based on the request xslt.
        /// </remarks>
        /// <param name="param1">The search criteria</param>
        /// <returns>The request string</returns>
        public string GenerateCSWGetRecordsRequest(CswSearchCriteria searchCriteria)
        {
            XmlElement root = null;
            if ((getRecordsRequest == null)||!searchCriteria.RefinePreviousFilter)
            {
                getRecordsRequest = new XmlDocument();
                XmlDeclaration declaration = getRecordsRequest.CreateXmlDeclaration("1.0", "UTF-8", null);
                getRecordsRequest.AppendChild(declaration);
                // Create the root element
                root = getRecordsRequest.CreateElement("GetRecords");
                getRecordsRequest.AppendChild(root);
            }


            SetFilterCriteria(getRecordsRequest, searchCriteria);
            if (searchCriteria.RefinePreviousFilter)
            {
                //All And|Or|Not child elements of the GetRecords-root element need to be group in and And element
                root = getRecordsRequest.DocumentElement;
                //First get rid of childs that will be reset later
                XmlNode node = root.SelectSingleNode("StartPosition");
                if (node != null ) root.RemoveChild(node);
                node = root.SelectSingleNode("MaxRecords");
                if (node != null) root.RemoveChild(node);
                
                XmlElement xmlRefineLogicalOperator = getRecordsRequest.CreateElement("And");
                while (root.ChildNodes.Count > 0)
                {
                    xmlRefineLogicalOperator.AppendChild(root.FirstChild);
                }
                root.AppendChild(xmlRefineLogicalOperator);

            }
            XmlElement xmlElement = getRecordsRequest.CreateElement("StartPosition");
            xmlElement.InnerText = searchCriteria.StartPosition.ToString();
            root.AppendChild(xmlElement);

            xmlElement = getRecordsRequest.CreateElement("MaxRecords");
            xmlElement.InnerText = searchCriteria.MaxRecords.ToString();
            root.AppendChild(xmlElement);
            return TransformRequest(getRecordsRequest);
        }
        private string TransformRequest(XmlDocument doc)
        {
            
            StringWriter writer = new StringWriter();
            try
            {
                if (requestxsltobj == null)
                {
                    requestxsltobj = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings(true, true);
                    requestxsltobj.Load(requestxslt,settings,new XmlUrlResolver());
                }
                //do the actual transform of Xml
                requestxsltobj.Transform(doc, null, writer);
                writer.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return writer.ToString();
        }

        private void SetFilterCriteria(XmlDocument doc, CswSearchCriteria searchCriteria)
        {
            XmlElement root = doc.DocumentElement;
            //The logical operator between the filter properties
            XmlElement xmlFilterLogicalOperator = doc.CreateElement(Enum.GetName(typeof(CswSearchCriteria.LogicalOperator), searchCriteria.FilterLogicalOperator));
            root.AppendChild(xmlFilterLogicalOperator);
            //Regular expression instead of Split, in order to keep word combinations between double quotes 
            //"Bob the Builder" construction ---> gives 2 matches: '"Bob the Builder"' and 'construction'.
            Regex regex = new Regex(@"((""((?<token>.*?)(?<!\\)"")|(?<token>[\S]+))(\s)*)");
            MatchCollection matches = regex.Matches(searchCriteria.SearchText);
            XmlElement searchTextLogicalOperator=null;
            for (int i = 0; i < matches.Count; i++)
            {
                XmlElement xmlKeyWord = doc.CreateElement("KeyWord");
                xmlKeyWord.InnerText = this.XmlEscape(matches[i].Value.Trim().Replace(@"""", String.Empty));
                if ((matches.Count > 1)||(searchCriteria.SearchTextLogicalOperator == CswSearchCriteria.LogicalOperator.Not))
                {
                    if (i == 0)
                    {
                        searchTextLogicalOperator = doc.CreateElement(Enum.GetName(typeof(CswSearchCriteria.LogicalOperator), searchCriteria.SearchTextLogicalOperator));
                        xmlFilterLogicalOperator.AppendChild(searchTextLogicalOperator);
                    }
                    searchTextLogicalOperator.AppendChild(xmlKeyWord);
                }
                else
                {
                    xmlFilterLogicalOperator.AppendChild(xmlKeyWord);
                }
            }

            //XmlElement xmlLiveDataMap = doc.CreateElement("LiveDataMap");
            //xmlLiveDataMap.InnerText = searchCriteria.LiveDataAndMapOnly.ToString();
            //xmlFilterLogicalOperator.AppendChild(xmlLiveDataMap);

            XmlElement xmlTypesOfUseLogicalOperator=null;
            for (int i = 0; i < searchCriteria.TypesOfUse.Count; i++)
            {
                XmlElement xmlTypeOfUse = doc.CreateElement("TypeOfUse");
                xmlTypeOfUse.InnerText = searchCriteria.TypesOfUse[i];
                if (searchCriteria.TypesOfUse.Count > 1)
                {
                    if (i == 0)
                    {
                        xmlTypesOfUseLogicalOperator = doc.CreateElement("Or");
                        xmlFilterLogicalOperator.AppendChild(xmlTypesOfUseLogicalOperator);
                    }
                    xmlTypesOfUseLogicalOperator.AppendChild(xmlTypeOfUse);
                }
                else
                {
                    xmlFilterLogicalOperator.AppendChild(xmlTypeOfUse);
                }
            }

            if (!String.IsNullOrEmpty(searchCriteria.Organisation))
            {
                XmlElement xmlOrganisation = doc.CreateElement("Organisation");
                xmlOrganisation.InnerText = this.XmlEscape(searchCriteria.Organisation.Trim().Replace(@"""", String.Empty));
                xmlFilterLogicalOperator.AppendChild(xmlOrganisation);
            }
            if (searchCriteria.Envelope != null)
            {

                XmlElement xmlEnvelope = doc.CreateElement("Envelope");
                xmlFilterLogicalOperator.AppendChild(xmlEnvelope);

                XmlElement xe = doc.CreateElement("MinX");
                xe.InnerText=searchCriteria.Envelope.MinX.ToString();
                xmlEnvelope.AppendChild(xe);

                xe = doc.CreateElement("MinY");
                xe.InnerText = searchCriteria.Envelope.MinY.ToString();
                xmlEnvelope.AppendChild(xe);

                xe = doc.CreateElement("MaxX");
                xe.InnerText = searchCriteria.Envelope.MaxX.ToString();
                xmlEnvelope.AppendChild(xe);

                xe = doc.CreateElement("MaxY");
                xe.InnerText = searchCriteria.Envelope.MaxY.ToString();
                xmlEnvelope.AppendChild(xe);
            }

        }

        /// <summary>
        /// Generate a CSW request string to get metadata by ID. 
        /// </summary>
        /// <remarks>
        /// The CSW request string is built.
        /// The request is string is build based on the baseurl and record id
        /// </remarks>
        /// <param name="param1">The base url</param>
        /// <param name="param2">The record ID string</param>
        /// <returns>The request string</returns>
        public string GenerateCSWGetMetadataByIDRequestURL(string baseURL,string recordId) {
            
            StringBuilder requeststring = new StringBuilder();
            requeststring.Append(baseURL);

            if (baseURL.LastIndexOf("?")==(baseURL.Length -1)) { requeststring.Append(kvp); }
            else { requeststring.Append("?" + kvp); }

            requeststring.Append("&ID=" + recordId);
            return requeststring.ToString();

        }

        /// <summary>
        /// Read a CSW metadata response. 
        /// </summary>
        /// <remarks>
        /// The CSW metadata response is read.
        /// The CSw record is updated with the metadata
        /// </remarks>
        /// <param name="param1">The metadata response string</param>
        /// <param name="param2">The CSW record for the record</param>
        public void ReadCSWGetMetadataByIDResponse(string response,CswRecord record) {
            if (metadataxslt == null || metadataxslt.Equals("")) {
                record.FullMetadata = response;
                record.MetadataResourceURL = "";
            }
            else {
                //create the output stream
                StringWriter writer = new StringWriter();

                    TextReader textreader = new StringReader(response);
                    XmlTextReader xmltextreader = new XmlTextReader(textreader);
                    //load the Xml doc
                    XPathDocument xpathDoc = new XPathDocument(xmltextreader);
                    if (metadataxsltobj == null)
                    {
                        metadataxsltobj = new XslCompiledTransform();
                        //enable document() support
                        XsltSettings settings = new XsltSettings(true, true);
                        metadataxsltobj.Load(metadataxslt, settings, new XmlUrlResolver());

                    }

                                
                   //do the actual transform of Xml
                   metadataxsltobj.Transform(xpathDoc, null, writer);

                writer.Close();

                // full metadata or resource url
                String outputStr = writer.ToString();

                if (IsUrl(outputStr))
                {

                    if (outputStr.Contains("\u2715"))
                    {
                        DcList list = new DcList();
                        list.add(outputStr);

                        LinkedList<String> serverList = list.get(DcList.getScheme(DcList.Scheme.SERVER));
                        LinkedList<String> documentList = list.get(DcList.getScheme(DcList.Scheme.METADATA_DOCUMENT));

                        
                        if (serverList.Count > 0){
                            String serviceType = DeduceProtocolFromURL(serverList.First.Value);
                            if (serviceType.Equals("aims") || serviceType.Equals("ags") || serviceType.Equals("wms") || serviceType.Equals("wcs"))
                            {
                                record.MapServerURL = serverList.First.Value;
                            }
                        }
                        else
                            record.MapServerURL = "";

                        if(documentList.Count > 0)
                            record.MetadataResourceURL = documentList.First.Value;

                    }
                    else
                    {
                        if (DeduceProtocolFromURL(response).Equals("ags"))
                        {

                            outputStr = outputStr.Replace("http", "|http");
                            string[] s = outputStr.Split('|');
                            for (int i = 0; i < s.Length; i++)
                            {
                                if (s[i].ToString().Contains("MapServer"))
                                    record.MapServerURL = s[i];
                                else
                                    record.MetadataResourceURL = s[i];
                            }
                        }
                        else
                        {
                            record.MapServerURL = "";
                            record.MetadataResourceURL = outputStr;
                        }
                    }



                    record.FullMetadata = "";

                }
                else
                {
                    record.MapServerURL = "";
                    record.MetadataResourceURL = "";
                    record.FullMetadata = outputStr;
                }
            }
        }


        /// <summary>
        /// Transform a CSW metadata response. 
        /// </summary>
        /// <remarks>
        /// The CSW metadata response is read.
        /// The CSw record is updated with the metadata
        /// </remarks>
        /// <param name="param1">The metadata response string</param>
        /// <param name="param2">The CSW record for the record</param>
        public bool TransformCSWGetMetadataByIDResponse(string response, CswRecord record)
        {
            if (xsltStyledResponse == null || xsltStyledResponse.Equals(""))
            {
                record.FullMetadata = response;
                record.MetadataResourceURL = "";
                return false;
            }
            else
            {
                //create the output stream
                StringWriter writer = new StringWriter();

                TextReader textreader = new StringReader(response);
                XmlTextReader xmltextreader = new XmlTextReader(textreader);
                //load the Xml doc
                XPathDocument xpathDoc = new XPathDocument(xmltextreader);
                if (displayResponseXsltObj == null)
                {
                    displayResponseXsltObj = new XslCompiledTransform();
                    //enable document() support
                    XsltSettings settings = new XsltSettings(true, true);
                    displayResponseXsltObj.Load(xsltStyledResponse, settings, new XmlUrlResolver());

                }


                //do the actual transform of Xml
                displayResponseXsltObj.Transform(xpathDoc, null, writer);

                writer.Close();

                // full metadata or resource url
                String outputStr = writer.ToString();
                record.MapServerURL = "";
                record.MetadataResourceURL = "";
                record.FullMetadata = outputStr;

                return true;
            }
        }

        public static String DeduceProtocolFromURL(String url)
        {

                String serviceType = "unknown";
                url = url.ToLower();
                 if (url.Contains("service=wms") || url.Contains("com.esri.wms.esrimap") || url.Contains("/mapserver/wmsserver") || url.Contains("/wmsserver") || url.Contains("wms"))
                {
                    serviceType = "wms";
                }
                else if (url.Contains("service=wfs") || url.Contains("wfsserver"))
                {
                    serviceType = "wfs";
                }
                else if (url.Contains("service=wcs") || url.Contains("wcsserver"))
                {
                    serviceType = "wcs";
                }
                else if (url.Contains("com.esri.esrimap.esrimap"))
                {
                    serviceType = "aims";
                }
                else if ((url.Contains("arcgis/rest") || url.Contains("arcgis/services")) && url.Contains("mapserver"))
                {
                    serviceType = "ags";
                }
                else if (url.IndexOf("service=csw") > 0)
                {
                    serviceType = "csw";
                }
                else if (url.EndsWith(".nmf"))
                {
                    serviceType = "ArcGIS:nmf";
                }
                else if (url.EndsWith(".lyr"))
                {
                    serviceType = "ArcGIS:lyr";
                }
                else if (url.EndsWith(".mxd"))
                {
                    serviceType = "ArcGIS:mxd";
                }
                else if (url.EndsWith(".kml"))
                {
                    serviceType = "kml";
                }
                if (serviceType.Equals("image") || serviceType.Equals("feature"))
                {
                    serviceType = "aims";
                }
                return serviceType;
        }

        /// <summary>
        /// Check if a string is a URL.
        /// </summary>
        /// <remarks>
        /// This function only checks it the string contains http, https or ftp protocol. It doesn't validate the URL.
        /// </remarks>
        /// <param name="theString">string to be checked</param>
        /// <returns>true if the string is a URL</returns>
        private bool IsUrl(String theString)
        {
            if (theString.Length < 4)
                return false;
            else
            {
                if (theString.Substring(0, 5).Equals("http:", StringComparison.CurrentCultureIgnoreCase))
                    return true;
                else if (theString.Substring(0, 6).Equals("https:", StringComparison.CurrentCultureIgnoreCase))
                    return true;
                else if (theString.Substring(0, 4).Equals("ftp:", StringComparison.CurrentCultureIgnoreCase))
                    return true;
                else if (theString.Substring(0, 5).Equals("file:", StringComparison.CurrentCultureIgnoreCase))
                    return true;
                else if (theString.Contains("Server\u2715http:"))
                    return true;
                else
                    return false;
            }
        }

        #endregion

        /// <summary>
        /// replace specia lxml character  
        /// </summary>
        /// <remarks>
        /// Encode special characters (such as &, ", <, >, ') to percent values.
        /// </remarks>
        /// <param name="data">Text to be encoded</param>
        /// <returns>Encoded text.</returns>
        private string XmlEscape(string data) {
            data = data.Replace("&", "&amp;");
            data = data.Replace("<", "&lt;");
            data = data.Replace(">", "&gt;");
            data = data.Replace("\"", "&quot;");
            data = data.Replace("'", "&apos;"); 
            return data;
        }

    }
}
