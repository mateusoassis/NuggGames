using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
<<<<<<< HEAD
using UnityEngine.UI;
=======
using
>>>>>>> parent of a93efd6... Alguma coisa, não sei o que mexi
=======

=======
using UnityEngine.UI;
>>>>>>> 3d64e813cb90e2e36b2f68e4e125c6d897b9c05d
>>>>>>> 114beef4bfc779101b1c9c8a0520cc4176b26f3c

public class BossDamage : MonoBehaviour
{


    public float bossHP;
    public float bossHPCurrent;

    public static bool bossIsDead;
	
	public GameObject winPanelObject;
	
	[Header("Barra de HP do boss")]
	public Slider BossHPBar;
	public float bossFillBar;

    // Start is called before the first frame update
    void Start()
    {
        bossHPCurrent = bossHP;
        bossIsDead = false;
		BossHPBar.value = bossHPCurrent/bossHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHPCurrent <= 0)
        {
            Destroy(this.gameObject);
            bossIsDead = true;
			winPanelObject.SetActive(true);
			Time.timeScale = 0f;
        }
		BossHPBar.value = bossHPCurrent/bossHP;
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
