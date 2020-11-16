using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubemovement : MonoBehaviour
{
    public float scalemax;
    public float scalemin;
    public float maxOrbitSpeed;
    private float Orbitspeed;
    private Transform OrbitAnchor;
    private Vector3 OrbitDirection;
    private Vector3 maxscale;
    public float GrowingSpeed;
    private bool IsScaled=false;
    public int health;
    private bool isAlive = true;
    public GameObject EffectExpl;
    void CubeSettings()
    {
        OrbitAnchor = Camera.main.transform;
        float x = Random.Range(-1f,1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        OrbitDirection = new Vector3(x, y, z);

        Orbitspeed = Random.Range(0.5f, maxOrbitSpeed);
        float scale = Random.Range(scalemin, scalemax);
        maxscale = new Vector3(scale, scale, scale);
        transform.localScale = Vector3.zero;

        }
    private void Start()
    {
        CubeSettings();
    }
    private void Update()
    {
        RotateCube();
       if (!IsScaled)
        {
            ScaleObject();
        }
        
    }
    void RotateCube()
    {
        transform.RotateAround(OrbitAnchor.position, OrbitDirection, Orbitspeed * Time.deltaTime);
        transform.Rotate(OrbitDirection * 30 * Time.deltaTime);
    }
    

    void ScaleObject()
    {
        if(transform.localScale!= maxscale)// if not max we need to (next in if)
        {
            //meaning of Lerp - Scale (with transfromscale to what(to maxscale) in time)
            transform.localScale = Vector3.Lerp(transform.localScale, maxscale, Time.deltaTime * GrowingSpeed);
            IsScaled = true;
        }
    }
    public bool Hit(int hitDamage)
    {
        health -= hitDamage;
        if(health>=0 && isAlive)
        {
            StartCoroutine("DestroyCube");
            return true;
        }
        return false;
    }
    private IEnumerator DestroyCube()
    {
        isAlive = false;
        Instantiate(EffectExpl, transform.position, Quaternion.identity);
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
       

    }
    
}
