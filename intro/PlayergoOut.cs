using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class PlayergoOut : MonoBehaviour
{
   public GameObject player;
    public GameObject npc;

    private Vector3 target;

    public TalkText tt;
    public GameManager manager;
    public npcChange nC;

    private bool isMoving = false;

    [SerializeField] private float moveSpeed = 2f;

    void Start()
    {
        target = new Vector3(750f, 88f, transform.position.z);

        manager = FindObjectOfType<GameManager>();
        nC = FindObjectOfType<npcChange>();
    }

    void Update()
    {
        if (isMoving) return;

        if (tt != null && tt.clickCount == 7)
        {
            StartCoroutine(Scene66(true));
        }
    }

    public IEnumerator Scene66(bool scene6)
    {
        if (!scene6) yield break;

        isMoving = true;

        if (nC != null)
            nC.ChangeImage();

        while (Vector3.Distance(player.transform.position, target) > 0.05f)
        {
            if (player != null)
            {
                player.transform.position = Vector3.MoveTowards(
                    player.transform.position,
                    target,
                    moveSpeed * Time.deltaTime
                );
            }

            if (npc != null)
            {
                npc.transform.position = Vector3.MoveTowards(
                    npc.transform.position,
                    target,
                    moveSpeed * Time.deltaTime
                );
            }

            yield return null;
        }

        if (player != null)
            player.transform.position = target;

        if (npc != null)
            npc.transform.position = target;

        LoadSceneManager.LoadScene("stage1");
    }
}
