using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] bool isPlayerUnit;


    public Character Character { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;
    float startPos = 450;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }
    public void SetUp(Character character)
    {
        Character = character;
        image.sprite = Character.Base.BattleSprite;

        if (isPlayerUnit) 
        {
            transform.localScale = new Vector3(1f, 1f);
            //GetComponent<Image>().sprite = character.Base.BattleSprite; 
        }
        else 
        {
            transform.localScale = new Vector3(-1f, 1f);
            //GetComponent<Image>().sprite = character.Base.BattleSprite; 
        }
        image.color = originalColor;
        PlayerEnterAnimation();
        
    }

    public void PlayerEnterAnimation()
    {
        if (isPlayerUnit)
            image.transform.localPosition = new Vector3(-startPos, originalPos.y);
        else
            image.transform.localPosition = new Vector3(startPos, originalPos.y);
        image.transform.DOLocalMoveX(originalPos.x, 1.5f);
    }

    public void PlayAttackAnimation()
    {
        //aca se reproduce la animacion de ataque
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        else
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));

        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayHitAnimation()
    {
        //animacion de recibir golpe
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }
    
    public void PlayFaintAnimation()
    {
        //aca se puede poner animacion  de muerte
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
