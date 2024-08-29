using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int letterPerSeconds;
    [SerializeField] Text dialogText;
    [SerializeField] Color highlightedColor;

    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> actionText;
    [SerializeField] List<Text> moveText;

    [SerializeField] Text ppText;
    [SerializeField] Text typeText;

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    //a Interface return loop
    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSeconds);
        }
        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogBox(bool enable)
    {
        dialogText.enabled = enable;
    }

    public void EnableActionSelection(bool enable)
    {
        actionSelector.SetActive(enable);
    }

    public void EnableMoveSelection(bool enable)
    {
        moveSelector.SetActive(enable);
        moveDetails.SetActive(enable);
    }

    public void UpdateActionSelection(int selectionAction)
    {
        for (int i = 0; i < actionText.Count; i++)
        {
            if (i == selectionAction)
                actionText[i].color = highlightedColor;
            else
                actionText[i].color = Color.black;
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i = 0; i < moveText.Count; i++)
        {
            if (i == selectedMove)
                moveText[i].color = highlightedColor;
            else
                moveText[i].color = Color.black;
        }

        ppText.text = $"PP {move.PP} / {move.Base.PP}";
        typeText.text = move.Base.Type.ToString();
    }

    public void SetMoveNames(List<Move> moves)
    {
        for (int i = 0; i < moveText.Count; i++)
        {
            if (i < moves.Count)
                moveText[i].text = moves[i].Base.name;
            else
                moveText[i].text = "-";
        }
    }
}
