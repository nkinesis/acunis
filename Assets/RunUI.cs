using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunUI : MonoBehaviour
{
    public static int currentPlayer = 1;
    public static Player player1;
    public static Player player2;
    public static bool inputsBlocked = false;

    void Start()
    {
        RunUI.player1 = new Player();
        RunUI.player2 = new Player();
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            switchPlayer();
        }
        if (Input.GetKeyDown("q"))
        {
            if (RunUI.currentPlayer == 1 && getCurrentForce() < 40)
            {
                RunUI.player1.Force += 5;
            } else if (RunUI.currentPlayer == 2 && getCurrentForce() < 40)
            {
                RunUI.player2.Force += 5;
            }
        }
        if (Input.GetKeyDown("w"))
        {
            if (RunUI.currentPlayer == 1 && getCurrentForce() > 5)
            {
                RunUI.player1.Force -= 5;
            }
            else if (RunUI.currentPlayer == 2 && getCurrentForce() > 5)
            {
                RunUI.player2.Force -= 5;
            }
        }
    }
    void OnGUI()
    {

        GUI.Box(new Rect(10, 10, 120, 30), "Agora: Jogador " + RunUI.currentPlayer);
        GUI.Box(new Rect(200, 10, 120, 30), "Jogador 1: " + RunUI.player1.Score);
        GUI.Box(new Rect(300, 10, 120, 30), "Jogador 2:  " + RunUI.player2.Score);
        GUI.Box(new Rect(10, Screen.height - 30, 100, 30), "Força:  " + RunUI.getCurrentForce());

        GUI.Box(new Rect(Screen.width - 300, 10, 300, 30), "Pressione Z para passar a rodada.");


    }

    public static float getCurrentForce()
    {
        if (RunUI.currentPlayer == 1)
        {
            return RunUI.player1.Force;
        }
        else
        {
            return RunUI.player2.Force;
        }
    }


    public void switchPlayer()
    {
        RunUI.currentPlayer = (RunUI.currentPlayer == 1) ? 2 : 1;
        RunUI.inputsBlocked = false;

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject x = (GameObject) o;
            if (x.GetComponent<Rigidbody>() != null)
            {
                x.GetComponent<Rigidbody>().velocity = Vector3.zero;
                x.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }

        }
    }

}
