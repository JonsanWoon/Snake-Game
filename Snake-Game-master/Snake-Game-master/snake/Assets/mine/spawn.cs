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
    
    int previousScore = 0;
    GameControl gc;
    bool isRefreshed = true;
    
	// Use this for initialization
	void Start () {
		Spawn(foodPrefab);
        GameObject GameC = GameObject.Find("GameControl");
		gc = GameC.GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
        int score = gc.getScore();
        //if score not cange, refresh is true
        if(score == previousScore){
            isRefreshed =true;
        }else{
            isRefreshed =false;
            //update score
            previousScore = score;
        }
        
        if(isRefreshed==false){
            if(score%5 ==0){
                refresh();
            }
        }
        //spawn food
        GameObject[] manyFood = GameObject.FindGameObjectsWithTag("food");
		if(manyFood.Length<10){
            Spawn(foodPrefab);
        }
        //spawn power
        GameObject[] manyPower = GameObject.FindGameObjectsWithTag("power");
		if(manyPower.Length<3){
            Spawn(powerPrefab);
        }
	}
    
    void refresh(){
        //destroy  all food and object
        GameObject[] manyFood = GameObject.FindGameObjectsWithTag("food");
        foreach(GameObject food in manyFood){
            Destroy(food);
        }
        GameObject[] manyPower = GameObject.FindGameObjectsWithTag("power");
        foreach(GameObject power in manyPower){
            Destroy(power);
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
