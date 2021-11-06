using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSwawner;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject obstacleEasy;
    [SerializeField] GameObject items;
    [SerializeField] GameObject coinPrefab;


    // Start is called before the first frame update
    private void Start()
    {
        groundSwawner = GameObject.FindObjectOfType<GroundSpawner>();
        
    }

    private void OnTriggerExit(Collider other)
    {
        groundSwawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    /*public void SpawnObstacle()
    {
        //Choose a random point to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(3, 0);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        //Spawn the obstacle at the position
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);

    }
    public void SpawnObstacle2()
    {
        //Choose a random point to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        //Spawn the obstacle at the position
        Instantiate(obstaclePrefab2, spawnPoint.position, Quaternion.identity, transform);
    }
    */

    //隕石の生成する数、位置
    public void SpawnObstacle()
    {
        int meteorToSpawn = 3;
        for (int i = 0; i < meteorToSpawn; i++)
        {
            GameObject temp = Instantiate(obstaclePrefab, transform);
            temp.transform.position = GetRandomPointInCollider2(GetComponent<Collider>());
        }
    }

    //隕石を減らして簡単にする
    public void SpawnEasy()
    {
        int easyToSpawn = 1;
        for (int i = 0; i < easyToSpawn; i++)
        {
            GameObject temp = Instantiate(obstacleEasy, transform);
            temp.transform.position = GetRandomPointInCollider2(GetComponent<Collider>());
        }
    }

    //シールドの位置
    public void SpawnItems()
    {
        Instantiate(items, new Vector3(0,1,35),Quaternion.Euler(-8,-180,0),transform);
    }

    //コインの数、位置
    public void SpawnCoins()
    {
        int coinsToSpawn = 5;
        for(int i=0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    //コインの位置をランダムで指定
    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if(point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }

    //隕石の位置をランダムで指定
    Vector3 GetRandomPointInCollider2(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(10, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 60;
        return point;
    }
}
