using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private Transform obstaclePrefab;
    [SerializeField] private int obstacleCountTarget;
    [SerializeField] private float levelWidth;
    [SerializeField] private int levelDepth;
    [SerializeField] private int minScale;
    [SerializeField] private int maxScale;

    [SerializeField] private int Spread;

    private List<Transform> obstacles = new List<Transform>();
    public void Generate()
    {
        int count = 0, safe = 1000;
        while (count < obstacleCountTarget && safe >0)
        {
            Vector3 pos = new Vector3(Random.value * levelWidth - (levelWidth * 0.5f), 20, Random.value * levelDepth - (levelDepth * 0.5f));
            bool valid = true;



            for (int i = 0; i < obstacles.Count; i++)
            {
                if (Vector3.Distance(new Vector3(pos.x, 0, pos.z), new Vector3(obstacles[i].position.x, 0, obstacles[i].position.z)) < Spread)
 
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                obstacles.Add(CreateObject(pos));
                count++;
            }

           safe--;

        }
    }

    private Transform CreateObject(Vector3 pos)
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(pos, Vector3.down, out hitinfo, 100, LayerMask.GetMask("Ground")))
        {
            pos.y = hitinfo.point.y;
        }
        Transform tsfm = Instantiate(obstaclePrefab, transform.position, Quaternion.identity, transform);
        tsfm.localScale = Vector3.one * Random.Range(minScale, maxScale);
        return tsfm;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
