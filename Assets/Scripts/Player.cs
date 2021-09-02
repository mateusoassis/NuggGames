using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float bulletForce = 20f;
    
    public Rigidbody rb;

    public Transform firePoint;
    public GameObject bulletPrefab;

    Vector3 movement;
    Vector3 position;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Aim();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {

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

    void Aim()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit , 1000))
        {
            position = new Vector3(hit.point.x, 0, hit.point.z);
        }

        Quaternion newRotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);

        newRotation.x = 0f; 
        newRotation.z = 0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}
