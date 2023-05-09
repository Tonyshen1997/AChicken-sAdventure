using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(CharacterController))]

public class PlayerScriptNotUsed: MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;
    [SerializeField] private float speed;

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    [SerializeField] public float jumpPower;

    
    [SerializeField] GameObject defaultSpawn;
    public string lastSpawnName;

    private Vector3 impact = Vector3.zero;
    [SerializeField] float mass;

    private int _coinCount = 0;

    //private int count;
    //public TextMeshProUGUI countText;
    //public GameObject winTextObject;

    [SerializeField] GameObject deathPanel;

    private Animator animator;

    public bool isHidden = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        lastSpawnName = PlayerPrefs.GetString("lastCheckpointName", "nothing");

        if (lastSpawnName == "nothing") setSpawn(this.defaultSpawn);
    }

    private void Start()
    {
        Respawn();
        //count = 0;
        //SetCountText();
        //winTextObject.SetActive(false);

    }

    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        ApplyImpact();
    }

    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        impact = dir.normalized * force / mass;
    }

    public void ApplyImpact()
    {
        if (impact != Vector3.zero)
        {
            _characterController.Move(impact * Time.deltaTime);
        }
        // Reset the impact force to zero
        //impact = Vector3.zero;
    }

    private void ApplyRotation()
    {
        // prevent character zeroing out when no input
        if (_input.sqrMagnitude == 0)
        {
            return;
        }

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;

        // HACK: angle is global angle, change this if what to have camera follow POV.
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        _direction.y = _velocity;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
        if (_direction != Vector3.zero)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!_characterController.isGrounded) return;

        _velocity += jumpPower;

    }

    private void Respawn()
    {
        GameObject spawn = GameObject.Find(lastSpawnName);
        if (spawn == null) spawn = defaultSpawn;
        gameObject.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y + 3f, spawn.transform.position.z);
    }

    public void setSpawn(GameObject spawn)
    {
        lastSpawnName = spawn.gameObject.name;
        PlayerPrefs.SetString("lastCheckpointName", lastSpawnName);
    }



    public void death(string deathMessage)
    {
        Time.timeScale = 0f;
        OpenDeathPanel();
        //ToggleDeathPanel();
    }


    private void OpenDeathPanel()
    {
        deathPanel.SetActive(true);
    }

    private void ToggleDeathPanel()
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
    }

    public void setHidden(bool hidden)
    {
        isHidden = hidden;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            _coinCount++;
            Debug.Log("Collected " + _coinCount + " coins!");
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ball"))
    //    {

    //        other.gameObject.SetActive(false);
    //        count = count + 1;
    //        SetCountText();
    //    }
    //}
    //void SetCountText()
    //{
    //    countText.text = "Count: " + count.ToString();

    //    if (count >= 10)
    //    {
    //        winTextObject.SetActive(true);
    //        waiter();
    //        winTextObject.SetActive(false);
    //    }
    //}
    //IEnumerator waiter()
    //{
    //    yield return new WaitForSecondsRealtime(2);
    //}


}
