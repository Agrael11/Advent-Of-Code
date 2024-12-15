using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Visualizers;

namespace advent_of_code
{
    public class ChallangeHandling
    {
        private readonly static string baseString = "advent_of_code.Year{0:D4}.Day{1:D2}.Challange{2:D}, advent_of_code.Year{0:D4}.Day{1:D2}";
        public static async Task<(Stopwatch stopwatch, string result)> RunTaskAsync(int year, int day, int part, bool visualisation, Action<Stopwatch, bool, string>? FinishedCallback = null)
        {
            Stopwatch watch;
            var result = "";
            var challengeMethod = GetMethodInfo(year, day, part);

            var inputData = await GetInputAsync(year, day);
            if (inputData == null)
            {
                watch = Stopwatch.StartNew();
                watch.Stop();
                FinishedCallback?.Invoke(watch, false, "ERROR: Not possible to get Input File");
                return (watch, "ERROR: Not possible to get Input File");
            }

            if (challengeMethod is null)
            {
                watch = Stopwatch.StartNew();
                watch.Stop();
                FinishedCallback?.Invoke(watch, false, "ERROR: Not possible to Challange function");
                return (watch, "ERROR: Not possible to Challange function");
            }
            
            //PreWarm
            AOConsole.Enabled = false;
            try
            {
                challengeMethod.Invoke(null, [inputData]);
            }
            catch (Exception ex)
            {
                watch = Stopwatch.StartNew();
                watch.Stop();
                var error = new System.Text.StringBuilder();
                error.AppendLine("ERROR: Not possible to Pre-warm Challange function");
                FormatException(error, ex);
                FinishedCallback?.Invoke(watch, false, error.ToString());
                return (watch, error.ToString());
            }
            AOConsole.Enabled = visualisation;

            watch = Stopwatch.StartNew();
            try
            {
                result = challengeMethod.Invoke(null, [inputData])?.ToString() ?? "ERROR";
                watch.Stop();
            }
            catch (Exception ex)
            {
                watch.Stop();
                var error = new System.Text.StringBuilder();
                error.AppendLine("ERROR: Not possible to run Challange");
                FormatException(error, ex);
                FinishedCallback?.Invoke(watch, false, error.ToString());
                return (watch, error.ToString());
            }

            FinishedCallback?.Invoke(watch, true, result);

            return (watch, result);
        }

        private static void FormatException(System.Text.StringBuilder builder, Exception exception)
        {
            builder.AppendLine(exception.Message);
            if (exception.StackTrace is not null) builder.AppendLine(exception.StackTrace);
            if (exception.InnerException is not null)
            {
                builder.AppendLine();
                FormatException(builder, exception.InnerException);
            }
        }

        public static async Task<string?> GetInputAsync(int year, int day)
        {
            var state = await DownloadInputAsync(year, day);

            return state == DownloadState.FileExists || state == DownloadState.Downloaded
                ? FileHandling.ReadFile(Path.Combine("Inputs", $"inputData_{year}_{day}.txt"))
                : null;
        }

        private enum DownloadState { Downloaded, FileExists, FailedDownload, FailedFileWrite, NoCookie }
        private static async Task<DownloadState> DownloadInputAsync(int year, int day)
        {
            if (FileHandling.FileExists(Path.Combine("Inputs", $"inputData_{year}_{day}.txt")))
            {
                return DownloadState.FileExists;
            }

            if (!FileHandling.FileExists(Path.Join("Settings", "cookie.txt")))
            {
                return DownloadState.NoCookie;
            }

            string cookie;

            try
            {
                cookie = FileHandling.ReadFile(Path.Join("Settings", "cookie.txt"));
            }
            catch
            {
                return DownloadState.NoCookie;
            }

            byte[] data;

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Cookie", cookie);
                var response = client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
                response.Wait();

                if (response.Result.StatusCode != HttpStatusCode.OK)
                {
                    return DownloadState.FailedDownload;
                }

                data = await response.Result.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                return DownloadState.FailedDownload;
            }

            try
            {
                if (!FileHandling.DirectoryExists("Inputs"))
                {
                    FileHandling.CreateDirectory("Inputs");
                }
                FileHandling.WriteToFile(Path.Combine("Inputs", $"inputData_{year}_{day}.txt"), data);
            }
            catch
            {
                return DownloadState.FailedFileWrite;
            }

            return DownloadState.Downloaded;
        }

        public static List<int> GetAvailableYears()
        {
            List<int> possibleYears = [];
            for (var year = 2015; year <= DateTime.Now.Year; year++)
            {
                for (var day = 1; day <= 25; day++)
                {
                    if (ClassExists(year, day))
                    {
                        possibleYears.Add(year);
                        break;
                    }
                }
            }
            return possibleYears;
        }

        public static List<int> GetAvailableDays(int year)
        {
            List<int> possibleDays = [];
            for (var day = 1; day <= 25; day++)
            {
                if (ClassExists(year, day))
                    possibleDays.Add(day);
            }
            return possibleDays;
        }

        public static bool ClassExists(int year, int day)
        {
            var type = Type.GetType(string.Format(baseString, year, day, 1));
            return type != null;
        }

        public static MethodInfo? GetMethodInfo(int year, int day, int challange)
        {
            var type = Type.GetType(string.Format(baseString, year, day, challange));
            return type?.GetMethod("DoChallange");
        }

        public static string FormatTime(ulong milliseconds)
        {
            var milli = (uint)(milliseconds % 1000);
            var seconds = (uint)(milliseconds / 1000);
            var minutes = seconds / 60;
            seconds %= 60;
            var hours = minutes / 60;
            minutes %= 60;
            var time = "";
            if (hours > 0) time = hours.ToString().PadLeft(2, '0') + ":";
            if (minutes > 0 || hours > 0) time += minutes.ToString().PadLeft(2, '0') + ":";
            if (seconds > 0 && (minutes > 0 || hours > 0)) time += seconds.ToString().PadLeft(2, '0') + "." + milli.ToString().PadLeft(3, '0');
            else if (seconds > 0) time += seconds.ToString().PadLeft(2, '0') + "." + milli.ToString().PadLeft(3, '0') + "s";
            else time += milli.ToString().PadLeft(3, '0') + "ms";
            return time;
        }
    }
}
