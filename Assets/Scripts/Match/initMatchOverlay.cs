using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class initMatchOverlay : MonoBehaviour
    {
    public static GameObject overlay;
    void Awake()
    {
        overlay = GameObject.Find("overlay");
    }
    void Start()
        {


    OverlayNotActive();
        }
        public static void OverlayActive()
        {
            overlay.SetActive(true);
        }
        public void OverlayNotActive()
        {
            overlay.SetActive(false);
        }
    }
 
