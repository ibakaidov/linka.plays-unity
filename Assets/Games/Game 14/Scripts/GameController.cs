using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] public EyeFan Fan;
    [SerializeField] public GameObject Ball;
    [SerializeField] public GameObject firework;
    [SerializeField] public AudioSource SuccessAudio;
    public GameObject[] Levels = new GameObject[] { };


    private int currentLLevelIndex = 0;
    private Rigidbody rb;

    public float SPEED = 5;
    private GameObject Level;
    private bool reloaded = true;
    private float lastTS = 0;
    // Start is called before the first frame update
    void Start()
    {
        BallColider = Ball.GetComponent<SphereCollider>();
        rb = Ball.GetComponent<Rigidbody>();
        LoadLevel();
    }

    private void LoadLevel()
    {

        if (Level != null)
        {
            GameObject.Destroy(Level);
        }
        Level = GameObject.Instantiate(Levels[currentLLevelIndex>=Levels.Length?Levels.Length-1:currentLLevelIndex]);

        Ball.transform.position = spawn;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!reloaded)
        {
            if((Time.time - lastTS) >= 3)
            {
                currentLLevelIndex++;   
                if(currentLLevelIndex>=Levels.Length){
                    firework.SetActive(true);
                }
                LoadLevel();
                reloaded= true;
            }

            return;

        }
        if (Fan.IsHit && reloaded)
        {
            rb.AddForce(Vector3.right * SPEED);
        }

        if (finishColider.bounds.Intersects(BallColider.bounds))
        {
            reloaded = false;
            lastTS = Time.time;
            SuccessAudio.Play();
        }

    }

    Vector3 spawn {
        get
        {
            return GameObject.FindGameObjectWithTag("Respawn").transform.position;
        }
    }
    BoxCollider finishColider
    {
        get
        {
            return GameObject.FindGameObjectWithTag("Finish").GetComponent<BoxCollider>();
        }
    }

    public SphereCollider BallColider { get; private set; }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
