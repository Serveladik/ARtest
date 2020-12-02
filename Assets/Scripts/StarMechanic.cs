using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMechanic : MonoBehaviour
{
    [SerializeField] GameObject[] PathList;
    [SerializeField] float speed;
    private float moveTime;
    private Vector3 CurrentPath;
    private int currentPath;
    protected Vector3 startStartPos; 
    void Start()
    {
        CheckPath();
    }
    void CheckPath()
    {
        startStartPos = transform.position; 
        CurrentPath = PathList[currentPath].transform.position;
    }

    void Update()
    {
        StarMovement();
    }

    void StarMovement()
    {
        
        if(transform.position!=CurrentPath)
        {
            transform.position = Vector3.MoveTowards(transform.position,CurrentPath,speed/1000f);
        }
        else
        {
            if(currentPath < PathList.Length-1)
            {
                currentPath++;
                CheckPath();
            }
            else
            {
                currentPath=-1;
            }
        }
        
    }



}
