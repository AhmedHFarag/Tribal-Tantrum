using UnityEngine;
using System.Collections;

public class Attack_Sword : MonoBehaviour {

    public string EnemyTag;
    public GameObject GameManagerr;
    void OnTriggerEnter2D(Collider2D Obj)
    {
        //Debug.Log(Obj.gameObject.tag);
        if (this.gameObject.transform.root.gameObject.active == true)

            if (Obj.gameObject.tag == EnemyTag && 
                Obj.gameObject.transform.root.gameObject.GetComponent<PlatFormController>().Status==PlatFormController._Status.Fight &&
                this.gameObject.transform.root.gameObject.GetComponent<PlatFormController>().attack==true)
            {
                
                if (EnemyTag == "Box")
                {
                    GameManagerr.GetComponent<GameManager>().WakeNewBox();
                    GameManagerr.GetComponent<GameManager>().AddTriAngle(Obj.transform.position);
                }
                else
                {
                    GameManagerr.GetComponent<GameManager>().WakeNewTriangle();
                    GameManagerr.GetComponent<GameManager>().AddBox(Obj.transform.position);
                }
                Obj.gameObject.transform.root.gameObject.active = false;
                //transform.parent.parent.parent
                //Destroy(Obj.gameObject);
                Destroy(Obj.gameObject.transform.root.gameObject);
            }
    }
}
