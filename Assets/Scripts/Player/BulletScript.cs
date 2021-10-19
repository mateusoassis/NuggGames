using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine("TimeToDestroy");
    }

    public IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(this.gameObject);
    }
	
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Wall"){
			Destroy(this.gameObject);
		}
	}
}
