using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        { 
            StartMatch();
        }
    }
    public void StartMatch()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
