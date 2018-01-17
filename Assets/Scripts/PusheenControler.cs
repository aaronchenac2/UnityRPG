using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusheenControler : MonoBehaviour {
    public GameObject player;
    public Move move;
    public Stats stats;

    public GameObject seat;

    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (move.mounted)
        {
            Vector3 newPos = seat.transform.position;
            player.transform.position = newPos;
            Vector3 newRotation = transform.eulerAngles;
            player.transform.eulerAngles = new Vector3(-newRotation.z, newRotation.y, newRotation.x) + new Vector3(0, 90, 0);
            MoveControls();
        }
    }

    void MoveControls()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");



        if (Hacks.hacks)
        {
            transform.Translate(new Vector3(v * stats.speed * 3 * Time.deltaTime, 0, h * -stats.speed * 3 * Time.deltaTime));
            transform.Rotate(new Vector3(0, h * stats.rotationSpeed * Time.deltaTime, 0));
        }
        else
        {
            transform.Translate(new Vector3(v * stats.speed * 1.5f * Time.deltaTime, 0, h * -stats.speed * 1.5f * Time.deltaTime));
            transform.Rotate(new Vector3(0, h * stats.rotationSpeed * Time.deltaTime, 0));
        }

        Vector3 newPos = transform.position;
        newPos.y = Terrain.activeTerrain.SampleHeight(newPos);
        transform.position = newPos;
    }
}
