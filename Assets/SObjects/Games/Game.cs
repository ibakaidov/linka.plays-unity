using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "Game", menuName = "Data/Games/Game", order = 1)]
public class Game : ScriptableObject
{
    public string Name;
    public string Description;
    public string Order;
    public Sprite Icon;
    public GameCategory Category;
    public string  GameSceneId;
}