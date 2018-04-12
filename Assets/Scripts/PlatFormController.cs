using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]
public class PlatFormController : MonoBehaviour
{
    Rigidbody2D Player;
    public float Speed = 5;
    float elapsedTime = 0;
    public int id = 1;
    public bool grounded = true;
    public enum _Status { Idel, Fight };
    public _Status Status;
    public bool attack=false;
    public float jump;
    public LayerMask GroundLayers;
    public Transform GroundChecker;
    public AudioSource SwordSound;
    SpriteRenderer spRenderer;
    float shift = 0.0f;
    // Use this for initialization
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        spRenderer = GetComponentInChildren<SpriteRenderer>();
        if (this.tag == "Triangle") shift = Camera.main.orthographicSize + 3 + Random.Range(-1f, 3f);
        else shift = -Camera.main.orthographicSize - 3 - Random.Range(-1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Status == _Status.Fight)
        {

            float xAxis = Input.GetAxis(string.Format("P{0}Hori", id));
            //Debug.Log(xAxis);
            if (Player.transform.position.x > (Camera.main.transform.position.x + Camera.main.orthographicSize+1))
                Player.transform.position = new Vector2((Camera.main.transform.position.x + Camera.main.orthographicSize+1), Player.transform.position.y);

            if (Player.transform.position.x < (Camera.main.transform.position.x - Camera.main.orthographicSize-1))
                Player.transform.position = new Vector2((Camera.main.transform.position.x - Camera.main.orthographicSize-1), Player.transform.position.y);

            Player.velocity = new Vector2(xAxis * Speed, Player.velocity.y);
            
            Player.GetComponent<Animator>().SetFloat("X_Speed", Mathf.Abs(Player.velocity.x));
            //jump = Input.GetAxis(string.Format("P{0}Jump", id));
            if (xAxis > 0)
            {
                Player.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
            }
            if (xAxis < -0.1)
            {
                Player.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            }
            //if (xAxis < 0 && !spRenderer.flipX) spRenderer.flipX = true;
            //if (xAxis > 0 && spRenderer.flipX) spRenderer.flipX = false;
            grounded = Physics2D.OverlapCircle(GroundChecker.position, 0.2f, GroundLayers);
            if ((jump > 0.5 && (grounded)))
            {

                elapsedTime += Time.deltaTime;
                //   Player.velocity = new Vector2(Player.velocity.x, 10);
                //cube.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);         
            }
            if ((Input.GetKeyDown(KeyCode.Joystick1Button3)||Input.GetButtonDown("Fire1")) && id == 1)
            {
                // Debug.Log("firiing");
                SwordSound.Play();
                Player.GetComponent<Animator>().SetTrigger("Attack");
                
            }
            else
            if ((Input.GetKeyDown(KeyCode.Joystick2Button3) || Input.GetButtonDown("Fire2")) && id == 2)
            {
                // Debug.Log("firiing2");
                SwordSound.Play();
                Player.GetComponent<Animator>().SetTrigger("Attack");
               
            }
            if ((Input.GetKeyDown(KeyCode.Joystick1Button2)|| Input.GetButtonDown("Jump1")) && id == 1 && grounded)
            {
                Player.velocity = new Vector2(Player.velocity.x, 10);
               // Player.GetComponent<Animator>().SetTrigger("Jump");
                //Debug.Log("jumping");
            }
            if ((Input.GetKeyDown(KeyCode.Joystick2Button2) || Input.GetButtonDown("Jump2")) && id == 2 && grounded)
            {
                Player.velocity = new Vector2(Player.velocity.x, 10);
                Player.GetComponent<Animator>().SetTrigger("Jump");
                //Debug.Log("jumping2");
            }
        }
        else
        {
            float speed = 2.5f;
            float smooth = 1.0f - Mathf.Pow(0.5f, Time.deltaTime * speed);
            
            
            transform.position = Vector3.Lerp(transform.position, new Vector3(Camera.main.transform.position.x+shift, Camera.main.transform.position.y, 0), smooth);

        }
    }
    public void SetAttackTrue()
    {
        attack = true;
    }
    public void SetAttackFalse()
    {
        attack = false;
    }
    //public void  explode()
    //{
    //    var all_rigidbodies = FindObjectsOfType(Rigidbody2D);

    //    for (var r : Rigidbody2D in all_rigidbodies)
    //    {
    //        if (Vector2.Distance(r.transform.position, transform.position) < 6 && r.tag != "Player")
    //        {
    //            var px : float = r.transform.position.x - transform.position.x;
    //            var py : float = r.transform.position.y - transform.position.y;

    //            r.AddForce(Vector2(px, py).normalized * EXPLOSION_FORCE / Vector2.Distance(r.transform.position, transform.position));
    //        }
    //    }
    }
