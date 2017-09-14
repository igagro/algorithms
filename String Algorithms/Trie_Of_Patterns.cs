using System;
using System.Collections.Generic;

namespace StringAlgorithms
{
    class Trie_Of_Patterns
    {
        static List<Dictionary<char, int>> TrieConstruction(List<string> patterns)
        {
            var trie = new List<Dictionary<char, int>>();
            trie.Add(new Dictionary<char, int>());
            var index = 1;
            foreach (var pattern in patterns)
            {
                var currentNode = trie[0];
                for (int i = 0; i < pattern.Length; i++)
                {
                    var currentSymbol = pattern[i];
                    if (currentNode.ContainsKey(currentSymbol))
                    {
                        currentNode = trie[currentNode[currentSymbol]];
                    }
                    else
                    {
                        trie.Add(new Dictionary<char, int>());
                        currentNode.Add(currentSymbol, index);
                        currentNode = trie[index];
                        index++;
                    }
                }
            }
            return trie;
        }
    }
}
