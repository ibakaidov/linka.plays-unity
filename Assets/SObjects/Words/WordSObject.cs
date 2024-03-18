using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/WordSObject")]
public class WordSObject : ScriptableObject
{
    [SerializeField]
    public string word;
    [SerializeField]
    public string emoji;

    [SerializeField]
    public AudioClip clip;
    [SerializeField]
    public string category;

}
