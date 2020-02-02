using UnityEngine;
using Zenject;



public class PlayerController : MonoBehaviour
{
    [Inject]
    private GameConfig _config;
    [Inject]
    private GameController _gameController;
    [Inject]
    private UIController _uIController;

    public AudioSource tap;
    public AudioSource dead;
    public AudioSource score;

    private int MusicState;
    private Rigidbody2D _rigidbody;
    private Quaternion downRotation;
    private Quaternion forwardRotation;
    public void Update()
    {
        MusicState = PlayerPrefs.GetInt("Music");
    }

    public void GetBirbState()
    {
        _rigidbody = GameObject.FindWithTag("birb").GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 15);
    }
    public void BirbMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.rotation = forwardRotation;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector2.up * _config.tapForce, ForceMode2D.Force);
            if (MusicState == 1)
                tap.Play();
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, _config.tiltSmooth * Time.deltaTime);  
    }
    public void OnGameStarted()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = _config.birbStartPos;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    public void OnGameOverConfirmed()
    {
        _rigidbody.simulated = false;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    public void Playable()
    {
        _rigidbody.simulated = true;
        OnGameStarted();
    }
    public void AfterPause()
    {
        _rigidbody.simulated = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ScoreZone")
        {
            _gameController.OnPlayerScored();
            if (MusicState == 1)
                score.Play();
        }
        if(collision.gameObject.tag =="DeadZone")
        {
            _rigidbody.simulated = false;
            _uIController.OnPlayerDead();
            if (MusicState == 1)
               dead.Play();
        }
    }
}
