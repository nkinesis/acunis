using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveBall : MonoBehaviour
{
    public GameObject player;
    public float forceStrength = 1f;

    private void Update()
    {
        if (Input.GetMouseButton(0) && !RunUI.inputsBlocked && !RunUI.isPaused)
        {
            RaycastHit hit;
            RunUI.inputsBlocked = true;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.transform.gameObject.tag == "Terrain")
            {
                var mouseDir = hit.point - player.transform.position;
                mouseDir = mouseDir.normalized;
                player.GetComponent<Rigidbody>().AddForce(mouseDir * RunUI.getCurrentForce(), ForceMode.Impulse);
            }

        }
    }

}
