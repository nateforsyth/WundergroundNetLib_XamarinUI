using System.Collections.Generic;

namespace WundergroundNetLib.Interfaces
{
    public interface IWeatherData
    {
        CurrentConditions currentConditions { get; set; }
        List<IForecast> fourDayForecast { get; set; }
        ObservationLocation observationLocation { get; set; }
    }
}