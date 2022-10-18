using System;
using System.IO;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;
using Object = Script.Object;
using JsonHelper = Script.JsonHelper;


public class L1Spawner : MonoBehaviour
{
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject bgm;
    [SerializeField] private TextAsset Jsonfile;

    private GameObject _newItem;
    private Vector3 _spawnPos;
    private Note _noteHandler;
    private PlayerControl _playerHandler;
    private BgmController _bgmHandler;
    private float _playerX, _xLen;
    private int _ind = 0;
    private Object[] objArr;
    
 
    private void Awake()
    {
        string json = Jsonfile.text;
        Debug.Log("MyJson= "+json);
        objArr= JsonHelper.FromJson<Object>(json);
		Array.Sort(objArr, new ObjectComparer());
        _noteHandler = note.GetComponent<Note>();
        _playerHandler = player.GetComponent<PlayerControl>();
        _bgmHandler = bgm.GetComponent<BgmController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _playerX = _playerHandler.transform.position.x;
        //StartCoroutine(SpawnnewItem());
    }

    void FixedUpdate()
    {
        
        if (_bgmHandler.started && _ind < objArr.Length && objArr[_ind].TimeStamp[0] <= _bgmHandler.songPosition)
        {
            //Debug.Log("ind:" + _ind);
            //Debug.Log("time:"+_bgmHandler.songPosition);
            _xLen = _noteHandler.transform.localScale.x;

            float yPos = objArr[_ind].Pos switch
            {
                0 => 0,
                1 => 1,
                2 => -1,
                _ => 0,
            };

            if (objArr[_ind].TimeStamp[1] - objArr[_ind].TimeStamp[0] != 0)
            {
                _xLen = (objArr[_ind].TimeStamp[1] - objArr[_ind].TimeStamp[0]) * _noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            _spawnPos = new Vector3(_playerX + _noteHandler.speed * (2f / Time.fixedDeltaTime) + _xLen / 2, yPos, 0);

            // start spawn
            switch (objArr[_ind].Type)
            {
                case 0:
                    _newItem = Instantiate(gravSwitch, _spawnPos, Quaternion.identity);
                    Destroy(_newItem, 3f);
                    if (objArr[_ind].Pos == 1)
                    {
                        _newItem.transform.localScale = new Vector3(1, -1, 1);
                    }
                    break;
                case 1:
                    _newItem = Instantiate(note, _spawnPos, Quaternion.identity);
                    Destroy(_newItem, 3f);
                    break;
                case 2:
                    _newItem = Instantiate(longNote, _spawnPos, Quaternion.identity);
                    var newLongNote = _newItem.GetComponent<LongNote>();
                    newLongNote.SetLength(_xLen);
                    Destroy(_newItem, 3f/2.46f*_xLen);
                    break;
                case 3:
                    _newItem = Instantiate(block, _spawnPos, Quaternion.identity);
                    Destroy(_newItem, 3f);
                    break;
            }
            _ind++;
        }
    }

    private IEnumerator SpawnnewItem()
    {
        while (_ind < objArr.Length)
        {
            yield return new WaitForSeconds(objArr[_ind].TimeStamp[0]-objArr[_ind-1].TimeStamp[0]);
            //Debug.Log("loop start:"+Time.time);
            _xLen = _noteHandler.transform.localScale.x;

            float yPos = objArr[_ind].Pos switch
            {
                0 => -4,
                1 => 4,
                2 => 0,
            };

            if (objArr[_ind].TimeStamp[1] - objArr[_ind].TimeStamp[0] != 0)
            {
                _xLen = (objArr[_ind].TimeStamp[1] - objArr[_ind].TimeStamp[0]) * _noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            _spawnPos = new Vector3(_playerX + _noteHandler.speed * (1.8f / Time.fixedDeltaTime) + _xLen / 2, yPos, 0);

            // start spawn
            switch (objArr[_ind].Type)
            {
                case 0:
                    _newItem = Instantiate(gravSwitch, _spawnPos, Quaternion.identity);
                    Destroy(_newItem, 3f);
                    if (objArr[_ind].Pos == 1)
                    {
                        _newItem.transform.localScale = new Vector3(1, -1, 1);
                    }
                    break;
                case 1:
                    _newItem = Instantiate(note, _spawnPos, Quaternion.identity);
                    Destroy(_newItem, 3f);
                    break;
                case 2:
                    _newItem = Instantiate(longNote, _spawnPos, Quaternion.identity);
                    var newLongNote = _newItem.GetComponent<LongNote>();
                    newLongNote.SetLength(_xLen);
                    Destroy(_newItem, 3f/2.46f*_xLen);
                    break;
                case 3:
                    _newItem = Instantiate(block, _spawnPos, Quaternion.identity);
                    Destroy(_newItem, 3f);
                    break;
            }
            _ind++;
            //Debug.Log("loop end:"+Time.time);
        }
    }
}
