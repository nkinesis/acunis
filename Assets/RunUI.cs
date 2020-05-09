using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunUI : MonoBehaviour
{
    public static int currentPlayer = 1;
    public static int scoreP1 = 0;
    public static int scoreP2 = 0;
    public static int forceP1 = 0;
    public static int forceP2 = 0;
    public static bool inputsBlocked = false;

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            switchPlayer();
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Agora: Jogador " + RunUI.currentPlayer);
        GUI.Label(new Rect(200, 10, 100, 20), "Jogador 1: " + RunUI.scoreP1);
        GUI.Label(new Rect(300, 10, 100, 20), "Jogador 2:  " + RunUI.scoreP2);
        GUI.Label(new Rect(10, 370, 100, 20), "Força:  " + RunUI.forceP1);
    }

    public void switchPlayer()
    {
        RunUI.currentPlayer = (RunUI.currentPlayer == 1) ? 2 : 1;
        RunUI.inputsBlocked = false;
    }
}
