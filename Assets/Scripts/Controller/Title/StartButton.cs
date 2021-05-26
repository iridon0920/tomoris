using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private AudioSource StartSound;

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
        StartSound.PlayOneShot(StartSound.clip);
        SceneManager.LoadScene("GameScene");
    }
}
