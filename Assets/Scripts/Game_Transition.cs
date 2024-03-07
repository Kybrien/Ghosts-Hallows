using System.Collections.Generic;
using UnityEngine;
public class Game_Transition : MonoBehaviour
{
    public List<Transform> teleportationPoints;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision detected");
        if (collider.gameObject.CompareTag("Player"))
        {
            initBall.BallActive();
            initDrone.DroneActive();
            initMatchOverlay.OverlayActive();
            Timer.StartMatch();
            TeleportAllPlayers();

        }
    }

    private void TeleportAllPlayers()
    {
        var players = PlayerManager.Instance.GetAllPlayers();
        for (int i = 0; i < players.Count; i++)
        {
            if (i < teleportationPoints.Count)
            {
                players[i].transform.position = teleportationPoints[i].position;
                /*players[i].transform.LookAt(teleportationPoints[i].position + Vector3.forward);*/  // Adjust this if you need a specific orientation
            }
        }
    }


} 