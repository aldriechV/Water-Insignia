using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true; //checks for if a tile is walkable (stops mountains and stuff)
    public bool current = false; // if a player is standing on it
    public bool target = false; // if the tile we want to move to is safe
    public bool selectable = false; // checks for tiles you can pick

    public List<Tile> adjacencyList = new List<Tile>(); //Identifies Neighboring tiles

    //Needed BFS (Breadth First Search)
    public void visited = false; //Tile has been processed (usually done only once)
    public Tile parent = null; //Parent Tile; Needed to identify what tiles are walkable
    public int distance = 0; //how far each tile is from the start tile


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta; // Look to change these colors when possible
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }

        public void Reset(){
            adjacencyList.Clear();

            current = false; // if a player is standing on it
            target = false; // if the tile we want to move to is safe
            selectable = false; // checks for tiles you can pick

            visited = false;
            parent = null; 
            distance = 0; 
        }

        public void FindNeighbors(float jumpHeight) // jumpHeight is for flying units
        {
            Reset();
            
            CheckTile(Vector3.forward, jumpHeight);
            CheckTile(-Vector3.forward, jumpHeight);
            CheckTile(Vector3.right, jumpHeight);
            CheckTile(-Vector3.right, jumpHeight);
        }

        public void CheckTile(Vector3 direction, float jumpHeight)
        {
            Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
            Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

            foreach(Collider item in colliders)
            {
                Tile tile = item.GetComponent<Tile>();
                if (tile != null && tile.walkable)
                {
                    RaycastHit hit;
                    
                    if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                    {   
                        adjacencyList.Add(tile);
                    }
                }
            }
        }
    }
}
