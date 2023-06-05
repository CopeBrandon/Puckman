using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        mazeMover = GetComponent<MazeMover>();
        mazeMover.OnEnterNewTile += OnEnterNewTile;
    }

    MazeMover mazeMover;
    public float forwardWeight = 0.5f;
    // Update is called once per frame
    void Update()
    {

        //mazeMover.SetDesiredDirection(newDir.normalized);
    }
    void DoTurn(){
        Vector2 newDir = Vector2.zero;
        Vector2 oldDir = mazeMover.GetDesiredDirection();
        if(Mathf.Abs(oldDir.x) > 0){
            //moving left-right currently
            newDir.y = Random.Range(0,2) == 0 ? -1 : 1;
        } else {
            newDir.x = Random.Range(0,2) == 0 ? -1 : 1;
        }
        mazeMover.SetDesiredDirection(newDir.normalized);
    }

    void OnEnterNewTile(){
        if(Random.Range(0f, 1f) < forwardWeight){
            //TODO: IF WE WOULD BE HITTING A WALL, do DoTurn()
            if(mazeMover.IsGoingToHitWall()){
                DoTurn();
            }
            return;
        }
        DoTurn();
    }
}
