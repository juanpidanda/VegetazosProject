using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    public GameObject platform;
    public GameObject hazardPlant;

    public float timeToCheckPlants;
    
    private List<GameObject> plantList;
    public int hazardAmount;

    private float timeElapsed;
    private float totalTimeElapsed;

    private void Awake()
    {
        plantList = new List<GameObject>();
        for (int i = 0; i < hazardAmount; ++i)
        {
            GameObject newPlant = Instantiate(hazardPlant);
            plantList.Add(newPlant);
        }
    }

    void Start()
    {
        
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        totalTimeElapsed += Time.deltaTime;



        if(timeElapsed > timeToCheckPlants)
        {
            foreach(GameObject plant in plantList)
            {
                plant.SetActive(true);
            }
            timeElapsed = 0;
        }
    }

    public GameObject GetPlantHazard(int index)
    {
        return plantList[index];
    }

    public int GetPlantListLength()
    {
        return plantList.Count;
    }
}
