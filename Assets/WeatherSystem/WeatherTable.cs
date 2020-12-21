using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeatherTable", menuName = "Weather/WeatherTable", order = 2)]
public class WeatherTable : ScriptableObject
{
    public Weather[] weathers;

    private Weather currentWeather;

    void Start()
    {
    }

    // Sets the current weather
    public void setWeather(int weatherID)
    {
        currentWeather = weathers[weatherID];
    }

    public Weather getCurrentWeather()
    {
        return currentWeather;
    }

    public Weather getWeather(int weatherID)
    {
        return weathers[weatherID];
    }

    public int getNumberOfWeathers()
    {
        return weathers.Length;
    }
}
