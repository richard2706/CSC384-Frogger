using System;

[Serializable]
public class Achievement
{
    public string Title { get; private set; }

    public Achievement(string title)
    {
        Title = title;
    }
}
