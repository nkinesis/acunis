using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveBall : MonoBehaviour
{
    public GameObject player;

    public float moveDisPerSec = 10;
    public bool clicked = false;
    public Vector3 destination;

    private void Start()
    {
        // At first, set the destination to player's position.
        destination = player.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || clicked)
        {
            RaycastHit hit;
            Vector3 targetPos = new Vector3();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.transform.gameObject.tag == "Terrain")
            {
                // When click on the terreno, record the position.
                targetPos = hit.point;
                player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, moveDisPerSec * Time.deltaTime);
                clicked = true;
            }
            print(Math.Abs(player.transform.position.y - targetPos.y));
            if (Math.Abs(player.transform.position.x - targetPos.x) < 0.3f
                && Math.Abs(player.transform.position.y - targetPos.y) < 0.3f
                && Math.Abs(player.transform.position.z - targetPos.z) < 0.3f)
            {
                clicked = false;
            }

        }
    }

    // Check the mouse left button click
    //if (Input.GetMouseButtonDown(0))
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.transform.gameObject.tag == "Terrain")
    //    {
    //        // When click on the terreno, record the position.
    //        destination = hit.point;
    //    }
    //}

}
