using System;
using System.Collections.Generic;

namespace AndroidTestCross.Classes;

public class Shuffler
{
    
    //Shuffler should work
    public T[] shuffle<T>(T[] array, int baseSeed)
    {
        int seed = baseSeed * (array.Length + 1);
        int i = 0;
        T[] outArray = new T[array.Length];
        foreach (T o in array)
        {
            Randomize:
            i++;
            //Make a Random object with seed+i, so the seed can be changed if needed.
            Random rg = new Random(seed + i);
            
            //Generate random y, as new position of the value?
            int y = rg.Next(0, array.Length);
            if (!EqualityComparer<T>.Default.Equals(outArray[y], default(T)))//outArray[y] != t)
            {
                goto Randomize; 
            }   
            outArray[y] = o;
            
        }

        return outArray;
    }
    //Works, as long as there aren't any "null" bytes(value 0) in file, could work around by converting array to int array if it is a byte array.
    //And then calling this method for the int array, which has null as default value(or then use a vallue like 266 as "default" since bytes go to 255)
    public T[] unShuffle<T>(T[] array, int baseSeed)
    {
        T[] outArray = new T[array.Length];
        if (default(T) != null)
        {
            //convert array naar string array en roep deze func. op voor die string array, convert dan terug naar deze array
        }
        int seed = baseSeed * (array.Length + 1);
        int i2 = 0;
        for (int i = 0; i < array.Length; i++)
        {
            Randomize:
            i2++;
            
            Random rg = new Random(seed + i2);
            
            int y = rg.Next(0, array.Length);

            if (EqualityComparer<T>.Default.Equals(array[y], default(T)))//array[y] == null)
            {
                goto Randomize;
            }
            outArray[i] = array[y];
            array[y] = default(T);
        }
        return outArray;
    }
}