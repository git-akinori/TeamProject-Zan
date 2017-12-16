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

    static List<string[]> waveDataList = new List<string[]>();

    void Awake()
    {
        if (gameManager == null) gameManager = this;
        else if (gameManager != this) Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
		string filePath = "";

		// ウェーブデータを選択
		if (true) { filePath = filePaths[0]; }

		// ウェーブデータをロードしてリスト化
		waveDataList = csvReader.ReadCSV(filePath);
		//var arr = csvReader.ReadCSV(filePath).ToArray();
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

	public static GameManager Member { get { return gameManager; } }

	public List<string[]> WaveDataList { get { return waveDataList; } }

	public void ToHomeScene() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("Home");
	}
}
