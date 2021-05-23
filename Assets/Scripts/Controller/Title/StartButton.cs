using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void Start1PlayerMode()
    {
        TitleToGameDataSender.PlayerCount = 1;
        LoadGameScene();
    }

    public void Start2PlayerMode()
    {
        TitleToGameDataSender.PlayerCount = 2;
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
