using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager = null;
    
    enum EnumCsvName
    {
        StageData,
    }

    CsvReader csvReader = new CsvReader();
    string[] filePaths = new string[] { EnumCsvName.StageData.ToString() };
    static List<string[]> spawnData = new List<string[]>();

    [SerializeField]
    GameObject player;
    [SerializeField]
    Vector3 playerOffset;

    void Awake()
    {
        if (gameManager == null) gameManager = this;
        else if (gameManager != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // スポーン情報をロード
        spawnData = csvReader.ReadCSV(filePaths[0]);

        // プレイヤーを生成
        Instantiate(player, transform).transform.position += playerOffset;
    }
    float t;
    void Update()
    {
        t += Time.deltaTime;
        if (t > 3)
        {
            t = 0;
            SceneManager.LoadScene("Game");
        }
    }
}
