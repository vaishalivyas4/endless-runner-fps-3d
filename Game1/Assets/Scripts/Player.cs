using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    public float moveSpeed = 2f;

    Rigidbody rb;

    public float xLimit = 3.009f;

    public Transform cam;

    float curRotX = 0;
    Vector3 curCamRotation;
    public float maxRotAngle = 30f;
    public float rotSpeed = 5f;

    public float jumpForce = 300f;
    bool canJump = true;

    public Transform nozzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 800f;
    GameObject bullet;
    public float bulletExpTime = 2f;

    public GameObject reloadText;

    public int ammo = 10;
    public GameObject ammoText;

    public float rayRange = 400f;
    RaycastHit hit;

    public int lives = 3;
    public GameObject livesText;

    public GameObject overText;
    public GameObject restart;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        #region Movement
        hMovement();

        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 curPos = transform.position;
        curPos.x = Mathf.Clamp(curPos.x, -xLimit, xLimit);
        transform.position = curPos;
        #endregion

        #region Camera Rotation
        curRotX += (mouseY * rotSpeed);
        curRotX = Mathf.Clamp(curRotX, -maxRotAngle, maxRotAngle);
        curCamRotation = cam.rotation.eulerAngles;
        curCamRotation.x = -curRotX;
        cam.rotation = Quaternion.Euler(curCamRotation);
        cam.transform.Rotate(Vector3.right * mouseY * rotSpeed);
        #endregion

        #region Instantiate Bullet
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammo <= 0)
                resetAmmo();
        }

        //if (Input.GetButtonDown("Fire1") && ammo > 0)
        //{
        //    //FireRay();
        //    bullet = Instantiate(bulletPrefab, nozzle.position, nozzle.rotation);
        //    bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed);
        //    Destroy(bullet, bulletExpTime);

        //    if (ammo >= 1)
        //    {
        //        ammo--;
        //        ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
        //    }

        //    if (ammo <= 0 && !reloadText.activeInHierarchy)
        //    {
        //        reloadText.SetActive(true);
        //    }

        //}
        //ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
        #endregion

        #region Raycast Fire
        //void FireRay()
        //{
        //    Ray Start = new Ray(Camera.main.transform.position, Camera.main.transform.forward);


        //    if (Physics.Raycast(Start, out hit, rayRange))
        //    {
        //        if (hit.collider.tag == "Obstacle")
        //        {
        //            hit.collider.GetComponent<Rigidbody>().AddForce(Vector3.back * 3000f);
        //            hit.collider.GetComponent<Rigidbody>().AddForce(Vector3.up * 500f);
        //            Bullet.score++;
        //            Destroy(hit.collider.gameObject, 0.5f);
        //        }
        //    }
        //    Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * rayRange, Color.yellow, 1f);

        //}
        #endregion

        #region RayCast Mouse Fire
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {
                hitInfo.transform.gameObject.SetActive(false);
            }

            if (ammo >= 1)
            {
                ammo--;
                ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
            }

            if (ammo <= 0 && !reloadText.activeInHierarchy)
            {
                reloadText.SetActive(true);
            }
        }
        ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
        #endregion

        #region Jump
        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
            //canJump = false;
        }
        #endregion

        livesText.GetComponent<Text>().text = "Lives: " + lives;
    }

    public void hMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-3, 0, 0);            
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(3, 0, 0);
        }
    }

    void resetAmmo()
    {
        ammo = 10;
        ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
        reloadText.SetActive(false);
    }

    public void TakeDamage()
    {
        lives--;
        if (lives <= 0)
        {
            if (gameObject.CompareTag("WorldManager"))
                Destroy(gameObject);
            overText.SetActive(true);
            restart.SetActive(true);
            lives = 0;
            livesText.GetComponent<Text>().text = "Lives: " + lives;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Obstacle")
        {
            TakeDamage();
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Ground")
        {
            canJump = true;
        }

        if(col.gameObject.tag == "PowerUp")
        {
            ammo *= 2;
        }

        if(col.gameObject.tag == "Life")
        {
            lives++;
            livesText.GetComponent<Text>().text = "Lives " + lives;
        }
    }
}

