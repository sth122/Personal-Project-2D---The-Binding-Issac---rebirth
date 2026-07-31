using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private string savePath;
    private string fileName = "saveFile.json";

    public void Save(SaveData saveData)
    {
        saveData.isFileExist = true;
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"데이터 저장 {savePath}");
    }

    public SaveData Load()
    {
        if(File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.Log("No have save Data");
            return new SaveData();
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("세이브 파일 삭제");
        }
    }
}
