using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElementContainer : MonoBehaviour
{
    [SerializeField] private SolutionScriptableObject _solutionScriptableObject;
    private Dictionary<ElementsList, int> elementsDictionary = new Dictionary<ElementsList, int>();
    private bool _hasRightProportion;
    private bool _hasRightSolution;
    private bool _isMixed;

    private void Awake()
    {
        _solutionScriptableObject.SetDefaultValues();
    }

    public void AddElement(ElementsList elementType, int elementQuantity)
    {
        if(elementsDictionary.ContainsKey(elementType))
        {
            elementsDictionary[elementType] += elementQuantity;
        }
        else
        {
            elementsDictionary.Add(elementType, elementQuantity);
        }


        CheckElementsDictionary();
        foreach (KeyValuePair<ElementsList, int> kvp in elementsDictionary)
        {
            Debug.Log("Key = " + kvp.Key + "Value = " + kvp.Value);
        }
    }

    public void MixElements(bool isMixed)
    {
        if(!_hasRightSolution)
        return;
    
        _isMixed = isMixed;
        _solutionScriptableObject.rightSolutionMixed = _isMixed;
        Debug.Log("ЭЛЕМЕНТЫ МИКСАНУТЫ: " + _solutionScriptableObject.rightSolutionMixed);
    }

    public bool IsSolutionDone()
    {
        return _hasRightSolution && _isMixed;
    }

    private void CheckElementsDictionary()
    {
        if(elementsDictionary.Count > 1)
        _hasRightProportion = elementsDictionary.Values.All(x => x == elementsDictionary.Values.First());

        if(_hasRightProportion)
        {
            foreach (ElementsList key in elementsDictionary.Keys.ToList())
            {
                elementsDictionary[key] = 1;
            }
        }

        _hasRightSolution = CompareDictionaries(elementsDictionary, _solutionScriptableObject.requiredElementsDictionary);
        _solutionScriptableObject.hasRightSolution = _hasRightSolution;

        if(!_hasRightSolution)
        {
            _isMixed = false;
            _solutionScriptableObject.rightSolutionMixed = _isMixed;
        }
    }

    public bool CompareDictionaries<TKey, TValue>(Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2)
	{
		if (dict1 == dict2) return true;
		if ((dict1 == null) || (dict2 == null)) return false;
		if (dict1.Count != dict2.Count) return false;

		var valueComparer = EqualityComparer<TValue>.Default;

		foreach (var kvp in dict1)
		{
			TValue value2;
			if (!dict2.TryGetValue(kvp.Key, out value2)) return false;
			if (!valueComparer.Equals(kvp.Value, value2)) return false;
		}
		return true;
	}
}
