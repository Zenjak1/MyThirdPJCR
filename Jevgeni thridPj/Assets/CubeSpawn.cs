using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    public GameObject Spawnee;// Object in unity 
    public int AmountSpawnee;// amount of spawning things
    public float Timespawnee;// time to spawn
    private GameObject[] SpawneeList;
    private bool PositionSet;

    void Start()
    {
        //StartCoroutine(ChangePosition());
        StartCoroutine(SpawnLoop());
        SpawneeList = new GameObject[AmountSpawnee];


    }
    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(0.4f);
        if (!PositionSet)
        {
            if (VuforiaBehaviour.Instance.enabled) {
                SetPosition();
               
            }
        }
    }
    private bool SetPosition()
    {
        Transform cam = Camera.main.transform;
        transform.position = cam.forward * 5;
        return true;
    }
    private IEnumerator SpawnLoop()
    {
        StartCoroutine(ChangePosition());
        yield return new WaitForSeconds(2f);
        int i = 0;
        while (i <= (AmountSpawnee - 1))
        {
            SpawneeList[i] = SpawnElements();
            i++;
            yield return new WaitForSeconds(Random.Range(Timespawnee,Timespawnee*3));
        }
        
    }
    private GameObject SpawnElements()
    {
     
        GameObject spawnobject = Instantiate(Spawnee,(Random.insideUnitSphere*5), transform.rotation) as GameObject;
        float scale = Random.Range(0.5f, 3f);
        spawnobject.transform.localScale = new Vector3(scale, scale, scale);
        return spawnobject;
    }
}
