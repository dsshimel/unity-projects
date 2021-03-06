﻿using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;
    public int numIntervals;

    public float radiusInner;
    public float radiusOuter;
    public float angularVelocityMin;
    public float angularVelocityMax;
    public float intensityMin;
    public float intensityMax;
    public float scaleMin;
    public float scaleMax;

    private ISession session;

	void Start ()
    {
        var comets = new Dictionary<int, GameObject>();
        for (var i = 0; i < numSpheres; i++)
        {
            GameObject comet = Instantiate(spherePrefab);
            comets.Add(comet.GetInstanceID(), comet);
        }

        var manipulator = new Manipulator(comets);
        var bundleFactory = new BundleFactory(
            manipulator,
            radiusInner,
            radiusOuter,
            angularVelocityMin,
            angularVelocityMax,
            intensityMax,
            intensityMin,
            scaleMin,
            scaleMax);
        var playlistFactory = new PlaylistFactory(bundleFactory, intensityMax, intensityMin);

        session = new Session(playlistFactory.create(numIntervals), /* countdowTime= */ 1.0f);
        session.Start();
    }

    void Update ()
    {
        session.IncrementTime(Time.deltaTime);
	}
}
