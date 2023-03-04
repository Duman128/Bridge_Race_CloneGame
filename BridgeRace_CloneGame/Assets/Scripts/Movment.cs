using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float borderX;
    [SerializeField] private float borderZ;


    private void FixedUpdate()
    {
        MovmentFonc();
        BorderFonc();
    }

    private void MovmentFonc()
    {
        float AxisX = Input.GetAxisRaw("Horizontal");
        float AxisZ = Input.GetAxisRaw("Vertical");
        
        transform.Translate(new Vector3(AxisX, 0, AxisZ).normalized * speed);
    }

    private void BorderFonc()
    {
        if (transform.position.x >= borderX)
            transform.position = new Vector3(borderX - 0.2f, transform.position.y, transform.position.z);
        else if (transform.position.x <= -borderX)
            transform.position = new Vector3(-borderX + 0.2f, transform.position.y, transform.position.z);
        else if (transform.position.z >= borderZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, borderZ - 0.2f);
        else if (transform.position.z <= -borderZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, -borderZ + 0.2f);
    }
}
