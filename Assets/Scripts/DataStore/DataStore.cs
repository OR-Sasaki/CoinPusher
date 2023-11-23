
public static class DataStore
{
    public static int coinNum;
    public static int slotPointNum;

    public static SlotStatus slotStatus = new();
    
    public class SlotStatus
    {
        public bool inDirection;

        public int[] marks = new int[3];
        public bool isReach;
        public SlotMark hitMark;
    }

    public enum SlotMark
    {
        None,
        Coin1,
        Coin2,
        Coin3,
        Coin4,
        Present,
        Jackpot,
    }

    public enum StopType
    {
        Lcr,
        Lrc,
        Rcl
    }
}
