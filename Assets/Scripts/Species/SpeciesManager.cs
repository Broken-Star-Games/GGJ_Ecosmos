using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesManager : MonoBehaviour
{
    public SpeciesScriptableObject animalData;
    public List<GameObject> entityList = new List<GameObject>();
    public GameObject animalPrefab;

    [SerializeField] private GravityAttractor planet;

    public float animalCount;

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

    void Start() {
        // InstantiateAnimal();
    }
}
