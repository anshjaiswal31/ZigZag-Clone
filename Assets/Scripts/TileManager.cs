using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilesPrefab;

    public GameObject CurrentTile;

    Stack<GameObject> topTiles=new Stack<GameObject>();
    Stack<GameObject> leftTiles = new Stack<GameObject>();


    private static TileManager instance;

    public static TileManager Instance 
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TileManager>();
            return instance;
        }
    }

    void Start()
    {
        CreateTiles(100);
        for (int i = 1; i <= 50; i++)
            SpawnTile();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateTiles(int amount)
    {
        for(int x=0;x<amount;x++)
        {
            leftTiles.Push(Instantiate(tilesPrefab[1]));
            topTiles.Push(Instantiate(tilesPrefab[0]));
            leftTiles.Peek().SetActive(false);
            topTiles.Peek().SetActive(false);

            topTiles.Peek().name = "TopTile";
            leftTiles.Peek().name = "LeftTile";
        }
    }
    public void SpawnTile()
    {
        if (leftTiles.Count == 0 || topTiles.Count == 0)
        {
            CreateTiles(10);
        }
        int index = Random.Range(0, 2);
        if(index==0)
        {
            //get top tile
            GameObject tmp = topTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = CurrentTile.transform.GetChild(0).GetChild(index).position;
            CurrentTile = tmp;
        }
        else
        {
            //get left tile
            GameObject tmp = leftTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = CurrentTile.transform.GetChild(0).GetChild(index).position;
            CurrentTile = tmp;
        }
        int doPickUp = Random.Range(0, 7);
        if(doPickUp==0)
        {
            CurrentTile.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void addTopTile(GameObject obj)
    {
        topTiles.Push(obj);
        topTiles.Peek().SetActive(false);
    }
    public void addLeftTile(GameObject obj)
    {
        leftTiles.Push(obj);
        leftTiles.Peek().SetActive(false);
    }
}
