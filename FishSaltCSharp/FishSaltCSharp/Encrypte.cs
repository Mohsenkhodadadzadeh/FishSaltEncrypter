using System;
using System.Text;

namespace FishSaltCSharp
{
    class Encrypte
    {
        // Max Salt Count
        private const int SALT_MAX_LENGHT = 10;
        //Max Odd Even Count
        private const int ODD_EVEN_MAX_LENGHT = 10;

        //Min char Ascii showable is zero
        private const int MIN_ASCII_CODE = 32;

        //max char ascii showable is }
        private const int MAX_ASCII_CODE = 125;

        //Get random salt size
        private int Salt_Count;

        // Get Random odd even size
        private int odd_even_count;

        // salt data
        private char[] salt_data;

        //Odd even data
        private char[] odd_even_data;

        Random rnd;
        private void Initalize()
        {

            rnd = new Random();

            // Initalize Random Salt
            Salt_Count = rnd.Next(MIN_ASCII_CODE, MAX_ASCII_CODE); // from 0 ascii to z ascii
            salt_data = new char[((Salt_Count - MIN_ASCII_CODE - 1) % SALT_MAX_LENGHT) + 1];

            //Initalize random odd-even-code
            odd_even_count = rnd.Next(MIN_ASCII_CODE, MAX_ASCII_CODE);  // from 0 ascii to z ascii
            odd_even_data = new char[((odd_even_count - MIN_ASCII_CODE - 1) % ODD_EVEN_MAX_LENGHT) + 1];


        }

        public Encrypte()
        {
            Initalize();
        }

        public String Encrypte_Data(String data)
        {
            //Initalize salt data
            for (int i = 0; i < salt_data.Length; i++)
            {
                salt_data[i] = (char)rnd.Next(MIN_ASCII_CODE, MAX_ASCII_CODE);
            }

            // Initalize odd even data
            for (int i = 0; i < odd_even_data.Length; i++)
            {
                odd_even_data[i] = (char)rnd.Next(MIN_ASCII_CODE, MAX_ASCII_CODE);
            }

            char[] encData = new char[data.Length];

            //Encryption Data
            int odd_even_loop = 1;
            int salt_loop = 1;
            for (int i = 0; i < encData.Length; i++)
            {
                // Get Odd Even state
                if (odd_even_loop > ((odd_even_count - MIN_ASCII_CODE) % ODD_EVEN_MAX_LENGHT))
                {
                    odd_even_loop = 1;
                }
                Boolean odd_even_state = HashEvenOdd(odd_even_data[odd_even_loop - 1]);

                // Get Salt value
                if (salt_loop > ((Salt_Count - MIN_ASCII_CODE) % SALT_MAX_LENGHT))
                {
                    salt_loop = 1;
                }
                int salt_value = salt_data[salt_loop - 1];

                int ValueData;

                if (odd_even_state)
                {
                    ValueData = (int)data.ToCharArray()[i] + salt_value;

                    if (ValueData > MAX_ASCII_CODE)
                    {
                        ValueData = ValueData - (MAX_ASCII_CODE - MIN_ASCII_CODE);
                    }
                }
                else
                {
                    ValueData = (int)data.ToCharArray()[i] - salt_value;

                    if (ValueData < MIN_ASCII_CODE)
                    {
                        ValueData = ValueData + (MAX_ASCII_CODE - MIN_ASCII_CODE);
                    }
                }
                char chrtest = (char)ValueData;
                encData[i] = chrtest;

                odd_even_loop++;
                salt_loop++;

            }

            StringBuilder RetObj = new StringBuilder();
            RetObj.Append((char)Salt_Count);
            RetObj.Append(salt_data);
            RetObj.Append((char)odd_even_count);
            RetObj.Append(odd_even_data);
            RetObj.Append(encData);
            // return

            return RetObj.ToString();
        }
        private Boolean HashEvenOdd(char rule)
        {
            // first Example
            if (((int)rule % 2) == 1)
            {
                return true;
            }
            return false;

            // Secend Example

            int i = 0;
        }
    }
}