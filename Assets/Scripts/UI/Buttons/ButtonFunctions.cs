using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LevelSelectionPanel;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject AnyPanel;

    public void StartGame()
    {
        // reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("IntroArea");
        Time.timeScale = 1f;
        ResetPlayerSpawn();
    }

    public void QuitGame()
    {
        Application.Quit();
        ResetPlayerSpawn();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ResetPlayerSpawn();
    }

    public void ToggleLevelSelectionPanel()
    {
        LevelSelectionPanel.SetActive(!LevelSelectionPanel.activeSelf);
        MainMenu.SetActive(!MainMenu.activeSelf);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
        ResetPlayerSpawn();
        Time.timeScale = 1f;
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
        ResetPlayerSpawn();
        Time.timeScale = 1f;
    }

    public void LoadLevel2Demo()
    {
        SceneManager.LoadScene("Level2Demo");
        ResetPlayerSpawn();
        Time.timeScale = 1f;
    }

    public void LoadIntroArea()
    {
        SceneManager.LoadScene("IntroArea");
        ResetPlayerSpawn();
        Time.timeScale = 1f;
    }

    private void ResetPlayerSpawn()
    {
        PlayerPrefs.SetString("lastCheckpointName", "nothing");
    }

    public void ToggleAnyPanel()
    {
        AnyPanel.SetActive(!AnyPanel.activeSelf);
    }
}
