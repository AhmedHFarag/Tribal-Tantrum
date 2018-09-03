using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
    public GameObject Box;
    public GameObject Tri;
    public GameObject cam;
    public GameObject Explosion_AnimationBox;
    public GameObject Explosion_AnimationTri;
    public GameObject BigEgg;
    public float BigEggRotation = 0;
    public Stack Boxes = new Stack();
    public Stack Triangles = new Stack();
    public GameObject StartGameButton;
	public Text ModeText;
	public GameObject Darkness;
	public float ModeChangeDelay = 30f;
	public float ModeChangeDuration = 30f;
	[HideInInspector]
	public bool CanJump = true;
	public int Dir = 1;
	// Use this for initialization
	void Start()
    {
		if (Instance==null) {
			Instance = this;
		} else {
			Destroy(this);
		}
    }
	public void StartGame() {
		GameObject temp;
		//BigEggRotation = BigEgg.GetComponent<Transform>().eulerAngles.z;
		for (int i = 0; i < 3; i++) {

			temp = (GameObject)Instantiate(Box, new Vector2(i - 7, 0f), Quaternion.identity);
			temp.gameObject.active = true;
			Boxes.Push(temp);
			temp = (GameObject)Instantiate(Tri, new Vector2(7 - i, 0f), Quaternion.identity);
			temp.gameObject.active = true;
			Triangles.Push(temp);
		}
		ResetModes();

		WakeNewBox();
		WakeNewTriangle();
		StopAllCoroutines();
		StartGameButton.SetActive(false);
		StartCoroutine(ModeChange());
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
			ResetModes();

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
			ResetModes();
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
	void ResetModes() {
		Time.timeScale = 1;
		CanJump = true;
		Dir = 1;
		Darkness.SetActive(false);
	}
	IEnumerator ModeChange() {
		while (true) {
			yield return new WaitForSeconds(ModeChangeDelay);
			int r = Random.Range(0, 5);
			switch (r) {
				case 0:
					Time.timeScale = 2;
					ModeText.text = "Speed Up";
					Debug.Log("Speed up");
					break;
				case 1:
					Time.timeScale = 0.5f;
					ModeText.text = "Speed Down";
					Debug.Log("Slow down");
					break;
				case 2:
					//noJumping
					ModeText.text = "No Jump";
					CanJump = false;
					break;
				case 3:
					//Reverse controls
					ModeText.text = "Reverse";
					Dir = -1;
					break;
				case 4:
					ModeText.text = "Darkness";
					Darkness.SetActive(true);
					break;
				default:
					break;
			}
			ModeText.gameObject.GetComponent<Animator>().SetTrigger("Change");
			yield return new WaitForSeconds(ModeChangeDuration*Time.timeScale);
			ResetModes();
		}
		yield return null;
	}
}
