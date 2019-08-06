using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace Phone.Extensions
{
    public static class TasksExtentions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void FireAndForget(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }
        }
    }
}
