using UnityEngine;
using System.Collections;

public class InGameCanvas : MonoBehaviour {

	private Canvas _canvas;

	void Awake () {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
	}
	
	
	void Update () {
	
	}
}
