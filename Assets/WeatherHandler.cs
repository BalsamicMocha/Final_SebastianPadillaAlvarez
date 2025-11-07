using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using static System.Net.WebRequestMethods;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using Bloom = UnityEngine.Rendering.Universal.Bloom;

public class WeatherHandler : MonoBehaviour
{
    [SerializeField] private float lat;
    [SerializeField] private float lon;
    [SerializeField] private WeatherData weatherData;
    [SerializeField] private Volume globalVolume;

    private Bloom bloom;

    private string apiKey = "2d7c3a3aaa7c036a4477e3a3612813e1";

    private string url;

    private string jsonRAW;


    private void Start()
    {
        url = $"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude=hourly,daily&appid={apiKey}&units=metric";

        StartCoroutine(UpdateWeather());
    }

    IEnumerator UpdateWeather()
    {
        UnityWebRequest request = new UnityWebRequest(url);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            jsonRAW = request.downloadHandler.text;
            Debug.Log(jsonRAW);

            ReadJson();
            ApplyWeatherToVolume();
        }
    }

    private void ReadJson()
    {
        var weatherJson = JSON.Parse(jsonRAW);

        weatherData.timeZone = weatherJson["timezone"].Value;
        weatherData.temp = float.Parse(weatherJson["current"]["temp"].Value);
        weatherData.weatherDescription = weatherJson["current"]["weather"][0]["description"].Value;
    }

    private void ApplyWeatherToVolume()
    {        
                
        globalVolume.profile.TryGet(out bloom);

        if (bloom != null)
        {          
            bloom.intensity.value = Mathf.Lerp(0.3f, 2f, Mathf.InverseLerp(0, 40, weatherData.temp));
           
            bloom.tint.value = Color.Lerp(Color.cyan, Color.red, Mathf.InverseLerp(0, 40, weatherData.temp));
        }

    }
}
