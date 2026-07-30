using UnityEngine;
using UnityEngine.UI;

public class FileSelectUI : MonoBehaviour
{
    public Image fileEmptyImage;
    public Image fileDataImage;

    private SaveData currentSaveData;

    private void Start()
    {
    }

    public void OnClickFile()

    {
        if(currentSaveData.isFileExist)
        {

        }
        else
        {
            SaveManager.Instance.Save(new SaveData());
        }
    }

    public void OnClickDeleteFile()
    {
        SaveManager.Instance.DeleteSave();
    }
}
