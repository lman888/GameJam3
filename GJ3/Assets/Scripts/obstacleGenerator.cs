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
    private List< List<Vector3> > spownerSetPositions;

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
        Vector3 groundBound;
        Vector3 midSpawnerPos;
        Vector3 leftSpawnerPos;
        Vector3 rightSpawnerPos;
        
        groundBound = ground.GetComponent<Renderer>().bounds.size;
        
        midSpawnerPos = new Vector3(groundBound.x, groundBound.y, groundBound.z + floorOffset);
        leftSpawnerPos = midSpawnerPos + new Vector3(midSpawnerPos.x - sideOffsets, midSpawnerPos.y, midSpawnerPos.z);
        
        RaycastHit leftHit;
        
        Physics.Raycast(leftSpawnerPos, Vector3.forward, out leftHit, Mathf.Infinity);
    }
}
