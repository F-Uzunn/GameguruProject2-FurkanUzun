using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SteamScriptableObject/ScriptableObject", order = 1)] //Assetlerin oldugu kisimda bir scriptable object acmamizi saglayan kisim
public class GameData : ScriptableObject
{
    //BURAYA DATA ICIN KULLANMAK ISTEDGINIZ VERILER GELECEK
    public List<Rooms> rooms = new List<Rooms>();
}

[System.Serializable] // Leveller icin dizdigimiz kisim
public class Rooms
{
}