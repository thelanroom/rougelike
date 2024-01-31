using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Character")]
    public Character hero;
    public Character boss;
    public Transform heroSpot, bossSpot;

    [Header("UI")]
    public Button startBtn;
    public Button replayBtn;

    public void Start()
    {
        Prepare();
        startBtn.gameObject.SetActive(true);
        replayBtn.gameObject.SetActive(false);
        hero.OnCharacterDie = HandleCharacterDie;
        boss.OnCharacterDie = HandleCharacterDie;
    }

    public void Prepare()
    {
        hero.transform.position = heroSpot.position;
        boss.transform.position = bossSpot.position;

        hero.Reset();
        boss.Reset();
    }

    public void StartGame()
    {
        hero.SetCharacterState(CharacterState.Ready);
        boss.SetCharacterState(CharacterState.Ready);
    }

    public void Replay()
    {
        StartCoroutine(DoReplay());
    }

    IEnumerator DoReplay()
    {
        Prepare();
        yield return new WaitForSeconds(1f);
        StartGame();
    }

    private void HandleCharacterDie()
    {
        replayBtn.gameObject.SetActive(true);
    }
}
