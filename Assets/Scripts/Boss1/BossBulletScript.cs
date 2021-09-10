using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed;

    [SerializeField]
    Vector3 dir2;

    private void OnEnable()
    {
        Invoke("Destroy", 2.5f);
    }

    void Start()
    {
        moveSpeed = 12f;
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
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }	
}
