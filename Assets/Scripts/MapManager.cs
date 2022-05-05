using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Player player;
    public GameObject environment;

    public GameObject stoneNode;
    public GameObject metalNode;
    public GameObject tree;
    public GameObject pickup;

    public int metalNodes;
    public int stoneNodes;
    public int trees;
    public int pickups;

    float range;
    int edgeSize = 2;
    // Start is called before the first frame update
    void Start()
    {
        range = 50f;
        player = Player.instance;

        //spawn 3x3
        SpawnLand(new Vector3(0, 0, 0));
        SpawnLand(new Vector3(50, 0,0));
        SpawnLand(new Vector3(-50, 0, 0));

        SpawnLand(new Vector3(0, 0, 50));
        SpawnLand(new Vector3(0, 0,-50));
        SpawnLand(new Vector3(50, 0, 50));

        SpawnLand(new Vector3(-50, 0, -50));
        SpawnLand(new Vector3(50, 0, -50));
        SpawnLand(new Vector3(-50, 0, 50));
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.transform.position.x) >= range || Mathf.Abs(player.transform.position.z) >= range)
        {
            range += 50;
            UpgradeSquare();
        }
    }

    public void UpgradeSquare()
    {
        for (int x = -edgeSize; x <= edgeSize; x++)
        {   
            SpawnLand(new Vector3(x*50, 0,edgeSize*50));
            SpawnLand(new Vector3(edgeSize*50, 0, x*50));

            if (x != -edgeSize)
            {
                SpawnLand(new Vector3(-x * 50, 0, -edgeSize * 50));
                SpawnLand(new Vector3(-edgeSize * 50, 0, -x * 50));
            }
        }
        edgeSize++;
    }

    public void SpawnLand(Vector3 pos)
    {
        GameObject plane = Instantiate(environment, pos, environment.transform.rotation,transform);

        for(int i=0; i < stoneNodes; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(-25,25), 0, Random.Range(-25, 25));
            Vector3 randomRot = new Vector3(0, Random.Range(-25, 25), 0);

            GameObject StoneNode = Instantiate(stoneNode, newPos + pos, environment.transform.rotation * Quaternion.Euler(randomRot), plane.transform);
            StoneNode.transform.localScale = Vector3.one * Random.Range(0.2f,0.5f);
            StoneNode.GetComponent<Node>().maxHealth = (int)(StoneNode.transform.localScale.x* 75);
            StoneNode.GetComponent<Node>().currentHealth = (int)(StoneNode.transform.localScale.x* 75);
        }

        for (int i = 0; i < trees; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(-25, 25), 0, Random.Range(-25, 25));
            Vector3 randomRot = new Vector3(0, Random.Range(-25, 25), 0);

            GameObject newTree = Instantiate(tree, newPos + pos, environment.transform.rotation * Quaternion.Euler(randomRot), plane.transform);
            newTree.transform.localScale = Vector3.one * Random.Range(0.1f, 0.2f);
            newTree.GetComponent<Tree>().maxHealth = (int)(newTree.transform.localScale.x * 75);
            newTree.GetComponent<Tree>().currentHealth = (int)(newTree.transform.localScale.x * 75);
        }

        for (int i = 0; i < pickups; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(-25, 25), 0, Random.Range(-25, 25));
            Vector3 randomRot = new Vector3(0, Random.Range(-25, 25), 0);

            GameObject newPickUp = Instantiate(pickup, newPos + pos, environment.transform.rotation * Quaternion.Euler(randomRot), plane.transform);
            newPickUp.transform.localScale = Vector3.one * Random.Range(1, 2);
        }
    }

}
