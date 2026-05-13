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

    void Start()
    {
        
        target.Set(750, 88, transform.position.z);


        manager = GameObject.FindObjectOfType<GameManager>();

        nC = GameObject.FindObjectOfType<npcChange>();
        


    }


    void Update()
    {
        if (tt != null && tt.clickCount == 7)
        {

        StartCoroutine(Scene66(true));
        }
    }
    public IEnumerator Scene66(bool scene6)
    {
        nC.ChangeImage();
        do
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 0.1f);
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, target, 0.1f);
            yield return new WaitForSeconds(0.01f);

        } while ((player.transform.position != target));
        player.transform.position = target;
        npc.transform.position = target;

        LoadSceneManager.LoadScene("stage1");

    }
}
