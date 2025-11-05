using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using static System.Net.WebRequestMethods;
using UnityEngine.Networking;

public class WeatherHandler : MonoBehaviour
{
    [SerializeField] private float lat;
    [SerializeField] private float lon;
    [SerializeField] private WeatherData weatherData;

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
            jsonRAW= request.downloadHandler.text;
            Debug.Log(jsonRAW);
        }
    }
}
