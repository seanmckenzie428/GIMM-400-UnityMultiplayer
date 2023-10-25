using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Bike_Sphere : MonoBehaviour
{
    private Rigidbody sphereRB;
    
    public int lapsCompleted = 0;

    public Material player1Mat, player2Mat;
    public Transform spawn1, spawn2;
    public MeshRenderer frontWheel, backWheel;

    [NonSerialized]
    public Transform[] spawnPoints;
    [NonSerialized]
    public Transform[] respawnPoints;
    
    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;

    private Vector2 move;
    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;
    
    private float normalDrag;
    public float modifiedDrag;
    
    public float alignToGroundTime;

    private PlayerInput _playerInput;
    private Transform _mesh;
    private Text winnerText;
    
    void Start()
    {
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();
        winnerText.text = "";
        
        _mesh = transform.GetChild(0);
        _playerInput = GetComponent<PlayerInput>();
        // Spawn(_playerInput.playerIndex);
        // Get Sphere Rigidbody
        sphereRB = GetComponentInChildren<Rigidbody>();
        // Detach Sphere from car
        sphereRB.transform.parent = null;

        normalDrag = sphereRB.drag;
        
        // Set Player Material
        if (_playerInput.playerIndex == 0)
        {
            frontWheel.material = player1Mat;
            backWheel.material = player1Mat;
        }
        else
        {
            frontWheel.material = player2Mat;
            backWheel.material = player2Mat;
        }
        Invoke(nameof(Spawn), 0.1f);
    }

    private void Spawn()
    {
        sphereRB.position = spawnPoints[_playerInput.playerIndex].position;
    }

    private void Respawn(int respawnPointIndex)
    {
        transform.position = respawnPoints[respawnPointIndex].position;
    }

    public void OnMove(InputValue inputValue)
    {
        print(inputValue.Get<Vector2>());
        move = inputValue.Get<Vector2>();
    }
    
    void Update()
    {
        // Get Input
        moveInput = move.y;
        turnInput = move.x;
        

        // Calculate Turning Rotation
        float newRot = turnInput * turnSpeed * Time.deltaTime * moveInput;
        
        if (isCarGrounded)
            transform.Rotate(0, newRot, 0, Space.World);

        // Set Cars Position to Our Sphere
        transform.position = sphereRB.transform.position;

        // Raycast to the ground and get normal to align car with it.
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);
        
        // Rotate Car to align with ground
        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);
        
        // Tilt the Bike
        Vector3 tilt = _mesh.rotation.eulerAngles;
        tilt.x = turnInput * 20f;
        _mesh.rotation = Quaternion.Euler(tilt);
        
        // Calculate Movement Direction
        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;
        
        // Calculate Drag
        sphereRB.drag = isCarGrounded ? normalDrag : modifiedDrag;
    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration); // Add Movement
        else
            sphereRB.AddForce(transform.up * -200f); // Add Gravity
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Finish"))
        {
            lapsCompleted++;
        }

        if (lapsCompleted >= 4)
        {
            winnerText.text = "Player " + (_playerInput.playerIndex + 1) + " Wins!";
        }
    }
}