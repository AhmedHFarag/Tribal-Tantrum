using UnityEngine;
using System.Collections;

public class MovingBackground : MonoBehaviour {


    public float speed = 0.1f;
    Vector3 initPosition;
    float width;
	void Start () {
        initPosition = gameObject.transform.position;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	void Update () {

        float xAxis = Input.GetAxis("P1Hori");

        gameObject.transform.position -= new Vector3(speed * xAxis, 0, 0);


        if (gameObject.transform.position.x <= initPosition.x - width + Camera.main.transform.position.x)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + width, initPosition.y, initPosition.z);

        if (gameObject.transform.position.x >= initPosition.x + width  + Camera.main.transform.position.x)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - width, initPosition.y, initPosition.z);
        
	}
}
