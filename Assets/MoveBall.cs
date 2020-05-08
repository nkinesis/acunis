using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveBall : MonoBehaviour
{
    public GameObject player;
    public Vector3 calc;

    private void Start()
    {
        //calc = new Camera();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.transform.gameObject.tag == "Terrain")
            {
                var mouseDir = hit.point - player.transform.position;
                mouseDir = mouseDir.normalized;
                player.GetComponent<Rigidbody>().AddForce(mouseDir * 10);
            }
        }
    }

}
