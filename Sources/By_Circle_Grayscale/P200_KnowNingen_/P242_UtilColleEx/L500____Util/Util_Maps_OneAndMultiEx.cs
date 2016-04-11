using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P218_Starlight__.L___500_Struct;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P242_UtilColleEx.L500____Util
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
