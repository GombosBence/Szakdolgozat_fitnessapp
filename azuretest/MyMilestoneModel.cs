namespace azuretest
{
    public class MyMilestoneModel
    {

        public MyMilestoneModel(string milestoneName, int goal, int score)
        {
            MilestoneName = milestoneName;
            Goal = goal;
            Score = score;
        }

        public string MilestoneName { get; set; } = null!;

        public int Goal { get; set; }

        public int Score { get; set; }
    }
}
