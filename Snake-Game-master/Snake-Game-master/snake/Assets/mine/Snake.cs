using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    Vector2 direction;
    Vector2 space;
    float speed =0.1f;
    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();
    public GameObject tailPrefab;
	//public GameObject gameC;
    bool ate = false;
    bool isAlive = true;
    
    public Transform borderTop;
    public Transform borderRight;
    public Transform borderLeft;
    public Transform borderBottom;
	
	GameControl gc;

    
    enum Direction {up, down, left, right};
    Direction currentDirection;
    
	// Use this for initialization
	void Start () {
        //currentDirection = Direction.right;
        direction = new Vector2(0.1f, 0f);
        currentDirection = Direction.right;
		InvokeRepeating("Move", 0.3f, 0.3f);
		GameObject GameC = GameObject.Find("GameControl");
		gc = GameC.GetComponent<GameControl>();
		
	}
	
	// Update is called once per frame
	void Update () {
        //Move();
        
        //if currentDirection is down , snake cannot up
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && currentDirection!=Direction.down){
            direction = new Vector2(0f, speed);
            currentDirection = Direction.up;
        }
        
        //if currentDirection is right , snake cannot left
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && currentDirection!=Direction.right){
            direction = new Vector2(-speed, 0f);
            currentDirection = Direction.left;
        }
        
        //if currentDirection is up , snake cannot down
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && currentDirection!=Direction.up){
            direction = new Vector2(0f, -speed);
            currentDirection = Direction.down;
        }
        
        //if currentDirection is left , snake cannot right
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && currentDirection!=Direction.left){
            direction = new Vector2(speed, 0f);
            currentDirection = Direction.right;
        }
        
		
	}
    
    void Move(){
        
        Vector2 currentpos = transform.position;
        transform.Translate(direction);
        
        //if eat food
        if(ate){
            print("ate");
            //create tailprefab
            GameObject grow =(GameObject)Instantiate(tailPrefab,currentpos,Quaternion.identity);
            //add prefab into list
            tail.Insert(0, grow.transform);
            
            ate=false;
        }
        
        //move tail
        if(tail.Count>0){
            print("add tail");
            tail[tail.Count-1].position = currentpos;
            tail.Insert(0, tail[tail.Count-1]);
            tail.RemoveAt(tail.Count-1);
        }
    }
    void Shrink(){
        space = new Vector2(speed, 0f);
        borderLeft.transform.Translate(space);
        space = new Vector2(-speed, 0f);
        borderRight.transform.Translate(space);
        space = new Vector2(0f, speed);
        borderBottom.transform.Translate(space);
        space = new Vector2(0f, -speed);
        borderTop.transform.Translate(space);
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        
        //if touch food, delete food
        if(other.name.Equals("food(Clone)")){
            ate =true;
            Destroy(other.gameObject);
			gc.AddScore();
        }else if(other.name.Equals("power(Clone)")){
            ate =true;
            Destroy(other.gameObject);
            Shrink();
			gc.AddScore();
        }else if(other.tag == "border"){

            //check life
            isAlive = gc.LifeText();
            if(isAlive==false){
                Destroy(gameObject);
                gc.GameOverText();
                GameObject[] bodies = GameObject.FindGameObjectsWithTag("snakebody");
                foreach(GameObject body in bodies){
                    Destroy(body);
                }
            }else{
                //destroy body
                GameObject[] bodies = GameObject.FindGameObjectsWithTag("snakebody");
                foreach(GameObject body in bodies){
                    Destroy(body);
                }
                tail.Clear();
                
                //head at center
                gameObject.transform.position = new Vector2(0f,0f);
            }
			
        }
        
        
        
    }
}
