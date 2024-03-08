using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene : MonoBehaviour
{
    [SerializeField] private Transform initPoint;
    [SerializeField] private Transform ballPos;

    [SerializeField] private Transform player2Pos;
    [SerializeField] private Transform playerPos;

    [SerializeField] private Transform rock1;
    [SerializeField] private Transform rock2;
    [SerializeField] private Transform tree;
    [SerializeField] private Transform mushroom;
    [SerializeField] private Transform tree2;

    [SerializeField] private int numberObstacles;
    [SerializeField] private float minDistance;

    [SerializeField] private int spread;



    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomObstacle();  
    }

  
    void GenerateRandomObstacle()
    {
        List<Vector3> position = new();
        List<GameObject> obstaclesList = new();

        GameObject obstacles = new GameObject("Obstacles");

        obstaclesList.Add(player2Pos.gameObject);
        obstaclesList.Add(playerPos.gameObject);



        Vector3 positionInitiale = new Vector3(initPoint.position.x, initPoint.position.y, initPoint.position.z);
        GameObject item = CreateNewItem(position.Count, positionInitiale);
        item.transform.parent = obstacles.transform;
        obstaclesList.Add(item);
        position.Add(positionInitiale);


        Debug.Log(numberObstacles + " Obstacles generes");

        int safeCount = 1;
        while (position.Count > 0 && safeCount < numberObstacles)
        {

            int selectedIndex = Random.Range(0, position.Count);
            Vector3 point = position[selectedIndex];

            int tries = 1000;
            while (tries > 0)
            {

                Vector3 newPos = point + GetRandomPoint();

                bool valid = true;
                if (newPos.x > initPoint.position.x + 55 || newPos.x < initPoint.position.x - 55 || newPos.z > initPoint.position.z+55 || newPos.z < initPoint.position.z-55)
                    {
                    valid = false;
                }

                for (int i = 0; i < position.Count && valid; i++)
                {
                    if (Vector3.Distance(newPos, new Vector3(obstaclesList[i].transform.position.x, 0, obstaclesList[i].transform.position.z)) < minDistance)
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    item = CreateNewItem(position.Count, newPos);

                    item.transform.parent = obstacles.transform;
                    obstaclesList.Add(item);
                    position.Add(newPos);
                    
                }
                /*Debug.Log("Tries: " + tries);*/
                tries--;
            }
            /*Debug.Log("SafeCount: " + safeCount);*/
            safeCount++;
        }



        Vector3 GetRandomPoint()
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            return new Vector3(randomPoint.x, 0, randomPoint.y) * spread;
        }

        GameObject GetPrefab()
        {
            int prefabGenerated = Random.Range(0, 4);

            switch (prefabGenerated)
            {
                case 0: 
                   return rock1.gameObject;
                case 1:
                    return rock2.gameObject;
                case 2:
                    return tree.gameObject;
                case 3:
                    return mushroom.gameObject;
                case 4:
                    return tree2.gameObject;
                default:
                    return null;
            }
        }

        GameObject CreateNewItem(int i, Vector3 newPos)
        {
            GameObject itemCreated = null;

            itemCreated = Instantiate(GetPrefab(), newPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
            itemCreated.name = "item" + i;

            return itemCreated;
        }
        Debug.Log("Map Generated");
    }
}

