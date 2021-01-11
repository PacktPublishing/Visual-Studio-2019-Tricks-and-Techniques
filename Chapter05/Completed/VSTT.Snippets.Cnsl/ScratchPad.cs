using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace VSTT.Snippets.Cnsl
{
    internal class ScratchPad
    {
        // Insert snippets here
        public ScratchPad()
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                foreach (var item in collection)
                {
                    var myEnum = EmployeeType.Contractor;
                    switch (myEnum)
                    {
                        case EmployeeType.Volunteer:
                            break;

                        case EmployeeType.Unpaid_Intern:
                            break;

                        case EmployeeType.Hourly:
                            break;

                        case EmployeeType.Salaried:
                            break;

                        case EmployeeType.Contractor:
                            break;

                        default:
                            break;
                    }
                }

                stopwatch.Stop();
                Log.LogInformation($"Executed {nameof(ScratchPad)}.{MethodBase.GetCurrentMethod().Name} in {(stopwatch.Elapsed.TotalMilliseconds)} Milliseconds");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}