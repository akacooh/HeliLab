using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Image background;
    [SerializeField] private Text startText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Button startButton;
    [SerializeField] private Text buttonText;

    //For different methods on one button
    public UnityAction ButtonFunc;

    // Start is called before the first frame update
    void Start()
    {
        ButtonFunc = StartGame;
        startButton.onClick.AddListener(CallAction);
        playerController.GameEnded.AddListener(RestoreUI);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Soldiers saved: " + playerController.savedAllies;
    }

    public void StartGame()
    {
        background.enabled = false;
        startButton.gameObject.SetActive(false);
        startText.enabled = false;
        playerController.gameOver = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //like a pause menu on gameover
    private void RestoreUI()
    {
        ButtonFunc = RestartGame;
        background.enabled = true;
        startText.text = "You saved " + playerController.savedAllies + " soldier(s)";
        scoreText.enabled = false;
        startText.enabled = true;
        startButton.gameObject.SetActive(true);
        buttonText.text = "RESTART";
        Time.timeScale = 0;       
    }

    private void CallAction()
    {
        ButtonFunc();
    }
}
