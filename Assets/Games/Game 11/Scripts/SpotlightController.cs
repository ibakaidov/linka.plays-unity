using Shell;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;

public class SpotlightController : MonoBehaviour
{

    public Sprite[] Animales = new Sprite[] {};
    public Sprite[] Blots = new Sprite[] {};
    public AudioSource ChangeSource;
    public AudioSource NewTaskSource;
    private Sprite currentAnimal;
    private Sprite currentBlot;
    private Image image;
    private Vector3 defaultScale;
    private bool lastHit;
    private float lastTime=0;
    private Vector3 defaultPosition;
    private Color standardBackground;
    private bool lastState;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<EnableDisableListener>().Enable += () => { OnEnable(); };
        GetComponentInParent<EnableDisableListener>().Disable += () => { OnDisable(); };
        image = GetComponent<Image>();
        defaultScale = image.rectTransform.localScale;
        defaultPosition = image.rectTransform.position;

        GetRandomAnimal();
        GetRandomBolt();

    }

    private void OnDisable()
    {
        Camera.main.backgroundColor = standardBackground;
    }

    private void OnEnable()
    {
        standardBackground = Camera.main.backgroundColor;
        Camera.main.backgroundColor = Color.black;
     
        if (defaultPosition != null)
        {
            image.rectTransform.localScale = defaultScale;

            image.rectTransform.position = defaultPosition;
        }
    }
    // Update is called once per frame
    void Update()
    {
        bool state = Time.time - lastTime < 3f;
        image.sprite = (state ? currentBlot : currentAnimal);
        if(lastState && !state)
        {
            Timeout();
        }
        lastState = state;

        var point = TobiiAPI.GetGazePoint();
        if (!point.IsValid)
        {
            lastHit = false;
            return;
        }


        Vector2 guipoint = point.Screen;
        bool gazeOnButton = RectTransformUtility.RectangleContainsScreenPoint(image.rectTransform, guipoint);


        if(gazeOnButton) {
            if(!lastHit)  OnHit();
            lastHit = true;
        }
        else
        {
            lastHit = false;
        }
        
    }

    private void Timeout()
    {
        Move();

    }

    private void OnHit()
    {
        Debug.Log("hit");
        if (Animales.Contains(image.sprite))
        {
            lastTime = Time.time;


            ChangeSource.Play();
        }
    }

    void Move()
    {
         GetRandomBolt();
         GetRandomAnimal();
        float width = Screen.width;
        float height = Screen.height;
        image.rectTransform.position = new Vector2(Random.Range(width*0.1f, width*0.75f), Random.Range(0.45f, height*0.85f)) ;
        image.rectTransform.localScale *= 0.9F;
        image.rectTransform.localScale *= 0.9F;
        image.rectTransform.localScale *= 0.9F;
        NewTaskSource.Play();
    }

    private void GetRandomAnimal()
    {

        currentAnimal = Animales[Random.Range(0, Animales.Length)];
    }
    private void GetRandomBolt() { currentBlot = Blots[Random.Range(0, Blots.Length)]; }
}
