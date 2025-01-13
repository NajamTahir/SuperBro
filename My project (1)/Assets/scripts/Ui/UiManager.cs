using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header ("Gamer Over Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause Panel")]
    [SerializeField] private GameObject pausePanel;


    private void Awake(){
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pausePanel.activeSelf){
                PauseGame(false);
            }else{
                PauseGame(true);
            }
        }
    }
    public void GameOver(){
        gameOverPanel.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu(){
        SceneManager.LoadScene(0);
    }
    public void Quit(){
        Application.Quit();


        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void PauseGame(bool status){
        pausePanel.SetActive(status);
        if(status){ //when the pause status is true the game will be paused and if false the game will be resumed
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
    }
    public void SoundVolume(){
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume(){
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
}
