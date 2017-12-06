using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager = null;
    
    CsvReader csvReader = new CsvReader();
    string[] filePaths = new string[] {
		"WaveData_Stage1",
		"WaveData_Stage2",
		"WaveData_Stage3",
		"WaveData_Stage4",
		"WaveData_Stage5",};

    static List<string[]> waveData = new List<string[]>();

    void Awake()
    {
        if (gameManager == null) gameManager = this;
        else if (gameManager != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
		string filePath = "";

		// ウェーブデータを選択
		if (true) { filePath = filePaths[0]; }

        // ウェーブデータをロード
        waveData = csvReader.ReadCSV(filePath);
    }

    float t;
    void Update()
    {
        //t += Time.deltaTime;
        if (t > 3)
        {
            t = 0;
            SceneManager.LoadScene("Game");
        }
    }

	public void ToHomeScene() { SceneManager.LoadScene("Home"); }
}
