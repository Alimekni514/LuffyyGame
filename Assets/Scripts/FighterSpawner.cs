using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSpawner : MonoBehaviour
{
    public GameObject gamem;
    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to Luffy's attack event
        Luffy.OnFighterSpawned += InitializeBoss;
    }
    void InitializeBoss()
    {
        gamem.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        Luffy.OnBossSpawned -= InitializeBoss;
    }
}
