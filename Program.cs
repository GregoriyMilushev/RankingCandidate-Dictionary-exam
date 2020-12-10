using System;
using System.Collections.Generic;
using System.Linq;

namespace RankingContest
{
    class Program
    {
        static void Main(string[] args)
        {
            var contestsPassword = new Dictionary<string, string>();
            var users = new Dictionary<string, Dictionary<string, int>>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "end of contests")
                {
                    break;
                }

                var contests = input.Split(":");
                string contest = contests[0];
                string password = contests[1];

                if (!contestsPassword.ContainsKey(contest))
                {
                    contestsPassword[contest] = password;
                }

            }

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "end of submissions")
                {
                    break;
                }

                var contests = input.Split("=>");

                string contest = contests[0];
                string password = contests[1];
                string user = contests[2];
                int points = int.Parse(contests[3]);

                if (contestsPassword.ContainsKey(contest))
                {
                    if (contestsPassword[contest] == password)
                    {
                        if (!users.ContainsKey(user))
                        {
                            users[user] = new Dictionary<string, int>();
                        }

                        if (!users[user].ContainsKey(contest))
                        {
                            users[user].Add(contest, points);
                        }

                        if (points > users[user][contest])
                        {
                            users[user][contest] = points;
                        }
                    }
                }
            }

            string bestCandidate = string.Empty;
            int totalPoints = 0;

            foreach (var user in users)
            {
                int sum = 0;

                foreach (var contest in user.Value)
                {
                    sum += contest.Value;
                }

                if (sum > totalPoints)
                {
                    totalPoints = sum;
                    bestCandidate = user.Key;
                }
            }

            Console.WriteLine($"Best candidate is {bestCandidate} with total {totalPoints} points.");

            Console.WriteLine("Ranking:");

            foreach (var user in users.OrderBy(x =>x.Key))
            {
                Console.WriteLine($"{user.Key}");

                foreach (var (contest,points) in user.Value.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"#  {contest} -> {points}");
                }
            }
        }
    }
}
