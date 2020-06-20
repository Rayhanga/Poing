using System;
using System.Collections;
using UnityEngine;

public class PaddleMove : MonoBehaviour{
    public float speed = 10;
    public string axis;
    public bool bot = false;
    public float diff;

    public GameObject batasBawah;
    public GameObject batasAtas;
    public GameObject bolaObj;

    void Start(){
        batasBawah = GameObject.Find("BatasBawah");
        batasAtas = GameObject.Find("BatasAtas");
        bolaObj = GameObject.Find("Bola");

        diff = StaticValues.DIFFICULTY;
    }

    void Update(){
        float move = Input.GetAxis(axis) * speed * Time.deltaTime;
        if (bot){
            if(bolaObj){
                float distance = bolaObj.transform.position.y - transform.position.y;
                move = distance * Time.deltaTime * diff;
            }
        }
        float nextPos = transform.position.y + move;

        if (nextPos + (GetComponent<Renderer>().bounds.size.y/2) > batasAtas.transform.position.y){
            move = 0;
        }

        if (nextPos - (GetComponent<Renderer>().bounds.size.y/2) < batasBawah.transform.position.y){
            move = 0;
        }
        
        transform.Translate(0, move, 0);
    }
}
