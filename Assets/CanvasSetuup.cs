using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetuup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetUICanvas(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
