using System;
using System.Text;
using System.Collections.Generic;

namespace pdok4arcgis
{
   public class CswSearchCriteria
    {
        public enum LogicalOperator
        {
            And,
            Or,
            Not
        }
        #region "Properties"
        public int StartPosition
        {
            get { return _startPosition; }
            set 
            {
                if (value >= 0) _startPosition = value;
                else throw new Exception("startPosition should be a value equal or larger than 0.");
            }
        }

        public int MaxRecords
        {
            get { return _maxRecords; }
            set
            {
                if (value >= 0) _maxRecords = value;
                else throw new Exception("maxRecords should be a value equal or larger than 0.");
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }
        public LogicalOperator SearchTextLogicalOperator
        {
            get { return _searchTextLO; }
            set { _searchTextLO = value; }
        }
        public LogicalOperator FilterLogicalOperator
        {
            get { return _filterLO; }
            set { _filterLO = value; }
        }
        public string Organisation
        {
            get { return _organisation; }
            set { _organisation = value; }
        }

        public Boolean LiveDataAndMapOnly
        {
            get { return _isLiveDataAndMapOnly; }
            set { _isLiveDataAndMapOnly = value; }
        }
        public Boolean RefinePreviousFilter
        {
            get { return _refinePreviousFilter; }
            set { _refinePreviousFilter = value; }
        }
        public List<string> TypesOfUse
        {
            get { return _typesOfUse; }
            set { _typesOfUse = value; }
        }

        public Envelope Envelope
        {
            get { return _envelope; }
            set { _envelope = value; }
        }
        #endregion

        private int _startPosition;
        private int _maxRecords;
        private string _searchText;
        private string _organisation;
        private Boolean _isLiveDataAndMapOnly;
        private Boolean _refinePreviousFilter;
        private Envelope _envelope;
        private List<string> _typesOfUse = new List<string>();
        private LogicalOperator _searchTextLO;
        private LogicalOperator _filterLO;
    }

}
