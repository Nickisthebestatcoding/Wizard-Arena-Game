using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;

    public Transform firePoint;
    public Transform lightningSpawnPoint;

    public float fireForce = 10f;

    public float fireballCooldown = 0.5f;
    public float lightningCooldown = 1.5f;

    private float nextFireballTime = 0f;
    private float nextLightningTime = 0f;

    private enum SpellType { Fireball, Lightning }
    private SpellType currentSpell = SpellType.Fireball;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSpell = SpellType.Lightning;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSpell = SpellType.Fireball;

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentSpell == SpellType.Fireball && Time.time >= nextFireballTime)
            {
                CastFireball();
                nextFireballTime = Time.time + fireballCooldown;
            }
            else if (currentSpell == SpellType.Lightning && Time.time >= nextLightningTime)
            {
                CastLightning();
                nextLightningTime = Time.time + lightningCooldown;
            }
        }
    }

    void CastFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }

    void CastLightning()
    {
        Instantiate(lightningPrefab, lightningSpawnPoint.position, lightningSpawnPoint.rotation, lightningSpawnPoint);
    }
}
