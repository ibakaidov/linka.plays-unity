using Shell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFan : MonoBehaviour
{
    private BoxCollider colider;
    private bool isHit;
    private Animation animation;
    private ParticleSystem particle;

    public bool IsHit { get { return isHit; } }
    // Start is called before the first frame update
    void Start()
    {
        colider = GetComponent<BoxCollider>();
        animation = GetComponent<Animation>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = SightMaster.Point;
        Ray ray = Camera.main.ScreenPointToRay(point);
        isHit = colider.Raycast(ray, out RaycastHit hit, float.MaxValue);

        animation.enabled = isHit;
        var shape = particle.shape;
        shape.shapeType = isHit ? ParticleSystemShapeType.Cone : ParticleSystemShapeType.Sphere;
        var main = particle.main;
            main.startLifetime = IsHit ? 5 : 1; 
    }
}
