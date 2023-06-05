using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeMover : MonoBehaviour
{
    float Speed = 3; //how many tiles you travel per second
    public Vector2 desiredDirection; //the current direction we want to go in
    Vector2 targetPos; //always a legal empty tile

    public delegate void OnEnterNewTileDelegate();
    public event OnEnterNewTileDelegate OnEnterNewTile;

    // Start is called before the first frame update
    void Start(){
        targetPos = transform.position;

    }
    // Update is called once per graphics frame
    void Update(){
        UpdateTargetPosition();
        MoveToTargetPosition();
 }
    //once per physics engine frame
    void FixedUpdate(){
        return;
    }
    void UpdateTargetPosition(bool force=false){
        if(force==false){
            float distanceToTarget = Vector3.Distance(transform.position, targetPos);
            if(distanceToTarget>0){
                return;
            }
        }
        if(GetTileAt(targetPos)!=null){
            OnEnterNewTile();
        }
        targetPos += desiredDirection;
        targetPos = FloorPosition(targetPos);
        if(IsTileEmpty(targetPos)){
            return;
        }
        targetPos = transform.position;
    }
    Vector2 FloorPosition(Vector2 pos){
        return new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }
    public bool IsGoingToHitWall(){
        return GetTileAt(targetPos + desiredDirection) != null;
    }
    bool IsTileEmpty(Vector2 pos){
        return GetTileAt(pos) == null;
    }
    TileBase GetTileAt(Vector2 pos){
        Vector3Int cellPos = GameManager.WallTilemap.WorldToCell(pos);
        return GameManager.WallTilemap.GetTile(cellPos);
    }
    void MoveToTargetPosition(){
        float distanceThisUpdate = Speed*Time.deltaTime;
        Vector2 distToTarget = targetPos - (Vector2) transform.position;
        Vector2 movementThisUpdate = distToTarget.normalized * distanceThisUpdate;

        if(distToTarget.SqrMagnitude() < movementThisUpdate.SqrMagnitude()){
            transform.position = targetPos;
            return;
        }

        transform.Translate(movementThisUpdate);
    }

    public void SetDesiredDirection(Vector2 newDir){
        Vector2 testPos = targetPos + newDir;
        if(IsTileEmpty(testPos) == false){
            //return;
        }
        Vector2 oldDir = desiredDirection;
        desiredDirection = newDir;
        

        if(Vector2.Dot(oldDir, newDir) < 0){
            UpdateTargetPosition(true);
        }
    }
    public Vector2 GetDesiredDirection(){
        return desiredDirection;
    }
}
