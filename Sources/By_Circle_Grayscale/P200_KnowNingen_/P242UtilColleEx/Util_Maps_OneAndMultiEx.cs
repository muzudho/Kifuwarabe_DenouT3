﻿using Grayscale.P035Collection.L500Struct;
using Grayscale.P218Starlight.I500Struct;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P242UtilColleEx.L500Util
{
    public abstract class Util_Maps_OneAndMultiEx<T1, T2>
    {

        public static int CountAllElements(Maps_OneAndMulti<Finger, Starbeamable> collection)
        {
            int count = 0;

            foreach (KeyValuePair<Finger, List<Starbeamable>> entry in collection.Items)
            {
                foreach (Starbeamable starbeam in entry.Value)
                {
                    count++;
                }
            }

            return count;
        }


    }
}