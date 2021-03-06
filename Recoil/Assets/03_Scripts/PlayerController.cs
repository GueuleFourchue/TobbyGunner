﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public Rigidbody2D rb;

    public Transform gunHolder;
    public Transform charaSprite;
    public Transform gunSprite;
    public Animator gunAnimator;
    public SpriteRenderer fireSprite;
    public Image fireOverlayEffect;
    public ParticleSystem fireParticle;
    public UIAnimations UIAnims;
    public Animator animator;
    public GameManager gameManager;
    public ChunksManager chunksManager;
    public GameObject dottedLine;
    public CanvasGroup invuVignette;

    [Header("CameraEffects")]
    public Colorful.SmartSaturation saturationEffect;
    public UnityStandardAssets.ImageEffects.MotionBlur motionBlur;
    public UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration vignette;
    public Colorful.ContrastVignette constrastVignette;
    public UnityStandardAssets.ImageEffects.BlurOptimized blur;

    [Header("Shoot")]
    public GameObject bulletPrefab;
    public Transform bulletsHolder;
    public Transform bulletsInstancePosition;
    public float bulletSpeed;
    public int bulletShootNumber;
    public int superBulletShootNumber;
    public float shootRecoil;

    [Header("Super Shoot")]
    public float slomoTimeToHold;
    public float slomoValue;
    public float slomoLerp;
    public float slomoCooldown;
    public float slomoDuration;
    public float superShootRecoil;
    public float supershootRecoilDamage;
    public float invulnerabilityDuration;
    public float superInvulnerabilityDuration;
    public UI_Shield UiShield;

    [Header("Bounds")]
    public float boundXPosition;

    //[HideInInspector]
    public List<Transform> shootTargets;

    Vector2 shootPosition;
    Vector2 shootDirection;
    Vector3 charaScale;
    Vector3 gunScale;


    bool shoot;
    bool superShoot;
    public bool invulnerability;
    public bool superInvulnerability;
    bool charaInvertedSprite;
    bool gunInvertedSprite;

    bool isSlomo;
    float slomoCooldownTimer;
    float slomoTimer;
    bool canSlomo = true;
    float SlomoDurationTimer;

    float invulnerabilityTimer;
    float cameraSize;

    SoundsManager soundsManager;
    public AudioMixer audioMixer;

    void Start()
    {
        gunScale = gunSprite.localScale;
        charaScale = charaSprite.localScale;
        soundsManager = GameObject.FindObjectOfType<SoundsManager>();
        cameraSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -boundXPosition)
        {
            transform.position = new Vector3(-boundXPosition, transform.position.y, transform.position.z);
        }
        if (transform.position.x > boundXPosition)
        {
            transform.position = new Vector3(boundXPosition, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        slomoCooldownTimer += Time.deltaTime;
        if (slomoCooldownTimer > slomoCooldown && !canSlomo)
        {
            canSlomo = true;
            //UIAnims.SlomoIconAnimOn();
        }

        //Slomo
        if (Input.GetMouseButton(0) && canSlomo)
        {
            slomoTimer += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            slomoTimer = 0;
            if (isSlomo)
            {
                EndSlowMotion();
                SuperShoot();
            }
        }
        if (slomoTimer > slomoTimeToHold && canSlomo)
        {
            SlowMotion();
            SlomoDurationTimer += Time.deltaTime;
        }
        if (SlomoDurationTimer > slomoDuration)
        {
            EndSlowMotion();
            SuperShoot();
        }

        if (invulnerability && !superInvulnerability)
        {
            invulnerabilityTimer += Time.deltaTime;
            if (invulnerabilityTimer > invulnerabilityDuration)
            {
                invulnerabilityTimer = 0;
                invulnerability = false;

                //SpriteInvuColor
                SpriteRenderer charaSpriteRenderer = charaSprite.GetComponent<SpriteRenderer>();
                charaSpriteRenderer.DOColor(new Color(1,1,1,1), 0.1f);
            }
        }

        if (superInvulnerability)
        {
            invulnerabilityTimer += Time.deltaTime;
            if (invulnerabilityTimer > superInvulnerabilityDuration)
            {
                invulnerabilityTimer = 0;
                superInvulnerability = false;

                //SpriteInvuColor
                SpriteRenderer charaSpriteRenderer = charaSprite.GetComponent<SpriteRenderer>();
                charaSpriteRenderer.DOColor(new Color(1, 1, 1, 1), 0.1f);
                
            }
        }
    }

    void FixedUpdate()
    {
        if (shoot)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(shootDirection * shootRecoil, ForceMode2D.Impulse);
            shoot = false;
        }
        if (superShoot)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(shootDirection * superShootRecoil, ForceMode2D.Impulse);
            superShoot = false;
        }
    }

    void Shoot()
    {
        shootPosition = camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        soundsManager.PlaySound("Shoot");

        if (shootTargets.Count != 0)
        {
            rb.velocity = Vector2.zero;

            //ChooseTarget
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;

            foreach (Transform t in shootTargets)
            {
                Vector3 directionToTarget = t.position - transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = t;
                }
            }

            shootDirection = -(new Vector2(bestTarget.position.x, bestTarget.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        }
        else
        {
            //shootDirection = -(shootPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
            if (shootPosition.x >= 0)
            {
                shootDirection = new Vector2(-0.5f, 1);
            }
            else
            {
                shootDirection = new Vector2(0.5f, 1);
            }
            shoot = true;
        }

        gunAnimator.Play("Shoot");

        CharaSpriteRotation();
        GunSpriteRotation();
        ShootAnim();
        InstantiateBullets(bulletShootNumber, false);
    }

    void SuperShoot()
    {
        shootPosition = camera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        shootDirection = -(shootPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        superShoot = true;

        gunAnimator.Play("Shoot");
        animator.Play("Supershoot");

        Invulnerability();
        

        CharaSpriteRotation();
        GunSpriteRotation();
        ShootAnim();
        InstantiateBullets(superBulletShootNumber, true);
    }

    void CharaSpriteRotation()
    {
        if (Input.mousePosition.x < Screen.width / 2 && !charaInvertedSprite)
        {
            charaInvertedSprite = true;
            charaSprite.localScale = new Vector3(-charaScale.x, charaScale.y, charaScale.z);
        }
        else if (Input.mousePosition.x > Screen.width / 2 && charaInvertedSprite)
        {
            charaInvertedSprite = false;
            charaSprite.localScale = new Vector3(charaScale.x, charaScale.y, charaScale.z);
        }
    }

    void GunSpriteRotation()
    {
        float CharaPositionX = camera.WorldToScreenPoint(transform.position).x;

        if (Input.mousePosition.x < Screen.width / 2 && !gunInvertedSprite)
        {
            gunInvertedSprite = true;
            gunSprite.localScale = new Vector3(gunScale.x, -gunScale.y, gunScale.z);
        }
        else if (Input.mousePosition.x > Screen.width / 2 && gunInvertedSprite)
        {
            gunInvertedSprite = false;
            gunSprite.localScale = new Vector3(gunScale.x, gunScale.y, gunScale.z);
        }

        float angle = Mathf.Atan2(-shootDirection.y, -shootDirection.x) * Mathf.Rad2Deg;
        gunHolder.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ShootAnim()
    {
        charaSprite.DOKill();
        charaSprite.DOScaleY(charaScale.y * 1.2f, 0.1f).OnComplete(() =>
        {
            charaSprite.DOScaleY(charaScale.y, 0.1f);
        });

        fireParticle.Play();

        fireSprite.DOKill();
        fireSprite.DOFade(1, 0.1f).OnComplete(() =>
        {
            fireSprite.DOFade(0, 0.1f);
        });

        fireOverlayEffect.DOKill();
        fireOverlayEffect.DOFade(0.01f, 0.05f).OnComplete(() =>
        {
            fireOverlayEffect.DOFade(0f, 0.05f);
        });
    }

    void InstantiateBullets(int bulletsCount, bool superShoot)
    {
        for (int i = 0; i < bulletsCount; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletsHolder);
            bullet.transform.position = bulletsInstancePosition.position;
            bullet.GetComponent<BulletBehaviour>().bulletSpeed = bulletSpeed;
            bullet.GetComponent<BulletBehaviour>().originDirection = -shootDirection;
            bullet.GetComponent<BulletBehaviour>().isSuperShoot = superShoot;
            bullet.GetComponent<BulletBehaviour>().Move();
        }
    }

    void SlowMotion()
    {
        if (!isSlomo)
        {
            isSlomo = true;
            audioMixer.DOKill();
            audioMixer.DOSetFloat("LowpassFrequency", 3000f, 0.15f);
            soundsManager.PlaySound("SlowMotion");
        }
        Time.timeScale = Mathf.Lerp(Time.timeScale, slomoValue, slomoLerp);
        Time.fixedDeltaTime = 0.02F * Time.timeScale;

        if (GetComponent<BoxCollider2D>().enabled == true)
            GetComponent<BoxCollider2D>().enabled = false;

        if (!dottedLine.activeSelf)
            dottedLine.SetActive(true);

        //DottedLineRotation
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dottedLine.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //CameraEffects
        if (saturationEffect.enabled == false)
            saturationEffect.enabled = true;
        saturationEffect.Boost = Mathf.Lerp(saturationEffect.Boost, 0.3f, slomoLerp);

        if (vignette.enabled == false)
            vignette.enabled = true;

        if (constrastVignette.enabled == false)
            constrastVignette.enabled = true;
        constrastVignette.Darkness = Mathf.Lerp(constrastVignette.Darkness, 30f, slomoLerp);

        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 14.5f, slomoLerp);
    }

    void EndSlowMotion()
    {
        audioMixer.DOKill();
        audioMixer.DOSetFloat("LowpassFrequency", 22000f, 0.2f);
        soundsManager.StopLastSound();
        soundsManager.PlaySound("SuperShoot"); 
        GetComponent<BoxCollider2D>().enabled = true;

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isSlomo = false;

        SlomoDurationTimer = 0;

        canSlomo = false;
        slomoCooldownTimer = 0;

        dottedLine.SetActive(false);

        //CameraEffects
        saturationEffect.Boost = 1;
        saturationEffect.enabled = false;
        //motionBlur.enabled = false;
        vignette.enabled = false;
        constrastVignette.enabled = false;
        camera.DOKill();
        camera.DOOrthoSize(cameraSize, 0.2f);
        //UIAnims.SlomoIconAnimOff();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Monster"))
        {
            if (invulnerability || superInvulnerability)
            {
                collision.transform.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * 50, ForceMode2D.Impulse);
                collision.transform.GetComponent<MonsterBehaviour>().TakesDamage(supershootRecoilDamage);

                camera.DOKill();
                camera.DOShakePosition(0.1f, 0.2f, 50, 95).OnComplete(() =>
                {
                    camera.transform.localPosition = Vector3.zero;
                });
            }
            else
            {
                Death();
            }
           
        }
        if (collision.transform.CompareTag("KillingBlock"))
        {
            if (invulnerability || superInvulnerability)
            {
                collision.transform.GetComponent<KillingBlock>().Destroy();

                camera.DOKill();
                camera.DOShakePosition(0.1f, 0.5f, 50, 95).OnComplete(() =>
                {
                    camera.transform.localPosition = Vector3.zero;
                });
                soundsManager.PlaySound("BlockBreak");
            }
            else
            {
                Death();
            }
        }
        if (collision.transform.CompareTag("CoinBlock"))
        {
            if (invulnerability || superInvulnerability)
            {
                collision.transform.GetComponent<CoinBlock>().Destroy();

                camera.DOKill();
                camera.DOShakePosition(0.1f, 0.5f, 50, 95).OnComplete(() =>
                {
                    camera.transform.localPosition = Vector3.zero;
                });
                soundsManager.PlaySound("BlockCoinsBreak");
            }
        }
        if (collision.transform.CompareTag("ShieldBlock"))
        {
            if (invulnerability || superInvulnerability)
            {
                collision.transform.GetComponent<ShieldBlock>().Destroy();

                camera.DOKill();
                camera.DOShakePosition(0.1f, 0.5f, 50, 95).OnComplete(() =>
                {
                    camera.transform.localPosition = Vector3.zero;
                });
                soundsManager.PlaySound("BlockInvuBreak");
                SuperInvulnerability();
            }
        }
    }

    public void Death()
    {
        chunksManager.SaveBestLevel();
        gameManager.SaveData();
        gameManager.CharacterDeath(this.transform, GetComponent<BoxCollider2D>(), rb, animator, shootRecoil);
        soundsManager.PlaySound("Death");
        //Effect
        blur.enabled = true;
        DOTween.To(() => blur.blurSize, x => blur.blurSize = x, 7,0.8f);
        this.enabled = false;
    }

    public void YellowColor()
    {
        //SpriteInvuColor
        SpriteRenderer charaSpriteRenderer = charaSprite.GetComponent<SpriteRenderer>();
        charaSpriteRenderer.color = Color.white;
        charaSpriteRenderer.DOKill();
        charaSpriteRenderer.DOColor(new Color(charaSpriteRenderer.color.r, charaSpriteRenderer.color.g, 0.3f, charaSpriteRenderer.color.a), 0.1f);
    }

    public void Invulnerability()
    {
        invulnerabilityTimer = 0;
        invulnerability = true;
        YellowColor();
        UiShield.DecreaseFillValue(invulnerabilityDuration);
       
        //Invu Vignette
        invuVignette.alpha = 1;
        invuVignette.DOKill();
        invuVignette.DOFade(0.5f, invulnerabilityDuration).OnComplete(() =>
        {
            invuVignette.DOFade(0f, 1);
        });
    }
    public void SuperInvulnerability()
    {
        invulnerabilityTimer = 0;
        superInvulnerability = true;
        YellowColor();
        UiShield.DecreaseFillValue(superInvulnerabilityDuration);
        
        //Invu Vignette
        invuVignette.alpha = 1;
        invuVignette.DOKill();
        invuVignette.DOFade(0.5f, superInvulnerabilityDuration).OnComplete(() =>
        {
            invuVignette.DOFade(0f, 1);
        });
    }
}
