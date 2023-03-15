using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishParentScript : MonoBehaviour
{
    public Camera cam;
    public List<GameObject> fishes;
    public int count;

    readonly static Color[] colors = { Color.red, Color.green, Color.blue, Color.white, Color.yellow, Color.cyan, Color.magenta };

    void OnEnable()
    {
        var screen = (Vector2)cam.ViewportToWorldPoint(Vector2.one);
        for (int i = 0; i < count; i++)
        {
            var fish = Instantiate(fishes[Random.Range(0, fishes.Count)], gameObject.transform);
            fish.transform.position = new Vector2(Random.Range(-screen.x * 0.5f, screen.x * 0.8f), Random.Range(-screen.y, screen.y) * 0.8f);
            fish.transform.localScale *= Random.Range(0.5f, 1.2f);
            var renderers = fish.GetComponentsInChildren<SpriteRenderer>();
            var color = colors[Random.Range(0, colors.Length)] / 2 + Color.white / 2;
            for (int j = 0; j < renderers.Length; j++)
            {
                if (renderers[j].name.ToLower().Contains("eye")) { continue; }
                renderers[j].color = color;
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
