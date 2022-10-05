using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = Script.Object;

public class tempL2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject bgm;

    private GameObject newItem, newMine;
    private Vector3 spawnPos;
    private Block blockHandler;
    private Note noteHandler;
    private PlayerControl playerHadler;
    private BgmController _bgmHandler;
    private float playerX, xLen;
    private int ind = 1;

    private Object[] objArr = 
    {
        new Object(new []{0f, 0f}, 0, 1),
new Object(new []{0.545f, 0.545f}, 1, 1),
new Object(new []{0.818f, 0.818f}, 1, 1),
new Object(new []{1.091f, 1.091f}, 0, 1),
new Object(new []{1.091f, 1.091f}, 1, 3),
new Object(new []{1.091f, 1.091f}, 2, 3),
new Object(new []{1.091f, 1.091f}, 3, 1),
new Object(new []{1.636f, 1.636f}, 1, 1),
new Object(new []{1.909f, 1.909f}, 1, 1),
new Object(new []{1.909f, 1.909f}, 2, 3),
new Object(new []{2.182f, 2.182f}, 0, 3),
new Object(new []{2.182f, 2.182f}, 1, 1),
new Object(new []{2.182f, 2.182f}, 2, 1),
new Object(new []{2.182f, 2.182f}, 3, 3),
new Object(new []{2.727f, 2.727f}, 1, 1),
new Object(new []{3.0f, 3.0f}, 1, 1),
new Object(new []{3.273f, 3.273f}, 0, 1),
new Object(new []{3.273f, 3.273f}, 1, 1),
new Object(new []{3.273f, 3.273f}, 2, 3),
new Object(new []{3.273f, 3.273f}, 3, 1),
new Object(new []{3.545f, 3.545f}, 1, 1),
new Object(new []{3.818f, 3.818f}, 1, 1),
new Object(new []{4.091f, 4.091f}, 1, 1),
new Object(new []{4.364f, 4.364f}, 0, 3),
new Object(new []{4.364f, 4.364f}, 1, 1),
new Object(new []{4.364f, 4.364f}, 2, 1),
new Object(new []{4.364f, 4.364f}, 3, 3),
new Object(new []{4.909f, 4.909f}, 1, 1),
new Object(new []{5.182f, 5.182f}, 1, 1),
new Object(new []{5.455f, 5.455f}, 0, 1),
new Object(new []{5.455f, 5.455f}, 1, 1),
new Object(new []{5.455f, 5.455f}, 2, 1),
new Object(new []{5.455f, 5.455f}, 3, 1),
new Object(new []{6.0f, 6.0f}, 1, 1),
new Object(new []{6.273f, 6.273f}, 1, 1),
new Object(new []{6.273f, 6.273f}, 2, 1),
new Object(new []{6.545f, 6.545f}, 0, 3),
new Object(new []{6.545f, 6.545f}, 1, 1),
new Object(new []{6.545f, 6.545f}, 2, 3),
new Object(new []{6.545f, 6.545f}, 3, 3),
new Object(new []{7.091f, 7.091f}, 1, 1),
new Object(new []{7.364f, 7.364f}, 1, 1),
new Object(new []{7.636f, 7.636f}, 0, 1),
new Object(new []{7.636f, 7.636f}, 1, 1),
new Object(new []{7.636f, 7.636f}, 2, 1),
new Object(new []{7.636f, 7.636f}, 3, 1),
new Object(new []{7.909f, 7.909f}, 1, 1),
new Object(new []{8.182f, 8.182f}, 1, 1),
new Object(new []{8.455f, 8.455f}, 1, 1),
new Object(new []{8.727f, 8.727f}, 0, 1),
new Object(new []{8.727f, 8.727f}, 1, 1),
new Object(new []{8.727f, 8.727f}, 2, 1),
new Object(new []{8.727f, 8.727f}, 3, 1),
new Object(new []{9.273f, 9.273f}, 1, 1),
new Object(new []{9.545f, 9.545f}, 1, 1),
new Object(new []{9.818f, 9.818f}, 0, 3),
new Object(new []{9.818f, 9.818f}, 1, 1),
new Object(new []{9.818f, 9.818f}, 2, 3),
new Object(new []{9.818f, 9.818f}, 3, 3),
new Object(new []{10.364f, 10.364f}, 1, 1),
new Object(new []{10.636f, 10.636f}, 1, 3),
new Object(new []{10.636f, 10.636f}, 2, 3),
new Object(new []{10.909f, 10.909f}, 0, 1),
new Object(new []{10.909f, 10.909f}, 1, 1),
new Object(new []{10.909f, 10.909f}, 2, 1),
new Object(new []{10.909f, 10.909f}, 3, 1),
new Object(new []{11.455f, 11.455f}, 1, 1),
new Object(new []{11.727f, 11.727f}, 1, 1),
new Object(new []{12.0f, 12.0f}, 0, 3),
new Object(new []{12.0f, 12.0f}, 1, 3),
new Object(new []{12.0f, 12.0f}, 2, 1),
new Object(new []{12.0f, 12.0f}, 3, 3),
new Object(new []{12.273f, 12.273f}, 1, 1),
new Object(new []{12.545f, 12.545f}, 1, 1),
new Object(new []{12.818f, 12.818f}, 1, 1),
new Object(new []{13.091f, 13.091f}, 0, 3),
new Object(new []{13.091f, 13.091f}, 1, 1),
new Object(new []{13.091f, 13.091f}, 2, 3),
new Object(new []{13.091f, 13.091f}, 3, 3),
new Object(new []{13.636f, 13.636f}, 1, 1),
new Object(new []{13.909f, 13.909f}, 1, 1),
new Object(new []{14.182f, 14.182f}, 0, 3),
new Object(new []{14.182f, 14.182f}, 1, 1),
new Object(new []{14.182f, 14.182f}, 2, 1),
new Object(new []{14.182f, 14.182f}, 3, 3),
new Object(new []{14.727f, 14.727f}, 1, 1),
new Object(new []{15.0f, 15.0f}, 1, 1),
new Object(new []{15.0f, 15.0f}, 2, 1),
new Object(new []{15.273f, 15.273f}, 0, 3),
new Object(new []{15.273f, 15.273f}, 1, 3),
new Object(new []{15.273f, 15.273f}, 2, 3),
new Object(new []{15.273f, 15.273f}, 3, 3),
new Object(new []{15.818f, 15.818f}, 1, 1),
new Object(new []{16.091f, 16.091f}, 1, 1),
new Object(new []{16.364f, 16.364f}, 0, 3),
new Object(new []{16.364f, 16.364f}, 1, 1),
new Object(new []{16.364f, 16.364f}, 2, 1),
new Object(new []{16.364f, 16.364f}, 3, 3),
new Object(new []{16.636f, 16.636f}, 1, 1),
new Object(new []{16.909f, 16.909f}, 1, 1),
new Object(new []{17.182f, 17.182f}, 1, 1),
new Object(new []{17.455f, 17.455f}, 0, 0),
new Object(new []{17.455f, 17.455f}, 1, 0),
new Object(new []{17.455f, 17.455f}, 2, 0),
new Object(new []{17.455f, 17.455f}, 3, 0),
new Object(new []{17.659f, 17.659f}, 0, 1),
new Object(new []{17.727f, 17.727f}, 0, 1),
new Object(new []{18.0f, 18.0f}, 0, 3),
new Object(new []{18.273f, 18.273f}, 0, 3),
new Object(new []{18.545f, 18.545f}, 0, 3),
new Object(new []{18.545f, 18.545f}, 1, 3),
new Object(new []{18.545f, 18.545f}, 2, 3),
new Object(new []{18.545f, 18.545f}, 3, 3),
new Object(new []{18.818f, 18.818f}, 2, 3),
new Object(new []{19.091f, 19.091f}, 2, 3),
new Object(new []{19.364f, 19.364f}, 0, 3),
new Object(new []{19.364f, 19.364f}, 2, 1),
new Object(new []{19.636f, 19.636f}, 0, 1),
new Object(new []{19.636f, 19.636f}, 1, 1),
new Object(new []{19.636f, 19.636f}, 2, 1),
new Object(new []{19.636f, 19.636f}, 3, 1),
new Object(new []{19.909f, 19.909f}, 2, 1),
new Object(new []{20.182f, 20.182f}, 2, 1),
new Object(new []{20.455f, 20.455f}, 2, 1),
new Object(new []{20.727f, 20.727f}, 0, 1),
new Object(new []{20.727f, 20.727f}, 1, 1),
new Object(new []{20.727f, 20.727f}, 2, 1),
new Object(new []{20.727f, 20.727f}, 3, 1),
new Object(new []{21.0f, 21.0f}, 2, 1),
new Object(new []{21.273f, 21.273f}, 2, 1),
new Object(new []{21.545f, 21.545f}, 2, 1),
new Object(new []{21.818f, 21.818f}, 0, 3),
new Object(new []{21.818f, 21.818f}, 1, 1),
new Object(new []{21.818f, 21.818f}, 2, 1),
new Object(new []{21.818f, 21.818f}, 3, 1),
new Object(new []{22.091f, 22.091f}, 2, 1),
new Object(new []{22.364f, 22.364f}, 2, 1),
new Object(new []{22.636f, 22.636f}, 2, 1),
new Object(new []{22.909f, 22.909f}, 0, 1),
new Object(new []{22.909f, 22.909f}, 1, 1),
new Object(new []{22.909f, 22.909f}, 2, 1),
new Object(new []{22.909f, 22.909f}, 3, 1),
new Object(new []{23.182f, 23.182f}, 2, 3),
new Object(new []{23.455f, 23.455f}, 2, 3),
new Object(new []{23.727f, 23.727f}, 0, 1),
new Object(new []{23.727f, 23.727f}, 2, 1),
new Object(new []{24.0f, 24.0f}, 0, 1),
new Object(new []{24.0f, 24.0f}, 1, 3),
new Object(new []{24.0f, 24.0f}, 2, 1),
new Object(new []{24.0f, 24.0f}, 3, 3),
new Object(new []{24.273f, 24.273f}, 2, 3),
new Object(new []{24.545f, 24.545f}, 2, 1),
new Object(new []{24.818f, 24.818f}, 2, 1),
new Object(new []{25.091f, 25.091f}, 0, 1),
new Object(new []{25.091f, 25.091f}, 1, 3),
new Object(new []{25.091f, 25.091f}, 2, 1),
new Object(new []{25.091f, 25.091f}, 3, 3),
new Object(new []{25.364f, 25.364f}, 2, 1),
new Object(new []{25.636f, 25.636f}, 2, 3),
new Object(new []{25.909f, 25.909f}, 2, 3),
new Object(new []{26.182f, 26.182f}, 0, 3),
new Object(new []{26.182f, 26.182f}, 1, 1),
new Object(new []{26.182f, 26.182f}, 2, 1),
new Object(new []{26.182f, 26.182f}, 3, 1),
new Object(new []{26.455f, 26.455f}, 2, 1),
new Object(new []{26.727f, 26.727f}, 2, 1),
new Object(new []{27.0f, 27.0f}, 2, 3),
new Object(new []{27.273f, 27.273f}, 0, 3),
new Object(new []{27.273f, 27.273f}, 1, 1),
new Object(new []{27.273f, 27.273f}, 2, 1),
new Object(new []{27.273f, 27.273f}, 3, 1),
new Object(new []{27.545f, 27.545f}, 2, 3),
new Object(new []{27.818f, 27.818f}, 2, 1),
new Object(new []{28.091f, 28.091f}, 0, 1),
new Object(new []{28.091f, 28.091f}, 2, 3),
new Object(new []{28.364f, 28.364f}, 0, 1),
new Object(new []{28.364f, 28.364f}, 1, 3),
new Object(new []{28.364f, 28.364f}, 2, 3),
new Object(new []{28.364f, 28.364f}, 3, 3),
new Object(new []{28.636f, 28.636f}, 2, 1),
new Object(new []{28.909f, 28.909f}, 2, 3),
new Object(new []{29.182f, 29.182f}, 2, 1),
new Object(new []{29.455f, 29.455f}, 0, 1),
new Object(new []{29.455f, 29.455f}, 1, 3),
new Object(new []{29.455f, 29.455f}, 2, 3),
new Object(new []{29.455f, 29.455f}, 3, 3),
new Object(new []{29.727f, 29.727f}, 2, 1),
new Object(new []{30.0f, 30.0f}, 2, 1),
new Object(new []{30.273f, 30.273f}, 2, 1),
new Object(new []{30.545f, 30.545f}, 0, 3),
new Object(new []{30.545f, 30.545f}, 1, 1),
new Object(new []{30.545f, 30.545f}, 2, 1),
new Object(new []{30.545f, 30.545f}, 3, 1),
new Object(new []{30.818f, 30.818f}, 2, 3),
new Object(new []{31.091f, 31.091f}, 2, 3),
new Object(new []{31.364f, 31.364f}, 2, 1),
new Object(new []{31.636f, 31.636f}, 0, 1),
new Object(new []{31.636f, 31.636f}, 1, 1),
new Object(new []{31.636f, 31.636f}, 2, 1),
new Object(new []{31.636f, 31.636f}, 3, 1),
new Object(new []{31.909f, 31.909f}, 2, 1),
new Object(new []{32.182f, 32.182f}, 2, 1),
new Object(new []{32.455f, 32.455f}, 0, 1),
new Object(new []{32.455f, 32.455f}, 2, 1),
new Object(new []{32.727f, 32.727f}, 0, 1),
new Object(new []{32.727f, 32.727f}, 1, 3),
new Object(new []{32.727f, 32.727f}, 2, 1),
new Object(new []{32.727f, 32.727f}, 3, 3),
new Object(new []{33.0f, 33.0f}, 2, 1),
new Object(new []{33.273f, 33.273f}, 2, 1),
new Object(new []{33.545f, 33.545f}, 2, 3),
new Object(new []{33.818f, 33.818f}, 0, 1),
new Object(new []{33.818f, 33.818f}, 1, 3),
new Object(new []{33.818f, 33.818f}, 2, 3),
new Object(new []{33.818f, 33.818f}, 3, 3),
new Object(new []{34.091f, 34.091f}, 0, 3),
new Object(new []{34.091f, 34.091f}, 1, 1),
new Object(new []{34.091f, 34.091f}, 2, 1),
new Object(new []{34.091f, 34.091f}, 3, 1),
new Object(new []{34.364f, 34.364f}, 0, 3),
new Object(new []{34.364f, 34.364f}, 1, 3),
new Object(new []{34.364f, 34.364f}, 2, 3),
new Object(new []{34.364f, 34.364f}, 3, 3),
new Object(new []{34.636f, 34.636f}, 0, 3),
new Object(new []{34.636f, 34.636f}, 1, 3),
new Object(new []{34.636f, 34.636f}, 2, 1),
new Object(new []{34.636f, 34.636f}, 3, 3),
new Object(new []{34.909f, 34.909f}, 0, 0),
new Object(new []{34.909f, 34.909f}, 1, 0),
new Object(new []{34.909f, 34.909f}, 2, 0),
new Object(new []{34.909f, 34.909f}, 3, 0),
new Object(new []{35.455f, 35.455f}, 0, 1),
new Object(new []{35.727f, 35.727f}, 0, 1),
new Object(new []{36.0f, 36.0f}, 0, 1),
new Object(new []{36.0f, 36.0f}, 1, 3),
new Object(new []{36.0f, 36.0f}, 2, 3),
new Object(new []{36.0f, 36.0f}, 3, 1),
new Object(new []{36.545f, 36.545f}, 0, 1),
new Object(new []{36.818f, 36.818f}, 0, 1),
new Object(new []{36.818f, 36.818f}, 3, 1),
new Object(new []{37.091f, 37.091f}, 0, 1),
new Object(new []{37.091f, 37.091f}, 1, 1),
new Object(new []{37.091f, 37.091f}, 2, 1),
new Object(new []{37.091f, 37.091f}, 3, 3),
new Object(new []{37.636f, 37.636f}, 0, 1),
new Object(new []{37.909f, 37.909f}, 0, 1),
new Object(new []{38.182f, 38.182f}, 0, 1),
new Object(new []{38.182f, 38.182f}, 1, 3),
new Object(new []{38.182f, 38.182f}, 2, 3),
new Object(new []{38.182f, 38.182f}, 3, 1),
new Object(new []{38.455f, 38.455f}, 0, 1),
new Object(new []{38.727f, 38.727f}, 0, 3),
new Object(new []{39.0f, 39.0f}, 0, 1),
new Object(new []{39.273f, 39.273f}, 0, 1),
new Object(new []{39.273f, 39.273f}, 1, 3),
new Object(new []{39.273f, 39.273f}, 2, 3),
new Object(new []{39.273f, 39.273f}, 3, 1),
new Object(new []{39.818f, 39.818f}, 0, 1),
new Object(new []{40.091f, 40.091f}, 0, 1),
new Object(new []{40.364f, 40.364f}, 0, 3),
new Object(new []{40.364f, 40.364f}, 1, 1),
new Object(new []{40.364f, 40.364f}, 2, 1),
new Object(new []{40.364f, 40.364f}, 3, 1),
new Object(new []{40.909f, 40.909f}, 0, 1),
new Object(new []{41.182f, 41.182f}, 0, 1),
new Object(new []{41.182f, 41.182f}, 3, 1),
new Object(new []{41.455f, 41.455f}, 0, 1),
new Object(new []{41.455f, 41.455f}, 1, 1),
new Object(new []{41.455f, 41.455f}, 2, 1),
new Object(new []{41.455f, 41.455f}, 3, 3),
new Object(new []{42.0f, 42.0f}, 0, 1),
new Object(new []{42.273f, 42.273f}, 0, 1),
new Object(new []{42.545f, 42.545f}, 0, 1),
new Object(new []{42.545f, 42.545f}, 1, 3),
new Object(new []{42.545f, 42.545f}, 2, 3),
new Object(new []{42.545f, 42.545f}, 3, 1),
new Object(new []{42.818f, 42.818f}, 0, 1),
new Object(new []{43.091f, 43.091f}, 0, 1),
new Object(new []{43.364f, 43.364f}, 0, 3),
new Object(new []{43.636f, 43.636f}, 0, 1),
new Object(new []{43.636f, 43.636f}, 1, 3),
new Object(new []{43.636f, 43.636f}, 2, 3),
new Object(new []{43.636f, 43.636f}, 3, 1),
new Object(new []{44.182f, 44.182f}, 0, 1),
new Object(new []{44.455f, 44.455f}, 0, 1),
new Object(new []{44.727f, 44.727f}, 0, 3),
new Object(new []{44.727f, 44.727f}, 1, 3),
new Object(new []{44.727f, 44.727f}, 2, 3),
new Object(new []{44.727f, 44.727f}, 3, 3),
new Object(new []{45.273f, 45.273f}, 0, 1),
new Object(new []{45.545f, 45.545f}, 0, 1),
new Object(new []{45.545f, 45.545f}, 3, 1),
new Object(new []{45.818f, 45.818f}, 0, 1),
new Object(new []{45.818f, 45.818f}, 1, 3),
new Object(new []{45.818f, 45.818f}, 2, 3),
new Object(new []{45.818f, 45.818f}, 3, 1),
new Object(new []{46.364f, 46.364f}, 0, 1),
new Object(new []{46.636f, 46.636f}, 0, 1),
new Object(new []{46.909f, 46.909f}, 0, 1),
new Object(new []{46.909f, 46.909f}, 1, 3),
new Object(new []{46.909f, 46.909f}, 2, 3),
new Object(new []{46.909f, 46.909f}, 3, 3),
new Object(new []{47.182f, 47.182f}, 0, 1),
new Object(new []{47.454f, 47.454f}, 0, 1),
new Object(new []{47.727f, 47.727f}, 0, 1),
new Object(new []{48.0f, 48.0f}, 0, 1),
new Object(new []{48.0f, 48.0f}, 1, 3),
new Object(new []{48.0f, 48.0f}, 2, 3),
new Object(new []{48.0f, 48.0f}, 3, 1),
new Object(new []{48.545f, 48.545f}, 0, 1),
new Object(new []{48.818f, 48.818f}, 0, 1),
new Object(new []{49.091f, 49.091f}, 0, 1),
new Object(new []{49.091f, 49.091f}, 1, 3),
new Object(new []{49.091f, 49.091f}, 2, 3),
new Object(new []{49.091f, 49.091f}, 3, 3),
new Object(new []{49.636f, 49.636f}, 0, 1),
new Object(new []{49.909f, 49.909f}, 0, 3),
new Object(new []{49.909f, 49.909f}, 3, 1),
new Object(new []{50.182f, 50.182f}, 0, 1),
new Object(new []{50.182f, 50.182f}, 1, 1),
new Object(new []{50.182f, 50.182f}, 2, 1),
new Object(new []{50.182f, 50.182f}, 3, 1),
new Object(new []{50.727f, 50.727f}, 0, 1),
new Object(new []{51.0f, 51.0f}, 0, 1),
new Object(new []{51.273f, 51.273f}, 0, 3),
new Object(new []{51.273f, 51.273f}, 1, 3),
new Object(new []{51.273f, 51.273f}, 2, 3),
new Object(new []{51.273f, 51.273f}, 3, 1),
new Object(new []{51.545f, 51.545f}, 0, 3),
new Object(new []{51.818f, 51.818f}, 0, 1),
new Object(new []{52.091f, 52.091f}, 0, 1),
new Object(new []{52.364f, 52.364f}, 0, 0),
new Object(new []{52.364f, 52.364f}, 1, 0),
new Object(new []{52.364f, 52.364f}, 2, 0),
new Object(new []{52.364f, 52.364f}, 3, 0),
new Object(new []{52.568f, 52.568f}, 1, 3),
new Object(new []{52.636f, 52.636f}, 1, 1),
new Object(new []{52.909f, 52.909f}, 1, 3),
new Object(new []{53.182f, 53.182f}, 1, 3),
new Object(new []{53.454f, 53.454f}, 0, 3),
new Object(new []{53.454f, 53.454f}, 1, 3),
new Object(new []{53.454f, 53.454f}, 2, 3),
new Object(new []{53.454f, 53.454f}, 3, 1),
new Object(new []{53.727f, 53.727f}, 3, 1),
new Object(new []{54.0f, 54.0f}, 3, 1),
new Object(new []{54.273f, 54.273f}, 1, 3),
new Object(new []{54.273f, 54.273f}, 3, 1),
new Object(new []{54.545f, 54.545f}, 0, 1),
new Object(new []{54.545f, 54.545f}, 1, 3),
new Object(new []{54.545f, 54.545f}, 2, 1),
new Object(new []{54.545f, 54.545f}, 3, 1),
new Object(new []{54.818f, 54.818f}, 3, 1),
new Object(new []{55.091f, 55.091f}, 3, 1),
new Object(new []{55.364f, 55.364f}, 3, 1),
new Object(new []{55.636f, 55.636f}, 0, 1),
new Object(new []{55.636f, 55.636f}, 1, 1),
new Object(new []{55.636f, 55.636f}, 2, 1),
new Object(new []{55.636f, 55.636f}, 3, 3),
new Object(new []{55.909f, 55.909f}, 3, 1),
new Object(new []{56.182f, 56.182f}, 3, 3),
new Object(new []{56.454f, 56.454f}, 3, 1),
new Object(new []{56.727f, 56.727f}, 0, 3),
new Object(new []{56.727f, 56.727f}, 1, 1),
new Object(new []{56.727f, 56.727f}, 2, 3),
new Object(new []{56.727f, 56.727f}, 3, 1),
new Object(new []{57.0f, 57.0f}, 3, 1),
new Object(new []{57.273f, 57.273f}, 3, 1),
new Object(new []{57.545f, 57.545f}, 3, 3),
new Object(new []{57.818f, 57.818f}, 0, 3),
new Object(new []{57.818f, 57.818f}, 1, 1),
new Object(new []{57.818f, 57.818f}, 2, 3),
new Object(new []{57.818f, 57.818f}, 3, 1),
new Object(new []{58.091f, 58.091f}, 3, 3),
new Object(new []{58.364f, 58.364f}, 3, 1),
new Object(new []{58.636f, 58.636f}, 1, 1),
new Object(new []{58.636f, 58.636f}, 3, 1),
new Object(new []{58.909f, 58.909f}, 0, 3),
new Object(new []{58.909f, 58.909f}, 1, 1),
new Object(new []{58.909f, 58.909f}, 2, 3),
new Object(new []{58.909f, 58.909f}, 3, 1),
new Object(new []{59.182f, 59.182f}, 3, 1),
new Object(new []{59.454f, 59.454f}, 3, 1),
new Object(new []{59.727f, 59.727f}, 3, 1),
new Object(new []{60.0f, 60.0f}, 0, 3),
new Object(new []{60.0f, 60.0f}, 1, 3),
new Object(new []{60.0f, 60.0f}, 2, 3),
new Object(new []{60.0f, 60.0f}, 3, 1),
new Object(new []{60.273f, 60.273f}, 3, 3),
new Object(new []{60.545f, 60.545f}, 3, 1),
new Object(new []{60.818f, 60.818f}, 3, 1),
new Object(new []{61.091f, 61.091f}, 0, 3),
new Object(new []{61.091f, 61.091f}, 1, 1),
new Object(new []{61.091f, 61.091f}, 2, 3),
new Object(new []{61.091f, 61.091f}, 3, 1),
new Object(new []{61.364f, 61.364f}, 3, 1),
new Object(new []{61.636f, 61.636f}, 3, 1),
new Object(new []{61.909f, 61.909f}, 3, 3),
new Object(new []{62.182f, 62.182f}, 0, 3),
new Object(new []{62.182f, 62.182f}, 1, 1),
new Object(new []{62.182f, 62.182f}, 2, 3),
new Object(new []{62.182f, 62.182f}, 3, 1),
new Object(new []{62.454f, 62.454f}, 3, 1),
new Object(new []{62.727f, 62.727f}, 3, 1),
new Object(new []{63.0f, 63.0f}, 1, 1),
new Object(new []{63.0f, 63.0f}, 3, 1),
new Object(new []{63.273f, 63.273f}, 0, 1),
new Object(new []{63.273f, 63.273f}, 1, 3),
new Object(new []{63.273f, 63.273f}, 2, 1),
new Object(new []{63.273f, 63.273f}, 3, 1),
new Object(new []{63.545f, 63.545f}, 3, 3),
new Object(new []{63.818f, 63.818f}, 3, 1),
new Object(new []{64.091f, 64.091f}, 3, 1),
new Object(new []{64.364f, 64.364f}, 0, 3),
new Object(new []{64.364f, 64.364f}, 1, 1),
new Object(new []{64.364f, 64.364f}, 2, 3),
new Object(new []{64.364f, 64.364f}, 3, 1),
new Object(new []{64.636f, 64.636f}, 3, 1),
new Object(new []{64.909f, 64.909f}, 3, 1),
new Object(new []{65.182f, 65.182f}, 3, 1),
new Object(new []{65.454f, 65.454f}, 0, 3),
new Object(new []{65.454f, 65.454f}, 1, 3),
new Object(new []{65.454f, 65.454f}, 2, 3),
new Object(new []{65.454f, 65.454f}, 3, 1),
new Object(new []{65.727f, 65.727f}, 3, 1),
new Object(new []{66.0f, 66.0f}, 3, 1),
new Object(new []{66.273f, 66.273f}, 3, 3),
new Object(new []{66.545f, 66.545f}, 0, 3),
new Object(new []{66.545f, 66.545f}, 1, 3),
new Object(new []{66.545f, 66.545f}, 2, 3),
new Object(new []{66.545f, 66.545f}, 3, 1),
new Object(new []{66.818f, 66.818f}, 3, 1),
new Object(new []{67.091f, 67.091f}, 3, 1),
new Object(new []{67.364f, 67.364f}, 1, 1),
new Object(new []{67.364f, 67.364f}, 3, 1),
new Object(new []{67.636f, 67.636f}, 0, 1),
new Object(new []{67.636f, 67.636f}, 1, 1),
new Object(new []{67.636f, 67.636f}, 2, 1),
new Object(new []{67.636f, 67.636f}, 3, 1),
new Object(new []{67.909f, 67.909f}, 3, 1),
new Object(new []{68.182f, 68.182f}, 3, 1),
new Object(new []{68.454f, 68.454f}, 3, 1),
new Object(new []{68.727f, 68.727f}, 0, 1),
new Object(new []{68.727f, 68.727f}, 1, 1),
new Object(new []{68.727f, 68.727f}, 2, 1),
new Object(new []{68.727f, 68.727f}, 3, 3),
new Object(new []{69.0f, 69.0f}, 0, 1),
new Object(new []{69.0f, 69.0f}, 1, 1),
new Object(new []{69.0f, 69.0f}, 2, 1),
new Object(new []{69.0f, 69.0f}, 3, 1),
new Object(new []{69.273f, 69.273f}, 0, 3),
new Object(new []{69.273f, 69.273f}, 1, 3),
new Object(new []{69.273f, 69.273f}, 2, 3),
new Object(new []{69.273f, 69.273f}, 3, 1),
new Object(new []{69.545f, 69.545f}, 0, 3),
new Object(new []{69.545f, 69.545f}, 1, 3),
new Object(new []{69.545f, 69.545f}, 2, 3),
new Object(new []{69.545f, 69.545f}, 3, 1),
new Object(new []{69.818f, 69.818f}, 0, 3),
new Object(new []{69.818f, 69.818f}, 1, 3),
new Object(new []{69.818f, 69.818f}, 2, 3),
new Object(new []{69.818f, 69.818f}, 3, 3),
    };

    private void Awake()
    {
        noteHandler = note.GetComponent<Note>();
        playerHadler = player.GetComponent<PlayerControl>();
        _bgmHandler = bgm.GetComponent<BgmController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerX = playerHadler.transform.position.x;
        //StartCoroutine(SpawnNewItem());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("song position:"+ _bgmHandler.songPosition);
        Debug.Log("ind:"+ ind);
        if (_bgmHandler.started && ind < objArr.Length && objArr[ind].TimeStamp[0] <= _bgmHandler.songPosition + 2)
        {
            xLen = noteHandler.transform.localScale.x;

            float yPos = objArr[ind].Pos switch
            {
                0 => 4,
                1 => 1,
                2 => -1,
                3 => -4,
                _ => 0,
            };

            if (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0] != 0)
            {
                xLen = (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

            // start spawn
            switch (objArr[ind].Type)
            {
                // gravSwitch
                case 0:
                    newItem = Instantiate(gravSwitch, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    if (objArr[ind].Pos == 1)
                    {
                        newItem.transform.localScale = new Vector3(1, -1, 1);
                    }
                    break;
                // short note
                case 1:
                    newItem = Instantiate(note, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    break;
                // long note
                case 2:
                    newItem = Instantiate(longNote, spawnPos, Quaternion.identity);
                    var newLongNote = newItem.GetComponent<LongNote>();
                    newLongNote.SetLength(xLen);
                    Destroy(newItem, 3f/2.46f*xLen);
                    break;
                //block
                case 3:
                    newItem = Instantiate(block, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    break;
            }
            ind++;
        }
        
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
                3 => -4,
                _ => 0,
            };

            if (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0] != 0)
            {
                xLen = (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

            // start spawn
            switch (objArr[ind].Type)
            {
                // gravSwitch
                case 0:
                    newItem = Instantiate(gravSwitch, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    if (objArr[ind].Pos == 1)
                    {
                        newItem.transform.localScale = new Vector3(1, -1, 1);
                    }
                    break;
                // short note
                case 1:
                    newItem = Instantiate(note, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    break;
                // long note
                case 2:
                    newItem = Instantiate(longNote, spawnPos, Quaternion.identity);
                    var newLongNote = newItem.GetComponent<LongNote>();
                    newLongNote.SetLength(xLen);
                    Destroy(newItem, 3f/2.46f*xLen);
                    break;
                //block
                case 3:
                    newItem = Instantiate(block, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    break;
            }
            ind++;
        }
    }
}
