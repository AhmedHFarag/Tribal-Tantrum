using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public int count;
    public GameObject Game;
    public string Name;
    Vector3 oldPos;
    Vector3 outpos;
	// Use this for initialization
	void Start () {
        oldPos = this.transform.localPosition;
        outpos = new Vector3(0, 1000, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Name == "Triangle")
        {

            if (Game.GetComponent<GameManager>().Triangles.Count + 1 < count)
                this.gameObject.transform.localPosition = outpos;
            else
            {

                this.gameObject.transform.localPosition = oldPos;
            }
        }
        else
        {
            
            if (Game.GetComponent<GameManager>().Boxes.Count + 1 < count)
                this.gameObject.transform.localPosition = outpos;
            else
            {

                this.gameObject.transform.localPosition = oldPos;
            }
        }
    }
}
