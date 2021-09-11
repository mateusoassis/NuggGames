using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //velocidade de movimento do player e do tiro, respectivamente.

    public float moveSpeed = 10f;
    public float bulletForce = 20f;

    public float dashForce = 10f;
    private float dashDuration;
    public float startDashTime;

    public bool isNotMoving;
    public int direction;

    public Rigidbody rb;

    //Transform para localiza��o do ponto de onde o tiro vai sair/ Prefab da bala.

    public Transform firePoint;
    public GameObject bulletPrefab;
	
	public bool recentlyDamaged;
	
	public GameManagerScript gameManager;

    //Vetor de posi��o para utilizar no raycast.

    Vector3 position;
	
	//layer do ch�o para o raycast ser mirado apenas no ch�o 
	[SerializeField] public LayerMask layerMask;

    void Start()
    {
		gameManager = GameObject.Find("GameManagerObject").GetComponent<GameManagerScript>();
        rb = GetComponent<Rigidbody>();
		recentlyDamaged = false;
        dashDuration = startDashTime;
    }

    void Update()
    {
        //Definindo a mira para ficar funcionando 100% do tempo no update.

        Aim();

        //Definindo o bot�o esquerdo do mouse para atirar.

        if (Input.GetButtonDown("Fire1") && !gameManager.pausedGame)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {

        /*if (rb.velocity.x == 0 && rb.velocity.y == 0 && rb.velocity.z == 0)
        {
            isNotMoving = true;
            direction = 0;
        }*/

        //Inputs de WASD para movimenta��o em 8 dire��es utilizando a multiplica��o de velocidade por tempo.

        if (!recentlyDamaged)
        {

            if (Input.GetKey(KeyCode.W))
            {
                rb.MovePosition(rb.position + Vector3.forward * moveSpeed * Time.fixedDeltaTime);
                direction = 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.MovePosition(rb.position + Vector3.back * moveSpeed * Time.fixedDeltaTime);
                direction = 2;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.MovePosition(rb.position + Vector3.left * moveSpeed * Time.fixedDeltaTime);
                direction = 3;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.MovePosition(rb.position + Vector3.right * moveSpeed * Time.fixedDeltaTime);
                direction = 4;
            }

            if (Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.A))
            {
                direction = 5;
            }

            if (Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.D))
            {
                direction = 6;
            }

            if (Input.GetKey(KeyCode.S)&& Input.GetKey(KeyCode.A))
            {
                direction = 7;
            }

            if (Input.GetKey(KeyCode.S)&& Input.GetKey(KeyCode.D))
            {
                direction = 8;
            }


        }
            if (dashDuration <= 0)
            {
                direction = 0;
                dashDuration = startDashTime;
                rb.velocity = Vector3.zero;
            }
            else
            {
                dashDuration -= Time.deltaTime;

                if (Input.GetKey(KeyCode.Space) && direction == 1)
                {
                    Debug.Log("deu dash pra cima");
                    rb.velocity = Vector3.forward * dashForce;
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 2)
                {
                    rb.velocity = Vector3.back * dashForce;
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 3)
                {
                    rb.velocity = Vector3.left * dashForce;
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 4)
                {
                    rb.velocity = Vector3.right * dashForce;
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 5)
                {
                    rb.velocity = Vector3.forward + Vector3.left * dashForce;
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 6)
                {
                    rb.velocity = Vector3.forward + Vector3.right * dashForce;
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 7)
                {
                    rb.velocity = Vector3.back + Vector3.left * dashForce;
                }   
                else if (Input.GetKey(KeyCode.Space) && direction == 8)
                {
                    rb.velocity = Vector3.back + Vector3.right * dashForce;
                }
            }
        }

    void Aim()
    {
        //Utiliza��o de Raycast para identificar a posi��o do mouse na tela atrav�s da c�mera.

        RaycastHit hit;

        //Pegando o input de posi��o do mouse com raycast.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit , float.MaxValue, layerMask))
        {
            position = new Vector3(hit.point.x, 0, hit.point.z);
        }

        //Criando quaternion que define a rota��o do player em rela��o ao mouse.

        Quaternion newRotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);

        //Zerando o quaternion de x e z para evitar que o player rode de formas estranhas...

        newRotation.x = 0f; 
        newRotation.z = 0f;

        //controle da velocidade da rota��o.

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 20);
    }

    void Shoot()
    {
        //Instanciamento de prefab do tiro de personagem.

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        //Adi��o de for�a no tiro para impulsionar o prefab.

        bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Wall"){
			rb.velocity = Vector3.zero;
		}
	}
}
