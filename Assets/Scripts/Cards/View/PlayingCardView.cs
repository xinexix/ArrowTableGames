using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCardView : MonoBehaviour
{
    public GameObject cardFront;
    public GameObject cardBack;
    public Sprite frontSprite;
    public Sprite backSprite;
    public float revealDuration = 0.35f;

    private Quaternion _targetRotation;
    private bool _shouldShow = false;
    private bool _isShown = false;
    private float _revealProgress = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        cardFront.GetComponent<SpriteRenderer>().sprite = frontSprite;
        cardBack.GetComponent<SpriteRenderer>().sprite = backSprite;
    }

    public bool isRevealed => _isShown;

    public void hide()
    {
        if (_shouldShow)
        {
            _shouldShow = false;
            _targetRotation = Quaternion.Euler(0f, 0f, 0f);
            _revealProgress = 0f;
        }
    }

    public void show()
    {
        if (!_shouldShow)
        {
            _shouldShow = true;
            _targetRotation = Quaternion.Euler(0f, 180f, 0f);
            _revealProgress = 0f;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_shouldShow != _isShown)
        {
            stepReveal();
        }
    }

    private void stepReveal()
    {
        if (_revealProgress >= 1.0f)
        {
            revealComplete();
            return;
        }

        _revealProgress += Time.deltaTime / revealDuration;

        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _revealProgress);

    }

    private void revealComplete()
    {
        _isShown = _shouldShow;
        transform.rotation = _targetRotation;
    }
}
