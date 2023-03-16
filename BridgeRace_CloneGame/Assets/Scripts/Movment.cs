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
    }

    private void MovmentFonc()
    {
        float AxisX = Input.GetAxisRaw("Horizontal");
        float AxisZ = Input.GetAxisRaw("Vertical");
        
        transform.Translate(new Vector3(AxisX, 0, AxisZ).normalized * speed);
    }

}
