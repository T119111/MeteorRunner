using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] GameObject alienPrefab; //エイリアンのプレハブ
    float spawntime = 0.0f; //エイリアンが出現する時間間隔
    public int AlienSpawnIndex = 0;

    // Update is called once per frame
    void Update()
    {
        spawntime += Time.deltaTime;
        Quaternion rot = Quaternion.AngleAxis(180f, new Vector3(0.0f,1.0f,0.0f));

        if (spawntime >= 3.0f)
        {
            //Choose a random point to spawn the alien
            AlienSpawnIndex = Random.Range(0, 3);
            Transform spawnPoint = transform.GetChild(AlienSpawnIndex).transform;
            //Spawn the alien at the position
            Instantiate(alienPrefab, spawnPoint.position, rot, transform);
            spawntime = 0.0f;
        }
    }
}
