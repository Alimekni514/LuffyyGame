using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
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
            yield return new WaitForSeconds(Random.Range(5, 10));

            // Check if Luffy has reached the right position
            if (!LuffyReachedRightPos())
            {
                // If not, continue spawning snakes
                SpawnSnake();
            }
            else
            {
                // If Luffy has reached the right position, stop spawning snakes
                StopSpawnSnakes();
            }
        }


    }
    void SpawnSnake()
    {
        if (spawnedMonster == null)
        {
            spawnedMonster = Instantiate(monsterReference);
            // Right Side
            spawnedMonster.transform.position = rightPos.position;
            spawnedMonster.GetComponent<EnemySnake>().speed = -Random.Range(1, 4);
            
            
        }
    }
    void StopSpawnSnakes()
    {
        // Implement logic to stop spawning snakes
        StopCoroutine(SpawnSnakes());
    }
    bool LuffyReachedRightPos()
    {
        // Assuming Luffy is a GameObject, you might want to replace it with the actual reference to your Luffy object.
        GameObject luffy = GameObject.FindGameObjectWithTag("Player");

        if (luffy != null)
        {
                return luffy.transform.position.x >= rightPos.position.x; 
        }

        // Return false if Luffy is not found or not close enough to the right position.
        return false;
    }
}

