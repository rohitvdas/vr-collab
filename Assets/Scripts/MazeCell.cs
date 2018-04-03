using UnityEngine;
using UnityEngine.Networking;

public class MazeCell : NetworkBehaviour
{
    public bool enablePathTracing = false;

	public IntVector2 coordinates;

	public MazeRoom room;

	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

	private int initializedEdgeCount;

    public Material redMaterial;

    public GameObject floor;

	public bool IsFullyInitialized {
		get {
			return initializedEdgeCount == MazeDirections.Count;
		}
	}

	public MazeDirection RandomUninitializedDirection {
		get {
			int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
			for (int i = 0; i < MazeDirections.Count; i++) {
				if (edges[i] == null) {
					if (skips == 0) {
						return (MazeDirection)i;
					}
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
		}
	}

	public void Initialize (MazeRoom room) {
		room.Add(this);
		transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
	}

    public void CreateCoin(Coin coinPrefab, float coinProbability)
    {
        Coin coin = Random.value < coinProbability ? coinPrefab : null;
        if (coin==null)
        {
            return;
        }

        Coin newCoin = Instantiate(coin) as Coin;
        newCoin.transform.parent = this.transform;
        newCoin.transform.localPosition = Vector3.zero;
    }

	public MazeCellEdge GetEdge (MazeDirection direction) {
		return edges[(int)direction];
	}

	public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
		edges[(int)direction] = edge;
		initializedEdgeCount += 1;
	}

	public void Show () {
		gameObject.SetActive(true);
	}

	public void Hide () {
		gameObject.SetActive(false);
	}

	public void OnPlayerEntered () {
		//room.Show();
		for (int i = 0; i < edges.Length; i++) {
			edges[i].OnPlayerEntered();
		}
	}
	
	public void OnPlayerExited () {
		//room.Hide();
		for (int i = 0; i < edges.Length; i++) {
			edges[i].OnPlayerExited();
		}
	}

    private void Update()
    {

        /*
        Player[] players = FindObjectsOfType<Player>();
        Player navigator = null;
        foreach (Player play in players)
        {
            if (play.tag=="Player")
            {
                navigator = play;
            }
        }

        if (navigator == null) return;

        Vector3 playerPos = navigator.transform.position;
        IntVector2 cellPos = this.coordinates;

        double minX, minZ, maxX, maxZ;
        minX = cellPos.x - 0.5;
        maxX = cellPos.x + 0.5;
        minZ = cellPos.z - 0.5;
        maxZ = cellPos.z + 0.5;

        if (playerPos.x <= maxX && playerPos.z <= maxZ && playerPos.x >= minX && playerPos.z >= minZ)
        {
            this.GetComponent<MeshRenderer>().material = redMaterial;
        }
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if (!enablePathTracing) return;

        Debug.Log("collision occurred");
        if (other.CompareTag("Player"))
        {
            floor.GetComponent<MeshRenderer>().material = redMaterial;
        }
    }
}