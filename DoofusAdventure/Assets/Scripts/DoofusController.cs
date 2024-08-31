using UnityEngine;
using UnityEngine.SceneManagement;

public class DoofusController : MonoBehaviour
{
    public float speed = 9f;
    public float smoothTime = 0.1f;
    public float raycastDistance = 1.1f;
    public LayerMask pulpitLayer;
    public float fallThreshold = 90; // Y position below which the game ends

    private Rigidbody rb;
    private Vector3 currentVelocity = Vector3.zero;
    private bool isGameOver = false;

    public float initialHeight = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on DoofusController GameObject!");
        }
    }
    public void DoofusStartGame()
    {
        transform.position = new Vector3(0, initialHeight + 1f, 0);

        isGameOver = false;
    }

    public void FixedUpdate()
    {

        if (isGameOver || rb == null) return;
       
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 targetVelocity = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * speed;

        Vector3 smoothVelocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
        rb.velocity = new Vector3(smoothVelocity.x, rb.velocity.y, smoothVelocity.z);

        if (!IsOnPulpit())
        {
            rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
        }

        // Check if Doofus has fallen below the threshold
        if (transform.position.y < fallThreshold)
        {
            GameOver();
        }
    }

    bool IsOnPulpit()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, pulpitLayer);
    }

    void GameOver()
    {
        isGameOver = true;
        GameManager.Instance.GameOver();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnNewPulpitReached()
    {
        ScoreManager.Instance.IncrementPulpitCount();
    }
}