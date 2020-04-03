using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{

    #region fields

    private static MemoryManager _instance; // SingleTon  private instance

    #endregion


    #region Singleton
    public static MemoryManager Instance
    {
        get
        {
            //logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("MemoryManager");
                go.AddComponent<MemoryManager>();
                go.tag = "MemoryManager";
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        Debug.Log(UIManager.Instance.name);
        InitialMemoryLoading();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        SceneLoadingManager.Instance.LoadTitleScene(); // Loading the title scene after the splash scene
    }

    #region Initial Memory Loading

    void InitialMemoryLoading() // Initially Loading Variables from  device memory
    {
        #region Loading PlayerInfo Array

        if(PlayerPrefs.HasKey("PlayerNames"))
        {
            GameManager.Instance.playersArray = PlayerPrefsExtension.GetStringArray("PlayerNames");
            GameManager.Instance.playersList = new List<string>();
            GameManager.Instance.playersList.AddRange(GameManager.Instance.playersArray);
        }
        else
        {

            GameManager.Instance.playersArray = new string[0];
            PlayerPrefsExtension.SetStringArray("PlayerNames", GameManager.Instance.playersArray);
        }
        #endregion
    }

    #endregion
}
