using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    

    [SerializeField] GameObject CheckPointOne;
    [SerializeField] GameObject CheckPointTwo;



    private void Update()
    {
        if(transform.position.y < -1f)
        {
            Die();
        }
    }


    void Die()
    {
        string checkPoint = PlayerPrefs.GetString("CheckPoint");
        
            if(checkPoint == "")
            {
                Invoke(nameof(ReloadLevel), 1.3f);
            }

            if (checkPoint == "one")
            {
                transform.position = CheckPointOne.transform.position;
            }
        
            if (checkPoint == "two")
            {
                transform.position = CheckPointTwo.transform.position;
            }   
    }
    
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            Die();
        }
    }
}
