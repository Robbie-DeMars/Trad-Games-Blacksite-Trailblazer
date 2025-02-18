using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;


public class Pathway : MonoBehaviour
{
    [SerializeField] List<int> Predetermined_Events;
    [SerializeField] TMP_Text Text; 
    [SerializeField] TMP_Text EventText;

    bool ready = false;
    private List<Tuple<int, float>> _event_and_when; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _event_and_when = new List<Tuple<int, float>>();
        Text.text = "";
        for (int i = 0; i < Predetermined_Events.Count; i++)
        {
            float _event_time = UnityEngine.Random.value;
            _event_and_when.Add(new Tuple<int, float>(Predetermined_Events[i], _event_time));
            Debug.Log(Predetermined_Events[i].ToString() + " at " + _event_time.ToString());
            Text.text += "\n" + Predetermined_Events[i].ToString() + " at " + _event_time.ToString();
        }
        _event_and_when.Sort(CompareTuples);
        ready = true;
    }

    public void NextButtonClicked() {
        if (ready) {
            if (_event_and_when.Count <= 0) {EventText.text = "No more events!"; return; }
            //collect and manage data
            Tuple<int, float> _event = _event_and_when[_event_and_when.Count - 1];
            _event_and_when.RemoveAt(_event_and_when.Count-1);

            //do the stuff with the data (start the encounter?)
            EventText.text = "Event " + _event.Item1.ToString() + " at " + _event.Item2.ToString();
        }
    }

    private static int CompareTuples(Tuple<int,float> x, Tuple<int,float> y) {
        if (x.Item2 < y.Item2) {  return 1; }
        if (x.Item1 == y.Item1) { return 0; }
        return -1;
    }
}
