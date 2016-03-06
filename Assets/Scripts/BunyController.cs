using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunyController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    private BoxCollider2D myBunnyCollider;
    private float bunnyhurtTime=-1;
    private float startTime;
    private int jumpsleft = 2;

    public Text scoreText;
    public float bunnyJunmpForce = 700f;
    public AudioSource jumpSFx;
    public AudioSource deathSFx;

    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myBunnyCollider = GetComponent<BoxCollider2D>();

        startTime = Time.time;
    }

// Update is called once per frame
void Update()
    {
        if (bunnyhurtTime == -1)
        { 
            if (Input.GetButtonUp("Jump") && jumpsleft > 0)
            {
                if(myRigidBody.velocity.y <0)
                {
                    myRigidBody.velocity = Vector2.zero ;
                }

                if (jumpsleft == 1)
                {
                    myRigidBody.AddForce(transform.up * bunnyJunmpForce * 0.7f);
                }
                else
                {
                    myRigidBody.AddForce(transform.up * bunnyJunmpForce);
                }
                jumpsleft--;
                jumpSFx.Play();
            }
            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y);
            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else
        {
            if (Time.time > bunnyhurtTime + 2)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
	}

    //Collision detection with cactus
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (MoveLeft moveleLter in FindObjectsOfType<MoveLeft>())
            {
                moveleLter.enabled = false;
            }
            
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }  

            bunnyhurtTime = Time.time;
            myAnim.SetBool("bunnyHurt", true);
            myRigidBody.velocity = Vector3.zero;
            myRigidBody.AddForce(transform.up * bunnyJunmpForce);
            myBunnyCollider.enabled = false;         
            deathSFx.Play();
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsleft = 2;
        }
    }
}
