using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
	[Header("Tiro 1")]
	public BossFirePattern golpe1;
	public float golpe1Duration;
	public float golpe1Delay;
	
	[Header("Tiro 2")]
	public BossFirePattern2 golpe2;
	public float golpe2Duration;
	public float golpe2Delay;
	
	[Header("Body Slam")]
	public BossBodySlam golpe3;
	public float golpe3Duration;
	public float golpe3Delay;
	public bool isBodySlamming;
	
	
	[Header("Ignora, André")]
	public Transform player;
	public Rigidbody playerRigidbody;
	
	public Player playerScript;
	public Vector3 knockbackDirection;
	public float knockbackForce;
	
	public int state;
	public int currentState;
	// 0 = tiro pra todo lado
	// 1 = tiro em 180º
	// 2 = tiro mirado no jogador
	
	
    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player").GetComponent<Transform>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();
		playerScript = GameObject.Find("Player").GetComponent<Player>();
		golpe1 = GameObject.Find("BossManager").GetComponent<BossFirePattern>();
		golpe2 = GameObject.Find("BossManager").GetComponent<BossFirePattern2>();
		golpe3 = GameObject.Find("BossManager").GetComponent<BossBodySlam>();
		currentState = 1;
        //golpe1.InvokeRepeating("Fire", 2f, 0.5f);
		//golpe2.InvokeRepeating("Fire", 2f, 1f);
		
		StartCoroutine("StartBoss");
    }
	
	public void ChangeState(int state){
		switch (state){
			case 1:
				transform.LookAt(player.position, Vector3.up);
				golpe1.InvokeRepeating("Fire", 2f, golpe1Delay);
				StartCoroutine("CancelInvoke1");
				break;
			case 2:
				transform.LookAt(player.position, Vector3.up);
				golpe2.InvokeRepeating("Fire", 1f, golpe2Delay);
				StartCoroutine("CancelInvoke2");
				break;
			case 3:
				isBodySlamming = true;
				transform.LookAt(player.position, Vector3.up);
				golpe3.ResetPlayerPosition();
				golpe3.InvokeRepeating("MoveToSlam", 2f, golpe3Delay);
				StartCoroutine("CancelInvoke3");
				break;
		}
		currentState = state;
	}
	
	private IEnumerator StartBoss()
	{
		yield return new WaitForSeconds(3f);
		ChangeState(1);
	}
	
	private IEnumerator CancelInvoke1(){
		yield return new WaitForSeconds(golpe1Duration);
		golpe1.CancelInvoke("Fire");
		ChangeState(2);
	}
	
	private IEnumerator CancelInvoke2(){
		yield return new WaitForSeconds(golpe2Duration);
		golpe2.CancelInvoke("Fire");
		ChangeState(3);
	}
	private IEnumerator CancelInvoke3(){
		yield return new WaitForSeconds(golpe3Duration);
		if(!isBodySlamming){
			golpe3.CancelInvoke("MoveToSlam");
			ChangeState(1);
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Player"){
			playerScript.recentlyDamaged = true;
			Debug.Log("Favor não encostar no boss");
			knockbackDirection = transform.position - player.position;
			playerRigidbody.AddForce(-knockbackDirection * knockbackForce, ForceMode.VelocityChange);
			StartCoroutine("ImmuneTime");
		}
	}
	
	public IEnumerator ImmuneTime()
    {
        yield return new WaitForSeconds(0.2f);
        playerScript.recentlyDamaged = false;
		playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.VelocityChange);
    }
}
