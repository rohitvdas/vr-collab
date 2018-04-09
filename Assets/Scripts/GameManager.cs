using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class GameManager : NetworkBehaviour {

	public Maze mazePrefab;

	public Player playerPrefab;

	public Maze mazeInstance;

	private Player playerInstance;

    public int coinsCollected;

    public Text coinText;

    public float countdownVal;

    public Text countdownText;

    public AudioClip coin;

    public Canvas textCanvas;

    AudioSource aScorce;

	private void Start () {
		BeginGame();
        aScorce = this.GetComponent<AudioSource>();
        //coinText = textCanvas.transform.GetChild(0).GetComponent<Text>();
        //countdownText = textCanvas.transform.GetChild(1).GetComponent<Text>();
	}
	
	private void Update () {
        /*
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
        */
    }

    public IEnumerator StartCountdown()
    {
        while (countdownVal>0)
        {
            yield return new WaitForSeconds(1.0f);
            countdownVal--;
            this.countdownText.text = "Seconds Remaining: " + this.countdownVal.ToString();
        }
    }

    //[ClientRpc]
    private void BeginGame () {
		//Camera.main.clearFlags = CameraClearFlags.Skybox;
		//Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.Generate();
        NetworkServer.Spawn(mazeInstance.gameObject);
        //playerInstance = Instantiate(playerPrefab) as Player;
        //playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        //Camera.main.clearFlags = CameraClearFlags.Depth;
		//Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        this.coinsCollected = 0;
        this.coinText.text = "Coins Collected: " + this.coinsCollected.ToString();
        this.countdownText.text = "Seconds Remaining: " + this.countdownVal.ToString();
        StartCoroutine(StartCountdown());
    }

    private void RestartGame () {
		//StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
		BeginGame();
	}

    public void coinCollected()
    {
        aScorce.clip = coin;
        aScorce.Play();
        this.coinsCollected++;
        this.coinText.text = "Coins Collected: " + this.coinsCollected.ToString();
    }

    /*
    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), this.coinsCollected.ToString());
    }
    */
}