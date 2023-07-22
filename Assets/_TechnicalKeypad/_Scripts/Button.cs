﻿using UnityEngine;

[RequireComponent(typeof(KMSelectable), typeof(Animator))]
public class Button : MonoBehaviour
{
    [SerializeField] private string _buttonPressSound;
    [SerializeField] private string _buttonReleaseSound;

    private KMAudio _audio;
    private Animator _animator;
    private KMHighlightable _highlight;

    public KMSelectable Selectable { get; private set; }

    protected virtual void Awake() {
        _audio = GetComponentInParent<KMAudio>();
        _animator = GetComponent<Animator>();
        _highlight = GetComponentInChildren<KMHighlightable>();
        
        Selectable = GetComponent<KMSelectable>();
        Selectable.OnInteract += () => {
            _animator.SetBool("IsPressed", true);
            if (!string.IsNullOrEmpty(_buttonPressSound))
                _audio.PlaySoundAtTransform(_buttonPressSound, transform);
            return false;
        };
        Selectable.OnInteractEnded += () => {
            _animator.SetBool("IsPressed", false);
            if (!string.IsNullOrEmpty(_buttonReleaseSound))
                _audio.PlaySoundAtTransform(_buttonReleaseSound, transform);
        };
    }

    public void Enable() => SetState(true);
    public void Disable() => SetState(false);
    public void SetState(bool shouldBeEnabled) => _highlight.gameObject.SetActive(shouldBeEnabled);
}