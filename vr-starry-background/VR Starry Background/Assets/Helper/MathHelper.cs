public static class MathHelper
{
    public static bool IsBetween(float value, float minInclusive, float maxExclusive)
    {
        return minInclusive <= value && value < maxExclusive;
    }
}