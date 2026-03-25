using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 11f;
    [SerializeField] float boostSpeed = 16f;

    SurfaceEffector2D surfaceEffector2D;
    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    ScoreManager scoreManager;

    Vector2 moveVector;
    bool canControlPlayer = true;
    float previousRotation;
    float totalRotation;
    int flipCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControlPlayer)
        {
            RotatePlayer();
            BoostPlayer();
            CalculateFlips();
        }
    }

    void RotatePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();

        if (moveVector.x < 0)
        {
            myRigidbody2D.AddTorque(torqueAmount);
        }
        else if (moveVector.x > 0)
        {
            myRigidbody2D.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {
        if (moveVector.y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);

        //allow the player a 20 degrees off
        if (totalRotation > 340 || totalRotation < -340)
        {
            flipCount += 1;
            totalRotation = 0;
            scoreManager.AddScore(100);
        }

        previousRotation = currentRotation;
    }

    public void DisableControls()
    {
        canControlPlayer = false;
    }

    public void ActivatePowerup(PowerupSO powerup)
    {
        if (powerup.GetPowerupType() == "speed")
        {
            baseSpeed += powerup.GetValueChange();
            boostSpeed += powerup.GetValueChange();
        }
        else if (powerup.GetPowerupType() == "torque")
        { 
            myRigidbody2D.AddTorque(powerup.GetValueChange());
        }
    }
}
