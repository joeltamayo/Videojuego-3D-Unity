using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HelixContoller : MonoBehaviour
{
    private Vector2 lastPosition;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;

    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();

    public float helixDistance;

    private List<GameObject> spawnedLevels = new List<GameObject>();


    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
        LoadStage(0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;
            if (lastPosition == Vector2.zero)
            {
                lastPosition = currentTapPosition;
            }
            float distance = lastPosition.x - currentTapPosition.x;
            lastPosition = currentTapPosition;

            transform.Rotate(Vector3.up * distance);

        }

        if (Input.GetMouseButtonUp(0))
        {
            lastPosition = Vector2.zero;
        }
    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Math.Clamp(stageNumber, 0, allStages.Count - 1)];

        if (stage == null)
        {
            Debug.Log("No Stages");
            return;
        }

        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;

        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        transform.localEulerAngles = startRotation;

        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosY -= levelDistance;

            GameObject level = Instantiate(helixLevelPrefab, transform);

            level.transform.localPosition = new Vector3(0, spawnPosY, 0);

            spawnedLevels.Add(level);

            int partsToDisable = 12 - stage.levels[i].partCount;

            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(UnityEngine.Random.Range(0, level.transform.childCount)).gameObject;

                if (!disabledParts.Contains(randomPart))

                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
            }

            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }

            }

            List<GameObject> deathParts = new List<GameObject>();
            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject ramdomPart = leftParts[(UnityEngine.Random.Range(0, leftParts.Count))];

                if(!deathParts.Contains(ramdomPart))
                {
                    ramdomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(ramdomPart);

                }
            }


        }
    }
}
