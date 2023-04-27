using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] Character selectedCharacter;
    [SerializeField] List<Character> characterList;
    [SerializeField] Transform atkRef;
    [SerializeField] UnityEvent onTakeDamage;
    [SerializeField] bool isBot;

    public Character SelectedCharacter { get => selectedCharacter; }
    public List<Character> CharacterList { get => characterList;}

    public void Prepare()
    {
        selectedCharacter = null;
    }
    public void SelectCharacter(Character character)
    {
        selectedCharacter = character;
    }

    public void SetPlay(bool value)
    {
        if(isBot)
        {
            int index = Random.Range(0,maxExclusive: characterList.Count);
            selectedCharacter = CharacterList[index];
        }
        else
        {
             foreach (var character in CharacterList)
                {
                    character.Button.interactable = value;
                }
        }
               
    }

    // Cara Rumit yang tidak dipakai //
    /* public void Update()
    {
        if (direction == Vector3.zero)
        {
            return;
        }

        if (Vector3.Distance(selectedCharacter.transform.position, atkRef.position) > 0.1f)
        {
            selectedCharacter.transform.position += direction * Time.deltaTime;

        }
        else
        {
            direction = Vector3.zero;
            selectedCharacter.transform.position = atkRef.position;
        }
    }

    Vector3 direction = Vector3.zero; */
    public void Attack()
    {
        /* direction = atkRef.position - selectedCharacter.transform.position; */

        selectedCharacter.transform
            .DOMove(atkRef.position,1f,true)
            .SetEase(Ease.InOutBounce);

    }

    public bool IsAttacking()
    {
        if(selectedCharacter== null){
            return false;
        }
        return DOTween.IsTweening(selectedCharacter.transform, true);
    }

    public void TakeDamage(int damageValue)
    {
        selectedCharacter.ChangeHP(-damageValue);
        var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        spriteRend.DOColor(Color.red, 0.1f).SetLoops(6, LoopType.Yoyo);
        onTakeDamage.Invoke();
    }

    public bool IsDamaging(){
        if(selectedCharacter== null){
            return false;
        }
        var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        return DOTween.IsTweening(spriteRend);
    }

    public void Remove(Character character)
    {
        if(CharacterList.Contains(character) == false){
            return;
        }
        if(selectedCharacter == character){
            selectedCharacter = null;
        }

        character.Button.interactable = false;
        character.gameObject.SetActive(false);
        characterList.Remove(character);
    }

    public void Return()
    {
        selectedCharacter.transform.DOMove(selectedCharacter.InitialPosition,1f);
    }
        

    public bool IsReturning(){
        if(selectedCharacter== null){
            return false;
        }
        return DOTween.IsTweening(selectedCharacter.transform);
    }

}
