using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CsvReader
{

    List<string[]> dataList = new List<string[]>();

    // filePath : 拡張子はつけない
    public void ReadCSV(string filePath)
    {
        // csvをロード
        TextAsset csv = Resources.Load(filePath) as TextAsset;
        // csvをテキストに変換
        StringReader reader = new StringReader(csv.text);

        while (reader.Peek() > -1)
        {
            // 1行を格納
            string line = reader.ReadLine();

            // 空欄の場合は無視
            System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
            // CR＋LR の 改行 で区切る
            string[] stringArray = line.Split(new char[] { '\r', '\n' }, option);

            // [,] で 区切る
            stringArray = line.Split(',');

            // 先頭が '/' なら追加しない
            if (stringArray[0] == "//") return;

            dataList.Add(stringArray);
        }
    }

    public List<string[]> DataList { get { return dataList; } }
}
