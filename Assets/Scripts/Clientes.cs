using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Clientes : MonoBehaviour
{

    [Header("Simulación")]
    public int initialClients = 0;
    public int actualClients;
    public int EntradaPerMin = 3;
    public int SalidaPerMin = 1;

    [Header("Tiempo")]
    public float secondsPerMin = 3f;
    private int min = 0;
    private float timer = 0;

    [Header("Visual")]
    public GameObject clientPrefab;
    public Transform shopArea;  
    public float spacing = 1f; //Espacio de los clientes
    public float rowSpacing = 1.5f; //Espacio de las filas
    public int clientsPerRow = 15; // Cantidad de clientes en la fila
    private List<GameObject> clientObjects = new List<GameObject>();

    //posicion inicial (preguntar al profe)
    public float startX = 5f;     
    public float startY = -4f;


    void Start()
    {
        actualClients = initialClients;
        DrawShop();
        Debug.Log("min " + min + ": " + actualClients + " Clientes");
    }



    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secondsPerMin)
        {
            timer = 0;
            SimulateMinute();
        }
    }

    void SimulateMinute()
    {

        min++;

        int salida;

        // Condicion para no restar al inicio
        if (actualClients > 0)
        {
            salida = SalidaPerMin;
        }
        else
        {
            salida = 0;
        }

        actualClients += EntradaPerMin;
        actualClients -= salida;

        if (actualClients < 0) actualClients = 0;

        ClearShop();
        DrawShop();

        Debug.Log("Minuto " + min + ": llegan " + EntradaPerMin + ", Cliente atendido " + salida + " , Total de Clientes:  " + actualClients + ", actualClients");
    }

    void DrawShop()
    {
        for (int i = 0; i < actualClients; i++)
        {
            //Fila y columna
            int row = i / clientsPerRow; // 0 / 15
            int col = i % clientsPerRow; // 0 % 15

            //Lugar o coordenadas
            float x = startX - col * spacing;
            float y = startY + row * rowSpacing; ;

            Vector3 rowPos = new Vector3(x, y, 0f);
            Vector3 worldPos = shopArea.position + rowPos;

            GameObject client = Instantiate(clientPrefab, worldPos, Quaternion.identity);
            clientObjects.Add(client);
        }

    }

    private void ClearShop()
    {
        foreach (GameObject client in clientObjects)
        {
            Destroy(client);
        }
        clientObjects.Clear();

    }
}