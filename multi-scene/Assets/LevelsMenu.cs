using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("GameLevel1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("GameLevel2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("GameLevel3");
    }

    public void ResetProgres()
    {
        SaveLoad.data.levelsDone = 0;
        SaveLoad.Save();
    }

    private void OnEnable()
    {
        transform.Find("Level2").gameObject.SetActive(SaveLoad.data.levelsDone >= 1);
        transform.Find("Level3").gameObject.SetActive(SaveLoad.data.levelsDone >= 2);
    }

    private void Awake()
    {
        SaveLoad.Load();
    }
}
