using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement: MonoBehaviour
{
   private Rigidbody2D playerRd;
    private SpriteRenderer characterSprite;
    private Animator charAni;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;

    [Header("State")]
    public bool isGround;
    public bool canDoubleJump;
    private int jumpCount;

    public bool movable = true;
    public bool isBeat = false;

    [Header("External")]
    public SoundEffect_Manager soundEffect;
    public STAGE_Maintain stageNow;

    private string sceneName;

    void Awake()
    {
        playerRd = GetComponent<Rigidbody2D>();
        characterSprite = GetComponent<SpriteRenderer>();
        charAni = GetComponent<Animator>();

        jumpCount = 1;
        canDoubleJump = false;
    }

    void Start()
    {
        soundEffect = FindObjectOfType<SoundEffect_Manager>();
        stageNow = FindObjectOfType<STAGE_Maintain>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneName = scene.name;
    }

    void Update()
    {
        if (!movable) return;

        if (Inventory.invectoryActivated) return;

        Move();
        Jump();
    }

    private void Move()
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (input == 0) return;

        charAni.SetTrigger("RUN");

        transform.localScale = new Vector3(input > 0 ? 1 : -1, 1, 1);

        playerRd.velocity = new Vector2(input * speed, playerRd.velocity.y);
    }

    private void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.LeftAlt)) return;

        if (!isGround && jumpCount <= 0) return;

        charAni.SetTrigger("JUMP");
        soundEffect?.Effect_Sound("JUMP");

        playerRd.velocity = new Vector2(playerRd.velocity.x, jumpPower);

        jumpCount--;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("ground")) return;

        isGround = true;
        jumpCount = canDoubleJump ? 2 : 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            soundEffect?.Effect_Sound("ITEMGET");
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Gun"))
        {
            soundEffect?.Effect_Sound("ITEMGET");
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("DoubleJump"))
        {
            soundEffect?.Effect_Sound("ITEMGET");
            collision.gameObject.SetActive(false);
            canDoubleJump = true;
        }
    }

    public IEnumerator OnBeatTime(float beatTime)
    {
        isBeat = true;
        soundEffect?.Effect_Sound("DAMAGE");

        for (int i = 0; i < beatTime * 3; i++)
        {
            characterSprite.color = (i % 2 == 0)
                ? new Color32(255, 120, 160, 90)
                : new Color32(255, 120, 160, 180);

            yield return new WaitForSeconds(0.3f);
        }

        characterSprite.color = Color.white;
        isBeat = false;
    }

    public void DIE()
    {
        if (stageNow != null)
            stageNow.sceneNow = sceneName;

        SceneManager.LoadScene("GameOver");
    }
}
