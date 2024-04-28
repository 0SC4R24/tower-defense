using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }

    private string m_sceneName;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName) {
        // Debug.Log("Loading scene: " + sceneName);
        m_sceneName = sceneName;
        SceneManager.LoadScene(AppScenes.LOADING_SCENE, LoadSceneMode.Single);
    }

    public string getSceneName() {
        return m_sceneName;
    }
}
