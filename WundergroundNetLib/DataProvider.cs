﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WundergroundNetLib.Interfaces;

namespace WundergroundNetLib
{
    public class DataProvider : IDataProvider
    {
        private static DataProvider _provider = null; // singleton for minimal resource usage

        /// <summary>
        /// Static singleton class instantiating a DataProvider where none exist at the point of retrieval ensuring only one instance is created within scope, when required.
        /// </summary>
        public static DataProvider DefaultProvider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new DataProvider();
                }
                return _provider;
            }
        }

        private DataProvider() // ensure that we can instantiate a new DataProvider within Static constructor
        {

        }

        /// <summary>
        /// Get the combined json file including conditions, forecast and astronomy data and deserialise into 
        /// customised weather data classes using your string coordinates, executed as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="location"></param>
        /// <param name="dataFeatures"></param>
        /// <returns></returns>
        public async Task<IWeatherData> GetCombinedDataAsync(string latitude, string longitude)
        {
            UriProvider uriProvider = new UriProvider();
            Uri pwsUri = uriProvider.CreateCombinedDataUriFromCoordinates(latitude, longitude);
            return await CombinedWeatherDataAsync(pwsUri);
        }

        /// <summary>
        /// Get the combined json file including conditions, forecast and astronomy data and deserialise into 
        /// customised weather data classes using your double coordinates, executed as an asynchronous operation.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public async Task<IWeatherData> GetCombinedDataAsync(double latitude, double longitude)
        {
            UriProvider uriProvider = new UriProvider();
            Uri pwsUri = uriProvider.CreateCombinedDataUriFromCoordinates(latitude, longitude);
            return await CombinedWeatherDataAsync(pwsUri);
        }

        /// <summary>
        /// Get the combined json file including conditions, forecast and astronomy data and deserialise into 
        /// customised weather data classes using a specific pws station id ("ICANTERB275"), executed as an asynchronous operation.
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        public async Task<IWeatherData> GetCombinedDataAsync(string stationID)
        {
            UriProvider uriProvider = new UriProvider();
            Uri pwsUri = uriProvider.CreateCombinedDataUriFromPwsStationID(stationID);
            return await CombinedWeatherDataAsync(pwsUri);
        }

        /// <summary>
        /// Receives a uri and uses this to download a json file and deserialise it into the custom WeatherData object as an async operation.
        /// </summary>
        /// <param name="pwsUri"></param>
        /// <returns></returns>
        private async Task<IWeatherData> CombinedWeatherDataAsync(Uri pwsUri)
        {
            // Download Json data
            JsonProvider jsonProvider = new JsonProvider();
            string jsonData = await jsonProvider.DownloadJsonStringAsync(pwsUri);

            // Deserialise Json file into custom object
            JsonDeserializer jsonDeserialize = new JsonDeserializer();
            return await jsonDeserialize.JsonToWeatherDataAsync(jsonData);
        }
    }
}
