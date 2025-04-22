using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum FleshTypes{
    Meat,
    Plant,
    // Weekend   = Saturday & Sunday
}

// should actually be Teeth types, as an animal's diet is usually down to what teeth that have
public enum DietTypes{
    Carnivore,
    Herbivore,
}

public enum SpeedTypes{
    Slow = -1,
    Medium = 0,
    Fast = 1,
}

public enum SizeTypes{
    Tiny = -2,
    Small = -1,
    Medium = 0,
    Large = 1,
    Massive = 2,
}

public enum DefenceTypes{
    Armoured,
    Camouflage,
    Poison,
}

public enum RealmTypes{
    Aquatic,
    Terrestrial,
    Aerial
}

// public enum HabitatTypes{
//     Cold,
//     ExtremeCold, 
//     Hot,
//     ExtremeHot,
//     Dark,
// }

public enum OffenceTypes{
    Pierce,
    Venom,
    Jumper,
    Raptor,
    Fisher,
    Smell,
    Sight,
    Hearing,
    Nocturnal,
    Immune,
}

[CreateAssetMenu(fileName = "SpeciesObject", menuName = "SpeciesScriptableObjects")]
public class SpeciesScriptableObject : ScriptableObject
{
    public string speciesName;
    public FleshTypes fleshType;
    public DietTypes[] dietTags;
    public DefenceTypes[] defenceTags;
    public OffenceTypes[] offenceTags;
    public RealmTypes[] realmTags;
    public SizeTypes size;
    public SpeedTypes speed;
    public int foodConsumed; //food consumed per entity
    public int foodProduced; //food produced per entity

    public static int intthing = 1;
}

public static class StaticMfer {
    private static Dictionary<FleshTypes, DietTypes[]> FoodToDietMap = new Dictionary<FleshTypes, DietTypes[]>{
        {FleshTypes.Meat, new DietTypes[]{DietTypes.Carnivore}},
        {FleshTypes.Plant, new DietTypes[]{DietTypes.Herbivore}},
    };

    private static Dictionary<RealmTypes, OffenceTypes[]> RealmToOffenceMap = new Dictionary<RealmTypes, OffenceTypes[]>{
        {RealmTypes.Aerial, new OffenceTypes[]{OffenceTypes.Jumper}},
        {RealmTypes.Terrestrial, new OffenceTypes[]{OffenceTypes.Raptor}},
        {RealmTypes.Aquatic, new OffenceTypes[]{OffenceTypes.Fisher}},
    };

    private static Dictionary<DefenceTypes, OffenceTypes[]> DefenceToOffenceMap = new Dictionary<DefenceTypes, OffenceTypes[]>{
        {DefenceTypes.Armoured, new OffenceTypes[]{OffenceTypes.Pierce}},
        {DefenceTypes.Camouflage, new OffenceTypes[]{OffenceTypes.Smell}},
        {DefenceTypes.Poison, new OffenceTypes[]{OffenceTypes.Immune}},
    };
    
    // return true if Prey *can* be eaten by Predator
    public static bool compareFoodToDiet(FleshTypes preyInput, DietTypes[] predInput) {
        DietTypes[] counterList = FoodToDietMap[preyInput];
        foreach (DietTypes dietTag in predInput) {
            if (counterList.Contains(dietTag)) {
                return true;
            }
        }
        return false;
    }

    public static bool compareSpeed(SpeedTypes preyInput, SpeedTypes predInput) {
        return preyInput <= predInput;
    } 

    public static bool compareSize(SizeTypes preyInput, SizeTypes predInput) {
        return preyInput <= predInput;
    } 

    public static bool compareRealmToOffence(RealmTypes[] preyInput, OffenceTypes[] predInput, RealmTypes[] predRealmsInput) {
        
        
        foreach (RealmTypes realm in preyInput) { // realm = aquatic
            foreach (RealmTypes predRealm in predRealmsInput) {
                if (predRealm == realm) {
                    return true;
                }
            }
            OffenceTypes[] counterList = RealmToOffenceMap[realm]; // counterList = [fisher, aquatic]
            foreach (OffenceTypes offenceTag in predInput) { // fisher
                if (counterList.Contains(offenceTag)) { // if [fisher, aquatic] contains aquatic
                    return true;
                }
            }
        }
        return false;
    } 

    public static bool compareDefenceToOffence(DefenceTypes[] preyInput, OffenceTypes[] predInput) {
        foreach (DefenceTypes realm in preyInput) { // defence = armoured
            OffenceTypes[] counterList = DefenceToOffenceMap[realm]; // counterList = [pierce]
            foreach (OffenceTypes offenceTag in predInput) { // smell | pierce
                if (counterList.Contains(offenceTag)) { // 
                    continue; //if [pierce] contains pierce
                } else {
                    return false; //if [pierce] contains smell
                }
            }
        }
        return true;
    }
}


//  public static void isInFoodToDietMap(FleshTypes input, out DietTypes[] namestill) {
//  namestill = FoodToDietMap[input];
// } 
//
// StaticMfer.isInFoodToDietMap(speciesList[i].animalData.fleshType, out DietTypes[] shitfart);