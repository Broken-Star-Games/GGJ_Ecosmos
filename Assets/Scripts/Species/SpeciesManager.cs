using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesManager : MonoBehaviour
{
    public SpeciesScriptableObject animalData;
    public List<GameObject> entityList = new List<GameObject>();
    public GameObject animalPrefab;

    public List<SpeciesManager> predatorList = new List<SpeciesManager>();

    [SerializeField] private GravityAttractor planet;

    public float animalCount;
    public float activeAnimalCount; // ones that are properly fed and producing
    public float totalFoodReceieved;
    public float totalFoodProduced;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnSpread = 5;

    private void InstantiateAnimal() {
        int count = (int)Math.Floor(animalCount);
        for (int i = 0; i < count; i++) {
            Vector3 spawnPosition = spawnPoint.position + new Vector3( UnityEngine.Random.Range(-spawnSpread, spawnSpread), 0, UnityEngine.Random.Range(-spawnSpread, spawnSpread) );

            GameObject entity = Instantiate(animalPrefab, spawnPosition, spawnPoint.rotation);
            entity.GetComponent<SpeciesEntityBehaviour>().animalData = animalData;
            entity.GetComponent<GravityBody>().planet = planet;
        }
    }

    public void CheckIfSatiated() {
        float temp = totalFoodReceieved;
        for (int i = 0; i < Math.Floor(animalCount); i++) {
            temp -= animalData.foodConsumed;
            if (temp > 0) {
                print("a " + animalData.name + " has eated enough");
                activeAnimalCount = i+1;
            } else {
                break;
            }
        }

        totalFoodProduced = activeAnimalCount * animalData.foodProduced;

        SendFood();
    }

    void Start() {
        // InstantiateAnimal();
    }

    public void SendFood() {
        totalFoodProduced = activeAnimalCount * animalData.foodProduced;
        foreach (SpeciesManager pred in predatorList) {
            pred.totalFoodReceieved = totalFoodProduced / predatorList.Count;
            print(animalData.name + " sent out " + pred.totalFoodReceieved + " yummers to " + pred.animalData.name);
            pred.CheckIfSatiated();
        }
    }
}
