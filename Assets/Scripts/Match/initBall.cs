using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class initBall : MonoBehaviour
{
    public static GameObject ball;
    void Awake()
    {
        ball = GameObject.Find("Match_Ball");
    }
    void Start()
    {
        BallNotActive();
    }
    public static void BallActive()
    {
        ball.SetActive(true);
    }
    public void BallNotActive()
    {
        ball.SetActive(false);
    }
}

