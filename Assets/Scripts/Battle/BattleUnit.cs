using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] private bool isPlayerUnit;

    public Pokemon Pokemon { get; set; }
    Image image;
    Vector3 orignalPos;
    Color orignalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        orignalPos = image.transform.localPosition;
        orignalColor = image.color;
    }

    public void SetUp(Pokemon pokemon)
    {
        Pokemon = pokemon;
        if (isPlayerUnit)
            image.sprite = Pokemon.Base.BackSprite;
        else
            image.sprite = Pokemon.Base.FontSprite;

        image.color = orignalColor;

        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
        if (isPlayerUnit)
            image.transform.localPosition = new Vector3(-500f, orignalPos.y);
        else
            image.transform.localPosition = new Vector3(500f, orignalPos.y);

        image.transform.DOLocalMoveX(orignalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequece = DOTween.Sequence();
        if (isPlayerUnit)
            sequece.Append(image.transform.DOLocalMoveX(orignalPos.x + 50f, 0.25f));
        else
            sequece.Append(image.transform.DOLocalMoveX(orignalPos.x - 50f, 0.25f));

        sequece.Append(image.transform.DOLocalMoveX(orignalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {
        var sequece = DOTween.Sequence();
        sequece.Append(image.DOColor(Color.gray, 0.1f));
        sequece.Append(image.DOColor(orignalColor, 0.1f));
    }

    public void PlayFaintedAnimation()
    {
        var sequece = DOTween.Sequence();
        sequece.Append(image.transform.DOLocalMoveY(orignalPos.y - 150f, 0.5f));
        sequece.Join(image.DOFade(0f, 0.5f));
    }
}
