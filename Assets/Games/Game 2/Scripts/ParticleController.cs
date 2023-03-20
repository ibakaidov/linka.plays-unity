using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    public void SetSprite(Sprite sprite)
    {
        ps.textureSheetAnimation.SetSprite(0, sprite);
    }
}
