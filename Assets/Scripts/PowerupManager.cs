using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] PowerupSO powerup;
    PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layerIndex)
        {
            //Activate the powerup
            player.ActivatePowerup(powerup);
        }
    }
}
