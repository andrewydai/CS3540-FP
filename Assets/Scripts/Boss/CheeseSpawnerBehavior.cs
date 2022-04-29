using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSpawnerBehavior : MonoBehaviour
{
    public GameObject cheeseEggPrefab;
    public int length;
    public int lengthSpread;
    public int width;
    List<int> spawnRows = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        int spawnCount = length / lengthSpread;
        for(int i = 0; i < spawnCount; i++)
        {
            spawnRows.Add(i);
        }
    }

    public void SpawnCheese()
    {
        ShuffleRows();
        for(int i = 0; i < spawnRows.Count; i++)
        {
            int zPos = Random.Range(0, width);
            int xPos = lengthSpread * i;
            Instantiate(cheeseEggPrefab, new Vector3(xPos, 0, zPos) + transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShuffleRows()
    {
        for (int i = 0; i < spawnRows.Count; i++)
        {
            int temp = spawnRows[i];
            int randomIndex = Random.Range(i, spawnRows.Count);
            spawnRows[i] = spawnRows[randomIndex];
            spawnRows[randomIndex] = temp;
        }
    }
}
