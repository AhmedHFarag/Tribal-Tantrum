using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject Box;
    public GameObject Tri;
    public GameObject cam;
    public GameObject Explosion_AnimationBox;
    public GameObject Explosion_AnimationTri;
    public GameObject BigEgg;
    public float BigEggRotation = 0;
    public Stack Boxes = new Stack();
    public Stack Triangles = new Stack();
     
    // Use this for initialization
    void Start()
    {
        GameObject temp;
        //BigEggRotation = BigEgg.GetComponent<Transform>().eulerAngles.z;
        for (int i = 0; i < 3; i++)
        {
            
            temp = (GameObject)Instantiate(Box, new Vector2(i - 7, 0f), Quaternion.identity);
            temp.gameObject.active = true;
            Boxes.Push(temp);
            temp = (GameObject)Instantiate(Tri, new Vector2(7 - i, 0f), Quaternion.identity);
            temp.gameObject.active = true;
            Triangles.Push(temp);
        }
        WakeNewBox();
        WakeNewTriangle();
    }
    // Update is called once per frame
    void Update()
    {
        float speed = 1.5f;
        float smooth = 1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed);
        BigEgg.GetComponent<Transform>().eulerAngles = Vector3.Lerp(BigEgg.GetComponent<Transform>().eulerAngles, new Vector3(0,0, BigEggRotation), smooth);
        if (Boxes.Count < Triangles.Count) BigEggRotation = 270;
        else if (Boxes.Count == Triangles.Count) BigEggRotation = 0;
        else BigEggRotation = 90;

    }
    public void WakeNewBox()
    {

        try {
            Box = (GameObject)Boxes.Pop();
        }catch(System.Exception e)
        {
            Debug.Log("Triangles Winnnnnnnnnnnnnnnn");
            SceneManager.LoadScene("Tri_Win");
            //cam.active = false;
            //Application.Quit();
        }

            //cam.GetComponent<CameraFollow>().PlayerOne = Box.transform;
            cam.GetComponent<CameraFollow>().AddPlayer(Box.transform, Tri.transform);
            Box.GetComponent<PlatFormController>().Status = PlatFormController._Status.Fight;
    }
    public void WakeNewTriangle()
    {
        try {
            Tri = (GameObject)Triangles.Pop();
        }catch(System.Exception e)
        {
            Debug.Log("Cube Winnnnnnnnnnn");
            SceneManager.LoadScene("Box_Win");
           // cam.active = false;
            //Application.Quit();
        }
            //cam.GetComponent<CameraFollow>().PlayerTwo = Tri.transform;
            cam.GetComponent<CameraFollow>().AddPlayer(Box.transform, Tri.transform);
            Tri.GetComponent<PlatFormController>().Status = PlatFormController._Status.Fight;

    }
    public void AddTriAngle(Vector3 Position)
    {
        GameObject Exp;
        Exp= (GameObject)Instantiate(Explosion_AnimationBox, new Vector3(Position.x, Position.y + 1, Position.z), Quaternion.identity);
        Exp.gameObject.active = true;
        GameObject temp;
        temp = (GameObject)Instantiate(Tri, new Vector3(Position.x,Position.y+1,Position.z), Quaternion.identity);
        temp.GetComponent<PlatFormController>().Status = PlatFormController._Status.Idel;
        Triangles.Push(temp);

    }
    public void AddBox(Vector3 Position)
    {
        GameObject Exp;
        Exp = (GameObject)Instantiate(Explosion_AnimationTri, new Vector3(Position.x, Position.y + 1, Position.z), Quaternion.identity);
        Exp.gameObject.active = true;
        GameObject temp;
        temp = (GameObject)Instantiate(Box, new Vector3(Position.x, Position.y + 1, Position.z), Quaternion.identity);
        temp.GetComponent<PlatFormController>().Status = PlatFormController._Status.Idel;
        Boxes.Push(temp);
    }
}
