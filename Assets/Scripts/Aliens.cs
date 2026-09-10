using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Aliens : MonoBehaviour
{
    [Header("Simulación")]
    public int initialHumans = 10;
    public int initialAliens = 10;
    public int actualHumans;
    public int actualAliens;
    public int abductionsPerDay = 2;

    [Header("Tiempo")]
    public float secondsPerDay = 1f;
    private int day = 0;
    private float timer = 0;

    [Header("Visual")]
    public GameObject humanPrefab;
    public GameObject alienPrefab;
    public Transform cityArea;
    private List<GameObject> humanObjects = new List<GameObject>();
    private List<GameObject> alienObjects = new List<GameObject>();

    void Start()
    {
        actualAliens = initialAliens;
        actualHumans = initialHumans;
        DrawCity();
        Debug.Log("Dia " + day + ": " + actualHumans + " humanos y " + actualAliens + " aliens");
    }

    void DrawCity()
    {
        for (int i = 0; i < actualHumans; i++)
        {
            Vector3 randomPos = new Vector3(
                UnityEngine.Random.Range(-cityArea.localScale.x / 2, cityArea.localScale.x / 2),
                UnityEngine.Random.Range(-cityArea.localScale.y / 2, cityArea.localScale.y / 2),
                0
            );

            Vector3 worldPos = cityArea.position + randomPos;

            GameObject human = Instantiate(humanPrefab, worldPos, Quaternion.identity);
            humanObjects.Add(human);
        }

        for (int i = 0; i < actualAliens; i++)
        {
            Vector3 randomPos = new Vector3(
                UnityEngine.Random.Range(-cityArea.localScale.x / 2, cityArea.localScale.x / 2),
                UnityEngine.Random.Range(-cityArea.localScale.y / 2, cityArea.localScale.y / 2),
                0
            );

            Vector3 worldPos = cityArea.position + randomPos;

            GameObject alien = Instantiate(alienPrefab, worldPos, Quaternion.identity);
            alienObjects.Add(alien);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secondsPerDay)
        {
            timer = 0;
            SimulateDay();
        }
    }

    void SimulateDay()
    {
        if (actualHumans <= 0 || actualAliens <= 0) return;

        day++;

        int abductions = actualAliens * abductionsPerDay;

        if (abductions > actualHumans) abductions = actualHumans;

        actualHumans -= abductions;

        if (day % 3 == 0 && actualAliens > 0)
        {
            actualAliens--;
            Debug.Log("Alien muerto");
        }

        ClearCity();
        DrawCity();

        Debug.Log("Dia " + day + ": " + actualHumans + " humanos y " + actualAliens + " aliens");

        if (actualHumans <= 0)
        {
            Debug.Log("La humanidad ha caido");
        }
        else if (actualAliens <= 0)
        {
            Debug.Log("La humanidad ha ganado la guerra");
        }
    }

    private void ClearCity()
    {
        foreach (GameObject human in humanObjects)
        {
            Destroy(human);
        }
        foreach (GameObject alien in alienObjects)
        {
            Destroy(alien);
        }

        humanObjects.Clear();
        alienObjects.Clear();
    }
}