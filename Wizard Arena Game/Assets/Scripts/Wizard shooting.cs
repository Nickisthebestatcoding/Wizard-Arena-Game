using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject lightningPrefab;
    public GameObject iceBulletPrefab;
    public GameObject tornadoPrefab;

    public Transform firePoint;
    public Transform lightningSpawnPoint;
    public float fireForce = 10f;

    public float fireballCooldown = 0.5f;
    public float lightningCooldown = 1.5f;
    public float iceBulletCooldown = 1f;
    public float tornadoCooldown = 2f;

    private float nextFireballTime = 0f;
    private float nextLightningTime = 0f;
    private float nextIceBulletTime = 0f;
    private float nextTornadoTime = 0f;

    private enum SpellType { Fireball = 0, IceBullet = 2, Tornado = 3, Lightning = 4 }
    private SpellType currentSpell = SpellType.Fireball;

    private ShopManagerScript shopManager;
    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;

    private float spellSwitchCooldown = 1f;
    private float nextSpellSwitchTime = 0f;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManagerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Switch spells if cooldown is up
        if (Time.time >= nextSpellSwitchTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) currentSpell = SpellType.Fireball;
            if (Input.GetKeyDown(KeyCode.Alpha2) && IsSpellUnlocked(SpellType.IceBullet)) currentSpell = SpellType.IceBullet;
            if (Input.GetKeyDown(KeyCode.Alpha3) && IsSpellUnlocked(SpellType.Tornado)) currentSpell = SpellType.Tornado;
            if (Input.GetKeyDown(KeyCode.Alpha4) && IsSpellUnlocked(SpellType.Lightning)) currentSpell = SpellType.Lightning;
        }

        // Handle spell casting
        if (Input.GetButtonDown("Fire1"))
        {
            if (!IsSpellUnlocked(currentSpell)) return;

            switch (currentSpell)
            {
                case SpellType.Fireball:
                    if (Time.time >= nextFireballTime)
                    {
                        CastFireball();
                        nextFireballTime = Time.time + fireballCooldown;
                    }
                    break;
                case SpellType.Lightning:
                    if (Time.time >= nextLightningTime)
                    {
                        CastLightning();
                        nextLightningTime = Time.time + lightningCooldown;
                    }
                    break;
                case SpellType.IceBullet:
                    if (Time.time >= nextIceBulletTime)
                    {
                        CastIceBullet();
                        nextIceBulletTime = Time.time + iceBulletCooldown;
                    }
                    break;
                case SpellType.Tornado:
                    if (Time.time >= nextTornadoTime)
                    {
                        CastTornado();
                        nextTornadoTime = Time.time + tornadoCooldown;
                    }
                    break;
            }
        }

        // Healing with health flask when pressing Q
        if (Input.GetKeyDown(KeyCode.Q) && shopManager.healthFlaskCount > 0)
        {
            Heal();
        }
    }

    void Heal()
    {
        // Heal the player here (you can add health restoration logic)
        Debug.Log("Healed!");
        shopManager.healthFlaskCount--;
    }

    bool IsSpellUnlocked(SpellType spell)
    {
        if (spell == SpellType.Fireball) return true; // Fireball is always unlocked
        return shopManager.spellsUnlocked[(int)spell]; // Check if the spell is unlocked
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

    void CastTornado()
    {
        GameObject tornado = Instantiate(tornadoPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }
}
