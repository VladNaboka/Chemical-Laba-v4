using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElementContainer : MonoBehaviour
{
    [SerializeField] private List<SolutionScriptableObject> _possibleSolutionScriptableObjects = new List<SolutionScriptableObject>();
    [SerializeField] private StageScriptableObject _stageScriptableObject;
    [SerializeField] private List<ElementsList> _alreadyInsideElements = new List<ElementsList>();
    [SerializeField]private SolutionScriptableObject _solutionScriptableObject;
    private Dictionary<ElementsList, int> _elementsDictionary = new Dictionary<ElementsList, int>();
    private Dictionary<ElementsList, int> _heatedElementsDictionary = new Dictionary<ElementsList, int>();
    private bool _hasRightProportion;
    private bool _hasRightSolution;
    private bool _isMixed;

    private void Awake()
    {
        foreach (SolutionScriptableObject solution in _possibleSolutionScriptableObjects)
        {
            solution.SetDefaultValues();
        }

        if(_alreadyInsideElements.Count > 0)
            foreach (ElementsList element in _alreadyInsideElements)
                AddElement(element, 1);
    }

    public void AddElement(ElementsList elementType, int elementQuantity)
    {
        if(_elementsDictionary.ContainsKey(elementType))
        {
            _elementsDictionary[elementType] += elementQuantity;
        }
        else
        {
            _elementsDictionary.Add(elementType, elementQuantity);
        }

        CheckElementsDictionary();
        foreach (KeyValuePair<ElementsList, int> kvp in _elementsDictionary)
        {
            Debug.Log("Key = " + kvp.Key + " " + "Value = " + kvp.Value);
        }
    }

    public void MixElements(bool isMixed)
    {
        if(!_hasRightSolution)
        return;
    
        _isMixed = isMixed;
        Debug.Log("ЭЛЕМЕНТЫ МИКСАНУТЫ: " + _isMixed);
    }

    public void HeatElements()
    {
        _heatedElementsDictionary = AddDictionaries(_heatedElementsDictionary, _elementsDictionary);
        _elementsDictionary.Clear();
        Debug.Log("ЭЛЕМЕНТЫ НАГРЕТЫ");
    }

    public bool IsSolutionDone()
    {
        if(_solutionScriptableObject == null)
            return false;

        if(_solutionScriptableObject.hardElement)
        {
            return CompareDictionaries(_elementsDictionary, _solutionScriptableObject.requiredElementsDictionary) 
                && CompareDictionaries(_heatedElementsDictionary, _solutionScriptableObject.requiredHeatedElementsDictionary);
        }
        else if (_solutionScriptableObject.needsMixing)
        {
            return _hasRightSolution && _isMixed;
        }
        else
        {
            return _hasRightSolution;
        }
    }

    public void CheckStage()
    {
        if(_stageScriptableObject != null)
        _stageScriptableObject.DoStageCallback(IsSolutionDone());
    }

    private void CheckElementsDictionary()
    {
        _solutionScriptableObject = _possibleSolutionScriptableObjects.Find(x => CompareDictionaryKeys(_elementsDictionary, x.requiredElementsDictionary));
        
        if(_solutionScriptableObject == null)
            return;

        if(_elementsDictionary.Count == _solutionScriptableObject.requiredElementsDictionary.Count && !_solutionScriptableObject.hardElement)
        _hasRightProportion = _elementsDictionary.Values.All(x => x == _elementsDictionary.Values.First());

        if(_hasRightProportion)
        {
            foreach (ElementsList key in _elementsDictionary.Keys.ToList())
            {
                _elementsDictionary[key] = 1;
            }
        }

        _hasRightSolution = CompareDictionaries(_elementsDictionary, _solutionScriptableObject.requiredElementsDictionary);
        _solutionScriptableObject.isSolutionDone = _hasRightSolution;

        if(!_hasRightSolution)
        {
            _isMixed = false;
        }

        if(_stageScriptableObject != null)
        _stageScriptableObject.DoStageCallback(IsSolutionDone());
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

    public bool CompareDictionaryKeys<TKey, TValue>(Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2)
    {
        if (dict1 == dict2) return true;
        if ((dict1 == null) || (dict2 == null)) return false;
        if (dict1.Count != dict2.Count) return false;

        var keyComparer = EqualityComparer<TKey>.Default;

        foreach (var key in dict1.Keys)
        {
            if (!dict2.ContainsKey(key)) return false;
        }
        return true;
    }

    Dictionary<ElementsList, int> AddDictionaries(Dictionary<ElementsList, int> dict1, Dictionary<ElementsList, int> dict2)
    {
        Dictionary<ElementsList, int> result = new Dictionary<ElementsList, int>(dict1); // Создаем копию первого словаря

        // Проходимся по элементам второго словаря
        foreach (KeyValuePair<ElementsList, int> kvp in dict2)
        {
            // Если ключ уже существует в результирующем словаре, то складываем значения
            if (result.ContainsKey(kvp.Key))
            {
                result[kvp.Key] += kvp.Value;
            }
            else
            {
                // Если ключа нет в результирующем словаре, просто добавляем его
                result.Add(kvp.Key, kvp.Value);
            }
        }

        return result;
    }
}
