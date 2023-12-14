using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monsterReference;
    [SerializeField]
    private Transform rightPos;
    private GameObject spawnedMonster;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSnakes());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnSnakes()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0,1));

            SpawnEye();
        }


    }
    void SpawnEye()
    {
        if (spawnedMonster == null)
        {
            spawnedMonster = Instantiate(monsterReference);
            // Right Side
            spawnedMonster.transform.position = rightPos.position;
            spawnedMonster.transform.localScale = new Vector3(-2f, 2f, 0f);
            spawnedMonster.GetComponent<Eye>().speed = -Random.Range(1, 4);
            print("speed" + spawnedMonster.GetComponent<Eye>().speed);


        }
    }
}
