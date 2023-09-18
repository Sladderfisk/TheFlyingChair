using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float flyingSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool canDie;

    private Rigidbody playerRb;

    private bool dead;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) GameManager.Instance.RestartScene();
        if (Input.GetKeyDown(KeyCode.Escape)) GameManager.Instance.LoadMenu();
    }

    private void FixedUpdate()
    {
        if (dead) return;
        Rotate(InputManager.MoveInput.x * -1);
        FlyUp();
    }

    private void Rotate(float direction)
    {
        float newZRotation = playerRb.rotation.eulerAngles.z + direction * rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0, 0, newZRotation);
        playerRb.MoveRotation(rotation);
    }

    private void FlyUp()
    {
        if (!InputManager.JumpInput) return;
        
        Vector2 flyingForce = transform.up * flyingSpeed;
        playerRb.AddForce(flyingForce, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (dead || canDie) return;
        
        if (DeathCondition())
        {
            dead = true;
            Debug.Log("Dead");
            GameManager.Instance.Die(1.0f);
        }
    }

    private bool DeathCondition()
    {
        return WrongDirection();
    }

    private bool WrongDirection()
    {
        float zRotation = playerRb.rotation.eulerAngles.z;
        return zRotation is > 90 or < -90;
    }
}
