using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class EndGame : MonoBehaviour
{
    [SerializeField] private string notice;
    [SerializeField] private Text massage;
    [SerializeField] private Button nextlevel;
    [SerializeField] private Button contine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("lol");

        if (other.gameObject.GetComponent<Player>())
        {
            nextlevel.interactable = false;
            contine.interactable = false;
            StartCoroutine(TextPrinting(notice));
            ButtonController.S.Pause();

        }
    }

    private IEnumerator TextPrinting(string text)
    {
        massage.text = "";
        foreach (char c in text)
        {
            massage.text += c;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

}
