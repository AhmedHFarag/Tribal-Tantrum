using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public void End_Explosion()
    {
        Destroy(this.gameObject);
    }
}
