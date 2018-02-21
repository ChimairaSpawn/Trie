using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A2Trie
{
    /// <summary>
    /// The Trie Tree
    /// </summary>
    public class Trie<KeyType, ValueType>
    {

        private TrieNode<ValueType> root; // root node of tree, used for navigation
        
        /// <summary>
        /// Create an Empty Trie
        /// </summary>
        public Trie()
        {
            root = new TrieNode<ValueType>();
        }
                
        /// <summary>
        /// add the key/value pair (k,v) to the Trie. If k exists then v is updated
        /// </summary>
        /// <param name="k">Key</param>
        /// <param name="v">Value</param>
        internal void Add(KeyType k, ValueType v)
        {
            TrieNode<ValueType> curr = root;          

            string Key = k.ToString(); // "KeyType" doesn't have the methods  that a string does, so it needs to be converted
            Key.ToUpper(); // this will add consistency in the tree, since chars are case sensitive
            int endOfKey = Key.Length - 1; // used to determine when to flag end of word, -1 because .Length counts from 1.
                        
            //Loop that will add the characters  to the Trie, one Char at a time from the key (string)
            for (int chIndex = 0; chIndex < Key.Length; chIndex++)
            {
                // if the current node has the specified char in its dictionary, move down the Trie Tree
                if (curr.link.Keys.Contains(Key[chIndex])) 
                {
                    curr = curr.link[Key[chIndex]]; // curr becomes the child node
                }
                // if the letter isn't in the dictionary, then create a new node.
                else
                {
                    //genereic new TrieNode, to become the child
                    TrieNode<ValueType> nextTrieNode = new TrieNode<ValueType>();

                    // flag new Node as EndofWord ? ? ?
                    if (chIndex == endOfKey)
                    {
                        // in this case : v =  a string, the french translation of the english string
                        nextTrieNode.val = v;
                    }

                    // add the new trienode and the current char in the string to the dictionary
                    curr.link.Add(Key[chIndex], nextTrieNode);
                    // advance curr to the next value
                    curr = nextTrieNode;              
                }
            }//end loop        
        }

        /// <summary>
        /// removes the key k (and its value v) from the trie. If the key k is not found then false is returned; otherwise true is returned
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        internal bool Remove(string k)
        {
            //used to navigate the tree
            TrieNode<ValueType> curr = root;// insertion point for the trie
            TrieNode<ValueType> prevCurr; // used for backtracking the Trie
            string Key = k; // allows string to be changed into some other data type
            Key.ToUpper(); // this will add consistency in the tree, since chars are case sensitive
            int chIndex = 0;

            while (curr.link.Count != 0 && chIndex < Key.Length)// a for loop wouldn't cut it in this scenario
            {
                prevCurr = curr;

                // this is the first time I've used a try..catch since 1020H
                try
                {
                    curr = curr.link[Key[chIndex]]; // was crashing because the char at Key{chIndex] wasn't in the Dictionary
                }
                catch(KeyNotFoundException knf)
                {
                    //Console.WriteLine(knf); // error output
                    return true; 
                }                
                
                //if no links found, erase this node along the way, backtracking all the way to root if nessesary
                if (curr.link.Count == 0 )
                {
                    curr = prevCurr; // since the program needs to back up one node to remove this one.
                    Console.WriteLine("Removing '{0}' at index {2} from '{1}'", Key[chIndex], Key, Convert.ToString(chIndex)); // Verbose output
                    curr.link.Remove(Key[chIndex]); // remove the node with the keyt being the current char at chIndex of k
                    Remove(k); // RECURSION 
                    return true;
                }
                chIndex++;
            }
            return false;
        }

        /// <summary>
        /// returns the value v that corresponds to the full key k. If the key k is not found then the default value of ValueType is returned
        /// </summary>
        /// <param name="k"></param>
        /// <returns>Value stored in the last node of the key</returns>
        internal ValueType Translate(string k)
        {
            Console.WriteLine("Translating {0}...", k);

            //used to navigate the tree
            TrieNode<ValueType> curr = root;
            string Key = k; // allows string to be changed into some other data type
            Key.ToUpper(); // this will add consistency in the tree, since chars are case sensitive

             //Loop that will traverse the Trie to the final letter of the word
            for (int chIndex = 0; chIndex < Key.Length; chIndex++)
            {
                // if the current node has the specified char in its dictionary, move down the Trie Tree
                if (curr.link.Keys.Contains(Key[chIndex])) 
                {
                    curr = curr.link[Key[chIndex]]; // curr becomes the child node

                    //if 
                    if (chIndex == (Key.Length - 1))
                    {
                        // val is the Translation
                        return curr.val;
                    }
                }                
                // if the letter isn't in the dictionary, then the word doesn't exist.
                else
                {
                    Console.Write("Error - The word '{0}' does not exist ", k);
                    return default(ValueType);
                }
            }//end loop 

            // this is here so that all code paths return a value, but it will never make it here.
            Console.WriteLine("Error - Outside of for loop in [internal ValueType Translate(string k)] - Default Value being Returned");
            return default(ValueType);       
        }
       
        /// <summary>
        /// Return all values that succeed the given string
        /// </summary>
        /// <param name="k"></param>
        /// <returns>Values that begin with K</returns>
/*      internal ValueType AutoComplete(KeyType k)
        {

        }
*/
        /// <summary>
        /// Print the Trie in key/value pairs
        /// </summary>
        internal void Print()
        {            
            TrieNode<ValueType> curr = root;
            string engWord = ""; // using StringBuilder because of its ability to save the keys into a string
            Print(curr, engWord); // doing it this way since it's recursive and easier to traverse  
        }
        /// <summary>
        /// Extension to Print()
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="EnglishWord"></param>
        void Print(TrieNode<ValueType> curr, string EnglishWord)
        {
            StringBuilder engWord = new StringBuilder(EnglishWord);

            if (curr.val != null)
            {
                Console.WriteLine("{0} -- > {1}", engWord.ToString(), curr.val.ToString() ); // print the 
            }
            
            for (char bruteForce = 'a'; bruteForce <= 'z'; bruteForce++)
            {
                if (curr.link.ContainsKey(bruteForce))
                {
                    //curr = curr.link[bruteForce];
                    Print( curr.link[bruteForce], ( engWord.Append(bruteForce).ToString() ) ); // Recursively Call this Print using the next node
                    //engWord.Append(bruteForce); // add the char that is present in the KeyList to the ongoing string
                               
                    ///TODO: get this method working
                    /// the trick is int he order of th erecursion
                    /// it's 3am
                    /// it retians the most recent char from the previous print and carries that  forward througout the print                            
                }
            }
        }
    }
}
