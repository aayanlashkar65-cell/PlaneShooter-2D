using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseButton;
    public GameObject GameOverPannel;
    public GameObject levelCompletePanel;
    public GameObject EndText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        EndText.SetActive(false);
        levelCompletePanel.SetActive(false);
        GameOverPannel.SetActive(false);
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pauseGame()
    {
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
         PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
    public IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(2f);
        EndText.SetActive(true);
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        levelCompletePanel.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void gameOver()
    {
        GameOverPannel.SetActive(true);
        PauseButton.SetActive(false);
    }
}
