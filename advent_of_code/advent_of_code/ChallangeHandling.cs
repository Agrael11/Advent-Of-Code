using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code
{
    public class ChallangeHandling
    {

        public static async Task<(Stopwatch stopwatch, string result)> RunTaskAsync(int year, int day, int part, Action<Stopwatch, string>? FinishedCallback = null)
        {
            Stopwatch watch;
            string result = "";
            MethodInfo? challengeMethod = GetMethodInfo(year, day, part);

            string? inputData = await GetInputAsync(year, day);
            if (inputData == null)
            {
                watch = Stopwatch.StartNew();
                watch.Stop();
                FinishedCallback?.Invoke(watch, "ERROR: Not possible to get Input File");
                return (watch, "ERROR: Not possible to get Input File");
            }

            if (challengeMethod is null)
            {
                watch = Stopwatch.StartNew();
                watch.Stop();
                FinishedCallback?.Invoke(watch, "ERROR: Not possible to Challange function");
                return (watch, "ERROR: Not possible to Challange function");
            }

            watch = Stopwatch.StartNew();
            try
            {
                result = challengeMethod.Invoke(null, [inputData])?.ToString() ?? "ERROR";
                watch.Stop();
            }
            catch (Exception ex)
            {
                watch.Stop();
                FinishedCallback?.Invoke(watch, $"ERROR: {ex.Message}");
                return (watch, $"ERROR: {ex.Message}");
            }

            FinishedCallback?.Invoke(watch, result);

            return (watch, result);
        }

        private static async Task<string?> GetInputAsync(int year, int day)
        {
            DownloadState state = await DownloadInputAsync(year, day);

            if (state == DownloadState.FileExists || state == DownloadState.Downloaded)
            {
                return FileHandling.ReadFile(Path.Combine("Inputs", $"inputData_{year}_{day}.txt"));
            }

            return null;
        }


        enum DownloadState { Downloaded, FileExists, FailedDownload, FailedFileWrite, NoCookie }
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

            string cookie = FileHandling.ReadFile(Path.Join("Settings", "cookie.txt"));

            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("Cookie", cookie);
                Task<HttpResponseMessage> response = client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
                response.Wait();

                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    byte[] data = await response.Result.Content.ReadAsByteArrayAsync();
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
                else
                {
                    return DownloadState.FailedDownload;
                }
            }
            catch
            {
                return DownloadState.FailedDownload;
            }
        }

        public static List<int> GetAvailableYears()
        {
            List<int> possibleYears = [];
            for (int year = 2015; year <= DateTime.Now.Year; year++)
            {
                for (int day = 1; day <= 25; day++)
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
            for (int day = 1; day <= 25; day++)
            {
                if (ClassExists(year, day))
                    possibleDays.Add(day);
            }
            return possibleDays;
        }

        public static bool ClassExists(int year, int day)
        {
            Type? type = Type.GetType($"advent_of_code.Year{year.ToString().PadLeft(2, '0')}.Day{day.ToString().PadLeft(2, '0')}.Challange1, advent_of_code.Year{year.ToString().PadLeft(2, '0')}.Day{day.ToString().PadLeft(2, '0')}");
            if (type == null) { return false; }
            return true;
        }

        public static MethodInfo? GetMethodInfo(int year, int day, int challange)
        {
            Type? type = Type.GetType($"advent_of_code.Year{year.ToString().PadLeft(2, '0')}.Day{day.ToString().PadLeft(2, '0')}.Challange{challange}, advent_of_code.Year{year.ToString().PadLeft(2, '0')}.Day{day.ToString().PadLeft(2, '0')}");
            if (type == null) { return null; }
            return type.GetMethod("DoChallange");
        }

        public static string FormatTime(ulong milliseconds)
        {
            uint milli = (uint)(milliseconds % 1000);
            uint seconds = (uint)(milliseconds / 1000);
            uint minutes = seconds / 60;
            seconds %= 60;
            uint hours = minutes / 60;
            minutes %= 60;
            string time = "";
            if (hours > 0) time = hours.ToString().PadLeft(2, '0') + ":";
            if (minutes > 0 || hours > 0) time += minutes.ToString().PadLeft(2, '0') + ":";
            if (seconds > 0 && (minutes > 0 || hours > 0)) time += seconds.ToString().PadLeft(2, '0') + "." + milli.ToString().PadLeft(3, '0');
            else if (seconds > 0) time += seconds.ToString().PadLeft(2, '0') + "." + milli.ToString().PadLeft(3, '0') + "s";
            else time += milli.ToString().PadLeft(3, '0') + "ms";
            return time;
        }
    }
}
