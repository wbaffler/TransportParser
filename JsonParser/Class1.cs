using System;
using System.Text.Json;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;

namespace JsonParser
{

    public class TransportDataParser
    {
        public List<double> Longitude
        {
            get
            {
                return _longitude;
            }
        }

        public List<double> Latitude
        {
            get
            {
                return _latitude;
            }
        }

        public List<string> StopName
        {
            get
            {
                return _stopName;
            }
        }

        public List<string> District
        {
            get
            {
                return _district;
            }
        }

        public List<string> RouteNumber
        {
            get
            {
                return _routeNumber;
            }
        }

        public List<string> Track
        {
            get
            {
                return _track;
            }
        }

        public List<string> Type
        {
            get
            {
                return _typeOfTransport;
            }
        }

        private List<double> _longitude = new List<double>();
        private List<double> _latitude = new List<double>();
        private List<string> _stopName = new List<string>();
        private List<string> _district = new List<string>();
        private List<string> _routeNumber = new List<string>();
        private List<string> _track = new List<string>();
        private List<string> _typeOfTransport = new List<string>();

        private string _jsonString = "";
        private JsonElement _rootJson = new JsonElement();

        public void JsonParser(string path)
        {
            if (path == null)
                throw new FileNotFoundException();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string fileName = path;
            _jsonString = File.ReadAllText(fileName/*, System.Text.Encoding.GetEncoding(1251)*/);
            //Console.WriteLine(jsonString);
            JsonDocument doc = JsonDocument.Parse(_jsonString);
            _rootJson = doc.RootElement;
        }
        public void StopsParser()
        {

            var elements = _rootJson.EnumerateArray();
            foreach (var el in elements)
            {
                _longitude.Add(Convert.ToDouble(el.GetProperty("Longitude_WGS84").ToString()));
                _latitude.Add(Convert.ToDouble(el.GetProperty("Latitude_WGS84").ToString()));
                _stopName.Add(el.GetProperty("Name").ToString());
                _district.Add(el.GetProperty("District").ToString());
            }
        }

        public void TransportParser()
        {
            var elements = _rootJson.EnumerateArray();
            foreach (var el in elements)
            {
                _routeNumber.Add(el.GetProperty("RouteNumber").ToString());
                _track.Add(el.GetProperty("TrackOfFollowing").ToString());
                _typeOfTransport.Add(el.GetProperty("TypeOfTransport").ToString());
            }
        }
    }

}
