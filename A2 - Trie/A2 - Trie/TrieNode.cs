using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A2Trie
{
    class TrieNode<ValueType>
    { 
        //The Sorted Dictionary Contains all of the words that will be implemented by the Trie
        internal SortedDictionary<char, TrieNode<ValueType>> link;

        //contains the end-of-sequence information : French Translation.
        //                                           null otherwise     
        internal ValueType val;       

        /// <summary>
        /// Empty Contructor, Initializes the SortedDictionary
        /// </summary>
        public TrieNode()
        {
            link = new SortedDictionary<char, TrieNode<ValueType>>();
        }

    }
}
