using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
	[Euler(
			title: "Problem 59: XOR decryption",
			description: @"Each character on a computer is assigned a unique code and the preferred standard is ASCII (American Standard Code for Information Interchange). For example, uppercase A = 65, asterisk (*) = 42, and lowercase k = 107.

A modern encryption method is to take a text file, convert the bytes to ASCII, then XOR each byte with a given value, taken from a secret key. The advantage with the XOR function is that using the same encryption key on the cipher text, restores the plain text; for example, 65 XOR 42 = 107, then 107 XOR 42 = 65.

For unbreakable encryption, the key is the same length as the plain text message, and the key is made up of random bytes. The user would keep the encrypted message and the encryption key in different locations, and without both “halves”, it is impossible to decrypt the message.

Unfortunately, this method is impractical for most users, so the modified method is to use a password as a key. If the password is shorter than the message, which is likely, the key is repeated cyclically throughout the message. The balance for this method is using a sufficiently long password key for security, but short enough to be memorable.

Your task has been made easy, as the encryption key consists of three lower case characters. Using 0059_cipher.txt (right click and  “Save Link/Target As..”), a file containing the encrypted ASCII codes, and the knowledge that the plain text must contain common English words, decrypt the message and find the sum of the ASCII values in the original text."
			)
	]
	// ReSharper disable once UnusedType.Global
	public class Problem0059 : ISolution<int>
	{
		private List<char> _fileData;

		public Problem0059()
		{
			_fileData = EulerData.Get.Resource("0059_cipher.txt")
									.Select(s => (char)int.Parse(s))
									.ToList();
		}

		public bool HaveImplementedAnalyticSolution => false;
		
		public int BruteForceSolution()
		{
			var chars = Enumerable.Range('a', 26).Select(c => (char)c).ToArray();
			foreach (var a in chars)
			{
				foreach (var b in chars)
				{
					foreach (var c in chars)
					{
						var key = new[] { a, b, c };
						var decrypted = new StringBuilder();
						for (var i = 0; i < _fileData.Count; i++)
						{
							decrypted.Append((char)(_fileData[i] ^ key[i % key.Length]));
						}
						var decryptedString = decrypted.ToString();
						// see https://en.wikipedia.org/wiki/Most_common_words_in_English, I picked the most common words with more than 2 chars
						if (decryptedString.Contains(" the ")
							&& decryptedString.Contains(" and ")
							&& decryptedString.Contains(" that "))
						{
							return decryptedString.Sum(ch => ch);
						}
					}
				}
			}

			// if we get here, we didn't find a solution
			return -1;
		}
		
		public int AnalyticSolution()
		{
			throw new NotImplementedException();
		}

		public int ExpectedSolution()
		{
			return 129_448;
		}
	}
}
