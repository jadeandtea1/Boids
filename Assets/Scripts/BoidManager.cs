using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    Boid[] boidList;
    public GameObject boidPrefab;
    public BoidSettings settings;

    float screenWidth;
    float screenHeight;
    float spawnAngle;

    void Awake(){
        screenHeight = Camera.main.orthographicSize;
        screenWidth = Camera.main.aspect * Camera.main.orthographicSize;

        for(int i = 0; i < settings.boidCount; i++){
            Vector3 spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight), 0);
            spawnAngle = Random.Range(0, 360);
            GameObject newBoid = (GameObject)Instantiate (boidPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            newBoid.transform.localScale = Vector3.one * settings.boidSize;
        }
    }
    void Start()
    {
        boidList = FindObjectsOfType<Boid> ();
        for(int i = 0; i < boidList.Length; i++){
            boidList[i].Initialize(settings);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Boid boid in boidList){
            if(boid.transform.position.x > screenWidth){
                boid.transform.position = new Vector3(-screenWidth, boid.transform.position.y, boid.transform.position.z);
            } else if (boid.transform.position.x < -screenWidth){
                boid.transform.position = new Vector3(screenWidth, boid.transform.position.y, boid.transform.position.z);
            }
            if(boid.transform.position.y > screenHeight){
                boid.transform.position = new Vector3(boid.transform.position.x, -screenHeight, boid.transform.position.z);
            } else if (boid.transform.position.y < -screenHeight){
                boid.transform.position = new Vector3(boid.transform.position.x, screenHeight, boid.transform.position.z);
            }
            boid.cohesion = alignSpacing(boidList, boid);
            boid.align = alignDirection(boidList, boid);
            boid.avoidance = avoidance(boidList, boid);

            boid.UpdateBoid();
        }
    }

    Vector3 alignSpacing(Boid[] boidList, Boid boid){
        Vector3 avg = Vector3.zero;
        int total = 0;
        foreach(Boid other in boidList){
            if(boid != other){
                float distance = Vector3.Distance(boid.transform.position, other.transform.position);
                if(distance < settings.detectionRadius){
                    avg += other.transform.position;
                    total++;
                }
            }  
        }
        if(total > 0){
            avg = avg / total;
        }
        return avg;
    }

    Vector3 alignDirection(Boid[] boidList, Boid boid){
        Vector3 avg = Vector3.zero;
        int total = 0;
        foreach(Boid other in boidList){
            if(boid != other){
                float distance = Vector3.Distance(boid.transform.position, other.transform.position);
                if(distance < settings.detectionRadius){
                    avg += other.velocity;
                    total++;
                }
            }  
        }
        if(total > 0){
            avg = avg / total;
        }
        return avg;
    }

    Vector3 avoidance(Boid[] boidList, Boid boid){
        Vector3 avg = Vector3.zero;
        float distance;
        int total = 0;
        foreach(Boid other in boidList){
            distance = Vector3.Distance(boid.transform.position, other.transform.position);
            if(boid != other){
                if(distance < settings.avoidanceRadius){
                    avg += ((boid.transform.position - other.transform.position) / distance);
                    total++;
                }
            }
        }
        if(total > 0){
            avg = avg / total;
        }
        return avg;
    }
}