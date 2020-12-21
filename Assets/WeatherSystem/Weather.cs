using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weather", menuName = "Weather/Weather", order = 1)]
public class Weather : ScriptableObject
{
    [SerializeField]
    public int weatherID;

    public bool usesParticles;
    public Material weatherParticle;
    public float weatherPower;
    public Color weatherColor;

    public bool PreviousWeatherRequired;
    public Weather PreviousWeather;
}
