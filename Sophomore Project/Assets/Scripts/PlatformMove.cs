﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Rafael Montes
    2/9/2020
    This script will make the platforms move and will allow player to ride on platform.
*/
public class PlatformMove : MonoBehaviour
{
    private bool moveRight = true;
    private bool moveUp = true;
    private float startingX, startingY;

    public GameObject player;
    public bool MoveHorizontal;
    public bool MoveVertical;
    public float moveSpeed;
    public float distanceHorizontal;
    public float distanceVertical;


    //Get starting position of object 
    private void Start()
    {
        startingX = transform.position.x;
        startingY = transform.position.y;
    }

    void FixedUpdate()
    {
        if(MoveHorizontal)
            Horizontal();

        if(MoveVertical)
            Vertical();
    }

    //Moves platform right to left 
    void Horizontal()
    {
        if (transform.position.x > (startingX + distanceHorizontal))
            moveRight = false;
        if (transform.position.x < (startingX - distanceHorizontal))
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
    }
    
    //Moves platform up and down 
    private void Vertical()
    {
        if (transform.position.y > (startingY + distanceVertical))
            moveUp = false;
        if (transform.position.y < (startingY - distanceVertical))
            moveUp = true;

        if (moveUp)
            transform.position = new Vector2(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime));
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime));
    }

    //Will make player "stick" when touching platform
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    //Will make player "unstick" when leaving platform 
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}
