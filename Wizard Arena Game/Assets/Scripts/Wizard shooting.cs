using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;
    public GameObject iceBulletPrefab;
    public GameObject tornadoPrefab;  // <-- Tornado prefab added

    public Transform firePoint;
    public Transform lightningSpawnPoint;

    public float fireForce = 10f;

    public float fireballCooldown = 0.5f;
    public float lightningCooldown = 1.5f;
    public float iceBulletCooldown = 1f;
    public float tornadoCooldown = 2f;  // <-- Tornado cooldown

    private float nextFireballTime = 0f;
    private float nextLightningTime = 0f;
    private float nextIceBulletTime = 0f;
    private float nextTornadoTime = 0f;  // <-- Tornado cooldown timer

    private enum SpellType { Fireball, Lightning, IceBullet, Tornado }  // <-- Added Tornado
    private SpellType currentSpell = SpellType.Fireball;

    void Update()
    {
        // Spell switching logic
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSpell = SpellType.Lightning;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSpell = SpellType.Fireball;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentSpell = SpellType.IceBullet;

        if (Input.GetKeyDown(KeyCode.Alpha4))  // <-- Press 4 for Tornado
            currentSpell = SpellType.Tornado;

        // Casting logic
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
            else if (currentSpell == SpellType.IceBullet && Time.time >= nextIceBulletTime)
            {
                CastIceBullet();
                nextIceBulletTime = Time.time + iceBulletCooldown;
            }
            else if (currentSpell == SpellType.Tornado && Time.time >= nextTornadoTime)  // <-- Tornado logic
            {
                CastTornado();
                nextTornadoTime = Time.time + tornadoCooldown;
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

    void CastIceBullet()
    {
        GameObject iceBullet = Instantiate(iceBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = iceBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }

    void CastTornado()  // <-- Tornado Cast Method
    {
        GameObject tornado = Instantiate(tornadoPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }
}
