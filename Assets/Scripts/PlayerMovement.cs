using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    private Vector3 direction;
    public GameObject ps;
    private bool isDead = false;

    
    private int score = 0;

    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isDead)
        {
            if(direction==Vector3.forward)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.forward;
            }
            score+=1;
            ScoreText.text = "Score : "+score.ToString();
            
        }
        float amoutToMove = speed * Time.deltaTime;
        transform.Translate(direction * amoutToMove);
    }
    void OnTriggerEnter(Collider ob)
    {
        if(ob.gameObject.tag =="Pickup")
        {
            ob.gameObject.SetActive(false);
            Instantiate(ps, transform.position, Quaternion.identity);
            score += 3;
            ScoreText.text = "Score : " + score.ToString();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Tile")
        {
            RaycastHit hit;
            Ray downRay = new Ray(transform.position, -Vector3.up);
            if(!Physics.Raycast(downRay,out hit))
            {
                //Kill the player
                isDead = true;
                transform.GetChild(0).transform.SetParent(null);
                TileManager.Instance.Dead();
                Destroy(gameObject);
            }
        }
    }
}
