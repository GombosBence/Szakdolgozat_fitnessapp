using azuretest.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace azuretest.ScheduledTasks
{
    public class BackgroundServices : BackgroundService
    {
        AspWebApiDbContext dbContext = new AspWebApiDbContext();
        private readonly ILogger<BackgroundServices> _logger;
        private Timer _timer;

        public BackgroundServices(ILogger<BackgroundServices> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background service started.");
            
            
        }

       

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background service stopped.");
            _timer?.Dispose();
            return Task.CompletedTask;
        }


        public bool CheckCalorieGoalStreak(int userId, int consecutiveDays)
        {
            
                var user = dbContext.UserInformations.First(u => u.UserId == userId);
                List<MealsHistory> mealsHistory = dbContext.MealsHistories.ToList();


                if (user == null) // User with the given ID not found
                {
                    throw new ArgumentException("User not found");
                }

                // Get the start date and end date for the period we want to check
                var startDate = DateTime.Now.AddDays(-consecutiveDays);
                var endDate = DateTime.Now.AddDays(-1);

                int calorieGoal = (int)(user.CalorieGoal == null ? 0 : user.CalorieGoal);

                // Loop through each date in the period we want to check
                int streak = 0;
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    // Get the meals for the current date
                    List<MealsHistory> mealsForDate = mealsHistory.Where(m => m.UserId == user.UserId && m.Date.ToString("yyyy-MM-dd") == date.ToString("yyyy-MM-dd"))
                                                         .ToList();

                    // If there are no meals for the current date, reset the streak and move on to the next date
                    if (mealsForDate.Count == 0)
                    {
                    if (user.MaximumCalStreak != null && user.MaximumCalStreak < streak)
                    {
                        user.MaximumCalStreak = streak;
                        dbContext.UserInformations.Update(user);
                        dbContext.SaveChanges();

                    }
                    if (user.MaximumCalStreak == null)
                    {
                        user.MaximumCalStreak = 0;
                        dbContext.UserInformations.Update(user);
                        dbContext.SaveChanges();
                    }

                        streak = 0;
                        continue;
                    }

                    // Calculate the total calories for the meals for the current date
                    int totalCaloriesForDate = mealsForDate.Sum(m => m.Calories);

                    // If the total calories for the current date exceeded the calorie goal, increment the streak
                    if (totalCaloriesForDate < calorieGoal)
                    {
                        streak++;
                    }
                    else
                {
                    if (user.MaximumCalStreak != null && user.MaximumCalStreak < streak)
                    {
                        user.MaximumCalStreak = streak;
                        dbContext.UserInformations.Update(user);
                        dbContext.SaveChanges();

                    }
                    if (user.MaximumCalStreak == null)
                    {
                        user.MaximumCalStreak = 0;
                        dbContext.UserInformations.Update(user);
                        dbContext.SaveChanges();
                    }
                    streak = 0;
                    }

                    // If we've reached the desired streak length, return true
                    if (streak >= consecutiveDays)
                    {
                        return true;
                    }
                }

                // If we made it through the loop without returning true, the streak was not achieved
                return false;
            
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            { 


                List<UserInformation> users = dbContext.UserInformations.ToList();
                List<UserMilestone> userMilestones = dbContext.UserMilestones.ToList();
                List<StepsHistory> userSteps = dbContext.StepsHistories.ToList();


                foreach (UserInformation user in users)
                {
                    if(userMilestones.FirstOrDefault(x => x.UserId == user.UserId) == null)
                    {
                        break;
                    }

                    //Check maximum steps

                    List<StepsHistory> specSteps = userSteps.Where(x => x.UserId == user.UserId).ToList();
                    var maximum = specSteps.OrderByDescending(o => o.Steps).First();
                    user.MaximumSteps = maximum.Steps;
                    dbContext.UserInformations.Update(user);
                    dbContext.SaveChanges();


                    //Check 1 day streak

                    if (CheckCalorieGoalStreak(user.UserId, 1))
                    {
                        if (!userMilestones.Any(x => x.UserId == user.UserId && x.MilestoneId == 4))
                        {
                            UserMilestone newMilestone = new UserMilestone();
                            newMilestone.UserId = user.UserId;
                            newMilestone.MilestoneId = 4;
                            dbContext.UserMilestones.Add(newMilestone);
                            dbContext.SaveChanges();
                        }
                    }

                    //Check 5 days streak

                    if (CheckCalorieGoalStreak(user.UserId, 5))
                    {
                        if (!userMilestones.Any(x => x.UserId == user.UserId && x.MilestoneId == 5))
                        {
                            UserMilestone newMilestone = new UserMilestone();
                            newMilestone.UserId = user.UserId;
                            newMilestone.MilestoneId = 5;
                            dbContext.UserMilestones.Add(newMilestone);
                            dbContext.SaveChanges();
                        }
                    }

                    //Check 10 days streak

                    if (CheckCalorieGoalStreak(user.UserId, 10))
                    {
                        if (!userMilestones.Any(x => x.UserId == user.UserId && x.MilestoneId == 6))
                        {
                            UserMilestone newMilestone = new UserMilestone();
                            newMilestone.UserId = user.UserId;
                            newMilestone.MilestoneId = 6;
                            dbContext.UserMilestones.Add(newMilestone);
                            dbContext.SaveChanges();
                        }
                    }

                    //Check 30 days streak

                    if (CheckCalorieGoalStreak(user.UserId, 30))
                    {
                        if (!userMilestones.Any(x => x.UserId == user.UserId && x.MilestoneId == 7))
                        {
                            UserMilestone newMilestone = new UserMilestone();
                            newMilestone.UserId = user.UserId;
                            newMilestone.MilestoneId = 7;
                            dbContext.UserMilestones.Add(newMilestone);
                            dbContext.SaveChanges();
                        }
                    }

                    //ADD points
                    List<Milestone> milestones = dbContext.Milestones.ToList();
                    int sum = 0;
                    foreach (var milestone in userMilestones)
                    {

                        sum += milestones.First(x => x.Id == milestone.MilestoneId).Score;

                    }
                    user.MilestoneScore = sum;
                    dbContext.UserInformations.Update(user);
                    dbContext.SaveChanges();
                    sum = 0;
                    
                }
                    await Task.Delay(60000, stoppingToken);
            }
        }
    }

}
