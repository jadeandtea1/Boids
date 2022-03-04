using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Sample")]
public class BoidSettings : ScriptableObject {
    public float minSpeed = 3;
    public float maxSpeed = 7;
    public float detectionRadius = 2.5f;
    public float avoidanceRadius = 1;
    public float maxTurnSpeed = 3;
    public int boidCount = 100;
    public float boidSize = 0.2f;

    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float avoidanceWeight = 1;
}
