namespace Net_4._0_Extentions
{
    using System.Collections;

    public static class ConverterExtension
    {
        public static string BitArrayToHex(BitArray bitArray)
        {
            int[] intArray = new int[1];
            bitArray.CopyTo(intArray, 0);

            return intArray[0].ToString("X");
        } 
    }
}