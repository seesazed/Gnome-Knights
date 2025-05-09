using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public WaveManager waveManager;
    public Light directionalLight;
    public GameObject shopUI;

    private int currentDay = 1;
    private int currentWave = 0;
    private int wavesPerNight;
    private bool isNight = true;
    private bool inShop = false;

    private readonly List<int> wavePattern = new List<int> { 3, 4, 6, 10 }; // Can expand or change later

    void Start()
    {
        BeginNight();
    }

    void Update()
    {
        if (isNight && waveManager.IsWaveCleared() && !waveManager.IsWaveInProgress())
        {
            currentWave++;

            if (currentWave >= wavesPerNight)
            {
                BeginDay();
            }
            else
            {
                waveManager.StartNextWave();
            }
        }
    }

    void BeginNight()
    {
        isNight = true;
        inShop = false;
        currentWave = 0;
        wavesPerNight = GetWaveCountForDay(currentDay);

        Debug.Log("Night begins! Day: " + currentDay + ", Waves this night: " + wavesPerNight);

        directionalLight.color = Color.black;
        shopUI.SetActive(false);

        // Lock and hide cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        waveManager.StartNextWave();
    }

    void BeginDay()
    {
        isNight = false;
        inShop = true;
        Debug.Log("Daytime! Shop is open.");

        directionalLight.color = Color.white;
        shopUI.SetActive(true);

        // Unlock and show cursor for shop
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnShopDone()
    {
        if (inShop)
        {
            currentDay++;
            BeginNight();
        }
    }

    int GetWaveCountForDay(int day)
    {
        if (day - 1 < wavePattern.Count)
            return wavePattern[day - 1];
        else
            return Mathf.RoundToInt(Mathf.Pow(day, 1.5f)); // Custom formula for later days
    }
}
