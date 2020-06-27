using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    #region Variables
    public GameObject ground;
    Material groundMat;

    GameObject obstaclePrefab;
    public GameObject[] obsPrefab;
    int obsSelector = 0;

    public Transform spawnPoint1, spawnPoint2, spawnPoint3;
    Transform spawnPoint;
    int spawnPointSelector = 1;

    public Transform belt;
    public float maxBeltSpeed = -0.3f;
    float beltSpeed;

    float curOffset = 0;

    public float worldScrollSpeed = 1f;
    public float groundScrollSpeed = 30f;

    public static WorldManager instance;

    //public static int lives = 3;

    //[SerializeField]
    //GameObject setLivesText;
    //public static GameObject livesText;
    
    //public GameObject overText;

    //public GameObject restart;
    #endregion

    #region Serialization
    //public void OnAfterDeserialize()
    //{
    //    setLivesText = livesText;
    //}

    //public void OnBeforeSerialize()
    //{
    //    setLivesText = livesText;
    //}
    #endregion
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        //setLivesText = livesText;
    }

    private void Start()
    {
        groundMat = ground.GetComponent<MeshRenderer>().material;
        InvokeRepeating("ObstacleGeneration", 0.5f, 2f);
     }

    private void Update()
    {
        worldScrollSpeed += 0.1f;
        beltSpeed = (worldScrollSpeed / 100);
        beltSpeed = Mathf.Clamp(beltSpeed, 0, maxBeltSpeed);
        curOffset += (-groundScrollSpeed / 1000);
        groundMat.mainTextureOffset = new Vector2(0, curOffset);
        belt.Translate(transform.forward * beltSpeed);

        //OnBeforeSerialize();
        //OnAfterDeserialize();
        //livesText.GetComponent<Text>().text = "Lives: " + lives;
    }

    void ObstacleGeneration()
    {
        spawnPointSelector = Random.Range(1, 3);
        if(spawnPointSelector == 1)
        {
            spawnPoint = spawnPoint1;
        }
        else if (spawnPointSelector == 2)
        {
            spawnPoint = spawnPoint2;
        }
        else
        {
            spawnPoint = spawnPoint3;
        }

        obsSelector = Random.Range(0, obsPrefab.Length);
        obstaclePrefab = obsPrefab[obsSelector];
        if(obsSelector == 1)
        {
            obsSelector = Random.Range(0, 3);
            if(obsSelector == 1)
            {
                Instantiate(obstaclePrefab, spawnPoint.position, spawnPoint.rotation, belt);
            }
            else
            {
                Instantiate(obstaclePrefab, spawnPoint.position + Vector3.up, spawnPoint.rotation, belt);
            }
        }
        else
        {
            Instantiate(obstaclePrefab, spawnPoint.position, spawnPoint.rotation, belt);
        }
    }

    //public void TakeDamage()
    //{
    //    lives--;
    //    if (lives <= 0)
    //    {
    //        Destroy(gameObject);
    //        overText.SetActive(true);
    //        restart.SetActive(true);
    //        //restartGame();
    //        lives = 0;
    //        livesText.GetComponent<Text>().text = "Lives: " + lives;
    //    }
    //}

    //public void restartGame()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //        SceneManager.LoadScene("Level 1");
    //}
}