using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class Canon : MonoBehaviour {


    public static Canon Instance { get; private set; }

    public float ChargeSpeed = 700f;
    public float MaxForce = 700f;
    public float MinForce = 100f;
    public GameObject ball;
        
    public bool CanShoot { get; protected set; }

    private SpriteRenderer _sprite;
    private Rigidbody2D _body;
    private GameObject _theBall;
    private float _shootingForce;


    void Awake()
    {
        Instance = this;
        CanShoot = false;
        _shootingForce = MinForce;
    }
	
    void OnDestroy ()
    {
        Instance = null;
    }

    void Update()
    {
        if (!LevelManager.Instance.GamePaused)
        {
            PointToMouse();
            Contract();


            // When the player can shoot a ball
            if (GameObject.FindGameObjectsWithTag("Balls").Length == 0 && LevelManager.Instance.IsPlaying)
            {
                CanShoot = true;                 // you cannot shoot when there's another ball in the game
            }

            // Charging of the force
            if (CanShoot && InputHandler.MouseLeft)
            {
                if (_shootingForce < MaxForce)
                {
                    _shootingForce += ChargeSpeed * Time.deltaTime;

                }
                else if (_shootingForce >= MaxForce)
                {
                    _shootingForce = MaxForce;
                }
            }

            // Shooting the ball
            if (CanShoot && InputHandler.MouseLeftUp)
            {
                ShootBall();
                CanShoot = false;
            }
        }
    }
    
    	
	void FixedUpdate()
    {


    }



    public void PointToMouse()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ShootBall()
    {
       
        Vector3 shootLocation = GetComponentsInChildren<Transform>()[1].position;        // get the nozzle point's position        
        _theBall = (GameObject)Instantiate(ball, shootLocation, Quaternion.identity);         
        _theBall.transform.position = shootLocation;                     

        _body = _theBall.GetComponent<Rigidbody2D>();
        Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        vector.Normalize();
        vector.x *= _shootingForce;
        vector.y *= _shootingForce;                                         
        _body.AddForce(vector, ForceMode2D.Impulse);                                    // add the approptiate force impulse

        _shootingForce = MinForce;    // reset the force
        
        
    }

    void Contract ()        // scales the sprite according to the current shooting power
    {
        transform.localScale = new Vector3(1f - (_shootingForce / MaxForce)*0.5f,1,1);
    }


}
