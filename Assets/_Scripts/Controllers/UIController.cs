using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [Header("Text Colors")]
    [SerializeField] private Color coinColor;
    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;
    
    [Header("Coin Text")]
    [SerializeField] private TMP_Text coinText;
    
    [Header("Final Text")]
    [SerializeField] private TMP_Text finalText;
    
    [Header("Canvas")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas finalCanvas;
    [SerializeField] private Canvas pauseCanvas;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ShowGameOver(bool hasWon)
    {
        finalCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        
        finalText.text = hasWon ? "You Won!" : "Game Over!";
        finalText.color = hasWon ? winColor : loseColor;
    }
    
    public void ShowPauseMenu(bool isPaused)
    {
        pauseCanvas.gameObject.SetActive(isPaused);
        mainCanvas.gameObject.SetActive(!isPaused);
        finalCanvas.gameObject.SetActive(false);
    }

    public void UpdateScore(int coins)
    {
        coinText.text = $"{coins}$";
    }
    
    public void BackToMainMenu()
    {
        LoadingManager.Instance?.LoadScene(AppScenes.MAIN_MENU_SCENE);
    }
}
