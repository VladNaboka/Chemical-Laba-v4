using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Solution", menuName = "ScriptableObjects/Solution")]
public class SolutionScriptableObject : ScriptableObject
{
    [SerializeField] private List<ElementsList> _requiredElements = new List<ElementsList>();
    [SerializeField] private int _proportion;
    public Dictionary<ElementsList, int> requiredElementsDictionary { get; private set; } = new Dictionary<ElementsList, int>();
    public bool hasRightSolution;
    public bool rightSolutionMixed;

    private void OnEnable()
    {
        foreach (ElementsList element in _requiredElements)
        {
            requiredElementsDictionary.Add(element, _proportion);
        }
    }
}
