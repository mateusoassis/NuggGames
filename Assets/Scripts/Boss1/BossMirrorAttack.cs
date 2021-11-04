using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMirrorAttack : MonoBehaviour
{
    public float rotationSpeed;

    public float reflectionSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
    }
}
