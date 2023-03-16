namespace azuretest
{
    public class MyMilestoneModel
    {

        public MyMilestoneModel(string milestoneName, int goal, int score, int prog)
        {
            MilestoneName = milestoneName;
            Goal = goal;
            Score = score;
            progress = prog;
        }

        public string MilestoneName { get; set; } = null!;

        public int Goal { get; set; }

        public int Score { get; set; }

        public int progress { get; set; }
    }
}
