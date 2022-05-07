public static class PrototypeProgression
{
    private static byte completion;

    public static void Completed(byte boss){
        completion |= boss;
    }

    public static bool CheckBit(int position){
        return ((completion & (byte) (1 << position)) > 0);
    }
}