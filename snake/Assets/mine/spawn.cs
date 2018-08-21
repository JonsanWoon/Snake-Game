using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

    public GameObject foodPrefab;
    public GameObject powerPrefab;
    
    public Transform borderTop;
    public Transform borderRight;
    public Transform borderLeft;
    public Transform borderBottom;
    
    
	// Use this for initialization
	void Start () {
		Spawn(foodPrefab);
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] manyFood = GameObject.FindGameObjectsWithTag("food");
		if(manyFood.Length<10){
            Spawn(foodPrefab);
        }
        GameObject[] manyPower = GameObject.FindGameObjectsWithTag("power");
		if(manyPower.Length<3){
            Spawn(powerPrefab);
        }
	}
    
    void Spawn(GameObject prefab) {
        float xpos;
        float ypos;
        float diff = 0.25f;
        
        //between border position [left,right->x; updown->y]
        xpos = Random.Range(borderLeft.position.x+diff,borderRight.position.x-diff);
        ypos = Random.Range(borderTop.position.y-diff,borderBottom.position.y+diff);
        
        Instantiate(prefab, new Vector2(xpos, ypos), Quaternion.identity);
    }
}
