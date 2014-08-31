using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	private LineRenderer lineRenderer;
    public GameObject shootPoint;
    public GameObject player;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        drawLine();
    }

    private void drawLine()
    {
        lineRenderer.SetPosition(0, Vector3.Lerp(player.transform.position, shootPoint.transform.position, 0.4f));
        lineRenderer.SetPosition(1, (player.transform.position - shootPoint.transform.position) * -1000);
        lineRenderer.SetWidth(0.05f, 0.05f);
    }
}
