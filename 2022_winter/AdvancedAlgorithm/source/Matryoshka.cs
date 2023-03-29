namespace BoxProject
{
    public static class MatryoshkaBoxesExtension
    {

        public static Box[] Matryoshka(this Box[] boxesArray)
        {
            int arrLen = boxesArray.GetLength(0);
            float[] dpWidth = new float[arrLen + 1];
            int[] seqLength = new int[arrLen];
            int Lmax = 0;
            /* initialize*/
            dpWidth[0] = -1;
            /* Dynamic Programming */
            for (int i = 0; i < arrLen; i++)
            {
                if(boxesArray[i].Width >= dpWidth[Lmax]){
                    dpWidth[Lmax+1] = boxesArray[i].Width;
                    Lmax++;
                    seqLength[i] = Lmax;
                }else{
                    int idx = BinarySearch(dpWidth, boxesArray[i].Width, startIdx:0, endIdx: Lmax);
                    dpWidth[idx] = boxesArray[i].Width;
                    seqLength[i] = idx;
                }
            }
            /* Restore Box Sequence*/
            Box[] boxSequence = new Box[Lmax];
            int l = Lmax;
            for(int i=arrLen-1;i>=0;i--){
                if (seqLength[i] != l){
                    continue;
                }else{
                    boxSequence[l-1] = boxesArray[i];
                    l--;
                }
            }            
            return boxSequence;
        }
        public static int BinarySearch(float[] array, float value, int startIdx, int endIdx){
            int mid = (startIdx + endIdx) / 2;
            if (array[mid] <= value && ( array[mid+1] > value)){
                return mid+1;
            }else if (array[mid+1] <= value){
                return BinarySearch(array, value, mid+1, endIdx);
            }else{
                return BinarySearch(array, value, startIdx, mid);
            }
        }
    }
}