using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomarScript : MonoBehaviour
{
    public Animator animator;
    public LineRenderer tongue;
    public Vector2 speed;
    private bool visible = false;

    readonly static Color[] colors = { Color.red, Color.green, Color.blue, Color.white, Color.yellow, Color.cyan, Color.magenta };

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)] / 2 + Color.white / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)speed * Time.deltaTime;
    }

    public void SightEnter()
    {
        if (!JojobaScript.instance.komars.Contains(gameObject)) { JojobaScript.instance.komars.Add(gameObject); };
    }

    public void SightExit()
    {
        if (JojobaScript.instance.komars.Contains(gameObject)) { JojobaScript.instance.komars.Remove(gameObject); };
    }

	void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            JojobaScript.instance.DeleteKomar();
            SightExit();
        }
        Destroy(gameObject);
    }
}
