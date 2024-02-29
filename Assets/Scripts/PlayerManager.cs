using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private List<int> playerIDs = new List<int>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public int RegisterPlayer()
    {
        int newID = playerIDs.Count + 1;
        playerIDs.Add(newID);
        return newID;
    }
}
