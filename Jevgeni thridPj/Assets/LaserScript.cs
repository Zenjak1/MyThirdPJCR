using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserScript : MonoBehaviour
{
    public float FireRate;
    public float FireRange;
    public float HitForce;
    public int LaserDmg;
    private LineRenderer LaserLine;
    bool LaserLineEnabled;
    private WaitForSeconds LaserDuration;
    float NextFire;
    // Start is called before the first frame update
    void Start()
    {
        LaserLine = GetComponent<LineRenderer>();


    }

    void Fire()
    {
        Transform cam = Camera.main.transform;
        NextFire = Time.time + FireRate;
        Vector3 RayOrigin = cam.position;
        LaserLine.SetPosition(0, transform.up * -10);
        RaycastHit hit;
        if (Physics.Raycast(RayOrigin, cam.forward, out hit, FireRange))
        {
            LaserLine.SetPosition(1,hit.point);
            Cubemovement cubectrl = hit.collider.GetComponent<Cubemovement>();
            if(cubectrl != null)
            {
                if (hit.rigidbody != null) {

                    hit.rigidbody.AddForce(-hit.normal * HitForce);
                    cubectrl.Hit(LaserDmg);
                        }
            }
        }
        else
        {
            LaserLine.SetPosition(1, cam.forward * FireRange);


        }
        StartCoroutine("LaserFX");


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();  
        }


    }
    private IEnumerator LaserFX()
    {
        LaserLine.enabled = true;
        yield return LaserDuration;
        LaserLine.enabled = false;
    }


}
