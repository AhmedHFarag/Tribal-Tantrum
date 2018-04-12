using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    //public Transform PlayerFollow;
    public Transform PlayerOne;
    public Transform PlayerTwo;
    public Camera cam;
    public float ScaleFactor = 1;
    // Use this for initialization
    void Start()
    {

    }
    public void AddPlayer(Transform Player1, Transform Player2)
    {
        if (Player1 != null && Player2 != null)
        {
            PlayerOne = Player1;
            PlayerTwo = Player2;
        }
    }
    public void AddPlayerOne(Transform Player1)
    {
        PlayerOne = Player1;
    }
    public void AddPlayerTwo(Transform Player2)
    {
        PlayerTwo = Player2;
    }

    Vector3 velocity;
    float Distance;
    float Size;
    // Update is called once per frame
    void Update()
    {
        try {
            Distance = Vector2.Distance(PlayerOne.localPosition, PlayerTwo.localPosition);
            Size = Distance * ScaleFactor;
            //transform.localPosition=new Vector2 ((Distance/2)+Mathf.Min(PlayerOne.localPosition.x, PlayerTwo.localPosition.x),transform.localPosition.y);
            if (Distance > 10 && Size < 10)
            {
                cam.orthographicSize = Size;
                // this.GetComponent<BoxCollider2D>().size.x = Size * 4f;

            }
        }catch(System.Exception e)
        {

        }
    }
    void LateUpdate()
    {
        try {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3((Distance / 2) + Mathf.Min(PlayerOne.localPosition.x, PlayerTwo.localPosition.x), transform.localPosition.y, transform.position.z), ref velocity, Time.deltaTime * 10);
        }catch(System.Exception e)
        {

        }
    }
}
