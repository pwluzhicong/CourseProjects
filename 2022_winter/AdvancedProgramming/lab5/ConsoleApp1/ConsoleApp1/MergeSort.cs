namespace BoxProject
{
    public static class MergeSortBoxesExtension
    {

        public static Box[] MergeSort(this Box[] boxesArray)
        {
            int arrLen = boxesArray.GetLength(0);
            Box[] boxesArrayTemp = new Box[arrLen];

            MergeSort(boxesArray, boxesArrayTemp, 0, arrLen);

            return boxesArray;
        }
        private static void MergeSort(Box[] boxesArray, Box[] boxesArrayTemp, int left, int right)
        {
            if (right <= left + 1)
            {
                return;
            }
            int mid = (left + right) / 2;
            MergeSort(boxesArray, boxesArrayTemp, left, mid);
            MergeSort(boxesArray, boxesArrayTemp, mid, right);
            Merge(boxesArray, boxesArrayTemp, left, mid, right);

        }
        private static void Merge(Box[] boxesArray, Box[] boxesArrayTemp, int left, int mid, int right)
        {
            int i = left, j = mid, p = left;
            while (i != mid || j != right)
            {
                if (i == mid || (j != right && boxesArray[i].greaterThan(boxesArray[j])))
                {
                    boxesArrayTemp[p] = boxesArray[j];
                    j++;
                    p++;
                }
                else
                {
                    boxesArrayTemp[p] = boxesArray[i];
                    i++;
                    p++;
                }

            }
            for (int k = left; k < right; k++)
            {
                boxesArray[k] = boxesArrayTemp[k];
            }
        }
        public static bool greaterThan(this Box b1, Box b2)
        {
            if (b1.Length > b2.Length)
            {
                return true;
            }
            else if (b1.Length == b2.Length)
            {
                return b1.Width > b2.Width;
            }
            else
            {
                return false;
            }
        }
    }

}