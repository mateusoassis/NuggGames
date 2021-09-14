using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDamage : MonoBehaviour
{


    private int lifeQnt;
    public int lifeQntANDRETEAMO;

    public GameManagerScript pauseMenuInvk;
	
	public GameObject losePanelObject;
	
	public Player playerScript;
	public BossState bossStateScript;
	
	[Header("HP do player, do boss e temporizador")]
	[SerializeField]TextMeshProUGUI PlayerHealth;
	[SerializeField]TextMeshProUGUI BossHealth;
	[SerializeField]TextMeshProUGUI TimeStamp;
	private float min;
	private float sec;
	

    // Start is called before the first frame update
    void Start()
    {
		playerScript = GameObject.Find("Player").GetComponent<Player>();
		bossStateScript = GameObject.Find("Boss").GetComponent<BossState>();
        lifeQnt = lifeQntANDRETEAMO;
        pauseMenuInvk = GameObject.Find("GameManagerObject").GetComponent<GameManagerScript>();
		PlayerHealth.SetText("HP: " + lifeQnt.ToString());
		// printar no TMPro text o hp do player
    }

    // pausar se zerar HP
	// temporizador que conta no topo-direita da tela
	// começa em 0min0sec, a cada 60sec, soma 1min e subtrai 60sec
	// depois printa no TMPro text
    void Update()
    {
		sec += Time.deltaTime;
		
		if(sec >= 60f){
			min += 1f;
			sec = 0;
		}
		
		TimeStamp.SetText(min.ToString() + "m " + Mathf.Floor(sec).ToString() + "s");

        if (lifeQnt <= 0)
        {
            losePanelObject.SetActive(true);
			Time.timeScale = 0f;
        }		
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "BossBullet")
        {
            lifeQnt--;
            col.gameObject.SetActive(false);
			PlayerHealth.SetText("HP: " + lifeQnt.ToString());
			bossStateScript.StartCoroutine("ImmuneTime");
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Boss")
        {
			PlayerHealth.SetText("HP: " + lifeQnt.ToString());
            lifeQnt--;
        }
    }
}
