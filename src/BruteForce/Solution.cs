using System.Collections.Generic;

public struct Solution
{

    /// <summary>
    /// Combination of vials
    /// Index of an element is the index in an array of available vial volumes
    /// Values of an element is the number of vials of the give volume (the volume defined by the index)
    /// </summary>
    public int[] Vials { get; }

    public int TotalVolume { get; }

    public Solution(int totalVolume, int[] vials)
    {
        Vials = vials;
        TotalVolume = totalVolume;
    }

}