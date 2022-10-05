using System.Collections;
using UnityEngine;
using Object = Script.Object;


public class GlobalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject bgm;

    private GameObject _newItem, newPlatform;
    private Vector3 _spawnPos;
    private Note _noteHandler;
    private PlayerControl _playerHandler;
    private BgmController _bgmHandler;
    private float _playerX, _xLen;
    private int _ind = 1;
    

    private Object[] objArr = 
    {
        new Object(new []{ 0.0f, 0.0f }, 0, 0, false), // index zero doesn't spawn
        new Object(new []{ 0.0f, 22.5f }, 2, 4, false),
        new Object(new []{ 0.0f, 0.0f }, 0, 1, true),
        new Object(new []{ 0.75f, 0.75f },0, 1, true), 
        new Object(new []{ 1.5f, 1.5f }, 0,1, true), 
        new Object(new []{ 2.25f, 2.25f }, 0,1, true), 
        new Object(new []{ 3.0f, 3.0f }, 0,1, true), 
        new Object(new []{ 3.75f, 3.75f },0,1, true), 
        new Object(new []{ 4.5f, 4.5f }, 0,1, true), 
        new Object(new []{ 5.25f, 5.25f }, 0,1, true), 
        new Object(new []{5.5f, 5.5f}, 1, 3, false),
        new Object(new []{ 6.0f, 6.0f }, 0,1, true), 
        new Object(new []{ 6.75f, 6.75f }, 0,1, true), 
        new Object(new []{ 7.5f, 7.5f }, 0,1, true), 
        new Object(new []{ 8.25f, 8.25f },0,1, true), 
        new Object(new []{ 9.0f, 9.0f }, 0,1, true), 
        new Object(new []{ 9.75f, 9.75f }, 0,1, true), 
        new Object(new []{ 10.5f, 10.5f }, 0,1, true), 
        new Object(new []{10.5f, 10.5f}, 1, 3, false), 
        new Object(new []{ 11.25f, 11.25f }, 0,1, true), 
        new Object(new []{ 12.0f, 12.0f }, 0,1, true), 
        new Object(new []{ 12.75f, 12.75f },0,1, true), 
        new Object(new []{ 13.5f, 13.5f }, 0,1, true), 
        new Object(new []{ 14.25f, 14.25f }, 0,1, true), 
        new Object(new []{ 15.0f, 15.0f }, 0,1, true), 
        new Object(new []{15.5f, 15.5f}, 1, 3, false),
        new Object(new []{ 15.75f, 15.75f }, 0,1, true), 
        new Object(new []{ 16.5f, 16.5f },0,1, true), 
        new Object(new []{ 17.25f, 17.25f }, 0,1, true), 
        new Object(new []{ 18.0f, 18.0f }, 0,1, true), 
        new Object(new []{ 18.75f, 18.75f }, 0,1, true), 
        new Object(new []{ 19.5f, 19.5f }, 0,1, true), 
        new Object(new []{ 20.25f, 20.25f },0,1, true), 
        new Object(new []{20.5f, 20.5f}, 1, 3, false),
        new Object(new []{ 21.0f, 22.35f }, 0,2, true), 
        new Object(new []{ 22.77f, 22.77f }, 0,0, true),
        new Object(new []{ 23.67f, 23.67f }, 1,1, true),
        new Object(new []{ 24.0f, 31.5f }, 2, 4, false), 
        new Object(new []{ 24.27f, 24.27f }, 1,1, true), 
        new Object(new []{ 24.87f, 24.87f },1,1, true), 
        new Object(new []{ 25.47f, 25.47f }, 1,1, true), 
        new Object(new []{25.5f, 25.5f}, 0, 3, false),
        new Object(new []{ 26.07f, 26.07f }, 1,1, true), 
        new Object(new []{ 26.67f, 26.67f }, 1,1, true), 
        new Object(new []{ 27.27f, 27.27f }, 1,1, true), 
        new Object(new []{ 27.87f, 27.87f },1,1, true), 
        new Object(new []{28.0f, 28.0f}, 0, 3, false),
        new Object(new []{ 28.47f, 28.47f }, 1,1, true), 
        new Object(new []{ 29.07f, 29.07f }, 1,1, true), 
        new Object(new []{ 29.6f, 31.64f }, 1,2, true), 
        new Object(new []{30.5f, 30.5f}, 0, 3, false),
        new Object(new []{ 32.07f, 32.07f }, 1,0, true), 
        new Object(new []{ 33.0f, 41f }, 2, 4, false),
        new Object(new []{ 33.27f, 33.27f },0,1, true), 
        new Object(new []{ 33.87f, 33.87f }, 0,1, true), 
        new Object(new []{ 34.4f, 41.0f }, 0,2, true), 
        new Object(new []{35.5f, 35.5f}, 1, 3, false), 
        new Object(new []{38.0f, 38.0f}, 1, 3, false),
        new Object(new []{ 41.37f, 41.37f }, 0,0, true), 
        new Object(new []{ 42.5f, 50.0f }, 2, 4, false),
        new Object(new []{ 41.6f, 43.64f }, 1,2, true), 
        new Object(new []{43.0f, 43.0f}, 0, 3, false),
        new Object(new []{ 44.05f, 46.05f },1,2, true), 
        new Object(new []{ 46.47f, 46.47f }, 1,1, true), 
        new Object(new []{ 46.77f, 46.77f }, 1,1, true), 
        new Object(new []{ 47.07f, 47.07f }, 1,1, true), 
        new Object(new []{ 47.37f, 47.37f }, 1,1, true), 
        new Object(new []{ 47.67f, 47.67f },1,1, true), 
        new Object(new []{ 47.97f, 47.97f }, 1,1, true), 
        new Object(new []{48.0f, 48.0f}, 0, 3, false),
        new Object(new []{ 48.27f, 48.27f }, 1,1, true), 
        new Object(new []{ 48.57f, 48.57f }, 1,1, true), 
        new Object(new []{ 48.87f, 48.87f }, 1,1, true), 
        new Object(new []{ 49.17f, 49.17f },1,1, true), 
        new Object(new []{ 49.47f, 49.47f }, 1,1, true), 
        new Object(new []{ 49.77f, 49.77f }, 1,1, true), 
        new Object(new []{ 50.07f, 50.07f }, 1,1, true), 
        new Object(new []{ 50.37f, 50.37f }, 1,1, true), 
        new Object(new []{ 50.67f, 50.67f },1,0, true), 
        new Object(new []{ 51.5f, 53.0f }, 2, 4, false),
        new Object(new []{ 51.2f, 53.24f }, 0,2, true), 
        new Object(new []{52.5f, 52.5f}, 1, 3, false),
        new Object(new []{ 53.67f, 53.67f }, 0,0, true), 
        new Object(new []{ 54.5f, 57.0f }, 2, 4, false),
        new Object(new []{ 53.9f, 55.64f }, 1,2, true), 
        new Object(new []{56.0f, 56.0f}, 0, 3, false),
        new Object(new []{ 56.07f, 56.07f }, 1,1, true), 
        new Object(new []{ 56.3f, 57.14f },1,2, true), 
        new Object(new []{ 57.42f, 57.42f }, 1,0, true), 
        new Object(new []{ 58.0f, 60.0f }, 2, 4, false),
        new Object(new []{ 58.47f, 58.47f }, 0,1, true), 
        new Object(new []{59.0f, 59.0f}, 1, 3, false),
        new Object(new []{ 59.07f, 59.07f }, 0,1, true), 
        new Object(new []{ 59.67f, 59.67f }, 0,1, true), 
        new Object(new []{ 60.87f, 60.87f },0,0, true), 
        new Object(new []{ 61.6f, 70.0f }, 2, 4, false),
        new Object(new []{ 62.07f, 62.07f }, 1,1, true), 
        new Object(new []{62.5f, 62.5f}, 0, 3, false),
        new Object(new []{ 62.67f, 62.67f }, 1,1, true), 
        new Object(new []{ 63.27f, 63.27f }, 1,1, true), 
        new Object(new []{ 63.87f, 63.87f }, 1,1, true), 
        new Object(new []{ 64.47f, 64.47f },1,1, true), 
        new Object(new []{65.0f, 65.0f}, 0, 3, false),
        new Object(new []{ 65.07f, 65.07f }, 1,1, true), 
        new Object(new []{ 65.67f, 65.67f }, 1,1, true), 
        new Object(new []{ 66.27f, 66.27f }, 1,1, true), 
        new Object(new []{ 66.87f, 66.87f }, 1,1, true), 
        new Object(new []{ 67.47f, 67.47f },1,1, true), 
        new Object(new []{67.5f, 67.5f}, 0, 3, false),
        new Object(new []{ 68.0f, 70.04f }, 1,2, true), 
        new Object(new []{ 70.47f, 70.47f }, 1,0, true), 
        new Object(new []{ 71.0f, 90.0f }, 2, 4, false),
        new Object(new []{ 71.67f, 71.67f }, 0,1, true), 
        new Object(new []{ 72.27f, 72.27f }, 0,1, true), 
        new Object(new []{72.5f, 72.5f}, 1, 3, false),
        new Object(new []{ 73.25f, 79.75f },0,2, true), 
        new Object(new []{75.0f, 75.0f}, 1, 3, false), 
        new Object(new []{77.5f, 77.5f}, 1, 3, false),
    };

    private void Awake()
    {
        _noteHandler = note.GetComponent<Note>();
        _playerHandler = player.GetComponent<PlayerControl>();
        _bgmHandler = bgm.GetComponent<BgmController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _playerX = _playerHandler.transform.position.x;
        //StartCoroutine(SpawnnewItem());
        // StartCoroutine(SpawnNewPlatform());
    }

    void FixedUpdate()
    {
        if (_ind < objArr.Length && objArr[_ind].TimeStamp[0] <= _bgmHandler.songPosition + 2 && _bgmHandler.started)
        {
            _xLen = _noteHandler.transform.localScale.x;

            float yPos = objArr[_ind].Pos switch
            {
                0 => -4,
                1 => 4,
                2 => 0,
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
                case 4:
                    newPlatform = Instantiate(platform, _spawnPos, Quaternion.identity);
                    var newPlatform_ = newPlatform.GetComponent<platform>();
                    newPlatform_.SetLength(_xLen);
                    Destroy(newPlatform, 3f/2.46f*_xLen);
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
                case 4:
                    newPlatform = Instantiate(platform, _spawnPos, Quaternion.identity);
                    var newPlatform_ = newPlatform.GetComponent<platform>();
                    newPlatform_.SetLength(_xLen);
                    Destroy(newPlatform, 3f/2.46f*_xLen);
                    break;
            }
            _ind++;
            //Debug.Log("loop end:"+Time.time);
        }
    }
    // private IEnumerator SpawnNewPlatform()
    // {
    //     while (_ind_platform < PlatformArr.Length/2)
    //     {
    //         yield return new WaitForSeconds(PlatformArr[_ind_platform, 0]-PlatformArr[_ind_platform-1, 0]);

    //         float yPos = 0;
            

    //         if (PlatformArr[_ind_platform, 1] - PlatformArr[_ind_platform, 0] != 0)
    //         {
    //             _xLen = (PlatformArr[_ind_platform, 1] - PlatformArr[_ind_platform, 0]) * _noteHandler.speed * (1 / Time.fixedDeltaTime);
    //         }

    //         _spawnPos = new Vector3(_playerX + _noteHandler.speed * (2f / Time.fixedDeltaTime) + _xLen / 2, yPos, 0);

    //         newPlatform = Instantiate(platform, _spawnPos, Quaternion.identity);
    //         Debug.Log(Time.)
    //         var newPlatform_ = newPlatform.GetComponent<platform>();
    //         newPlatform_.SetLength(_xLen);
    //         Destroy(newPlatform, 3f/2.46f*_xLen);
            
            
    //         _ind_platform++;
    //     }
    // }
}
