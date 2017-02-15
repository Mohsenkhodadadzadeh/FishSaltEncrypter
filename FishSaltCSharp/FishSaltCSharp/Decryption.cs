using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishSaltCSharp
{
    class Decryption
    {
        // Max Salt Count
        private const int SALT_MAX_LENGHT = 10;
        //Max Odd Even Count
        private const int ODD_EVEN_MAX_LENGHT = 10;

        //Min char Ascii showable is zero
        private const int MIN_ASCII_CODE = 60;

        //max char ascii showable is }
        private const int MAX_ASCII_CODE = 175;

        //Get random salt size
        private int Salt_Count;

        // Get Random odd even size
        private int odd_even_count;

        // salt data 
        private char[] salt_data;

        //Odd even data
        private char[] odd_even_data;


        private void Initalize()
        {

        }
        public String DecrypteData(String EncData)
        {
            char[] EncChar = EncData.ToCharArray();
            int StartRead = 0;

            
            //Initalize Salt size

            // Get salt data lenght
            Salt_Count = ((int)EncChar[StartRead] % SALT_MAX_LENGHT);
            // pass Salt_count
            StartRead = 1; 
            char[] Salt_Data = new char[Salt_Count];
            for(int i = StartRead; i < (StartRead + Salt_Count - 1); i++)
            {
                salt_data[i - StartRead] = EncChar[i];
            }
            // pass salt data 
            StartRead += Salt_Count; 


            //initalize odd_even_data

            // get oddevent string lenght
            odd_even_count = ((int)EncChar[StartRead] % ODD_EVEN_MAX_LENGHT); 
            // pass odd even count
            StartRead += 1; 
            odd_even_data = new char[odd_even_count];
            for(int i = StartRead; i < (StartRead + odd_even_count - 1); i++)
            {
                odd_even_data[i - StartRead] = EncChar[i];
            }

            //pass Odd even data
            StartRead += odd_even_count;

            char[] Encrypte_Data = new char[EncChar.Length - StartRead];

            for(int i = StartRead; i < EncChar.Length; i++)
            {
                Encrypte_Data[i - StartRead] = EncChar[i];
            }
            int Salt_Num_Read = 0;
            int odd_even_read = 0;
            char[] Orginal_data = new char[Encrypte_Data.Length];

            for (int i = 0; i < Encrypte_Data.Length - 1; i++)
            {
                if (odd_even_read >= odd_even_count)
                {
                    odd_even_read = 0;
                }
                if (Salt_Num_Read >= Salt_Count)
                {
                    Salt_Num_Read = 0;
                }

                Boolean Has_Odd_Even = HashEvenOdd(odd_even_data[odd_even_read]);
                int DecrypteVal ;
                if (Has_Odd_Even)
                {
                    DecrypteVal = ((int) Encrypte_Data[i] - (int)salt_data[Salt_Num_Read]);
                }else
                {
                    DecrypteVal = ((int)Encrypte_Data[i] + (int)salt_data[Salt_Num_Read]);
                }
                if (DecrypteVal > MAX_ASCII_CODE)
                {
                    DecrypteVal = DecrypteVal - MAX_ASCII_CODE;
                }
                else if (DecrypteVal < MIN_ASCII_CODE)
                {
                    DecrypteVal = DecrypteVal + MIN_ASCII_CODE;
                }
                Orginal_data[i] = (char)DecrypteVal;
                odd_even_read++;
                Salt_Num_Read++;
            }

            return Orginal_data.ToString();

         }
        //This method must eqal HashEvenOdd method in Encrypte Class 
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
