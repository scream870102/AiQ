using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Singleton
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    void Awake ( ) {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (this);
            name = "GameManager";
        }
    }
    #endregion Singleton
    UIManager _UIManager;
    string currentScene;
    public UIManager UIManager { get { return _UIManager; } }
    public GlobalData Data;
    public float MusicTime { get; set; }

    [SerializeField]
    string InitScene;
    void Init ( ) {
        _UIManager = new UIManager ( );
        SetScene (InitScene);

    }
    public void SetScene (string sceneName) {
        SceneManager.LoadScene (sceneName);
        this.currentScene = sceneName;

    }
}
