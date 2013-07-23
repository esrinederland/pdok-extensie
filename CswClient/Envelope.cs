using System;
using System.Text;

namespace pdok4arcgis
{
    /// <summary>
    /// Envelope defined as in geographical coordinate system. Used for CSW search criteria.
    /// </summary>
    public class Envelope
    {
        private double _minX, _minY, _maxX, _maxY;

        #region Properties
        /// <summary>
        /// Max longitude
        /// </summary>
        public double MinX
        {
            get { return _minX; }
            set 
            {
                //if (value < -180 || value > 180) { throw new ArgumentOutOfRangeException(); }
                //else
                //{
                _minX = value;
                //}
            }
        }

        /// <summary>
        /// Min latitude
        /// </summary>
        public double MinY
        {
            get { return _minY; }
            set
            {
                //if (value < -90 || value > 90) { throw new ArgumentOutOfRangeException(); }
                //else
                //{
                _minY = value;
                //}
            }
        }

        /// <summary>
        /// Max longitude
        /// </summary>
        public double MaxX
        {
            get { return _maxX; }
            set
            {
                //if (value < -180 || value > 180) { throw new ArgumentOutOfRangeException(); }
                //else
                //{
                _maxX = value;
                //}
            }
        }

        /// <summary>
        /// Max latitude
        /// </summary>
        public double MaxY
        {
            get { return _maxY; }
            set
            {
                //if (value < -90 || value > 90) { throw new ArgumentOutOfRangeException(); }
                //else
                //{
                _maxY = value;
                //}
            }
        }
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Envelope constructor
        /// </summary>
        public Envelope()
        {
            _minX = _minY = _maxX = _maxY = 0.0;
        }

        /// <summary>
        /// Envelope constructor
        /// </summary>
        /// <param name="minX">Min longitude of the envelope</param>
        /// <param name="minY">Min latitude of the envelope</param>
        /// <param name="maxX">Max longitude of the envelope</param>
        /// <param name="maxY">Max latitude of the envelope</param>
        public Envelope(double minX, double minY, double maxX, double maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }
        #endregion
    }
}
