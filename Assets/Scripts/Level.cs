using System;

class Level
{
    public float duration { get; set; }
    public int amount { get; set; }

    public Level(float duration, int amount)
    {
        this.duration = duration;
        this.amount = amount;
    }

    public static Level Easy = new Level(12f, 1);
    public static Level Medium = new Level(10f, 1);
    public static Level Hard = new Level(8f, 2);
}