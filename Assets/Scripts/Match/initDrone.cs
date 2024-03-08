using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class initDrone : MonoBehaviour
{
    public static GameObject drone;
    void Awake()
    {
        drone = GameObject.Find("Drone");
    }
    void Start()
    {
        DroneNotActive();
    }
    public static void DroneActive()
    {
        drone.SetActive(true);
    }
    public void DroneNotActive()
    {
       drone.SetActive(false);
    }
}

