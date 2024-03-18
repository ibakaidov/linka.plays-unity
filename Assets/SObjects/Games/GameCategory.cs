using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameCategory", menuName = "Data/Games/GameCategory", order = 1)]
public class GameCategory : ScriptableObject
{
    public string Name;
    public int Order;    
}
