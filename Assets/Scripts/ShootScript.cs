using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    Vector3 mousePos;

    void Start()
    {
        
    }

    void Update()
    {
        mousePos = Input.mousePosition;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 offset = new Vector3(mousePos.x - screenPoint.x, 0, mousePos.z - screenPoint.z);

        float angle = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
