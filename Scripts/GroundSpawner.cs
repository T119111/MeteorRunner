using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundTile;
    Vector3 nextSpawnPoint;

    //タイルの位置に対してオブジェクトを生成
    public void SpawnTile(bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if (spawnItems)
        {
            temp.GetComponent<GroundTile>().SpawnObstacle();
            temp.GetComponent<GroundTile>().SpawnEasy();
            temp.GetComponent<GroundTile>().SpawnItems();
            temp.GetComponent<GroundTile>().SpawnCoins();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトを繰り返して生成
        for(int i=0; i<55; i++)
        {
            //最初の3つ目のタイルまでオブジェクトを生成しない
            if (i < 3)
            {
                SpawnTile(false);
            }
            else if(i > 45)
            {
                SpawnTile(false);
            }
            else
            { 
                SpawnTile(true);
            }
        }
    }
}
