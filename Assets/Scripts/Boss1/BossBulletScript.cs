using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed;

    [SerializeField]
    Vector3 dir2;

    void Start()
    {
        //velocidade do tiro
        moveSpeed = 12f;
        //destruir o tiro após 8 segundos
		Invoke("Destroy", 8f);
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 dir)
    {

        moveDirection = dir;
        dir2 = moveDirection;
    }

    private void Destroy()
    {
        //Desativa as balas da pool do boss em vez de utilizar um destroy normal para não bugar na hora de chamar as variaveis
        gameObject.SetActive(false);
    }
	
	
    private void OnDisable()
    {
        CancelInvoke();
    }
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Wall"){
			Invoke("Destroy", 0f);
		} else if(col.gameObject.tag == "Player"){
            Debug.Log("Boss lhe acertou com tiro");
		}
	}	
}
