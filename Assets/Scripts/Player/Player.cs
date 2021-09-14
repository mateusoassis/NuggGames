using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //velocidade de movimento do player e do tiro, respectivamente.
	[Header("Velocidade de Personagem e Bala")]
    public float moveSpeed = 10f;
    public float bulletForce = 20f;

	[Header("Dash")]
    public float dashForce = 10f;
    private float dashDuration;
    public float startDashTime;

	[Header("Booleanos e Direção")]
    public bool isOnCoolDown;
    public bool isNotMoving;
	public bool recentlyDamaged;
    public int direction;

    public Rigidbody rb;

    [Header("Transform do firePoint da bala")]

    public Transform firePoint;
    public GameObject bulletPrefab;
	
	public GameManagerScript gameManager;

    //Vetor de posição para utilizar no raycast.

    Vector3 position;
	
	//layer do chão para o raycast ser mirado apenas no chão 
	[SerializeField] public LayerMask layerMask;
	
	[Header("Cooldown Bar")]
	public GameObject DashCDObject;
	public Slider DashCD;
	public float dashFillBar;
	public Transform DashBarPosition;

    void Start()
    {
		gameManager = GameObject.Find("GameManagerObject").GetComponent<GameManagerScript>();
        rb = GetComponent<Rigidbody>();
		recentlyDamaged = false;
        dashDuration = startDashTime;
        isOnCoolDown = false;
    }

    void Update()
    {
        //Definindo a mira para ficar funcionando 100% do tempo no update.

        Aim();
		
		
        //Definindo o botão esquerdo do mouse para atirar.

        if (Input.GetButtonDown("Fire1") && !gameManager.pausedGame)
        {
            Shoot();
        }
		
		if(isOnCoolDown){
			DashCDObject.gameObject.SetActive(true);
			DashCD.value += Time.deltaTime;
			Vector3 namePos = Camera.main.WorldToScreenPoint(DashBarPosition.position);
			DashCDObject.transform.position = namePos;
		} else {
			DashCD.value = 0f;
			DashCDObject.gameObject.SetActive(false);
		}
    }

    void FixedUpdate()
    {

        /*if (rb.velocity.x == 0 && rb.velocity.y == 0 && rb.velocity.z == 0)
        {
            isNotMoving = true;
            direction = 0;
        }*/

        //Inputs de WASD para movimentação em 8 direções utilizando a multiplicação de velocidade por tempo.

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

                if (Input.GetKey(KeyCode.Space) && direction == 1&& !isOnCoolDown)
                {
                    rb.velocity = Vector3.forward * dashForce;
                    isOnCoolDown = true;
                    StartCoroutine("ResetCooldown");
					
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 2 && !isOnCoolDown)
                {
                    rb.velocity = Vector3.back * dashForce;
                    isOnCoolDown = true;
                    StartCoroutine("ResetCooldown");
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 3 && !isOnCoolDown)
                {
                    rb.velocity = Vector3.left * dashForce;
                    isOnCoolDown = true;
                    StartCoroutine("ResetCooldown");
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 4 && !isOnCoolDown)
                {
                    rb.velocity = Vector3.right * dashForce;
                    isOnCoolDown = true;
                    StartCoroutine("ResetCooldown");
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 5 && !isOnCoolDown)
                {
                    rb.velocity = Vector3.forward + Vector3.left * dashForce;
                    isOnCoolDown = true;
                    StartCoroutine("ResetCooldown");
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 6 && !isOnCoolDown)
                {
                    rb.velocity = Vector3.forward + Vector3.right * dashForce;
                    isOnCoolDown = true;
                    StartCoroutine("ResetCooldown");
                }
                else if (Input.GetKey(KeyCode.Space) && direction == 7 && !isOnCoolDown)
                {
                    rb.velocity = Vector3.back + Vector3.left * dashForce;
                    isOnCoolDown = true;
                    StartCoroutine("ResetCooldown");
                }         
                else if (Input.GetKey(KeyCode.Space) && direction == 8 && !isOnCoolDown)
                {
                    rb.velocity = Vector3.back + Vector3.right * dashForce;
                    isOnCoolDown = true;
                    StartCoroutine("ResetCooldown");
                }
            }
        }

    void Aim()
    {
        //Utilização de Raycast para identificar a posição do mouse na tela através da câmera.

        RaycastHit hit;

        //Pegando o input de posição do mouse com raycast.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit , float.MaxValue, layerMask))
        {
            position = new Vector3(hit.point.x, 0, hit.point.z);
        }

        //Criando quaternion que define a rotação do player em relação ao mouse.

        Quaternion newRotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);

        //Zerando o quaternion de x e z para evitar que o player rode de formas estranhas...

        newRotation.x = 0f; 
        newRotation.z = 0f;

        //controle da velocidade da rotação.

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 20);
    }

    void Shoot()
    {
        //Instanciamento de prefab do tiro de personagem.

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        //Adição de força no tiro para impulsionar o prefab.

        bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Wall"){
			rb.velocity = Vector3.zero;
		}
	}

    public IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        isOnCoolDown = false;
    }
}
