using UnityEngine;

public class DistributedObjectGenerator : MonoBehaviour
{
    public GameObject door;
    public int numberOfDoors;
    public float corridorWidth;
    public float deviation;

    private static float RandomDirection() {
        return (Random.value - 0.5f) * 2;
    }

    private static float CalculateX(float baseX, float distanceX, int doorNumber, float xDeviation) {
        return baseX + distanceX * doorNumber + RandomDirection() * xDeviation;
    }

    private static Vector2 CalculatePosition(Vector2 basePosition, float distanceX, int doorNumber, float xDeviation) {
        return new Vector2(CalculateX(basePosition.x, distanceX, doorNumber, xDeviation), basePosition.y);
    }

    private void Start() {
        Vector2[] positions = new Vector2[numberOfDoors];
        Vector2 basePosition = GetComponent<Rigidbody2D>().position;
        float baseDistance = corridorWidth / numberOfDoors;

        for (int i = 0; i < positions.Length; i++)
            positions[i] = CalculatePosition(basePosition, baseDistance, i, deviation);

        positions.Do(pos => {
            GameObject instance = (GameObject)Instantiate(door);
            instance.transform.parent = transform;
            instance.GetComponent<Rigidbody2D>().position = pos;
        });
    }
}