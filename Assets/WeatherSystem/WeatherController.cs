using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class WeatherController : MonoBehaviour
{
    public WeatherTable availableWeatherTable;

    public ParticleSystem weatherParticles;
    public ParticleSystemRenderer weatherParticleRenderer;
    public ParticleSystem.VelocityOverLifetimeModule velocityOverLifeTime;
    public UnityEngine.Experimental.Rendering.Universal.Light2D weatherColor;

    public float minWeatherTime;
    public float maxWeatherTime;
    // Start is called before the first frame update
    void Start()
    {
        weatherParticleRenderer = GetComponent<ParticleSystemRenderer>();
        availableWeatherTable.setWeather(0);
        velocityOverLifeTime = weatherParticles.velocityOverLifetime;
        StartCoroutine(RotateWeathers());
    }

    IEnumerator RotateWeathers()
    {
        while (true)
        {
            // Weather change time
            yield return new WaitForSeconds(Random.Range(minWeatherTime, maxWeatherTime));

            bool validWeather = false;
            int randNum;
            do
            {
                randNum = Random.Range(0, availableWeatherTable.getNumberOfWeathers());

                if (availableWeatherTable.getCurrentWeather() != availableWeatherTable.getWeather(randNum))
                {
                    if (availableWeatherTable.getWeather(randNum).PreviousWeatherRequired)
                    {
                        if (availableWeatherTable.getCurrentWeather() == availableWeatherTable.getWeather(randNum).PreviousWeather)
                        {
                            availableWeatherTable.setWeather(randNum);
                            validWeather = true;
                        }
                    }

                    else
                    {
                        availableWeatherTable.setWeather(randNum);
                        validWeather = true;
                    }
                }

            } while (!validWeather);

            if(availableWeatherTable.getCurrentWeather().usesParticles)
            {
                weatherParticles.Play();
                weatherParticleRenderer.material = availableWeatherTable.getCurrentWeather().weatherParticle;
            }
            else
            {
                weatherParticles.Stop();
            }

            Debug.Log("Changed Weather to Weather ID: " + availableWeatherTable.getCurrentWeather().name);
        }
    }
    // Update is called once per frame
    void Update()
    {
        weatherParticles.emissionRate = Mathf.Lerp(weatherParticles.emissionRate, (10.0f * availableWeatherTable.getCurrentWeather().weatherPower), 0.005f);
        velocityOverLifeTime.speedModifierMultiplier = Mathf.Lerp(velocityOverLifeTime.speedModifierMultiplier, availableWeatherTable.getCurrentWeather().weatherPower, 0.005f);
        weatherColor.color = Color.Lerp(weatherColor.color, availableWeatherTable.getCurrentWeather().weatherColor, 0.005f);
    }
}
