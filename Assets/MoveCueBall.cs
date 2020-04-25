using UnityEngine;
using UnityEngine.AI;

public class MoveCueBall : MonoBehaviour
{

    public GameObject player;

    /// <summary>
    /// How long the player move for every frame.
    /// </summary>
    public float moveDisPerSec = 1;

    /// <summary>
    /// Record the mouse position.
    /// </summary>
    Vector3 destination;

    private void Start()
    {
        // At first, set the destination to player's position.
        destination = player.transform.position;
    }

    private void Update()
    {
        // Check the mouse left button click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.transform.gameObject.tag == "Terrain")
            {
                // When click on the terreno, record the position.
                destination = hit.point;
            }
        }
    }

    private void FixedUpdate()
    {
        // get the distance between the player and the destination pos
        float dis = Vector3.Distance(player.transform.position, destination);
        if (dis > 0)
        {
            // decide the moveDis for this frame. 
            //(Mathf.Clamp limits the first value, to make sure if the distance between the player and the destination pos is short than you set,
            // it only need to move to the destination. So at that moment, the moveDis should set to the "dis".)
            float moveDis = Mathf.Clamp(moveDisPerSec * Time.fixedDeltaTime, 0, dis);

            //get the unit vector which means the move direction, and multiply by the move distance.
            Vector3 move = (destination - transform.position).normalized * moveDis;
            player.transform.Translate(move);
        }
    }

}

