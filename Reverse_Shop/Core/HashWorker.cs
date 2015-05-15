﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
   public class HashWorker
    {
       public string GetMd5Hash(HashAlgorithm md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
       //public static bool VerifyMd5Hash(HashAlgorithm md5Hash, string input, string hash)
       // {
       //     // Hash the input.
       //    // string hashOfInput = GetMd5Hash(md5Hash, input);

       //     // Create a StringComparer an compare the hashes.
       //     StringComparer comparer = StringComparer.OrdinalIgnoreCase;

       //    // return 0 == comparer.Compare(hashOfInput, hash);
       // }
    }
}