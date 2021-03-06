﻿using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float dealyBeforeFirstActivate = 0f;
    public float dealyBeforeActivate = 5f;
    public float dealyBeforeDeactivate = 2f;
    public float animationSpeed = 7f;

    private bool activated = false;
    private Vector3 activatedPosition;
    private Vector3 deactivatedPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            Helpers.RestartLevel();
        }
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(dealyBeforeFirstActivate);

        activated = !activated;

        var waitBeforeActivate = new WaitForSeconds(dealyBeforeActivate);
        var waitBeforeDeactivate = new WaitForSeconds(dealyBeforeDeactivate);

        while (true)
        {
            if (activated)
            {
                yield return waitBeforeDeactivate;
            }
            else
            {
                yield return waitBeforeActivate;
            }

            activated = !activated;
        }
    }

    private void Start()
    {
        var originalRotation = transform.localRotation;

        transform.localRotation = Quaternion.identity;

        var sprite = GetComponent<SpriteRenderer>();
        var size = transform.TransformVector(sprite.bounds.size);
        var height = size.y * 2f;

        transform.localRotation = originalRotation;

        deactivatedPosition = transform.localPosition;
        activatedPosition = transform.localPosition + transform.up * height;
    }

    private void Awake()
    {
        StartCoroutine(Run());
    }

    private void Update()
    {
        var target = activated ? activatedPosition : deactivatedPosition;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, animationSpeed * Time.deltaTime);
    }
}
