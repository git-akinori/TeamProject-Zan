using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager = null;
    
    CsvReader csvReader = new CsvReader();
    const string stagePath = "stage";

    void Awake()
    {
        if (gameManager == null) gameManager = this;
        else if (gameManager != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        csvReader.ReadCSV(stagePath);
        Debug.Log(csvReader.DataList[0]);
    }

    void Update()
    {
        
    }
}
