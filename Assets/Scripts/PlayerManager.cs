using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private List<int> playerIDs = new List<int>();
    private Dictionary<int, GameObject> playerObjects = new Dictionary<int, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public int RegisterPlayer(GameObject playerObject)
    {
        int newID = playerIDs.Count + 1;
        playerIDs.Add(newID);
        playerObjects.Add(newID, playerObject);
        return newID;
    }

    public GameObject GetPlayerObject(int id)
    {
        playerObjects.TryGetValue(id, out GameObject playerObject);
        return playerObject;
    }

    public int GetPlayerCount()
    {
        return playerIDs.Count;
    }

    
    public List<GameObject> GetAllPlayers()
    {
        return new List<GameObject>(playerObjects.Values);
    }
}
