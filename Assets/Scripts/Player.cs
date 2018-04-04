using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public Camera cam;

	private MazeCell currentCell;

	private MazeDirection currentDirection;

    public bool isDeity = false;

    private Rigidbody playerObject;

    public float speed;

    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (isDeity)
        {
            this.transform.position = new Vector3(0, 15, -20);
            this.transform.Rotate(40, 0, 0);
        }
        else
        {
            this.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void SetLocation (MazeCell cell) {
		if (currentCell != null) {
			currentCell.OnPlayerExited();
		}
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
		currentCell.OnPlayerEntered();
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
		if (edge is MazePassage) {
			SetLocation(edge.otherCell);
		}
	}

	private void Look (MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
		currentDirection = direction;
	}

    private void FixedUpdate()
    {
        /*
        if (!isLocalPlayer)
        {
            return;
        }

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal * speed * Time.deltaTime, 0.0f, moveVertical * speed * Time.deltaTime);

        //playerObject.MovePosition(transform.position + movement);

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        */
    }

    private void Update () {
        
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
        
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDeity = true;
            transform.Rotate(20, 0, 0);
            this.tag = "Deity";
        }
        */


        Quaternion playerRotation = cam.transform.rotation;
        playerRotation.x = 0;
        playerRotation.z = 0;      

        this.transform.rotation = playerRotation;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //move = transform.TransformDirection(move);
        _controller.Move(move * Time.deltaTime * speed);
        //transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f, 0);
        /*
        if(move!=Vector3.zero)
        {
            transform.forward = move;
        }
        */

        if (isDeity)
        {
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject coin in coins)
            {
                coin.GetComponent<Renderer>().enabled = false;
            }
        }


        /*
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			Move(currentDirection);
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(currentDirection.GetNextClockwise());
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			Move(currentDirection.GetOpposite());
		}
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.Q)) {
			Look(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.E)) {
			Look(currentDirection.GetNextClockwise());
		}
        */
    }
}