using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            int daysLeftCount = 0;

            // Number of considered weekends
            int loops = 0;
            int weekedsCnt = weekEnds == null ? 0 : weekEnds.Length;

            // In case there is no weekend, just add the required number of days
            if (weekedsCnt == 0)
            {
                return startDate.AddDays(dayCount - 1);
            }

            do
            {
                if (weekedsCnt == loops) // Just in case the weekend is over and there are working days left
                {
                    daysLeftCount++;
                    if (daysLeftCount != dayCount)
                    {
                        startDate = startDate.AddDays(1);
                    }
                }  
                else if (startDate == weekEnds[loops].StartDate) // In case the day falls on a weekend, if the day falls on a weekend, we increase it until we reach the last weekend
                {
                    startDate = startDate.AddDays((weekEnds[loops].EndDate - weekEnds[loops].StartDate).TotalDays + 1);
                    loops++;
                }
                else if (startDate != weekEnds[loops].StartDate) // Checking whether the day does not fall on a weekend
                {
                    daysLeftCount++;
                    if (daysLeftCount != dayCount)
                    {
                        startDate = startDate.AddDays(1);
                    }
                }
            } while (daysLeftCount < dayCount);

            return startDate;
        }
    }
}
