using UnityEngine;
using UnityEditor;

public class Player
{
    private int score = 0;
    private float force = 10f;

    public int Score { get => score; set => score = value; }
    public float Force { get => force; set => force = value; }
}