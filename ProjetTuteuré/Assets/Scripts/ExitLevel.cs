using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ExitLevel : MonoBehaviour
{
    public GameObject winIndicator;
    private bool aFini = false;
    private Player player;
    private GameManager gameManager;
    private GameObject camera;
    bool fini = false;

    //Win
    public AudioClip winClip;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        camera = GameObject.Find("Main Camera");
    }

    private void FixedUpdate()
    {
        if (aFini)
        {
            gameManager.genereMap();
            gameManager.initieNiveau(true);
            player.transform.position = new Vector3(85.5f,-59f,0.0f);
            player.canOpenMenus = true;
            camera.transform.position = new Vector3(85.5f, -59, -7);
            aFini = false;
            fini = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !fini)
        {
            player.canOpenMenus = false;
            GameObject.Find("GameAudioManager").GetComponent<AudioSource>().Stop();
            StartCoroutine("ShowWin");
            StartCoroutine("PlayFinalAudio");
            fini = true;

        }
    }

    private IEnumerator ShowWin()
    {
        for(int i = 0; i < 4; i++)
        {
            
            winIndicator.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            winIndicator.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        
    }
    private IEnumerator PlayFinalAudio()
    {
        GameObject.Find("GameAudioManager").GetComponent<AudioSource>().PlayOneShot(winClip);
        yield return new WaitWhile(() => GameObject.Find("GameAudioManager").GetComponent<AudioSource>().isPlaying);
        aFini = true;
    }

    public void setPlayer(GameObject player)
    {
        this.player = player.GetComponent<Player>();
    }
}
