using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    BoidSettings settings;
    public Vector3 velocity;
    Vector3 acceleration;
    float speed;

    [HideInInspector]
    public Vector3 cohesion; 
    [HideInInspector] 
    public Vector3 align;  
    [HideInInspector] 
    public Vector3 avoidance;
    
    public void Initialize(BoidSettings settings){
        speed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.up * speed;
        this.settings = settings;
    }

    // Update is called once per frame
    public void UpdateBoid()
    {
        acceleration = Vector3.zero;

        acceleration += steerTowards(align) * settings.alignWeight;
        acceleration += steerTowards(cohesion) * settings.cohesionWeight;
        acceleration += steerTowards(avoidance) * settings.avoidanceWeight;

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity/speed;
        speed = Mathf.Clamp (speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        Ray direction = new Ray(transform.position, transform.forward);
        transform.position += velocity * Time.deltaTime;
    }

    Vector3 steerTowards(Vector3 vector){
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, settings.maxTurnSpeed);
    }
}
