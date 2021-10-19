using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{


    private int lifeQnt;
    public int lifeQntANDRETEAMO;

    public GameManagerScript pauseMenuInvk;

    // Start is called before the first frame update
    void Start()
    {
        lifeQnt = lifeQntANDRETEAMO;
        pauseMenuInvk = GameObject.Find("GameManagerObject").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeQnt <= 0)
        {
            Time.timeScale = 0;
            pauseMenuInvk.pausedGame = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "BossBullet")
        {
            lifeQnt--;
            col.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Boss")
        {
            lifeQnt--;
        }
    }
}
