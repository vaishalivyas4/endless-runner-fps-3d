using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public static int score = 0;
    static GameObject scoreText;

    public static Bullet instance;

    private void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreManager");
    }
    private void Update()
    {
        scoreText.GetComponent<Text>().text = "Score: " + score;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Obstacle")
        {
            score++;
            scoreText.GetComponent<Text>().text = "Score: " + score;
            Destroy(col.gameObject);
        }
    }
}
