using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : BehaviorBase
{
    public Vector2 start;
    public Vector2 target;
    bool active = false;
    bool stopped = true;
    TileList<ATile> openTiles;
    TileList<ATile> closedTiles;
    ATile targetTile;
    public GameObject square;
    bool solved = false;

    public override float run()
    {
        active = true;
        return 0f;
    }

    private void Update()
    {
        if (active && stopped)
        {
            solved = false;
            start = V2Int(transform.position);
            target = start + new Vector2(5, 0);
            openTiles = new TileList<ATile>(start, target);
            closedTiles = new TileList<ATile>(start, target);
            stopped = false;
            closedTiles.Add(start);
            addAdjacent(start);
            targetTile = target;
            targetTile.generateScore(target, start);
        }
        if (active)
        {
            if (checkSolved() && !solved)
            {
                solved = true;
                foreach(ATile solvedTile in closedTiles)
                {
                    Instantiate(square, solvedTile.position, Quaternion.identity);
                }
            }
            if (!solved)
            {
                ATile lowest = findTile();
                openTiles.Remove(closedTiles.Add(lowest));
                addAdjacent(lowest.position);
            }
        }
        else stopped = true;

        active = false;

    }

    public bool checkSolved()
    {
        foreach(ATile solvedTile in closedTiles)
        {
            if (solvedTile.position == targetTile.position) return true;
        }
        return false;
    }

    public override bool check()
    {
        return true;
    }

    public override Vector2 getDirection()
    {
        return Vector2.up;
    }

    void addAdjacent(Vector2 middle)
    {
        if(!checkExists(middle + new Vector2(0, 1)))
            openTiles.Add(middle + new Vector2(0, 1));
        if (!checkExists(middle + new Vector2(1, 0)))
            openTiles.Add(middle + new Vector2(1, 0));
        if (!checkExists(middle + new Vector2(-1, 0)))
            openTiles.Add(middle + new Vector2(-1, 0));
        if (!checkExists(middle + new Vector2(0, -1)))
            openTiles.Add(middle + new Vector2(0, -1));
    }

    bool checkExists(Vector2 position)
    {
        foreach (ATile open in openTiles)
        {
            if(open.position == position)
            {
                return true;
            }
        }

        foreach (ATile closed in closedTiles)
        {
            if (closed.position == position)
            {
                return true;
            }
        }
        return false;
    }

    Vector2 V2Int(Vector2 convert)
    {
        return new Vector2((int)convert.x, (int)convert.y);
    }

    public ATile findTile()
    {
        TileList<ATile> reversedTiles = openTiles;
        reversedTiles.Reverse();
        ATile lowestTile = reversedTiles.ToArray()[0];
        foreach(ATile tile in reversedTiles)
        {
            if(tile.score <= lowestTile.score)
            {
                lowestTile = tile;
            }
        }
        return lowestTile;
    }
}

public class TileList<T> : List<T> where T : ATile
{
    Vector2 start;
    Vector2 target;
    public TileList(Vector2 start, Vector2 target) : base(){
        this.start = start;
        this.target = target;
    }

    public new ATile Add(T item)
    {
        
        item.generateScore(target, start);
        base.Add(item);
        return item;
    }
}

public class ATile
{
    public Vector2 position;
    public int score;

    public ATile(Vector2 position, int score)
    {
        this.position = position;
        this.score = score;
    }

    public void generateScore(Vector2 target, Vector2 start)
    {
        score = ManhattanDistance(start, position) + ManhattanDistance(target, position);
    }

    int ManhattanDistance(Vector2 start, Vector2 end)
    {
        return (int)Mathf.Abs(start.x - end.x) + (int)Mathf.Abs(start.y - end.y);
    }

    public static implicit operator ATile(Vector2 position)
    {
        return new ATile(position, 0);
    }
}