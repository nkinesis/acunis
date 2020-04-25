using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour {

    public float force = 0.000001f;

    // Use this for initialization
    private void Start () {
	}
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform != null)
                {
                    Rigidbody rb;
                    if (rb = hit.transform.GetComponent<Rigidbody>())
                    {
                        print(hit.transform.gameObject.name);
                        move(rb);
                    }
                }
            }
        }
	}

    private void move(Rigidbody rig)
    {
        rig.AddForce(rig.transform.up * 1, ForceMode.Impulse);
    }
}
