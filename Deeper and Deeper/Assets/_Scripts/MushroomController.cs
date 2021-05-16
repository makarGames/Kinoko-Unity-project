using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MushroomController : MonoBehaviour
{
    public static MushroomController S;

    [SerializeField] private Text mushroomNotice;

    private int maxNumberOfMushrooms;
    private int _numberOfMushrooms;
    public int numberOfMushrooms
    {
        get
        {
            return _numberOfMushrooms;
        }
        set
        {
            _numberOfMushrooms = value;
            PlayerPrefs.SetInt("PickedMushrooms" + SceneManager.GetActiveScene().name, _numberOfMushrooms);

            string notice = _numberOfMushrooms + " из " + maxNumberOfMushrooms + " грибочков";
            StartCoroutine(TextPrinting(notice));
        }
    }


    private void Awake()
    {
        mushroomNotice.text = "";
        maxNumberOfMushrooms = 4;

        if (S == null)
            S = this;
    }

    private IEnumerator TextPrinting(string text)
    {
        foreach (char c in text)
        {
            mushroomNotice.text += c;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(5f);
        mushroomNotice.text = "";
    }

}
