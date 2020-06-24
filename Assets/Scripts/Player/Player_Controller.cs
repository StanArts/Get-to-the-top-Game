using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public KeyCode left, right, jump, shoot;

    public float speed;
    public float jumpForce;

    private Rigidbody2D playerRigidbody;

    private bool isGrounded;
    public Transform groundChecker;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    public GameObject playerEyes;

    public GameObject theBullet;
    public Transform attackPoint;

    public float shootingRate;
    public bool canShoot;

    public int maxAmmo = 5;
    public int currentAmmo;
    public float reloadTime;
    private bool isReloading = false;

    private float nextTimeToShoot = 0f;

    public float knockBackForce = 15f;
    private float knockBackCounter;

    public bool invincible;
    public float invincibilityLength;
    private float invicibilityCounter;

    public Vector2 respawnPosition;

    void Start()
    {
        extraJumps = extraJumpsValue;
        playerRigidbody = GetComponent<Rigidbody2D>();

        canShoot = true;

        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (knockBackCounter <= 0)
        {
            if (Input.GetKeyDown(jump) && extraJumps > 0)
            {
                playerRigidbody.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }

            else if (Input.GetKeyDown(jump) && extraJumps == 0 && isGrounded)
            {
                playerRigidbody.velocity = Vector2.up * jumpForce;
            }

            if (canShoot)
            {
                if (isReloading)
                {
                    return;
                }

                if (currentAmmo <= 0)
                {
                    StartCoroutine(Reload());
                    return;
                }

                if (Input.GetKeyDown(shoot) && Time.time >= nextTimeToShoot)
                {
                    nextTimeToShoot = Time.time + 1f / shootingRate;

                    Shoot();
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, checkRadius, whatIsGround);

        if (knockBackCounter <= 0)
        {
            if (Input.GetKey(left))
            {
                playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
                transform.localScale = new Vector2(-1f, 1f);
            }
            else if (Input.GetKey(right))
            {
                playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
                transform.localScale = new Vector2(1f, 1f);
            }
            else
            {
                playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            }

            invincible = false;
        }

        else if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            playerRigidbody.velocity = new Vector2(transform.localScale.x, 0.5f).normalized * knockBackForce;//new Vector2(-1.0f, 1.0f).normalized * knockBackForce;
            
        }

        if (invicibilityCounter > 0)
        {
            invicibilityCounter -= Time.deltaTime;
        }

        if (invicibilityCounter <= 0)
        {
            invincible = false;
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

    public void Shoot()
    {
        currentAmmo--;

        GameObject bulletClone = (GameObject)Instantiate(theBullet, attackPoint.position, attackPoint.rotation);
        bulletClone.transform.localScale = transform.localScale / 2;
        playerEyes.GetComponent<Animator>().SetTrigger("areTriggered");
    }

    public void KnockBack()
    {
        knockBackCounter = 0.1f;
        invicibilityCounter = invincibilityLength;
        invincible = true;
    }
}
