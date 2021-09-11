using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //velocidade de movimento do player e do tiro, respectivamente.

    public float moveSpeed = 10f;
    public float bulletForce = 20f;
    
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
        //Inputs de WASD para movimenta��o em 8 dire��es utilizando a multiplica��o de velocidade por tempo.
		if(!recentlyDamaged){
			if (Input.GetKey(KeyCode.A))
			{
				rb.MovePosition(rb.position + Vector3.left * moveSpeed * Time.fixedDeltaTime);
			}

			if (Input.GetKey(KeyCode.D))
			{
				rb.MovePosition(rb.position + Vector3.right * moveSpeed * Time.fixedDeltaTime);
			}

			if (Input.GetKey(KeyCode.W))
			{
				rb.MovePosition(rb.position + Vector3.forward * moveSpeed * Time.fixedDeltaTime);
			}

			if (Input.GetKey(KeyCode.S))
			{
				rb.MovePosition(rb.position + Vector3.back * moveSpeed * Time.fixedDeltaTime);
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
