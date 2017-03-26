using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Infinite : MonoBehaviour {
    Chunk[,] chunks = new Chunk[3,3];
    public GameObject chunk;
    public Transform player;
    [SerializeField]
    int seed;

	// Use this for initialization
	void Start () {
        seed = Random.Range(int.MinValue, int.MaxValue);
        for(int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                Chunk c = ((GameObject)Instantiate(chunk, new Vector3(x * 50, y * 50), Quaternion.identity)).GetComponent<Chunk>();
                chunks[x+1, y+1] = c;
                MapGenerator[] generators = c.GetComponentsInChildren<MapGenerator>();
                foreach(MapGenerator gen in generators)
                {
                    gen.offset = new Vector2(-x * MapGenerator.mapChunkSize, -y * MapGenerator.mapChunkSize);
                    gen.seed = seed;
                }
                WormGenerator[] wormGenerators = c.GetComponentsInChildren<WormGenerator>();
                foreach (WormGenerator gen in wormGenerators)
                {
                    gen.offset = new Vector2(-x * MapGenerator.mapChunkSize, -y * MapGenerator.mapChunkSize);
                    gen.seed = seed;
                    //gen.OnValidate();
                }
                CombineMap map = c.GetComponent<CombineMap>();
                map.OnValidate();

                c.chunkPosition = new Vector2(x, y);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Chunk c = GetChunk(player.transform.position);
        bool update = false;
        int xIndex = 0;
        int yIndex = 0;
        for (int x = 0; x < chunks.GetLength(0); x++)
        {
            for(int y = 0; y < chunks.GetLength(1); y++)
            {
                if (chunks[x,y] == c)
                {
                    xIndex = x;
                    yIndex = y;
                    if (!(x == 1 && y == 1))
                        update = true;
                    break;
                    
                }
            }
            if (update)
            {
                break;
            }
        }

        int xOffset  = xIndex - 1;
        int yOffset = yIndex - 1;

        if (update)
        {
            Chunk[,] tempChunks = new Chunk[3, 3];
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = 0; y < chunks.GetLength(1); y++)
                {
                    if (Mathf.Abs(x - xIndex) <= 1 && Mathf.Abs(y - yIndex) <= 1)
                    {
                        tempChunks[x - xOffset, y - yOffset] = chunks[x, y];
                    }else
                    {
                        Destroy(chunks[x, y].gameObject);
                    }
                }
            }
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = 0; y < chunks.GetLength(1); y++)
                {
                    if(tempChunks[x, y] == null)
                    {

                        Vector2 chunkPosition = new Vector2(x - 1 + tempChunks[1, 1].chunkPosition.x, y - 1 + tempChunks[1, 1].chunkPosition.y);
                        Chunk ch = ((GameObject)Instantiate(chunk, new Vector3(chunkPosition.x * 50, chunkPosition.y * 50), Quaternion.identity)).GetComponent<Chunk>();
                        ch.chunkPosition = chunkPosition;
                        MapGenerator[] generators = ch.GetComponentsInChildren<MapGenerator>();
                        foreach (MapGenerator gen in generators)
                        {
                            gen.offset = new Vector2(-chunkPosition.x * MapGenerator.mapChunkSize, -chunkPosition.y * MapGenerator.mapChunkSize);
                            gen.seed = seed;
                        }
                        WormGenerator[] wormGenerators = ch.GetComponentsInChildren<WormGenerator>();
                        foreach (WormGenerator gen in wormGenerators)
                        {
                            gen.offset = new Vector2(-chunkPosition.x * MapGenerator.mapChunkSize, -chunkPosition.y * MapGenerator.mapChunkSize);
                            gen.seed = seed;
                            //gen.OnValidate();
                        }
                        CombineMap map = ch.GetComponent<CombineMap>();
                        map.OnValidate();


                        tempChunks[x, y] = ch;
                    }
                }
            }
            chunks = tempChunks;
        }
        update = false;
	}

    public Chunk GetChunk(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / 50);
        int z = Mathf.FloorToInt(position.y / 50);
        foreach(Chunk chunk in chunks)
        {
            if(chunk.chunkPosition == new Vector2(x, z))
            {
                return chunk;
            }
        }
        return null;
    }
}
