using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : BehaviorBase {
    public Vector2 start;
    public Vector2 target;
    bool active = false;
    bool stopped = true;
    List<ATile> openTiles = new List<ATile>();
    List<ATile> closedTiles = new List<ATile>();

    public override float run()
    {
        active = true;
        return 0f;
    }

    private void Update()
    {
        if(active && stopped)
        {
            start = V2Int(transform.position);
            target = start + new Vector2(5, 3);
            stopped = false;
            closedTiles.Add(start);
            addAdjacent(start);
        }
        if (active)
        {
            
        }
        else stopped = true;

        active = false;
        
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
        openTiles.Add(middle + new Vector2(0, 1));
        openTiles.Add(middle + new Vector2(1, 0));
        openTiles.Add(middle + new Vector2(-1, 0));
        openTiles.Add(middle + new Vector2(0, -1));
    }
    
    Vector2 V2Int(Vector2 convert)
    {
        return new Vector2((int) convert.x, (int) convert.y);
    }


}

class ATile
{
    Vector2 position;
    int score;

    public ATile(Vector2 position, int score)
    {
        this.position = position;
        this.score = score;
    } //make a creation mechanism from vectors
}