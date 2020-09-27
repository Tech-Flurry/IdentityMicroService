using System;
using System.Threading;

namespace Domain.Infrastucture.Common
{
    public static class Functions
    {
        public static void SetTimeout(Action action, int delaySeconds)
        {
            var timer = new Timer(new TimerCallback(_ =>
            {
                action.Invoke();
            }), null, TimeSpan.FromSeconds(delaySeconds), Timeout.InfiniteTimeSpan);
        }
    }
}
