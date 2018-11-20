using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject item;
    public Vector3 spawnPoint;
    public float playerScore;
    public float timeBetweenSpawns;
    public int maxItems;
    int itemCount;
    float nextSpawnTime;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime && itemCount < maxItems)
        {
            nextSpawnTime = Time.time + timeBetweenSpawns;
            itemCount++;
            Instantiate(item, spawnPoint, Quaternion.identity);
        }
    }

    public void IncreaseSpawnTime(float seconds)
    {
        nextSpawnTime = Time.time + seconds;
    }

    public void OneLessItem()
    {
        itemCount--;
    }

    public bool AllItemsOnField()
    {
        return (itemCount == maxItems);
    }
}
