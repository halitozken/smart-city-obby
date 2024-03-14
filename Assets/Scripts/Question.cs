using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Question : MonoBehaviour
{
    [SerializeField] GameObject playerMovement;

    [SerializeField] GameObject questionOne;
    [SerializeField] GameObject questionTwo;
    [SerializeField] GameObject questionThree;

    [SerializeField] GameObject trueAnswer;
    [SerializeField] GameObject wrongAnswer;
    [SerializeField] TextMeshProUGUI wrongAnswerText;

    [SerializeField] GameObject checkPoint;
    [SerializeField] GameObject checkPointOne;
    [SerializeField] GameObject checkPointTwo;
    [SerializeField] GameObject checkPointThree;
    public void QuestionOneTrue()
    {
        trueAnswer.SetActive(true);
        ActivateScripts();
        transform.position = checkPointOne.transform.position;

        string checkPoint = "one";
        PlayerPrefs.SetString("CheckPoint", checkPoint);
        Invoke(nameof(closeAnswers), 1.3f);  
    }

    public void QuestionOneWrong()
    {
        wrongAnswer.SetActive(true);
        wrongAnswerText.text = "Ýnsanlarýn yaþam kalitesini artýrmak";
        Invoke(nameof(closeAnswers), 1.3f);
        
        string checkPointZero = "zero";
        PlayerPrefs.SetString("CheckPoint", checkPointZero);
       
        ActivateScripts();
        transform.position = checkPoint.transform.position;
    }

    public void QuestionTwoTrue()
    {
        trueAnswer.SetActive(true);
        ActivateScripts();
        transform.position = checkPointTwo.transform.position;

        string checkPoint = "two";
        PlayerPrefs.SetString("CheckPoint", checkPoint);
        Invoke(nameof(closeAnswers), 1.3f);
    }

    public void QuestionTwoWrong()
    {
        wrongAnswer.SetActive(true);
        wrongAnswerText.text = "Otopark yerlerini gösteren akýllý panolar";
        Invoke(nameof(closeAnswers), 1.3f);

        string checkPoint = "one";
        PlayerPrefs.SetString("CheckPoint", checkPoint);

        ActivateScripts();
        transform.position = checkPointOne.transform.position;
    }

    public void QuestionThreeTrue()
    {
        trueAnswer.SetActive(true);
        ActivateScripts();
        transform.position = checkPointThree.transform.position;

        string checkPoint = "three";
        PlayerPrefs.SetString("CheckPoint", checkPoint);
        Invoke(nameof(closeAnswers), 1.3f);
    }

    public void QuestionThreeWrong()
    {
        wrongAnswer.SetActive(true);
        wrongAnswerText.text = "Sürdürülebilirlik ve yaþam kalitesini artýrma";
        Invoke(nameof(closeAnswers), 1.3f);

        string checkPoint = "two";
        PlayerPrefs.SetString("CheckPoint", checkPoint);

        ActivateScripts();
        transform.position = checkPointTwo.transform.position;
    }

    public void closeAnswers()
    {
        questionOne.SetActive(false);
        questionTwo.SetActive(false);
        questionThree.SetActive(false);
        trueAnswer.SetActive(false);
        wrongAnswer.SetActive(false);
    }

    public void ActivateScripts()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }

 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Question1"))
        {
            questionOne.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }

        if (collision.gameObject.CompareTag("Question2"))
        {
            questionTwo.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
        
        if (collision.gameObject.CompareTag("Question3"))
        {
            questionThree.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
    }
}
