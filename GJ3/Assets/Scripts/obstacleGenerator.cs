using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private GameObject spawner;
    private float RayDistance = 1000.0f;
    private List<List<GameObject>> spawnerSetList = new List<List<GameObject>>();

    [Space(10)]

    [Tooltip("Distance between floor and spawners.")]
    [Range(0.0f, 20.0f)]
    public float floorOffset = 0.0f;

    [Space(5)]

    [Tooltip("Distance between spawner columns.")]
    [Range(1.0f, 10.0f)]
    public float sideOffsets = 3.0f;

    [Space(5)]

    [Tooltip("Distance between spawner rows. (by angle)")]
    [Range(1.0f, 30.0f)]
    public float rowOffset = 5.0f;

    [Space(5)]

    [Tooltip("Chance of spawning an obstacle. (min: 0 / max: 40)")]
    [Range(0.0f, 40.0f)]
    public float spawnChance = 20.0f;

    // Use this for initialization
    void Start()
    {
        spawner.transform.GetComponent<spawner>().SpawnChance = spawnChance;
        InitializeSpawnerSet();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnersSwitch();
    }

    private void InitializeSpawnerSet()
    {
        for (float angle = 0; angle <= 360; angle += rowOffset)
        {
            List<GameObject> spawnerSet = new List<GameObject>();
            Vector3 rayDir = Vector3.right;
            Quaternion rot = Quaternion.AngleAxis(angle, ground.transform.Find("Ground").transform.forward);
            rayDir = rot * rayDir;

            Vector3 rayShootPosition = (ground.transform.Find("Ground").transform.position + (rayDir * RayDistance));

            Ray r = new Ray(rayShootPosition, -rayDir);
            RaycastHit hitInfo;
            bool hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                spawnerSet.Add(Instantiate(spawner));
                spawnerSet[0].transform.position = hitInfo.point;// + new Vector3 (hitInfo.normal.x + floorOffset, hitInfo.normal.y, hitInfo.normal.z);
                spawnerSet[0].transform.up = hitInfo.normal;
                spawnerSet[0].transform.SetParent(ground.transform.Find("ObjectHolder").transform);
            }

            rayShootPosition += ground.transform.Find("Ground").transform.forward * sideOffsets;

            r = new Ray(rayShootPosition, -rayDir);
            hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                spawnerSet.Add(Instantiate(spawner));
                spawnerSet[1].transform.position = hitInfo.point;// + new Vector3(hitInfo.normal.x + floorOffset, hitInfo.normal.y, hitInfo.normal.z);
                spawnerSet[1].transform.up = hitInfo.normal;
                spawnerSet[1].transform.SetParent(ground.transform.Find("ObjectHolder").transform);
            }

            rayShootPosition -= 2 * ground.transform.Find("Ground").transform.forward * sideOffsets;
            r = new Ray(rayShootPosition, -rayDir);
            hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                spawnerSet.Add(Instantiate(spawner));
                spawnerSet[2].transform.position = hitInfo.point;// + new Vector3(hitInfo.normal.x + floorOffset, hitInfo.normal.y, hitInfo.normal.z);
                spawnerSet[2].transform.up = hitInfo.normal;
                spawnerSet[2].transform.SetParent(ground.transform.Find("ObjectHolder").transform);
            }

            spawnerSetList.Add(spawnerSet);
        }
    }

    private void SpawnersSwitch()
    {
        foreach (List<GameObject> set in spawnerSetList)
        {
            for (int i = 0; i < set.Count; ++i)
            {
                if (set[i].transform.position.y < 0)
                {
                    //Deactivate Spawner
                    set[i].GetComponent<spawner>().DeactivateSpawner();
                }

                if (set[i].transform.position.y >= 0)
                {
                    //Activate Spawner
                    set[i].GetComponent<spawner>().ActivateSpawner();
                }

                set[i].GetComponent<spawner>().SpawnChance = spawnChance;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (float angle = 0; angle <= 360; angle += rowOffset)
        {
            Vector3 rayDir = Vector3.right;
            Quaternion rot = Quaternion.AngleAxis(angle, ground.transform.Find("Ground").transform.forward);
            rayDir = rot * rayDir;

            Vector3 rayShootPosition = (ground.transform.Find("Ground").transform.position + (rayDir * RayDistance));

            Ray r = new Ray(rayShootPosition, -rayDir);
            RaycastHit hitInfo;
            bool hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                //Gizmos.DrawCube(hitInfo.point, new Vector3(2, 2, 2));
                Gizmos.DrawRay(hitInfo.point, hitInfo.normal * 3);
            }

            rayShootPosition += ground.transform.Find("Ground").transform.forward * sideOffsets;

            r = new Ray(rayShootPosition, -rayDir);
            hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                //Gizmos.DrawCube(hitInfo.point, new Vector3(2, 2, 2));
                Gizmos.DrawRay(hitInfo.point, hitInfo.normal * 3);
            }

            rayShootPosition -= 2 * ground.transform.Find("Ground").transform.forward * sideOffsets;
            r = new Ray(rayShootPosition, -rayDir);
            hitSomething = Physics.Raycast(r, out hitInfo, RayDistance);
            if (hitSomething)
            {
                //Gizmos.DrawCube(hitInfo.point, new Vector3(2, 2, 2));
                Gizmos.DrawRay(hitInfo.point, hitInfo.normal * 3);
            }
        }
    }
}
