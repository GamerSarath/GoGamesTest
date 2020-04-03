using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region fields

    private static GameManager _instance; // SingleTon  private instance
    public string[] playersArray;
    public List<string> playersList;
    [SerializeField]
    private int sceneloadedIndex; // currently loaded scene index

    #endregion

    #region Singleton
    public static GameManager Instance
    {
        get
        {
            //logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
                go.tag = "GameManager";
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion



    void Awake()
    {
        _instance = this;
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;



    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;

    }

    #region Level Loaded CallBack // Callback when finished loading a level
    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        sceneloadedIndex = scene.buildIndex;
        UIManager.Instance.ActionsNeededOnLevelLoad(sceneloadedIndex);

        switch (scene.buildIndex)
        {
            case 1:
                break;

        }
    }
    #endregion

 
}
public class PlayerInfo
{
    public enum PlayerType { Attacker, Defender };
    public string playerName;
    public PlayerType playerType;
    public string playerPlace;
    public string playerMobile;


}


