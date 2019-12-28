using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform araba;
    
    private void Update()
    {
        Vector3 konum = new Vector3(araba.position.x, araba.position.y, transform.position.z);
        transform.position = konum;
    }
}
