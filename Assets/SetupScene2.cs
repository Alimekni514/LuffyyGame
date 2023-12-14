using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SetupScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {// Retrieve the UI Canvas from the GameManager
        GameObject canvas = GameManager.Instance.GetUICanvas();
        GameObject player = GameManager.Instance.GetPlayerCharacter();
        GameObject luffysounds= GameManager.Instance.GetLuffySounds();

        if (canvas == null)
        {
            // Instantiate the UI Canvas in the new scene
            Instantiate(canvas);

        }
        if(player == null)
        {
            Instantiate(player);
          
        }
        if(luffysounds == null)
        {
            Instantiate(luffysounds);
        }
        if(player!=null)
        {
            // Set the position of the player
            player.transform.position = new Vector3(-8.5f, -0.9764096f, 0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
