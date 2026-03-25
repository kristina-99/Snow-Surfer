using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] PowerupSO powerup;

    PlayerController player;
    SpriteRenderer spriteRenderer;
    float timeLeft;



    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = powerup.GetTime();
    }

    private void Update()
    {
        CountDownTimer();
    }

    void CountDownTimer()
    {
        if (spriteRenderer.enabled == false)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    player.DeactivatePowerup(powerup);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layerIndex && spriteRenderer.enabled)
        {
            //Activate the powerup
            spriteRenderer.enabled = false;
            player.ActivatePowerup(powerup);
        }
    }
}
