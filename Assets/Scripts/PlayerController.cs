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
    private float highScore = 0f;
    private Label scoreText;
    private Label HighScoredText;
    public GameObject explosionEffect;
    private Button RestartButton;
    private Button HighScoreReset;
    private float currentScore = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        HighScoredText = uiDocument.rootVisualElement.Q<Label>("HighScoreLabel");
        RestartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        HighScoreReset = uiDocument.rootVisualElement.Q<Button>("HighScoreReset");
        RestartButton.style.display = DisplayStyle.None;

        HighScoreReset.style.display = DisplayStyle.None;
        scoreText.text = "Score: " + score;
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        HighScoredText.text = "High Score: " + highScore;
        RestartButton.clicked += ReloadScene;
        HighScoreReset.clicked += DeleteHighScore;
    }

    void Update()
    {
        score = Mathf.FloorToInt(elapsedTime * scoreMultiply);
        currentScore = score;
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
            PlayerPrefs.SetFloat("HighScore", score);

        }
    }
    void CheckHighScore()
    {
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            if(currentScore > PlayerPrefs.GetFloat("SavedHighScore"))
            {
                PlayerPrefs.SetFloat("SavedHighScore", currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("SavedHighScore", currentScore);
        }
        HighScoredText.text = currentScore.ToString();
        HighScoredText.text = PlayerPrefs.GetFloat("SavedHighScore").ToString();


    }
    void UpdateHighScore()
    {
       HighScoredText.text = $"High Score: {PlayerPrefs.GetFloat("SavedHighScore", 0f)}";
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        RestartButton.style.display = DisplayStyle.Flex;
        HighScoreReset.style.display = DisplayStyle.Flex;
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void DeleteHighScore()
    {
        PlayerPrefs.DeleteKey("SavedHighScore");
        UpdateHighScore();
    }
}