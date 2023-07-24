using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Solution", menuName = "ScriptableObjects/Solution")]
public class SolutionScriptableObject : ScriptableObject
{
    [SerializeField] private List<ElementsList> _requiredElements = new List<ElementsList>();
    [SerializeField] private List<ElementsList> _requiredHeatedElements = new List<ElementsList>();
    [SerializeField] private int _proportion;
    public Dictionary<ElementsList, int> requiredElementsDictionary { get; private set; } = new Dictionary<ElementsList, int>();
    public Dictionary<ElementsList, int> requiredHeatedElementsDictionary { get; private set; } = new Dictionary<ElementsList, int>();
    public bool needsMixing;
    public bool hardElement;
    public bool isSolutionDone;

    public void SetDefaultValues()
    {
        foreach (ElementsList element in _requiredElements)
        {
            if(!requiredElementsDictionary.ContainsKey(element))
            requiredElementsDictionary.Add(element, _proportion);
        }

        foreach (KeyValuePair<ElementsList, int> kvp in requiredElementsDictionary)
        {
            Debug.Log("Key = {0},Value = {1}"+ kvp.Key + kvp.Value);
        }

        foreach (ElementsList element in _requiredHeatedElements)
        {
            if(!requiredHeatedElementsDictionary.ContainsKey(element))
            requiredHeatedElementsDictionary.Add(element, _proportion);
        }

        isSolutionDone = false;
    }
}
