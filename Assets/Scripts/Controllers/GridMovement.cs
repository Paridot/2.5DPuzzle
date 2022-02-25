using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody rb;

    Grid3D map;

    [SerializeField] float driftTime;

    Coroutine drifting;

    private void Start() { 

        rb = GetComponent<Rigidbody>();
        map = GameObject.FindWithTag("Map").GetComponent<Grid3D>();

        var mover = GetComponent<IMover>();

        mover.OnMoveInput += Mover_OnMoveInput;
        mover.OnStop += Mover_OnStop;
    }

    private void Mover_OnStop()
    {
        rb.velocity = Vector3.zero;

        drifting = StartCoroutine(DriftToCellCenter());
    }

    private void Mover_OnMoveInput(Vector3 direction)
    {
        if (drifting != null)
        {
            StopCoroutine(drifting);
        }
        rb.velocity = direction * speed;
    }

    IEnumerator DriftToCellCenter()
    {
        var cell = map.GetNearestPoint(transform.position);
        var cellPos = map.CellToWorldPoint(cell);
        var startPos = transform.position;

        float driftingSince = 0;

        while (driftingSince < driftTime)
        {
            var percent = driftingSince/driftTime;
            transform.position = Vector3.Lerp(startPos, cellPos, percent);

            driftingSince += Time.deltaTime;
            yield return null;
        }

        transform.position = cellPos;
    }

}
