using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuCanvas : MonoBehaviour {

    private Canvas _canvas;
     
	void Awake () {
       _canvas = GetComponent<Canvas>();
       _canvas.worldCamera = Camera.main;
    }
	
	
	void Update ()
    {        
    
    }
}
