using System;
using System.Runtime.InteropServices;

namespace verbine_rpg
{
    class Math
    {
        static double RNG(double minimum, double maximum) // Generates a random number including the given min and max.
        {
            var random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
        public static string[] PlayerIDC(string[] charStats, double dam) // Incoming Damage (calc against player stats)
        {
            var healtharr = charStats[1];
            var maxhealtharr = charStats[2];
            var dodgearr = charStats[5];
            var defarr = charStats[4];
            var health = Convert.ToDouble(healtharr); // Convert to a double for math manipulaton
            var maxhealth = Convert.ToDouble(maxhealtharr);
            var dodge = Convert.ToDouble(dodgearr);
            dodge /= 10; // 50 dodge -> 5% chance, default 5 dex is 0.5% chance.
            var def = Convert.ToDouble(defarr);
            def /= 10; // 50 defence -> 10% damage reduction
            var dodgecheck = RNG(0, 100);
            if (dodgecheck <= dodge)
            {
                goto done; // Successful dodge
            }
            var adjdam = dam - def; // Defence damage reduction.
            health -= adjdam; // If dodge check fails, apply defence-adjusted damage.
            if (health <= 0) // If health is below 0, set to 0 (Critical Condition)
            {
                health = 0;
                // Todo: Critical Condition code here.
            }
            else if (health > maxhealth) // If health is above maximum, set to maximum (Impossible to overheal)
            {
                health = maxhealth;
            }
            done:
            healtharr = health.ToString(); // To return the complete charStats, convert back into string.
            charStats[1] = healtharr;
            return charStats; // Send back entire array.
        }

        public static string[] PlayerODC(string[] enemStats, double dam) // Outgoing Damage (calc against enemy stats)
        {
            var healtharr = enemStats[1];
            var maxhealtharr = enemStats[2];
            var dodgearr = enemStats[5];
            var defarr = enemStats[4];
            var health = Convert.ToDouble(healtharr); // Convert to a double for math manipulaton
            var maxhealth = Convert.ToDouble(maxhealtharr);
            var dodge = Convert.ToDouble(dodgearr);
            dodge /= 10; // 50 dodge -> 5% chance, default 5 dex is 0.5% chance.
            var def = Convert.ToDouble(defarr);
            def /= 10; // 50 defence -> 10% damage reduction
            var dodgecheck = RNG(0, 100);
            if (dodgecheck <= dodge)
            {
                goto done; // Successful dodge
            }
            var adjdam = dam - def; // Defence damage reduction.
            health -= adjdam; // If dodge check fails, apply defence-adjusted damage.
            if (health <= 0) // If health is below 0, set to 0 (Critical Condition)
            {
                health = 0;
                // Todo: Critical Condition code here.
            }
            else if (health > maxhealth) // If health is above maximum, set to maximum (Impossible to overheal) Todo:
            {
                health = maxhealth;
            }
            done:
            healtharr = health.ToString(); // To return the complete enemStats, convert back into string.
            enemStats[1] = healtharr;
            return enemStats; // Send back entire array.
        }
    }
}
