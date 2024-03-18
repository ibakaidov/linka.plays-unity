using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int x=1; x<8; x+=2){
            int skip = Random.Range(2, 8);
            for (int y=0; y<9; y++){
                if (y == skip){
                    GenerateCube(new Vector2(x, y), true);
                }
                else{
                    GenerateCube(new Vector2(x, y));
                }
            }
        }
        
    }

    GameObject GenerateCube(Vector2 vector, bool isPortal = false){
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(vector.x, 0.5f, vector.y);
        if (isPortal){
            cube.GetComponent<Renderer>().material.color = Color.blue;
            cube.transform.position = new Vector3(vector.x, -0.45f, vector.y);
            cube.GetComponent<BoxCollider>().isTrigger = true;

        }

        return cube;
    }

}
