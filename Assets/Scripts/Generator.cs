using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private Transform obstaclePrefab;
    [SerializeField] private int obstacleCountTarget;
    [SerializeField] private float levelWidth;
    [SerializeField] private float levelDepth;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float minDistance;
    [SerializeField] private int triesCount;

    [SerializeField] private int Spread;

    private List<Transform> obstacles = new List<Transform>();

    public void Start()
    {
        Debug.Log("Generator Awake");
        //Clear();
        GeneratePoisson();
    }
    public void Clear()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            Destroy(obstacles[i].gameObject);
        }
        obstacles.Clear();
    }
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

    public void GeneratePoisson()
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(Vector3.zero);
        int count = 0;
        while (points.Count > 0 && count < obstacleCountTarget)
        {
            int SelectedIndex = Random.Range(0, points.Count);
            Vector3 point = points[SelectedIndex];
            
            int tries = triesCount;
            while (tries > 0)
            {
                Vector3 newPos = point + GetRandomPoint();
                bool valid = true;
                for (int i = 0; i < obstacles.Count; i++)
                {
                    if (Vector3.Distance(newPos, obstacles[i].position) < minDistance)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {

                    obstacles.Add(CreateObject(newPos));
                    points.Add(newPos);
                }
                else
                {
                    points.RemoveAt(SelectedIndex);
                }
            }
           
        }
    }
    private Vector3 GetRandomPoint()
    {
        Vector2 randomPoints = Random.insideUnitSphere;
        return new Vector3(randomPoints.x, 0, randomPoints.y);

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



}
