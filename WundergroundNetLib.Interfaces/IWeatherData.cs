using System.Collections.Generic;

namespace WundergroundNetLib.Interfaces
{
    public interface IWeatherData
    {
        ICurrentConditions currentConditions { get; set; }
        List<IForecast> fourDayForecast { get; set; }
        IObservationLocation observationLocation { get; set; }
    }
}