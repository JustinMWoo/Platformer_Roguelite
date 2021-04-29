using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    public static GameManager Current { get { return _current; } }

    // Singleton
    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _current = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    public bool bossAlive;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "ShopScene")
        {
            // Teleport player to the shop camera view and disable moving and attacking 
            player.transform.position = Vector3.zero;
            player.transform.rotation = Quaternion.identity;
            player.GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            bossAlive = true;
            LoadLevel(scene);
            player.GetComponent<PlayerMovement>().enabled = true;
            player.transform.position = new Vector3(0, 2.5f, 0);
            
        }
    }


    public void LoseGame()
    {
        // Call from player when hp<=0
        // Open death card with stats (create prefab)
    }

    public void NextLevel()
    {
        if (!bossAlive)
        {
            // Remove forces from the player and reset the parameters for the animator
            // TODO: Move this to a reset function on the player?
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            player.GetComponent<Animator>().SetFloat("Speed", 0);
            player.GetComponent<PlayerMovement>().enabled = false;

            // Play animations for beating level (maybe show level stats)
            // Load between level scene
            SceneManager.LoadScene("ShopScene");
        }
    }

    // Spawn enemies and items
    private void LoadLevel(Scene scene)
    {

    }

    // Temp function to load first level
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1_1");
    }
}
