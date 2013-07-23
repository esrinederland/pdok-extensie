using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace pdok4arcgis {
   public class CswRecords :CswObjects {
        private int? numberOfRecordsMatched;
        public new CswRecord this[string key] {
            get { return (CswRecord)base[key]; }
        }

        public new CswRecord this[int index] {
            get {
                return (CswRecord)base[index];
            }
        }

       public void AddRecord(object key, CswRecord record) {
           base.Add(key, record);
       }
       public int? NumberOfRecordsMatched {

           get
           {
               return numberOfRecordsMatched;
           }
       }
       public void SetNumberOfRecordsMatched(XmlDocument transformedCSWGetRecordsResponse)
       {
           try
           {
               XmlNodeList xmlrecordsnodes = transformedCSWGetRecordsResponse.GetElementsByTagName("Records");
               string xmlatt = xmlrecordsnodes[0].Attributes["numberOfRecordsMatched"].Value;
               numberOfRecordsMatched = !string.IsNullOrEmpty(xmlatt) ? int.Parse(xmlatt) : default(int?);
           }
           catch
           {
               //if the attribute cannot be found in the transformed response, the extraction is probably not implemented by the stylesheet
               numberOfRecordsMatched = null;
           }
       }

    }
}
