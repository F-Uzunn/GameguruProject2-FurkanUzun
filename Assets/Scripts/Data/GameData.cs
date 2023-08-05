using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Data", menuName = "SteamScriptableObject/ScriptableObject", order = 1)] //Assetlerin oldugu kisimda bir scriptable object acmamizi saglayan kisim
public class GameData : ScriptableObject
{
    public int levelIndex;

    [Button]
    public void Reset()
    {
        levelIndex = 1;
    }
}
