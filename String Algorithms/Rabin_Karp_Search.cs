using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringAlgorithms
{
    class Rabin_Karp_Search
    {
        static Random rnd = new Random();

        static List<int> RobinKarp(string text, string pattern)
        {
            var T = text.Length;
            var P = pattern.Length;
            long p = 2017; //NextPrime((T * P)); Using Next Prime runs slow(need to find better algorithm for finding Big Prime number) // Implementing big prime number, larger than |T|*|P|
            var x = 263; // Multiplier, random prime number (in lectures it suggests to use 1<=x<=p-1)
            var result = new List<int>(); // Resulting list
            var pHash = PolyHash(pattern, p, x); // Hash value of pattern
            var H = PrecomputeHashes(text, P, p, x); // Hash value of substring
            for (int i = 0; i <= T - P; i++) // Iterate through all posible starting positions of pattern P int text T
            {
                if (pHash != H[i]) // If hash value of pattern isn't equal to hash value of substring then continue
                {
                    continue;
                }

                if (text.Substring(i, P) == pattern) // If hash values are equal
                {
                    result.Add(i); // Add it to the result list
                }
            }
            return result;
        }

        static long[] PrecomputeHashes(string text, int P, long p, int x)
        {
            var T = text.Length;
            var H = new long[T - P + 1]; // Resulting array which length is number of substrings of length of pattern, in text
            var S = text.Substring(T - P); // Last substring in text, which length is equal to pattern
            H[T - P] = PolyHash(S, p, x); // Hash value of last substring
            long y = 1; // Then we also need to precompute the value of x to the power of length of the pattern and store it in the variable y.            
            for (int i = 1; i <= P; i++) // To do that we need initialize it with 1 and then multiply it length of P times by x and take this module of p
            {
                y = (y * x) % p;
            }
            for (int i = T - P - 1; i >= 0; i--) // Goes from right to left
            {
                //long subtraction = text[i] - y * text[i + P];
                H[i] = ((x * H[i + 1]) % p + ((text[i] - y * text[i + P]) % p + p) % p) % p; // Hash values for all substrings of text, except last one which we already know his has value
            }
            return H;
        }

        static int PolyHash(string s, long p, int x)
        {
            long hash = 0; // Use long because integer overflow
            for (int i = s.Length - 1; i >= 0; i--)
            {
                hash = (hash * x + s[i]) % p; // Polynomial hash function
            }
            return (int)hash;
        }

        private static long NextPrime(int n)
        {
            long prime = n + 1;
            if (prime %2 == 0) // If number is even increment it by one so we can start with odd number
            {
                prime++;
            }
            
            while (!IsPrime(prime)) // Check if number larger then |T| * |P| is Prime number
            {
                prime=+2; // If not increment number by two because we only need odd numbers, and check again
            }           
            return prime;
        }

        private static bool IsPrime(long n)
        {
            if (n <= 1) return false;
            if (n == 2 || n == 3) return true;

            // Skip all even numbers
            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0) return false;
            }
            return true;
        }
    }
}