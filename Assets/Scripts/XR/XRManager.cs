using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class XRManager : MonoBehaviour
{
    int _score = 0;
    int score { get => _score; set => SetScore(value); }

    int _dead = 0;
    int dead { get => _dead; set => SetDead(value); }

    int maxScore = 0;

    public TMP_Text scoreText;
    public TMP_Text maxScoreText;
    public TMP_Text deadCount;
    public WorldMove world;

    public GameObject startUi;
    public InputAction startButton;
    public Image progressImage;

    float startTime = 0;
    bool paused => startUi.activeInHierarchy;
    Rigidbody body;

    private void SetScore(int value)
    {
        _score = value;
        if (_score > maxScore)
        {
            maxScore = _score;
            maxScoreText.text = maxScore.ToString();
        }
        scoreText.text = _score.ToString();
    }
    private void SetDead(int value)
    {
        _dead = value;
        deadCount.text = _dead.ToString();
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        body.position = new Vector3(0, 3, 0);
        score = 0;
        world.active = false;
        startUi.SetActive(true);

    }

    private void OnEnable()
    {
        startButton.Enable();
    }
    private void OnDisable()
    {
        startButton.Disable();
    }

    private void Update()
    {
        if (!startUi.activeInHierarchy)
            return;

        if (startButton.IsPressed())
        {
            startTime += Time.deltaTime;
            progressImage.fillAmount = startTime / 3;
            if (startTime > 3)
            {
                startUi.SetActive(false);
                startTime = 0;
                score = 0;
                world.active = true;
                body.isKinematic = false;
                body.position = new Vector3(0, 3, 0);
            }
        }
        else
        {
            startTime = 0;
            progressImage.fillAmount = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (paused)
            return;
        //Debug.Log(other.name);
        if (other.name.Equals("Score"))
            score++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (paused)
            return;

        //TODO dead
        if (!collision.collider.CompareTag("Column"))
            return;

        dead++;
        world.Rebuild();
        world.active = false;
        body.isKinematic = true;
        body.position = new Vector3(0, 3, 0);
        startUi.SetActive(true);
    }

}
