namespace advent_of_code.Year2024.Day13
{
    internal class ClawMachine
    {
        public long PrizeX;
        public long PrizeY;
        public long ButtonAX;
        public long ButtonAY;
        public long ButtonBX;
        public long ButtonBY;

        public ClawMachine(string[] input, int index)
        {
            var splitData = input[index].Split(',', StringSplitOptions.TrimEntries);
            ButtonAX = long.Parse(splitData[0][(splitData[0].LastIndexOf(' ') + 3)..]);
            ButtonAY = long.Parse(splitData[1][2..]);
            splitData = input[index + 1].Split(',', StringSplitOptions.TrimEntries);
            ButtonBX = long.Parse(splitData[0][(splitData[0].LastIndexOf(' ') + 3)..]);
            ButtonBY = long.Parse(splitData[1][2..]);
            splitData = input[index + 2].Split(',', StringSplitOptions.TrimEntries);
            PrizeX = long.Parse(splitData[0][(splitData[0].LastIndexOf(' ') + 3)..]);
            PrizeY = long.Parse(splitData[1][2..]);
        }
        public long FindCheapestTickets(long offset = 0)
        {
            // I Don't even know, spent too much time on white board trying to make this equation
            // A is Button A, B is Button B, C is Result
            /* 
                T1*Ax + T2*Bx = Cx
                T1*Ay + T2*By = Cy
            
                T1*Ax = Cx - T2*Bx
                T1 = (Cx-T2*Bx)/Ax

                T1*Ay + T2*By = Cy

                ((Cx-T2*Bx)/Ax) * Ay + T2*By = Cy
                ((Ay*(Cx-T2*Bx))/Ax) + T2*By = Cy
                ((Ay*(Cx-T2*Bx))/Ax) + T2*By = Cy

                Ay*(Cx-T2*Bx) + T2*By*Ax = Cy*Ax
                Ay*Cx - Ay*T2*Bx + T2*By*Ax = Cy*Ax

                -Ay*T2*Bx + T2*By*Ax = Cy*Ax - Ay*Cx
                T2*(-Ay*Bx + By*Ax) = Cy*Ax - Ay*Cx
                T2 = (Cy*Ax - Ay*Cx) / (-Ay*Bx + By*Ax)

                T2 = (Cy*Ax - Cx*Ay) / (Ax*By - Ay*Bx)
             */

            var targetX = PrizeX + offset;
            var targetY = PrizeY + offset;

            var numerator = (targetY * ButtonAX - targetX * ButtonAY);
            var denominator = (ButtonAX * ButtonBY - ButtonAY * ButtonBX);
            
            if ((denominator == 0) || (numerator % denominator != 0)) return -1;

            var t2 = numerator / denominator;

            var remainingX = targetX - (ButtonBX * t2);
            if (remainingX % ButtonAX != 0) return -1;

            var t1 = remainingX / ButtonAX;

            if (t1 < 0 || t2 < 0) return -1;

            return t1 * 3 + t2;
        }

        public override string ToString()
        {
            return $"A: [{ButtonAX},{ButtonAY}]; B: [{ButtonBX},{ButtonBY}]; P: [{PrizeX},{PrizeY}]";
        }
    }
}