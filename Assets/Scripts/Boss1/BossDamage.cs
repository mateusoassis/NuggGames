using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using

public class BossDamage : MonoBehaviour
{


    public float bossHP;
    public float bossHPCurrent;

    public static bool bossIsDead;

    // Start is called before the first frame update
    void Start()
    {
        bossHPCurrent = bossHP;
        bossIsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHPCurrent <= 0)
        {
            Destroy(this.gameObject);
            bossIsDead = true;
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "PlayerAttack")
        {
            bossHPCurrent--;
            Destroy(col.gameObject);
        }
    }
       
}
