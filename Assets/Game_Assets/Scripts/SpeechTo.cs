using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class SpeechTo : MonoBehaviour
{
    public BuildingManager bManager;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords;
    

    void Start()
    {
        bManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<BuildingManager>();
        keywords = new Dictionary<string, System.Action>();
        keywords.Add("barracks", () =>
        {
            BuildBarracks();
        });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    public void BuildBarracks()
    {
        Debug.Log("You can place Barracks");
        bManager.BarracksBuild();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

}
