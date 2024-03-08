using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class initArrow : MonoBehaviour
{
    public static GameObject arrow;
    void Awake()
    {
        arrow = GameObject.Find("Arrow");
    }
    void Start()
    {
        ArrowNotActive();
    }
    public static void ArrowActive()
    {
        arrow.SetActive(true);
    }
    public void ArrowNotActive()
    {
        arrow.SetActive(false);
    }
}

