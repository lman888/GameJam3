using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    private float spawnChance;
    private bool isActivated = false;
    public List<GameObject> obstacleList;

    public float SpawnChance
    {
        get { return spawnChance; }
        set { spawnChance = value; }
    }
    
    public void ActivateSpawner()
    {
        if (isActivated == false)
        {
            float rand = Random.Range(0, 100);

            if (rand <= spawnChance)
            {
                int objRand = Random.Range(0, obstacleList.Count);
                GameObject childObj = Instantiate(obstacleList[objRand]);
                childObj.transform.position = transform.position;
                childObj.transform.up = transform.up;
                childObj.transform.parent = transform;
            }

            isActivated = true;
        }
    }

    public void DeactivateSpawner()
    {
        if (isActivated == true)
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }

            isActivated = false;
        }
    }
}
