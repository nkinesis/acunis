using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score1 : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "CueBall")
        {
            other.transform.position = new Vector3(12.4f, 3.6f, 11.3f);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (other.tag == "PoolBall")
        {
            int x;
            x = (RunUI.currentPlayer == 1) ? ++RunUI.player1.Score : RunUI.player1.Score;
            x = (RunUI.currentPlayer == 2) ? ++RunUI.player2.Score : RunUI.player2.Score;
            Destroy(other);
        }

    }

}
