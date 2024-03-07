using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene : MonoBehaviour
{
    [SerializeField] private Transform ballPos;
    [SerializeField] private Transform player2Pos;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform goal1Pos;
    [SerializeField] private Transform goal2Pos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateRandomObstacle()
    {
        List<Vector3> position = new();
        List<GameObject> obstaclesList = new();

        GameObject obstacles = new GameObject("Obstacles");
        obstacles = obstacles;

        obstaclesList.Add(ballPos.gameObject);
        obstaclesList.Add(player2Pos.gameObject);
        obstaclesList.Add(playerPos.gameObject);
        obstaclesList.Add(goal1Pos.gameObject);
        obstaclesList.Add(goal2Pos.gameObject);



        Vector3 positionInitiale = new Vector3(-20, 0, -40);
        GameObject item = CreateNewItem(position.Count, positionInitiale);
        item.transform.parent = obstacles.transform;
        obstaclesList.Add(item);
        position.Add(positionInitiale);


        numberObstacles = Random.Range(30, 50);
        Debug.Log(numberObstacles);

        int safeCount = 0;
        while (position.Count > 0 && safeCount++ < numberObstacles)
        {

            int selectedIndex = Random.Range(0, position.Count);
            Vector3 point = position[selectedIndex];

            int tries = 400;
            while (tries-- > 0)
            {

                Vector3 newPos = point + GetRandomPoint();

                bool valid = true;
                if (newPos.x > 20 || newPos.x < -20 || newPos.z > 40 || newPos.z < -40)
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
                    break;
                }
            }

        }



        Vector3 GetRandomPoint()
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            return new Vector3(randomPoint.x, 0, randomPoint.y) * spread;
        }

        GameObject GetPrefab()
        {
            int prefabGenerated = Random.Range(0, 5);

            switch (prefabGenerated)
            {
                case 0: // Birch tree
                    return birchTree;
                case 1: // Oak tree
                    return oakTree;
                case 2: // Rock01
                    return rock01;
                case 3: // Rock02
                    return rock02;
                case 4: // Rock03
                    return rock03;
                default:
                    return null;
            }
        }

        GameObject CreateNewItem(int i, Vector3 newPos)
        {
            GameObject itemCreated = null;

            itemCreated = Instantiate(GetPrefab(), newPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
            itemCreated.tag = "Obstacle";
            itemCreated.name = "item" + i;

            return itemCreated;
        }
    }
}
}
