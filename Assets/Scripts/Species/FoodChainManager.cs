using System.Collections.Generic;
using UnityEngine;

public class FoodChainManager : MonoBehaviour
{
    public List<SpeciesManager> speciesList = new List<SpeciesManager>();

    public float[][] speciesAdjacencyMatrix;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateGraph();

        for (int i = 0; i < speciesAdjacencyMatrix.Length; i++) {
            string column = "";
            for (int j = 0; j < speciesAdjacencyMatrix[i].Length; j++) {
                column += speciesAdjacencyMatrix[i][j].ToString() + " ";
            }
            print(column);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateGraph() {
        speciesAdjacencyMatrix = new float[speciesList.Count][];
        for (int i = 0; i < speciesList.Count; i++) {
            speciesAdjacencyMatrix[i] = new float[speciesList.Count];
            for (int j = 0; j < speciesList.Count; j++) {
                if (j == i) { // species can't eat themselves. Cannibalism bad :(((
                    print("dont eat yourself");
                    continue;
                }

                SpeciesManager currentPrey = speciesList[i];
                SpeciesManager currentPredator = speciesList[j];

                if (!StaticMfer.compareFoodToDiet(currentPrey.animalData.fleshType, currentPredator.animalData.dietTags)) {
                    // Prey cannot be eated, so skip to next loop
                    print(currentPrey.animalData.name + " vs " + currentPredator.animalData.name + ": not right diet");
                    continue;
                }
                if (!StaticMfer.compareSpeed(currentPrey.animalData.speed, currentPredator.animalData.speed)) {
                    // Prey cannot be eated, so skip to next loop
                    print(currentPrey.animalData.name + " vs " + currentPredator.animalData.name + ": not right speed");
                    continue;
                }
                if (!StaticMfer.compareSize(currentPrey.animalData.size, currentPredator.animalData.size)) {
                    // Prey cannot be eated, so skip to next loop
                    print(currentPrey.animalData.name + " vs " + currentPredator.animalData.name + ": not right size");
                    continue;
                }
                if (!StaticMfer.compareRealmToOffence(currentPrey.animalData.realmTags, currentPredator.animalData.offenceTags, currentPredator.animalData.realmTags)) {
                    // Prey cannot be eated, so skip to next loop
                    print(currentPrey.animalData.name + " vs " + currentPredator.animalData.name + ": not right anti-realm");
                    continue;
                }
                if (!StaticMfer.compareDefenceToOffence(currentPrey.animalData.defenceTags, currentPredator.animalData.offenceTags)) {
                    // Prey cannot be eated, so skip to next loop
                    print(currentPrey.animalData.name + " vs " + currentPredator.animalData.name + ": not right offenceTags");
                    continue;
                }
                print("yay scran time! Here's your stats dumbass: " + (float)speciesList[i].animalData.foodProduced / (float)speciesList[j].animalData.foodConsumed);
                speciesAdjacencyMatrix[i][j] = (float)speciesList[i].animalData.foodProduced / (float)speciesList[j].animalData.foodConsumed; // prey saturation : predator hunger
            }
        }
    }
}

