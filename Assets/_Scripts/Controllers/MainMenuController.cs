using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas rulesCanvas;

    public void StartGame()
    {
        LoadingManager.Instance?.LoadScene(AppScenes.GAME_SCENE);
    }
    
    public void ShowRules()
    {
        rulesCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }
}
