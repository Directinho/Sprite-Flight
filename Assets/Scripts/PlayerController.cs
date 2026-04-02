using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    public float maxSpeed = 5f;
    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMultiply = 1f;
    Rigidbody2D rb;
    public UIDocument uiDocument;
    private Label scoreText;
    public GameObject explosionEffect;
    private Button RestartButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        RestartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        RestartButton.style.display = DisplayStyle.None;
        scoreText.text = "Score: " + score;
        RestartButton.clicked += ReloadScene;
    }

    void Update()
    {
        score = Mathf.FloorToInt(elapsedTime * scoreMultiply);
        elapsedTime += Time.deltaTime;
        Debug.Log("Score: " + score);
        scoreText.text = "Score: " + score;
        if (Mouse.current.leftButton.isPressed)
        {
            // Calcula a direção do mouse em relação ao jogador
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;

            // Movimentacao do jogador
            transform.up = direction;
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
            rb.AddForce(direction * thrustForce);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        RestartButton.style.display = DisplayStyle.Flex;
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}