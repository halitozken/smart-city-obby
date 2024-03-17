using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] GameObject CheckPoint;
    [SerializeField] GameObject CheckPointOne;
    [SerializeField] GameObject CheckPointTwo;
    [SerializeField] GameObject CheckPointThree;

    private void Update()
    {
        if(transform.position.y < -10f)
        {
            Die();
        }
    }


    void Die()
    {
        string checkPoint = PlayerPrefs.GetString("CheckPoint");
        
            if(checkPoint != null) 
            {
            transform.position = CheckPoint.transform.position;
            }

            if(checkPoint == "zero")
            {
                transform.position = CheckPoint.transform.position;
            }

            if (checkPoint == "one")
            {
                transform.position = CheckPointOne.transform.position;
            }
        
            if (checkPoint == "two")
            {
                transform.position = CheckPointTwo.transform.position;
            }

            if (checkPoint == "three")
            {
            transform.position = CheckPointThree.transform.position;
            }
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            Die();
        }

        
    }
}
