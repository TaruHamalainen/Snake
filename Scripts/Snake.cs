using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    public Color colorGreen;
    public Color colorYellow;
    public Color colorRed;
    public Color colorBlue;
    public string currentcolor;
    private SpriteRenderer spriteRenderer;


    public GameObject snakePartPrefab;
    private List<GameObject> snakeParts;

    private Vector2 direction = Vector2.right;

    private void Start()
    {
        snakeParts = new List<GameObject>();
        snakeParts.Add(this.gameObject);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        SetRandomColorToSnakeHeadAndParts();
    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void Update()
    {
        HandleInputs();
    }
    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector2.right;
    }
    private void ApplyMovement()
    {

        for (int i = snakeParts.Count - 1; i > 0; i--)
        {
            snakeParts[i].transform.position = snakeParts[i - 1].transform.position;
        }
        transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y, 0);
    }
    private void SetRandomColorToSnakeHeadAndParts()
    {
        int index = Random.Range(0, 4);

        switch (index)
        {
            case 0:
                currentcolor = "Green";
                spriteRenderer.color = colorGreen;
                snakePartPrefab.GetComponentInChildren<SpriteRenderer>().color = colorGreen;
                break;
            case 1:
                currentcolor = "Yellow";
                spriteRenderer.color = colorYellow;
                snakePartPrefab.GetComponentInChildren<SpriteRenderer>().color = colorYellow;
                break;
            case 2:
                currentcolor = "Red";
                spriteRenderer.color = colorRed;
                snakePartPrefab.GetComponentInChildren<SpriteRenderer>().color = colorRed;
                break;
            case 3:
                currentcolor = "Blue";
                spriteRenderer.color = colorBlue;
                snakePartPrefab.GetComponentInChildren<SpriteRenderer>().color = colorBlue;
                break;

        }
    }
    private void AddPartsToSnake()
    {
        GameObject newPart = Instantiate(snakePartPrefab);
        newPart.transform.position = snakeParts[snakeParts.Count - 1].transform.position;
        snakeParts.Add(newPart);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            AddPartsToSnake();
            SetRandomColorToSnakeHeadAndParts();
            GameManager.instance.SpawnAppleToRandomPosition();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.RestartLevel();
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            GameManager.instance.RestartLevel();
        }

    }
}
