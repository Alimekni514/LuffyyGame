using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuffySounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetLuffySounds(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
