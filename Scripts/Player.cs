using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // player variables
    [SerializeField]    // allows you to edit private variables at runtime in the Unity Game Engine
    private int _speed = 5;
    [SerializeField]
    private GameObject _laserPrefab;
    private Vector3 _direction = Vector3.zero;

    // firing variables
    [SerializeField]
    private float _spawnLaserOffset = 0.8f;
    private float _canFire = -1f;
    public float fireCoolDown = 0.2f;

    // world bound variables
    private float _x_bounds = 11.3f; // the player will wrap around; therefore only need one
    private float _y_bounds = -3.8f; // the positive bounds for y-axis is 0

    // Start is called before the first frame update
    void Start()
    {
        // set the starting position of the player to (0,0,0)
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // player movement
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) // check if there is input and the cool down has ended
        {
            FireLaser(fireCoolDown);
        }

    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // horizontal movement
        float verticalInput = Input.GetAxis("Vertical");     // vertical movement

        _direction = new Vector3(horizontalInput, verticalInput, 0);

        // Time.deltaTime refers to real time; If you are observing a real fast update in the game...
        // ... chances are that the action requires Time.deltaTime to be multiplied with it.
        transform.Translate(_direction * _speed * Time.deltaTime);

        // limit the player to the bounds of the world
        LimitPlayerToWorldBounds();
    }

    void LimitPlayerToWorldBounds()
    {
        // limit player in the y-axis (till the center of the screen because enemies will come from the top of the screen)
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _y_bounds, 0), 0);

        // wrap the player around the x-axis (teleport the player to the opposite side if the player goes out of bounds)
        if (transform.position.x > _x_bounds)
        {
            transform.position = new Vector3(-_x_bounds, transform.position.y, 0);
        }
        else if (transform.position.x < -_x_bounds)
        {
            transform.position = new Vector3(_x_bounds, transform.position.y, 0);
        }
    }

    void FireLaser(float coolDown)
    {
        // implement the cool down time
        _canFire = Time.time + coolDown;

        // Quaternion refers to the rotation of the gameObject and ".identity", in the simplest of terms, refers to the default rotation
        Instantiate(_laserPrefab, transform.position + new Vector3(0, _spawnLaserOffset, 0), Quaternion.identity);
    }

}
