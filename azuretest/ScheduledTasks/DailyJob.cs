using azuretest.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace azuretest.ScheduledTasks
{

    

    public class DailyJob : IJob
    {
        AspWebApiDbContext dbContext = new AspWebApiDbContext();
        public Task Execute(IJobExecutionContext context)
        {

            List<UserInformation> users = dbContext.UserInformations.ToList();
            List<UserMilestone> userMilestones = dbContext.UserMilestones.ToList();

            foreach (UserInformation user in users)
            {
                if (CheckCalorieGoalStreak(user.UserId, 1))
                {
                    if (userMilestones.FirstOrDefault(x => x.UserId == user.UserId && x.MilestoneId == 4) == null)
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
                    if (userMilestones.FirstOrDefault(x => x.UserId == user.UserId && x.MilestoneId == 5) == null)
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
                    if (userMilestones.FirstOrDefault(x => x.UserId == user.UserId && x.MilestoneId == 6) == null)
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
                    if (userMilestones.FirstOrDefault(x => x.UserId == user.UserId && x.MilestoneId == 7) == null)
                    {
                        UserMilestone newMilestone = new UserMilestone();
                        newMilestone.UserId = user.UserId;
                        newMilestone.MilestoneId = 7;
                        dbContext.UserMilestones.Add(newMilestone);
                        dbContext.SaveChanges();
                    }
                }

            }


            return Task.CompletedTask;
        }


        // This method will take in the user's ID and the number of consecutive days to check
        public bool CheckCalorieGoalStreak(int userId, int consecutiveDays)
        {
            using (var context = new AspWebApiDbContext()) // Replace with your own DbContext class
            {
                var user = context.UserInformations.Include(u => u.MealsHistories) // Eager load the meals histories for the user
                                                   .FirstOrDefault(u => u.UserId == userId);
                if (user == null) // User with the given ID not found
                {
                    throw new ArgumentException("User not found");
                }

                // Get the start date and end date for the period we want to check
                var startDate = DateTime.UtcNow.Date.AddDays(-(consecutiveDays)); // Subtract 1 because we want to include today
                var endDate = DateTime.UtcNow.Date;

                int calorieGoal = user.CalorieGoal ?? 0; // Default to 0 if the user hasn't set a calorie goal

                // Loop through each date in the period we want to check
                int streak = 0;
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    // Get the meals for the current date
                    var mealsForDate = user.MealsHistories.Where(m => m.Date.Date == date)
                                                         .ToList();

                    // If there are no meals for the current date, reset the streak and move on to the next date
                    if (mealsForDate.Count == 0)
                    {
                        streak = 0;
                        continue;
                    }

                    // Calculate the total calories for the meals for the current date
                    int totalCaloriesForDate = mealsForDate.Sum(m => m.Calories);

                    // If the total calories for the current date exceeded the calorie goal, increment the streak
                    if (totalCaloriesForDate < calorieGoal && totalCaloriesForDate > 1)
                    {
                        streak++;
                    }
                    else
                    {
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
        }

    }
}
