using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardousObject : MonoBehaviour
{
    private GameObject mainPlatform;
    public HazardManager hazardManager;
    public float range;
    public float verticalOffset;

    public float minLifeSpan;
    public float maxLifeSpan;
    private float lifeSpan;

    private float timeElapsed;

    private void Start()
    {
        mainPlatform = hazardManager.platform;
        range = mainPlatform.transform.localScale.x / 2;
        lifeSpan = Random.Range(minLifeSpan, maxLifeSpan);
    }

    private void OnEnable()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        float randomSeed = Random.Range(-range, range);
        lifeSpan = Random.Range(minLifeSpan, maxLifeSpan);
        for (int i = 0; i < hazardManager.GetPlantListLength(); ++i)
        {
            if (CheckForNearbyPlants(hazardManager.GetPlantHazard(i)))
            {
                float offset = Random.Range(-transform.localScale.x, transform.localScale.x);
                randomSeed += offset;
            }
        }
        Mathf.Clamp(randomSeed, -range, range);
        this.transform.position = new Vector2(randomSeed, mainPlatform.transform.position.y + verticalOffset);
    }

    private bool CheckForNearbyPlants(GameObject plant)
    {
        return Vector2.Distance(transform.position, plant.transform.position) < 1;
    }

}
