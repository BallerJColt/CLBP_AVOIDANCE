using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToPlace : MonoBehaviour
{

    public GameObject spawner;
    public ItemSpawner spawnerScript;
    public GameObject sceneObjects;
    public float HMDRot, percievedRot;
    WorldScaling ws;
    public bool isHeld;

    // Use this for initialization
    void Start()
    {
        spawner = GameObject.Find("Item Spawner");
        spawnerScript = spawner.GetComponent<ItemSpawner>();
        sceneObjects = GameObject.Find("Scene Objects");
        transform.parent = sceneObjects.transform;
        ws = sceneObjects.GetComponent<WorldScaling>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != sceneObjects.transform)
        {
            isHeld = true;
            HMDRot = ws.yRotation;
            percievedRot = ws.percievedAngle;
        }
        else
        {
            isHeld = false;
        }
    }

    void ScoreUp(float score)
    {
        spawnerScript.playerScore += score;
    }

    void Die(float score)
    {
        ScoreUp(score);
        if (spawnerScript.AllItemsOnField())
        {
            spawnerScript.IncreaseSpawnTime(5);
        }
        spawnerScript.OneLessItem();
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TargetPlatform")
        {
            Die(percievedRot);
        }
    }
}
