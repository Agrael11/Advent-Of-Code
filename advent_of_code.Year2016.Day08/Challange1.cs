﻿using advent_of_code.Helpers;

namespace advent_of_code.Year2016.Day08 
{
    public static class Challange1
    {
        private static readonly int ScreenWidth = 50;
        private static readonly int ScreenHeight = 6;
        private static readonly Grid<bool> Screen = new Grid<bool>(ScreenWidth, ScreenHeight);

        public static int DoChallange(string inputData)
        {
            var input = inputData.Replace("\r", "").TrimEnd('\n').Split('\n');
            //input = [ "rect 3x2","rotate column x=1 by 1","rotate row y=0 by 4","rotate column x=1 by 1"];

            var lightsOn = 0;

            foreach (var instruction in input)
            {
                if (instruction.StartsWith("rect"))
                {
                    //instruction format : fill *width*x*height*
                    var splitInstruction = instruction.Split(' ')[1].Split('x');
                    var width = int.Parse(splitInstruction[0]);
                    var height = int.Parse(splitInstruction[1]);
                    for (var x = 0; x < width; x++)
                    {
                        if (x >= ScreenWidth) break;
                        for (var y = 0; y < height; y++)
                        {
                            if (y >= ScreenHeight) break;
                            if (Screen[x, y] == false) lightsOn++;
                            Screen[x, y] = true;
                        }
                    }
                }
                else if (instruction.StartsWith("rotate"))
                {
                    //instruction format : rotate column by *instructionType x/y*=*index* by *count*
                    var splitInstruction = instruction.Split('=');
                    var instructionType = splitInstruction[0].Split(' ')[2];
                    var index = int.Parse(splitInstruction[1].Split(' ')[0]);
                    var count = int.Parse(splitInstruction[1].Split(' ')[2]);

                    if (instructionType == "x")
                    {
                        //rotate colunmn *index* by *count*
                        Screen.RotateColumn(index, count);
                    }
                    else if (instructionType == "y")
                    {
                        //rotate row *index* by *count*
                        Screen.RotateRow(index, count);
                    }
                }
            }

            return lightsOn;
        }
    }
}