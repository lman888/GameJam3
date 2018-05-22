using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleGenerator : MonoBehaviour
{


    [SerializeField]
    public GameObject ground;
    [SerializeField]
    public GameObject spawner;

    private List<List<GameObject>> spawnerSetList = new List<List<GameObject>>();


    public float floorOffset = 0.0f;
    public float sideOffsets = 3.0f;
    public float rowOffset = 10.0f;

    //private float worldRotation = 0.0f;

    public float RayDistance = 1000.0f;

    // Use this for initialization
    void Start()
    {
        //floorOffset = 0.0f;
        //sideOffsets = 3.0f;
        //rowOffset = 10.0f;

        InitializeSpawnerSet();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeSpawnerSet()
    {
        for (float angle = 0; angle <= 360; angle += rowOffset)
        {
            List<GameObject> spawnerSet = new List<GameObject>();
            Vector3 rayDir = Vector3.right;
            Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);
            rayDir = rot * rayDir;

            Vector3 rayShootPosition = (transform.position + (rayDir * RayDistance));

            Ray r = new Ray(rayShootPosition, -rayDir);
            RaycastHit hitInfo;
            bool hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                spawnerSet.Add(Instantiate(spawner));
                spawnerSet[0].transform.position = hitInfo.point;
                spawnerSet[0].transform.up = hitInfo.normal;
                spawnerSet[0].transform.SetParent(GameObject.Find("ObjectHolder").transform);
            }

            rayShootPosition += transform.forward * sideOffsets;

            r = new Ray(rayShootPosition, -rayDir);
            hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                spawnerSet.Add(Instantiate(spawner));
                spawnerSet[1].transform.position = hitInfo.point;
                spawnerSet[1].transform.up = hitInfo.normal;
                spawnerSet[1].transform.SetParent(GameObject.Find("ObjectHolder").transform);
            }

            rayShootPosition -= 2 * transform.forward * sideOffsets;
            r = new Ray(rayShootPosition, -rayDir);
            hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                spawnerSet.Add(Instantiate(spawner));
                spawnerSet[2].transform.position = hitInfo.point;
                spawnerSet[2].transform.up = hitInfo.normal;
                spawnerSet[2].transform.SetParent(GameObject.Find("ObjectHolder").transform);
            }

            spawnerSetList.Add(spawnerSet);
        }
    }

    void DeleteSpawners()
    {
        foreach (List<GameObject> set in spawnerSetList)
        {
            for (int i = 0; i < set.Count; ++i)
            {
                if (set[i].transform.position.y < 0)
                {
                    //spawnerSetList.Remove
                    Destroy(set[i]);
                }
            }
        }
    }



    //private void OnDrawGizmos()
    //{
    //    for (float angle = 0; angle <= 360; angle += rowOffset)
    //    {
    //        Vector3 rayDir = Vector3.right;
    //        Quaternion rot = Quaternion.AngleAxis(angle, transform.forward);
    //        rayDir = rot * rayDir;

    //        Vector3 rayShootPosition = (transform.position + (rayDir * RayDistance));

    //        Ray r = new Ray(rayShootPosition, -rayDir);
    //        RaycastHit hitInfo;
    //        bool hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
    //        if (hitSomething)
    //        {
    //            Gizmos.DrawCube(hitInfo.point, new Vector3(2, 2, 2));
    //            Gizmos.DrawRay(hitInfo.point, hitInfo.normal * 3);
    //        }

    //        rayShootPosition += transform.forward * sideOffsets;

    //        r = new Ray(rayShootPosition, -rayDir);
    //        hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
    //        if (hitSomething)
    //        {
    //            Gizmos.DrawCube(hitInfo.point, new Vector3(2, 2, 2));
    //            Gizmos.DrawRay(hitInfo.point, hitInfo.normal * 3);
    //        }

    //        rayShootPosition -= 2 * transform.forward * sideOffsets;
    //        r = new Ray(rayShootPosition, -rayDir);
    //        hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
    //        if (hitSomething)
    //        {
    //            Gizmos.DrawCube(hitInfo.point, new Vector3(2, 2, 2));
    //            Gizmos.DrawRay(hitInfo.point, hitInfo.normal * 3);
    //        }
    //    }
    //}
}
