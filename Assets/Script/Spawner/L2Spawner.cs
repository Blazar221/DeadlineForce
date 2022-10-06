using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;
using Object = Script.Object;


public class L2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject bgm;

    private GameObject newItem,newPlatform;
    private Vector3 spawnPos;
    private Note noteHandler;
    private PlayerControl playerHandler;
    private float playerX, xLen;
    private int ind = 1;
    

    private Object[] objArr = new Object[]
    {
        // best lane
		new Object(new float[]{0f, 0f}, 0, 1, false), // zero index not spawn
        new Object(new float[]{1.091f, 1.091f}, 3, 1, true),
        new Object(new float[]{2.182f, 2.182f}, 3, 3, true),
        new Object(new float[]{3.273f, 3.273f}, 3, 1, true),
        new Object(new float[]{3.545f, 3.545f}, 3, 1, true),
        new Object(new float[]{3.818f, 3.818f}, 3, 1, true),
        new Object(new float[]{4.091f, 4.091f}, 3, 1, true),
        new Object(new float[]{4.364f, 4.364f}, 3, 1, true),
        new Object(new float[]{5.455f, 5.455f}, 3, 1, true),
        new Object(new float[]{6.545f, 6.545f}, 3, 3, true),
        new Object(new float[]{7.636f, 7.636f}, 3, 1, true),
        new Object(new float[]{7.909f, 7.909f}, 3, 1, true),
        new Object(new float[]{8.182f, 8.182f}, 3, 1, true),
        new Object(new float[]{8.455f, 8.455f}, 3, 1, true),
        new Object(new float[]{8.727f, 8.727f}, 3, 1, true),
        new Object(new float[]{9.818f, 9.818f}, 2, 1, true),
        new Object(new float[]{10.909f, 10.909f}, 2, 3, true),
        new Object(new float[]{12.0f, 12.0f}, 2, 1, true),
        new Object(new float[]{12.273f, 12.273f}, 2, 1, true),
        new Object(new float[]{12.545f, 12.545f}, 2, 1, true),
        new Object(new float[]{12.818f, 12.818f}, 2, 1, true),
        new Object(new float[]{13.091f, 13.091f}, 2, 1, true),
        new Object(new float[]{14.182f, 14.182f}, 2, 1, true),
        new Object(new float[]{15.273f, 15.273f}, 2, 3, true),
        new Object(new float[]{16.364f, 16.364f}, 2, 1, true),
        new Object(new float[]{16.636f, 16.636f}, 2, 1, true),
        new Object(new float[]{16.909f, 16.909f}, 2, 1, true),
        new Object(new float[]{17.182f, 17.182f}, 2, 1, true),
        new Object(new float[]{17.455f, 17.455f}, 2, 0, true),
        new Object(new float[]{18.545f, 18.545f}, 1, 1, true),
        new Object(new float[]{18.818f, 18.818f}, 1, 1, true),
        new Object(new float[]{19.091f, 19.091f}, 1, 3, true),
        new Object(new float[]{19.364f, 19.364f}, 1, 1, true),
        new Object(new float[]{19.636f, 19.636f}, 1, 1, true),
        new Object(new float[]{19.841f, 19.841f}, 1, 1, true),
        new Object(new float[]{20.182f, 20.182f}, 1, 1, true),
        new Object(new float[]{20.455f, 20.455f}, 1, 1, true),
        new Object(new float[]{20.727f, 20.727f}, 1, 1, true),
        new Object(new float[]{21.0f, 21.0f}, 1, 1, true),
        new Object(new float[]{21.273f, 21.273f}, 1, 3, true),
        new Object(new float[]{21.545f, 21.545f}, 1, 1, true),
        new Object(new float[]{21.818f, 21.818f}, 1, 1, true),
        new Object(new float[]{22.023f, 22.023f}, 1, 1, true),
        new Object(new float[]{22.364f, 22.364f}, 1, 1, true),
        new Object(new float[]{22.636f, 22.636f}, 1, 1, true),
        new Object(new float[]{22.909f, 22.909f}, 1, 1, true),
        new Object(new float[]{23.182f, 23.182f}, 1, 1, true),
        new Object(new float[]{23.455f, 23.455f}, 1, 3, true),
        new Object(new float[]{23.727f, 23.727f}, 1, 1, true),
        new Object(new float[]{24.0f, 24.0f}, 1, 1, true),
        new Object(new float[]{24.205f, 24.205f}, 1, 1, true),
        new Object(new float[]{24.545f, 24.545f}, 1, 1, true),
        new Object(new float[]{24.818f, 24.818f}, 1, 1, true),
        new Object(new float[]{25.091f, 25.091f}, 1, 1, true),
        new Object(new float[]{25.364f, 25.364f}, 1, 1, true),
        new Object(new float[]{25.636f, 25.636f}, 1, 3, true),
        new Object(new float[]{25.909f, 25.909f}, 1, 1, true),
        new Object(new float[]{26.182f, 26.182f}, 1, 1, true),
        new Object(new float[]{26.386f, 26.386f}, 1, 1, true),
        new Object(new float[]{26.727f, 26.727f}, 1, 1, true),
        new Object(new float[]{27.0f, 27.0f}, 1, 1, true),
        new Object(new float[]{27.273f, 27.273f}, 1, 1, true),
        new Object(new float[]{27.545f, 27.545f}, 1, 1, true),
        new Object(new float[]{27.818f, 27.818f}, 1, 3, true),
        new Object(new float[]{28.091f, 28.091f}, 1, 1, true),
        new Object(new float[]{28.364f, 28.364f}, 1, 1, true),
        new Object(new float[]{28.568f, 28.568f}, 1, 1, true),
        new Object(new float[]{28.909f, 28.909f}, 1, 1, true),
        new Object(new float[]{29.182f, 29.182f}, 1, 1, true),
        new Object(new float[]{29.455f, 29.455f}, 1, 1, true),
        new Object(new float[]{29.727f, 29.727f}, 1, 1, true),
        new Object(new float[]{30.0f, 30.0f}, 1, 3, true),
        new Object(new float[]{30.273f, 30.273f}, 1, 1, true),
        new Object(new float[]{30.545f, 30.545f}, 1, 1, true),
        new Object(new float[]{30.75f, 30.75f}, 1, 1, true),
        new Object(new float[]{31.091f, 31.091f}, 1, 1, true),
        new Object(new float[]{31.364f, 31.364f}, 1, 1, true),
        new Object(new float[]{31.636f, 31.636f}, 1, 1, true),
        new Object(new float[]{31.909f, 31.909f}, 1, 1, true),
        new Object(new float[]{33.0f, 33.0f}, 1, 1, true),
        new Object(new float[]{34.091f, 34.091f}, 1, 1, true),
        new Object(new float[]{34.364f, 34.364f}, 1, 1, true),
        new Object(new float[]{34.636f, 34.636f}, 1, 1, true),
        new Object(new float[]{34.909f, 34.909f}, 1, 0, true),
        new Object(new float[]{36.0f, 36.0f}, 3, 1, true),
        new Object(new float[]{37.091f, 37.091f}, 3, 3, true),
        new Object(new float[]{38.182f, 38.182f}, 3, 1, true),
        new Object(new float[]{38.455f, 38.455f}, 3, 1, true),
        new Object(new float[]{38.727f, 38.727f}, 3, 1, true),
        new Object(new float[]{39.0f, 39.0f}, 3, 1, true),
        new Object(new float[]{39.273f, 39.273f}, 3, 1, true),
        new Object(new float[]{40.364f, 40.364f}, 3, 1, true),
        new Object(new float[]{41.455f, 41.455f}, 3, 3, true),
        new Object(new float[]{42.545f, 42.545f}, 3, 1, true),
        new Object(new float[]{42.818f, 42.818f}, 3, 1, true),
        new Object(new float[]{43.091f, 43.091f}, 3, 1, true),
        new Object(new float[]{43.364f, 43.364f}, 3, 1, true),
        new Object(new float[]{43.636f, 43.636f}, 3, 1, true),
        new Object(new float[]{44.727f, 44.727f}, 2, 1, true),
        new Object(new float[]{45.818f, 45.818f}, 2, 3, true),
        new Object(new float[]{46.909f, 46.909f}, 2, 1, true),
        new Object(new float[]{47.182f, 47.182f}, 2, 1, true),
        new Object(new float[]{47.454f, 47.454f}, 2, 1, true),
        new Object(new float[]{47.727f, 47.727f}, 2, 1, true),
        new Object(new float[]{48.0f, 48.0f}, 2, 1, true),
        new Object(new float[]{49.091f, 49.091f}, 2, 1, true),
        new Object(new float[]{50.182f, 50.182f}, 2, 3, true),
        new Object(new float[]{51.273f, 51.273f}, 2, 1, true),
        new Object(new float[]{51.545f, 51.545f}, 2, 1, true),
        new Object(new float[]{51.818f, 51.818f}, 2, 1, true),
        new Object(new float[]{52.091f, 52.091f}, 2, 1, true),
        new Object(new float[]{52.364f, 52.364f}, 2, 0, true),
        new Object(new float[]{53.454f, 53.454f}, 0, 1, true),
        new Object(new float[]{53.727f, 53.727f}, 0, 1, true),
        new Object(new float[]{54.0f, 54.0f}, 0, 3, true),
        new Object(new float[]{54.273f, 54.273f}, 0, 1, true),
        new Object(new float[]{54.545f, 54.545f}, 0, 1, true),
        new Object(new float[]{54.75f, 54.75f}, 0, 1, true),
        new Object(new float[]{55.091f, 55.091f}, 0, 1, true),
        new Object(new float[]{55.364f, 55.364f}, 0, 1, true),
        new Object(new float[]{55.636f, 55.636f}, 0, 1, true),
        new Object(new float[]{55.909f, 55.909f}, 0, 1, true),
        new Object(new float[]{56.182f, 56.182f}, 0, 3, true),
        new Object(new float[]{56.454f, 56.454f}, 0, 1, true),
        new Object(new float[]{56.727f, 56.727f}, 0, 1, true),
        new Object(new float[]{56.932f, 56.932f}, 0, 1, true),
        new Object(new float[]{57.273f, 57.273f}, 0, 1, true),
        new Object(new float[]{57.545f, 57.545f}, 0, 1, true),
        new Object(new float[]{57.818f, 57.818f}, 0, 1, true),
        new Object(new float[]{58.091f, 58.091f}, 0, 1, true),
        new Object(new float[]{58.364f, 58.364f}, 0, 3, true),
        new Object(new float[]{58.636f, 58.636f}, 0, 1, true),
        new Object(new float[]{58.909f, 58.909f}, 0, 1, true),
        new Object(new float[]{59.114f, 59.114f}, 0, 1, true),
        new Object(new float[]{59.454f, 59.454f}, 0, 1, true),
        new Object(new float[]{59.727f, 59.727f}, 0, 1, true),
        new Object(new float[]{60.0f, 60.0f}, 0, 1, true),
        new Object(new float[]{60.273f, 60.273f}, 0, 1, true),
        new Object(new float[]{60.545f, 60.545f}, 0, 3, true),
        new Object(new float[]{60.818f, 60.818f}, 0, 1, true),
        new Object(new float[]{61.091f, 61.091f}, 0, 1, true),
        new Object(new float[]{61.295f, 61.295f}, 0, 1, true),
        new Object(new float[]{61.636f, 61.636f}, 0, 1, true),
        new Object(new float[]{61.909f, 61.909f}, 0, 1, true),
        new Object(new float[]{62.182f, 62.182f}, 0, 1, true),
        new Object(new float[]{62.454f, 62.454f}, 0, 1, true),
        new Object(new float[]{62.727f, 62.727f}, 0, 3, true),
        new Object(new float[]{63.0f, 63.0f}, 0, 1, true),
        new Object(new float[]{63.273f, 63.273f}, 0, 1, true),
        new Object(new float[]{63.477f, 63.477f}, 0, 1, true),
        new Object(new float[]{63.818f, 63.818f}, 0, 1, true),
        new Object(new float[]{64.091f, 64.091f}, 0, 1, true),
        new Object(new float[]{64.364f, 64.364f}, 0, 1, true),
        new Object(new float[]{64.636f, 64.636f}, 0, 1, true),
        new Object(new float[]{64.909f, 64.909f}, 0, 3, true),
        new Object(new float[]{65.182f, 65.182f}, 0, 1, true),
        new Object(new float[]{65.454f, 65.454f}, 0, 1, true),
        new Object(new float[]{65.659f, 65.659f}, 0, 1, true),
        new Object(new float[]{66.0f, 66.0f}, 0, 1, true),
        new Object(new float[]{66.273f, 66.273f}, 0, 1, true),
        new Object(new float[]{66.545f, 66.545f}, 0, 1, true),
        new Object(new float[]{66.818f, 66.818f}, 0, 1, true),
        new Object(new float[]{67.909f, 67.909f}, 0, 1, true),
        new Object(new float[]{69.0f, 69.0f}, 0, 1, true),
        new Object(new float[]{69.273f, 69.273f}, 0, 1, true),
        new Object(new float[]{69.545f, 69.545f}, 0, 1, true),
        //new Object(new float[]{69.818f, 69.818f}, 0, 1, true),
		// long notes on best lane
        new Object(new float[]{0.477f, 0.818f}, 3, 2, true),
        new Object(new float[]{1.568f, 1.909f}, 3, 2, true),
        new Object(new float[]{2.659f, 3f}, 3, 2, true),
        new Object(new float[]{4.841f, 5.182f}, 3, 2, true),
        new Object(new float[]{5.932f, 6.273f}, 3, 2, true),
        new Object(new float[]{7.023f, 7.364f}, 3, 2, true),
        new Object(new float[]{9.205f, 9.545f}, 2, 2, true),
        new Object(new float[]{10.295f, 10.636f}, 2, 2, true),
        new Object(new float[]{11.386f, 11.727f}, 2, 2, true),
        new Object(new float[]{13.568f, 13.909f}, 2, 2, true),
        new Object(new float[]{14.659f, 15f}, 2, 2, true),
        new Object(new float[]{15.75f, 16.091f}, 2, 2, true),
        new Object(new float[]{32.182f, 32.727f}, 1, 2, true),
        new Object(new float[]{33.273f, 33.818f}, 1, 2, true),
        new Object(new float[]{35.386f, 35.727f}, 3, 2, true),
        new Object(new float[]{36.477f, 36.818f}, 3, 2, true),
        new Object(new float[]{37.568f, 37.909f}, 3, 2, true),
        new Object(new float[]{39.75f, 40.091f}, 3, 2, true),
        new Object(new float[]{40.841f, 41.182f}, 3, 2, true),
        new Object(new float[]{41.932f, 42.273f}, 3, 2, true),
        new Object(new float[]{44.114f, 44.455f}, 2, 2, true),
        new Object(new float[]{45.205f, 45.545f}, 2, 2, true),
        new Object(new float[]{46.295f, 46.636f}, 2, 2, true),
        new Object(new float[]{48.477f, 48.818f}, 2, 2, true),
        new Object(new float[]{49.568f, 49.909f}, 2, 2, true),
        new Object(new float[]{50.659f, 51f}, 2, 2, true),
        new Object(new float[]{67.091f, 67.636f}, 0, 2, true),
        new Object(new float[]{68.182f, 68.727f}, 0, 2, true),

		// second best lane
		new Object(new float[]{1.091f, 1.091f}, 1, 1, false),
        new Object(new float[]{1.909f, 1.909f}, 1, 3, false),
        new Object(new float[]{2.182f, 2.182f}, 1, 3, false),
        new Object(new float[]{3.273f, 3.273f}, 1, 1, false),
        new Object(new float[]{4.364f, 4.364f}, 1, 3, false),
        new Object(new float[]{5.455f, 5.455f}, 1, 1, false),
        new Object(new float[]{6.273f, 6.273f}, 1, 1, false),
        new Object(new float[]{6.545f, 6.545f}, 1, 1, false),
        new Object(new float[]{7.636f, 7.636f}, 1, 1, false),
        new Object(new float[]{8.727f, 8.727f}, 1, 1, false),
        new Object(new float[]{9.818f, 9.818f}, 1, 1, false),
        new Object(new float[]{10.636f, 10.636f}, 1, 1, false),
        new Object(new float[]{10.909f, 10.909f}, 1, 1, false),
        new Object(new float[]{12.0f, 12.0f}, 1, 1, false),
        new Object(new float[]{13.091f, 13.091f}, 1, 3, false),
        new Object(new float[]{14.182f, 14.182f}, 1, 1, false),
        new Object(new float[]{15.0f, 15.0f}, 1, 3, false),
        new Object(new float[]{15.273f, 15.273f}, 1, 1, false),
        new Object(new float[]{16.364f, 16.364f}, 1, 3, false),
        new Object(new float[]{17.659f, 17.659f}, 3, 1, false),
        new Object(new float[]{17.727f, 17.727f}, 3, 1, false),
        new Object(new float[]{18.0f, 18.0f}, 3, 3, false),
        new Object(new float[]{18.273f, 18.273f}, 3, 1, false),
        new Object(new float[]{18.545f, 18.545f}, 3, 3, false),
        new Object(new float[]{19.364f, 19.364f}, 3, 3, false),
        new Object(new float[]{19.636f, 19.636f}, 3, 1, false),
        new Object(new float[]{20.727f, 20.727f}, 3, 1, false),
        new Object(new float[]{21.818f, 21.818f}, 3, 1, false),
        new Object(new float[]{22.909f, 22.909f}, 3, 1, false),
        new Object(new float[]{23.727f, 23.727f}, 3, 1, false),
        new Object(new float[]{24.0f, 24.0f}, 3, 3, false),
        new Object(new float[]{25.091f, 25.091f}, 3, 1, false),
        new Object(new float[]{26.182f, 26.182f}, 3, 1, false),
        new Object(new float[]{27.273f, 27.273f}, 3, 1, false),
        new Object(new float[]{28.091f, 28.091f}, 3, 1, false),
        new Object(new float[]{28.364f, 28.364f}, 3, 3, false),
        new Object(new float[]{29.455f, 29.455f}, 3, 3, false),
        new Object(new float[]{30.545f, 30.545f}, 3, 1, false),
        new Object(new float[]{31.636f, 31.636f}, 3, 1, false),
        new Object(new float[]{32.455f, 32.455f}, 3, 1, false),
        new Object(new float[]{32.727f, 32.727f}, 3, 3, false),
        new Object(new float[]{33.818f, 33.818f}, 3, 1, false),
        new Object(new float[]{34.091f, 34.091f}, 3, 1, false),
        new Object(new float[]{34.364f, 34.364f}, 3, 1, false),
        new Object(new float[]{34.636f, 34.636f}, 3, 3, false),
        new Object(new float[]{36.0f, 36.0f}, 0, 1, false),
        new Object(new float[]{36.818f, 36.818f}, 0, 3, false),
        new Object(new float[]{37.091f, 37.091f}, 0, 1, false),
        new Object(new float[]{38.182f, 38.182f}, 0, 1, false),
        new Object(new float[]{39.273f, 39.273f}, 0, 3, false),
        new Object(new float[]{40.364f, 40.364f}, 0, 1, false),
        new Object(new float[]{41.182f, 41.182f}, 0, 1, false),
        new Object(new float[]{41.455f, 41.455f}, 0, 3, false),
        new Object(new float[]{42.545f, 42.545f}, 0, 3, false),
        new Object(new float[]{43.636f, 43.636f}, 0, 1, false),
        new Object(new float[]{44.727f, 44.727f}, 0, 3, false),
        new Object(new float[]{45.545f, 45.545f}, 0, 1, false),
        new Object(new float[]{45.818f, 45.818f}, 0, 1, false),
        new Object(new float[]{46.909f, 46.909f}, 0, 1, false),
        new Object(new float[]{48.0f, 48.0f}, 0, 1, false),
        new Object(new float[]{49.091f, 49.091f}, 0, 3, false),
        new Object(new float[]{49.909f, 49.909f}, 0, 1, false),
        new Object(new float[]{50.182f, 50.182f}, 0, 1, false),
        new Object(new float[]{51.273f, 51.273f}, 0, 1, false),
        new Object(new float[]{52.568f, 52.568f}, 2, 1, false),
        new Object(new float[]{52.636f, 52.636f}, 2, 1, false),
        new Object(new float[]{52.909f, 52.909f}, 2, 1, false),
        new Object(new float[]{53.182f, 53.182f}, 2, 1, false),
        new Object(new float[]{53.454f, 53.454f}, 2, 3, false),
        new Object(new float[]{54.273f, 54.273f}, 2, 3, false),
        new Object(new float[]{54.545f, 54.545f}, 2, 1, false),
        new Object(new float[]{55.636f, 55.636f}, 2, 1, false),
        new Object(new float[]{56.727f, 56.727f}, 2, 1, false),
        new Object(new float[]{57.818f, 57.818f}, 2, 1, false),
        new Object(new float[]{58.636f, 58.636f}, 2, 1, false),
        new Object(new float[]{58.909f, 58.909f}, 2, 3, false),
        new Object(new float[]{60.0f, 60.0f}, 2, 1, false),
        new Object(new float[]{61.091f, 61.091f}, 2, 1, false),
        new Object(new float[]{62.182f, 62.182f}, 2, 1, false),
        new Object(new float[]{63.0f, 63.0f}, 2, 3, false),
        new Object(new float[]{63.273f, 63.273f}, 2, 1, false),
        new Object(new float[]{64.364f, 64.364f}, 2, 1, false),
        new Object(new float[]{65.454f, 65.454f}, 2, 1, false),
        new Object(new float[]{66.545f, 66.545f}, 2, 3, false),
        new Object(new float[]{67.364f, 67.364f}, 2, 3, false),
        new Object(new float[]{67.636f, 67.636f}, 2, 1, false),
        new Object(new float[]{68.727f, 68.727f}, 2, 1, false),
        new Object(new float[]{69.0f, 69.0f}, 2, 1, false),
        new Object(new float[]{69.273f, 69.273f}, 2, 1, false),
        new Object(new float[]{69.545f, 69.545f}, 2, 3, false),
        //new Object(new float[]{69.818f, 69.818f}, 2, 1, false),

		// third and forth
        new Object(new float[]{1.091f, 1.091f}, 2, 3, false),
        new Object(new float[]{1.091f, 1.091f}, 0, 3, false),
        new Object(new float[]{2.182f, 2.182f}, 2, 1, false),
        new Object(new float[]{2.182f, 2.182f}, 0, 1, false),
        new Object(new float[]{3.273f, 3.273f}, 2, 1, false),
        new Object(new float[]{3.273f, 3.273f}, 0, 1, false),
        new Object(new float[]{4.364f, 4.364f}, 2, 1, false),
        new Object(new float[]{4.364f, 4.364f}, 0, 1, false),
        new Object(new float[]{5.455f, 5.455f}, 2, 3, false),
        new Object(new float[]{5.455f, 5.455f}, 0, 3, false),
        new Object(new float[]{6.545f, 6.545f}, 2, 1, false),
        new Object(new float[]{6.545f, 6.545f}, 0, 1, false),
        new Object(new float[]{7.636f, 7.636f}, 2, 1, false),
        new Object(new float[]{7.636f, 7.636f}, 0, 1, false),
        new Object(new float[]{8.727f, 8.727f}, 2, 3, false),
        new Object(new float[]{8.727f, 8.727f}, 0, 3, false),
        new Object(new float[]{9.818f, 9.818f}, 3, 1, false),
        new Object(new float[]{9.818f, 9.818f}, 0, 1, false),
        new Object(new float[]{10.909f, 10.909f}, 3, 1, false),
        new Object(new float[]{10.909f, 10.909f}, 0, 1, false),
        new Object(new float[]{12.0f, 12.0f}, 3, 3, false),
        new Object(new float[]{12.0f, 12.0f}, 0, 3, false),
        new Object(new float[]{13.091f, 13.091f}, 3, 1, false),
        new Object(new float[]{13.091f, 13.091f}, 0, 1, false),
        new Object(new float[]{14.182f, 14.182f}, 3, 1, false),
        new Object(new float[]{14.182f, 14.182f}, 0, 1, false),
        new Object(new float[]{15.273f, 15.273f}, 3, 3, false),
        new Object(new float[]{15.273f, 15.273f}, 0, 3, false),
        new Object(new float[]{16.364f, 16.364f}, 3, 1, false),
        new Object(new float[]{16.364f, 16.364f}, 0, 1, false),
        new Object(new float[]{18.545f, 18.545f}, 2, 3, false),
        new Object(new float[]{18.545f, 18.545f}, 0, 3, false),
        new Object(new float[]{19.636f, 19.636f}, 2, 3, false),
        new Object(new float[]{19.636f, 19.636f}, 0, 3, false),
        new Object(new float[]{20.727f, 20.727f}, 2, 1, false),
        new Object(new float[]{20.727f, 20.727f}, 0, 1, false),
        new Object(new float[]{21.818f, 21.818f}, 2, 3, false),
        new Object(new float[]{21.818f, 21.818f}, 0, 3, false),
        new Object(new float[]{22.909f, 22.909f}, 2, 3, false),
        new Object(new float[]{22.909f, 22.909f}, 0, 3, false),
        new Object(new float[]{24.0f, 24.0f}, 2, 3, false),
        new Object(new float[]{24.0f, 24.0f}, 0, 3, false),
        new Object(new float[]{25.091f, 25.091f}, 2, 1, false),
        new Object(new float[]{25.091f, 25.091f}, 0, 1, false),
        new Object(new float[]{26.182f, 26.182f}, 2, 1, false),
        new Object(new float[]{26.182f, 26.182f}, 0, 1, false),
        new Object(new float[]{27.273f, 27.273f}, 2, 1, false),
        new Object(new float[]{27.273f, 27.273f}, 0, 1, false),
        new Object(new float[]{28.364f, 28.364f}, 2, 1, false),
        new Object(new float[]{28.364f, 28.364f}, 0, 1, false),
        new Object(new float[]{29.455f, 29.455f}, 2, 1, false),
        new Object(new float[]{29.455f, 29.455f}, 0, 1, false),
        new Object(new float[]{30.545f, 30.545f}, 2, 1, false),
        new Object(new float[]{30.545f, 30.545f}, 0, 1, false),
        new Object(new float[]{31.636f, 31.636f}, 2, 1, false),
        new Object(new float[]{31.636f, 31.636f}, 0, 1, false),
        new Object(new float[]{32.727f, 32.727f}, 2, 1, false),
        new Object(new float[]{32.727f, 32.727f}, 0, 1, false),
        new Object(new float[]{33.818f, 33.818f}, 2, 3, false),
        new Object(new float[]{33.818f, 33.818f}, 0, 3, false),
        new Object(new float[]{34.091f, 34.091f}, 2, 1, false),
        new Object(new float[]{34.091f, 34.091f}, 0, 1, false),
        new Object(new float[]{34.364f, 34.364f}, 2, 1, false),
        new Object(new float[]{34.364f, 34.364f}, 0, 1, false),
        new Object(new float[]{34.636f, 34.636f}, 2, 1, false),
        new Object(new float[]{34.636f, 34.636f}, 0, 1, false),
        new Object(new float[]{36.0f, 36.0f}, 2, 3, false),
        new Object(new float[]{36.0f, 36.0f}, 1, 3, false),
        new Object(new float[]{37.091f, 37.091f}, 2, 1, false),
        new Object(new float[]{37.091f, 37.091f}, 1, 1, false),
        new Object(new float[]{38.182f, 38.182f}, 2, 3, false),
        new Object(new float[]{38.182f, 38.182f}, 1, 3, false),
        new Object(new float[]{39.273f, 39.273f}, 2, 3, false),
        new Object(new float[]{39.273f, 39.273f}, 1, 3, false),
        new Object(new float[]{40.364f, 40.364f}, 2, 3, false),
        new Object(new float[]{40.364f, 40.364f}, 1, 3, false),
        new Object(new float[]{41.455f, 41.455f}, 2, 3, false),
        new Object(new float[]{41.455f, 41.455f}, 1, 3, false),
        new Object(new float[]{42.545f, 42.545f}, 2, 1, false),
        new Object(new float[]{42.545f, 42.545f}, 1, 1, false),
        new Object(new float[]{43.636f, 43.636f}, 2, 3, false),
        new Object(new float[]{43.636f, 43.636f}, 1, 3, false),
        new Object(new float[]{44.727f, 44.727f}, 3, 3, false),
        new Object(new float[]{44.727f, 44.727f}, 1, 3, false),
        new Object(new float[]{45.818f, 45.818f}, 3, 1, false),
        new Object(new float[]{45.818f, 45.818f}, 1, 1, false),
        new Object(new float[]{46.909f, 46.909f}, 3, 3, false),
        new Object(new float[]{46.909f, 46.909f}, 1, 3, false),
        new Object(new float[]{48.0f, 48.0f}, 3, 3, false),
        new Object(new float[]{48.0f, 48.0f}, 1, 3, false),
        new Object(new float[]{49.091f, 49.091f}, 3, 1, false),
        new Object(new float[]{49.091f, 49.091f}, 1, 1, false),
        new Object(new float[]{50.182f, 50.182f}, 3, 3, false),
        new Object(new float[]{50.182f, 50.182f}, 1, 3, false),
        new Object(new float[]{51.273f, 51.273f}, 3, 3, false),
        new Object(new float[]{51.273f, 51.273f}, 1, 3, false),
        new Object(new float[]{53.454f, 53.454f}, 3, 1, false),
        new Object(new float[]{53.454f, 53.454f}, 1, 1, false),
        new Object(new float[]{54.545f, 54.545f}, 3, 1, false),
        new Object(new float[]{54.545f, 54.545f}, 1, 1, false),
        new Object(new float[]{55.636f, 55.636f}, 3, 1, false),
        new Object(new float[]{55.636f, 55.636f}, 1, 1, false),
        new Object(new float[]{56.727f, 56.727f}, 3, 1, false),
        new Object(new float[]{56.727f, 56.727f}, 1, 1, false),
        new Object(new float[]{57.818f, 57.818f}, 3, 3, false),
        new Object(new float[]{57.818f, 57.818f}, 1, 3, false),
        new Object(new float[]{58.909f, 58.909f}, 3, 1, false),
        new Object(new float[]{58.909f, 58.909f}, 1, 1, false),
        new Object(new float[]{60.0f, 60.0f}, 3, 1, false),
        new Object(new float[]{60.0f, 60.0f}, 1, 1, false),
        new Object(new float[]{61.091f, 61.091f}, 3, 1, false),
        new Object(new float[]{61.091f, 61.091f}, 1, 1, false),
        new Object(new float[]{62.182f, 62.182f}, 3, 1, false),
        new Object(new float[]{62.182f, 62.182f}, 1, 1, false),
        new Object(new float[]{63.273f, 63.273f}, 3, 1, false),
        new Object(new float[]{63.273f, 63.273f}, 1, 1, false),
        new Object(new float[]{64.364f, 64.364f}, 3, 1, false),
        new Object(new float[]{64.364f, 64.364f}, 1, 1, false),
        new Object(new float[]{65.454f, 65.454f}, 3, 1, false),
        new Object(new float[]{65.454f, 65.454f}, 1, 1, false),
        new Object(new float[]{66.545f, 66.545f}, 3, 1, false),
        new Object(new float[]{66.545f, 66.545f}, 1, 1, false),
        new Object(new float[]{67.636f, 67.636f}, 3, 1, false),
        new Object(new float[]{67.636f, 67.636f}, 1, 1, false),
        new Object(new float[]{68.727f, 68.727f}, 3, 3, false),
        new Object(new float[]{68.727f, 68.727f}, 1, 3, false),
        new Object(new float[]{69.0f, 69.0f}, 3, 1, false),
        new Object(new float[]{69.0f, 69.0f}, 1, 1, false),
        new Object(new float[]{69.273f, 69.273f}, 3, 3, false),
        new Object(new float[]{69.273f, 69.273f}, 1, 3, false),
        new Object(new float[]{69.545f, 69.545f}, 3, 3, false),
        new Object(new float[]{69.545f, 69.545f}, 1, 3, false),
        new Object(new float[]{69.818f, 69.818f}, 3, 1, false),
        //new Object(new float[]{69.818f, 69.818f}, 1, 1, false),



        //platform
        new Object(new float[]{0.0f, 17.45f}, 4, 4, false),
        new Object(new float[]{18.5f, 34.909f}, 4, 4, false),
        new Object(new float[]{36.00f, 52.00f}, 4, 4, false),
        new Object(new float[]{53.50f, 70.00f}, 4, 4, false),
        

    };


    private void Awake()
    {
        noteHandler = note.GetComponent<Note>();
        playerHandler = player.GetComponent<PlayerControl>();
        // sort the notes by time
        var watch = Stopwatch.StartNew();
        Array.Sort(objArr, new ObjectComparer());
        var elabpsedMs = watch.ElapsedMilliseconds;
        Debug.Log("Sorting took " + elabpsedMs + "ms");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerX = playerHandler.transform.position.x;
        StartCoroutine(SpawnNewItem());
        // StartCoroutine(SpawnNewPlatform());
    }

    private IEnumerator SpawnNewItem()
    {
        while (ind < objArr.Length)
        {
            if(objArr[ind].TimeStamp[0]-objArr[ind-1].TimeStamp[0] != 0){
                yield return new WaitForSeconds(objArr[ind].TimeStamp[0]-objArr[ind-1].TimeStamp[0]);
            }
            xLen = noteHandler.transform.localScale.x;

            float yPos = objArr[ind].Pos switch
            {
                0 => 4,
                1 => 1,
                2 => -1,
                4 => 0,
                _ => -4,
            };

            if (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0] != 0)
            {
                xLen = (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

            Debug.Log("obj: " + ind + " " + objArr[ind].TimeStamp[0] + " " + objArr[ind].TimeStamp[1] + " " + objArr[ind].Pos + " " + objArr[ind].Type);
            // start spawn
            switch (objArr[ind].Type)
            {
                case 0:
                    newItem = Instantiate(gravSwitch, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    if (objArr[ind].Pos == 0)
                    {
                        newItem.transform.localScale = new Vector3(1, -1, 1);
                    }
                    break;
                case 1:
                    newItem = Instantiate(note, spawnPos, Quaternion.identity);
                    if(objArr[ind].IsMain){
                        newItem.GetComponent<SpriteRenderer>().color = new Color32(6,248,230,255);
                        newItem.transform.localScale = new Vector3(1.2f, 1.2f);
                    }
                    Destroy(newItem, 3f);
                    break;
                case 2:
                    newItem = Instantiate(longNote, spawnPos, Quaternion.identity);
                    var newLongNote = newItem.GetComponent<LongNote>();
                    newLongNote.SetLength(xLen);
                    Destroy(newItem, 3f/2.46f*xLen);
                    break;
                case 3:
                    newItem = Instantiate(block, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    break;
                case 4:
                    newPlatform = Instantiate(platform, spawnPos, Quaternion.identity);
                    var newPlatform_ = newPlatform.GetComponent<platform>();
                    newPlatform_.SetLength(xLen);
                    Destroy(newPlatform, 3f/2.46f*xLen);
                    break;
            }
            ind++;
        }
    }
}