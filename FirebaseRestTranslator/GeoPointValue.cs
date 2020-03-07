using System.Collections.Generic;

namespace FirebaseRestTranslator
{
    public class GeoPointValue
    {
        private readonly double _latitude;
        private readonly double _longitude;

        public GeoPointValue(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public Dictionary<string, object> ToValue()
        {
            return new Dictionary<string, object>
            {
                ["latitude"] = _latitude,
                ["longitude"] = _longitude
            };
        }
    }
}