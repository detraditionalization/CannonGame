using UnityEngine;
using System.Collections;

public class BallTrack : MonoBehaviour {

    
	
	void Start () {
	
	}
	
	
	void Update () {



        if (InputHandler.MouseLeftUp && !LevelManager.Instance.GamePaused) 
        {
            Destroy(gameObject);
        }
	}
}
