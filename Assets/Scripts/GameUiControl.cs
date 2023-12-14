using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameUiControl : MonoBehaviour
{
    // Start is called before the first frame update
   public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
        //Get the active Scene to reload it 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void HomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
