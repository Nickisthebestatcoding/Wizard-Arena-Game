using UnityEngine;
using System.Collections;

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

    private float spellSwitchCooldown = 1f;     // Cooldown duration
    private float nextSpellSwitchTime = 0f;     // Time until next switch is allowed

    void Start()
    {
        shopManager = FindObjectOfType<ShopManagerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Handle spell switching
        if (Time.time >= nextSpellSwitchTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && IsSpellUnlocked(SpellType.Fireball))
                SwitchSpell(SpellType.Fireball);
            if (Input.GetKeyDown(KeyCode.Alpha2) && IsSpellUnlocked(SpellType.IceBullet))
                SwitchSpell(SpellType.IceBullet);
            if (Input.GetKeyDown(KeyCode.Alpha3) && IsSpellUnlocked(SpellType.Tornado))
                SwitchSpell(SpellType.Tornado);
            if (Input.GetKeyDown(KeyCode.Alpha4) && IsSpellUnlocked(SpellType.Lightning))
                SwitchSpell(SpellType.Lightning);
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

        // Heal the wizard when pressing Q, if health flasks are available
        if (Input.GetKeyDown(KeyCode.Q) && shopManager.healthFlaskCount > 0)
        {
            Heal();
        }
    }

    // Heal the wizard and decrease flask count
    void Heal()
    {
        // Heal the wizard (replace this with your health restoration logic)
        Debug.Log("Healed!");

        // Decrease health flask count
        shopManager.healthFlaskCount--;
    }

    // Switch spell and trigger flash color
    void SwitchSpell(SpellType newSpell)
    {
        currentSpell = newSpell;
        nextSpellSwitchTime = Time.time + spellSwitchCooldown;

        Color flashColor = Color.white;
        switch (newSpell)
        {
            case SpellType.Fireball:
                flashColor = Color.red;
                break;
            case SpellType.IceBullet:
                flashColor = Color.blue;
                break;
            case SpellType.Tornado:
                flashColor = Color.white;
                break;
            case SpellType.Lightning:
                flashColor = new Color(0.5f, 0f, 0.5f); // Purple
                break;
        }

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashColor(flashColor, 0.5f));
    }

    // Coroutine to flash color when switching spells
    IEnumerator FlashColor(Color flashColor, float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }

    // Check if the spell is unlocked
    bool IsSpellUnlocked(SpellType spell)
    {
        if (spell == SpellType.Fireball) return true; // Fireball is always unlocked
        return shopManager.spellsUnlocked[(int)spell];
    }

    // Cast fireball spell
    void CastFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }

    // Cast lightning spell
    void CastLightning()
    {
        Instantiate(lightningPrefab, lightningSpawnPoint.position, lightningSpawnPoint.rotation, lightningSpawnPoint);
    }

    // Cast ice bullet spell
    void CastIceBullet()
    {
        GameObject iceBullet = Instantiate(iceBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = iceBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }

    // Cast tornado spell
    void CastTornado()
    {
        GameObject tornado = Instantiate(tornadoPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = tornado.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = firePoint.up * fireForce;
    }
}
