using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject obtstacleTypeA;
    [SerializeField]
    public GameObject obtstacleTypeB;
    [SerializeField]
    public GameObject ground;
    [SerializeField]
    public GameObject spawner;

    private List<GameObject> spawnerSet;

    private List<List<GameObject>> spawnerSetList;

    float floorOffset;
    float sideOffsets;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void InitializeSpawnerSet()
    {
        Vector3 groundPos =  ground.transform.position;

        RaycastHit midHit;
        RaycastHit lftHit;
        RaycastHit rgtHit;

        Physics.Raycast(groundPos, Vector3.back, out midHit, Mathf.Infinity);
        Vector3 midSpawnerPos = midHit.point - new Vector3(floorOffset,0 , 0);

        Vector3 lftOffsetPos = midSpawnerPos - new Vector3(0, 0, sideOffsets);
        Physics.Raycast(lftOffsetPos, Vector3.forward, out lftHit, Mathf.Infinity);

        Vector3 rgtOffsetPos = midSpawnerPos - new Vector3(0, 0, sideOffsets);
        Physics.Raycast(rgtOffsetPos, Vector3.forward, out rgtHit, Mathf.Infinity);

        spawnerSet.Add(Instantiate(spawner));
        spawnerSet[0].transform.position = midSpawnerPos;
        spawnerSet[0].transform.up = midHit.normal;

        spawnerSet.Add(Instantiate(spawner));
        spawnerSet[1].transform.position = lftHit.transform.position + (lftHit.normal * floorOffset);
        spawnerSet[1].transform.up = lftHit.normal;

        spawnerSet.Add(Instantiate(spawner));
        spawnerSet[2].transform.position = lftHit.transform.position + (rgtHit.normal * floorOffset);
        spawnerSet[2].transform.up = rgtHit.normal;

        foreach (GameObject go in spawnerSet)
        {
            go.transform.SetParent(ground.transform.Find("ObjectHolder").transform);
        }
    }
}
