using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class uIManager : MonoBehaviour
{
    //public GameObject manager;
    public Text currentScoreText;
    public Text bestScoreText;


    public Slider slider;
    public Text actualLevel;
    public Text nextLevel;

    public Transform topTransform;
    public Transform goalTransform;

    public Transform ball;

    void Update()
    {
        currentScoreText.text = "Puntos: " + GameManager.singleton.currentScore;
        bestScoreText.text = "Record: " + GameManager.singleton.bestScore;

        ChangeSliderLvelProgress();
    }


    public void ChangeSliderLvelProgress() {
        actualLevel.text = "" + (GameManager.singleton.currentLevel +1);

        nextLevel.text = "" + (GameManager.singleton.currentLevel + 2);


        float totalDistance = (topTransform.position.y - goalTransform.position.y);

        float distanceLeft = totalDistance - (ball.position.y - goalTransform.position.y);

        float value = (distanceLeft / totalDistance);

        slider.value = Mathf.Lerp(slider.value, value, 5);
    }
}