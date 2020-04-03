using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    #region fields

    private static UIManager _instance; // SingleTon  private instance

    [SerializeField]
    int currentloadedGameSceneIndex;


    [SerializeField]
    Dropdown playerType;

    [SerializeField]
    InputField playerName;

    [SerializeField]
    InputField playerPlace;

    [SerializeField]
    InputField playerMobile;

    [SerializeField]
    GameObject homePanel;

    [SerializeField]
    GameObject loadplayerPanel;

    [SerializeField]
    GameObject newplayerPanel;

    [SerializeField]
    string playerInfojson = "";
    #endregion

    #region Singleton
    public static UIManager Instance
    {
        get
        {
            //logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("UIManager");
                go.AddComponent<UIManager>();
                go.tag = "UIManager";
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion

    void Awake()
    {
        _instance = this;
        SaveSystem.Init();
    }

    #region Actions Needed On Level Load
    public void ActionsNeededOnLevelLoad(int index)
    {
        Debug.Log("Actions Needed on Level Load Function is Called");
        currentloadedGameSceneIndex = index;
       
        switch (index) // switch case that determine which scene is loadeded depending upon on the buildindex of the currently loaded scene and performing the necessary actions
        {
            case 0:
                Debug.Log("Splash Screen is loaded ");
                break;

            case 1:

                playerName = GameObject.FindGameObjectWithTag("PlayerNameField").GetComponent<InputField>();

                playerPlace = GameObject.FindGameObjectWithTag("PlayerPlaceField").GetComponent<InputField>();

                playerMobile = GameObject.FindGameObjectWithTag("PlayerMobileField").GetComponent<InputField>();

                playerType = GameObject.FindGameObjectWithTag("PlayerTypeDropDown").GetComponent<Dropdown>();

                homePanel = GameObject.FindGameObjectWithTag("HomePanel");

                loadplayerPanel = GameObject.FindGameObjectWithTag("LoadPlayerPanel");

                newplayerPanel = GameObject.FindGameObjectWithTag("NewPlayerPanel");

                GameObject.FindGameObjectWithTag("SaveButton").GetComponent<Button>().onClick.AddListener(OnClickSaveButton);// Assigning the event OnClickNextButton to the button Next Button.

                GameObject.FindGameObjectWithTag("NextButton").GetComponent<Button>().onClick.AddListener(OnClickPlayButton);// Assigning the event OnClickRetrieveInfoButton to the button RetrieveInfoButton Button.

                GameObject.FindGameObjectWithTag("LoadPlayerButton").GetComponent<Button>().onClick.AddListener(OnClickLoadPlayerButton);// Assigning the event OnClickLoadPlayerButton to the button Load player Button.

                GameObject.FindGameObjectWithTag("NewPlayerButton").GetComponent<Button>().onClick.AddListener(OnClickNewPlayerButton);// Assigning the event OnClickNewPlayerButton to the button Newplayer Button.

                GameObject.FindGameObjectWithTag("LoadPlayerBackButton").GetComponent<Button>().onClick.AddListener(OnClickHomeButton);// Assigning the event OnClickHomeButton to the button home Button.

                GameObject.FindGameObjectWithTag("NewPLayerBackButton").GetComponent<Button>().onClick.AddListener(OnClickHomeButton);// Assigning the event OnClickHomeButton to the button home Button.

                GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>().onClick.AddListener(OnClickExitButton);// Assigning the event OnClickHomeButton to the button home Button.

                GameObject.FindGameObjectWithTag("LoadButton").GetComponent<Button>().onClick.AddListener(OnClickLoadButton);// Assigning the event OnClickLoadButton to the button load Button.
                #region Initially Setting HomePanel On
                homePanel.SetActive(true);
                loadplayerPanel.SetActive(false);
                newplayerPanel.SetActive(false);
                break;

            case 2:

                GameObject.FindGameObjectWithTag("HomeButton").GetComponent<Button>().onClick.AddListener(OnClickPlayHomeButton);// Assigning the event OnClickHomeButton to the button home Button.
                GameObject.FindGameObjectWithTag("JsonText").GetComponent<Text>().text = playerInfojson;
                Debug.Log("Main Scene is loaded ");
                break;

                #endregion


        }
    }
    #endregion

    #region OnClick Next Button Event

    void OnClickSaveButton()
    {
        PlayerInfo.PlayerType player = PlayerInfo.PlayerType.Attacker ;
        Debug.Log("Clicking save");
        if (playerType.value == 0) // assigning variable player the player type according to drop down value(player selected value)
        {
            player = PlayerInfo.PlayerType.Attacker; // assigning the var player the player type attacker
        }
        else
        {
            player = PlayerInfo.PlayerType.Defender;// assigning the var player the player type defender
        }

        // Populating Player Info with all the data in the new fields
        PlayerInfo playerInfo = new PlayerInfo { playerName = playerName.text, playerPlace = playerPlace.text, playerMobile = playerMobile.text, playerType = player };
        GameManager.Instance.playersList.Add(playerInfo.playerName);
        // saving the player name to the device saved array PlayerNames.  
        PlayerPrefsExtension.SetStringArray("PlayerNames", GameManager.Instance.playersList.ToArray());

        playerInfojson = JsonUtility.ToJson(playerInfo);
        Debug.Log("player info is " + playerInfojson);
        SaveSystem.Save(playerInfojson);
        
       
    }
    #endregion


    #region OnClickNextButton Event

    void OnClickPlayButton()
    {
        SceneLoadingManager.Instance.LoadMainScene();
        Debug.Log("clicked next");
    }
    #endregion

    #region OnClickPlayerButton Event

    void OnClickLoadPlayerButton()
    {
        loadplayerPanel.SetActive(true);
        homePanel.SetActive(false);
        newplayerPanel.SetActive(false);

        
    }
    #endregion

    #region OnClickNewPlayerButton Event

    void OnClickNewPlayerButton()
    {
        loadplayerPanel.SetActive(false);
        homePanel.SetActive(false);
        newplayerPanel.SetActive(true);
    }
    #endregion

    #region OnClickHomeButton Event

   
    void OnClickHomeButton()
    {
        loadplayerPanel.SetActive(false);
        homePanel.SetActive(true);
        loadplayerPanel.SetActive(false);
    }
    #endregion

    #region OnClickExitButton Event
    
    void OnClickExitButton()
    {
        Application.Quit();
    }
    #endregion

    #region OnClickPlayHomeButton Event
    void OnClickPlayHomeButton()
    {
        SceneLoadingManager.Instance.LoadTitleScene();
    }
    #endregion


    #region OnClickLoadButton Event
    void OnClickLoadButton()
    {
        Debug.Log("All the PLayer Information is available in the text file in the json. Easily updatable");
    }
    #endregion
}
