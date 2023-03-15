using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class JojobaScript : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public int count;
        public float scale;
        public float speedScale;
    }

    public Animator animator;
    LineRenderer tongue;
    public GameObject komar;
	public Transform placeForKomars;
	public AudioSource kvaSound;
	public Camera cam;
	public float tongueSpeed = 10;

	int komarsCount = 0;

	public List<GameObject> komars = new List<GameObject>();

    public static JojobaScript instance;

    public List<Level> levels;
    public float speed;
    static int level = 0;
    static int stage = 0;
    const int maxStage = 3;

	void Awake()
    {
        instance = this;
        tongue = gameObject.GetComponentInChildren<LineRenderer>();
    }

	private void Update() {
		if ( komars.Count>0 ) {
			tongue.SetPosition(1, Vector2.MoveTowards(tongue.GetPosition(1), komars[0].transform.position, Time.deltaTime * tongueSpeed));
			if ( Vector2.Distance(tongue.GetPosition(1), komars[0].transform.position) < 0.3f ) {
				MosquitoCounter.counter++;
				KVA();
                DeleteKomar();
                Destroy(komars[0]);
                komars.RemoveAt(0);
            }
		}
		else {
            tongue.SetPosition(1, Vector2.MoveTowards(tongue.GetPosition(1), tongue.GetPosition(0), Time.deltaTime * tongueSpeed * 2));
        }
	}

	// Start is called before the first frame update
	void OnEnable()
    {
        tongue.SetPosition(0, transform.position);
        tongue.SetPosition(1, transform.position);

        level = 0;
        stage = 0;
		komars.Clear();
        StartNextStage();
    }

    public void StartNextStage()
    {
        if (stage == maxStage)
        {
            stage = 0;
            level++;
        }
        stage++;
        if (level == levels.Count)
        {
            Win();
        }
        var screen = (Vector2)cam.ViewportToWorldPoint(Vector2.one);
        komarsCount = levels[level].count;

        for (int i = 0; i < levels[level].count; i++)
        {
            var nk = Instantiate(komar, placeForKomars);
            var script = nk.GetComponent<KomarScript>();
            var side = Random.value > 0.5f;
            if (stage == 1) //horizontal
            {
                nk.transform.position = new Vector2((side ? 1 : -1) * screen.x, Random.Range(-screen.y, screen.y) * 0.8f);
                script.speed = new Vector2((side ? -1 : 1) * speed * levels[level].speedScale * (Random.value + 1), 0);
            }
            else if (stage == 2) //vertical
            {
                nk.transform.position = new Vector2(Random.Range(-screen.x, screen.x) * 0.8f, (side ? 1 : -1) * screen.y);
                script.speed = new Vector2(0, (side ? -1 : 1) * speed * levels[level].speedScale * (Random.value + 1));
            }
            else if (stage == 3) //random
            {
                var side2 = Random.value > 0.5f;
                if (side)
                {
                    nk.transform.position = new Vector2(Random.Range(-screen.x, screen.x) * 0.8f, (side2 ? 1 : -1) * screen.y);
                }
                else
                {
                    nk.transform.position = new Vector2((side2 ? 1 : -1) * screen.x, Random.Range(-screen.y, screen.y) * 0.8f);
                }
                script.speed = (-(Vector2)nk.transform.position + Random.insideUnitCircle).normalized * speed * levels[level].speedScale * (Random.value + 1);
            }
            else //error
            {
                Debug.LogError("Something went wrong with state calculation");
            }
            nk.transform.localScale *= levels[level].scale;
            script.animator = animator;
            script.tongue = tongue;

            nk.transform.right = -script.speed;
        }
    }

    public void Win()
    {
        level = 0;
        stage = 0;
        StartNextStage();
    }

    public void DeleteKomar()
    {
        komarsCount--;
        Debug.Log(komarsCount);
        if (komarsCount == 0)
        {
            StartNextStage();
        }
    }

    public void KVA() {
		kvaSound.Play();
	}
}
