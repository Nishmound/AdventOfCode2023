
internal class Day14 : AdventDay<int>
{
    public int RunP1(StreamReader reader)
    {
        List < (bool[] round, bool[] cube)> layers = new();
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            bool[] layerRound = new bool[line.Length];
            bool[] layerCube = new bool[line.Length];
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '#') layerCube[i] = true;
                else if (line[i] == 'O')
                {
                    if (layers.Count == 0 || layers[^1].round[i] || layers[^1].cube[i])
                    {
                        layerRound[i] = true;
                        continue;
                    }
                    
                    for (int j = layers.Count - 1; j >= 0; j--)
                    {
                        if (layers[j].round[i] || layers[j].cube[i])
                        {
                            layers[j + 1].round[i] = true;
                            break;
                        }
                        else if (j == 0)
                        {
                            layers[0].round[i] = true;
                        }
                    }
                }
            }
            layers.Add((layerRound, layerCube));
        }
        /*
        foreach (var layer in layers)
        {
            foreach (var obs in layer)
            {
                if (obs) Console.Write('#');
                else Console.Write('.');
            }
            Console.WriteLine();
        }
        */
        return layers.Select((layer, i) => layer.round.Count(x => x) * (layers.Count - i)).Sum();
    }

    public int RunP2(StreamReader reader)
    {
        return 0;
    }
}