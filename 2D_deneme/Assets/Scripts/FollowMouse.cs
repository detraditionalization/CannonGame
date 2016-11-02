using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {
    public float Zvalue = -5;
    private Vector3 _mousePos;

    void Awake () {
        
	}
	
	
	void Update () {
        _mousePos = Input.mousePosition;
        var vector = Camera.main.ScreenToWorldPoint(_mousePos);
        vector.z = Zvalue;
        transform.position = vector;
        
    }
}
