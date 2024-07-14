namespace ArithmeticGenerator.Models;

/// <summary>
/// 数字
/// </summary>
/// <param name="part1Length">整数位数</param>
/// <param name="part2Length">小数位数</param>
public class CustomNumber(int part1Length, int part2Length)
{
    /// <summary>
    /// 整数位数
    /// </summary>
    public int Part1Length { get; set; } = part1Length;
    /// <summary>
    /// 小数位数
    /// </summary>
    public int Part2Length { get; set; } = part2Length;

    public override string ToString()
    {
        if (Part2Length == 0)
        {
            return $"{Part1Length}位整数";
        }
        return $"{Part1Length}位整数{Part2Length}位小数";
    }
}