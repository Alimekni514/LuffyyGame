using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private GameObject uiCanvas;
    private GameObject playerCharacter;
    private GameObject LuffySounds;
    private  String ActualScene;
    public GameObject GameOverobj;
    public GameObject GolemHealthBar;
    public GameObject luffyy;


    // Access the GameManager instance
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set the UI Canvas
    public void SetUICanvas(GameObject canvas)
    {
      
            uiCanvas = canvas;
            DontDestroyOnLoad(canvas);
       
    }

    // Access the UI Canvas
    public GameObject GetUICanvas()
    {
        return uiCanvas;
    }
    // Set the player character
    public void SetPlayerCharacter(GameObject player)
    {
       
     
            playerCharacter = player;
            DontDestroyOnLoad(playerCharacter);
       
   
    }

    // Access the player character
    public GameObject GetPlayerCharacter()
    {
        return playerCharacter;
    }
    public void SetLuffySounds(GameObject sound)
    {

        LuffySounds = sound;
        DontDestroyOnLoad(sound);

    }

    // Access the UI Canvas
    public GameObject GetLuffySounds()
    {
        return LuffySounds;
    }

    public void GameOver()
    {
       // Get the name of the active scene
        string currentSceneName = SceneManager.GetActiveScene().name;
        GameOverobj.SetActive(false);
        if (currentSceneName == "ForestScene")
        {
           
            GolemHealthBar.SetActive(false);
        }
       
        // Load the active scene by name
        if(currentSceneName=="SampleScene")
        {
            
            GameObject player = GameManager.Instance.GetPlayerCharacter();
            if (player == null)
            {
                print("hello");
                player = Instantiate(luffyy);
                // Set the position of the player
                player.transform.position = new Vector3(-8.5f, -0.9764096f, 0f);
            }

        }
        SceneManager.LoadScene(currentSceneName);
        

    }

 


}
