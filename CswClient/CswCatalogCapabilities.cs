using System;
using System.Collections.Generic;
using System.Text;

namespace pdok4arcgis {
    /// Public catalog containing catalog capabilities information.
    /// </summary>
    /// <remarks>
    /// </remarks>
   public class CswCatalogCapabilities {

       private string _getRecordByIDGetURL;
       private string _getRecordsPostURL;
       private bool _isSoapEndPoint = false;

       #region "Properties"

       internal string GetRecordByID_GetURL {
           set {
               _getRecordByIDGetURL = Utils.EnsureTrailingQuestionOrAmpersandInURL(value);
           }
           get {
               return _getRecordByIDGetURL;
           }

       }

       internal string GetRecords_PostURL {
           set {
               _getRecordsPostURL = value;
           }
           get {
               return _getRecordsPostURL;
           }
       }

       internal bool GetRecords_IsSoapEndPoint
       {
           set
           {
               _isSoapEndPoint = value;
           }
           get
           {
               return _isSoapEndPoint;
           }
       }

       #endregion



   }
}
