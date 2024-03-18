using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpawn : MonoBehaviour
{
    private int lastSpawnIndex = 0;
    public GameObject dragonPrefab;
    public float spawnInterval = 2f;
    private float spawnTimer = 0f;
    public float speed = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0;
            Spawn();
            spawnInterval *= 0.98f;
        }
    }
    void Spawn()
    {
        var random = Random.Range(0, transform.childCount);
        if(random == lastSpawnIndex)
        {
            random = (random + 1) % transform.childCount;
        }
        lastSpawnIndex = random;
        var point = transform.GetChild(random);
        var dragon = Instantiate(dragonPrefab, point.position, point.rotation);
        dragon.gameObject.SetActive(true);
        dragon.transform.SetParent(transform.parent);
        dragon.GetComponent<DragonMoveController>().speed = speed;
        speed *= 1.02f;
    }

}
