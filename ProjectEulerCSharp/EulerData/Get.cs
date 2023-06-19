using System;
using System.IO;
using System.Reflection;

namespace ProjectEulerCSharp.EulerData
{
    public static class Get
    {
        public static string[] Resource(string fileName)
        {
            var words = EulerData.Get.Resource(
                fileName: fileName,
                fileStr =>
                {
                    var namesNoQuotes = fileStr.Replace("\"", string.Empty);
                    return namesNoQuotes.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                });
            return words;
        }

        /// <summary>
        /// Helper method to work with embedded resource files without repeating a bunch of boilerplate code
        /// </summary>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="fileName">filename only, no path</param>
        /// <param name="fnForFileString">Do something to the resource file that is read in as 1 big string</param>
        /// <returns>whatever fnForFileString returns</returns>
        /// <exception cref="Exception"></exception>
        public static TRet Resource<TRet>(
            string fileName,
            Func<string, TRet> fnForFileString)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("ProjectEulerCSharp.EulerData." + fileName))
            {
                if (stream == null)
                {
                    throw new Exception($"Could not find embedded resource {fileName}!");
                }

                using (StreamReader reader = new(stream))
                {
                    var result = reader.ReadToEnd();
                    return fnForFileString(result);
                }
            }
        }

        /// <summary>
        /// Helper method to work with embedded resource files without repeating a bunch of boilerplate code
        /// </summary>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="fileName">filename only, no path</param>
        /// <param name="fnForFileStream">Do something to the resource file that is read in as a stream</param>
        /// <returns>whatever fnForFileStream returns</returns>
        /// <exception cref="Exception"></exception>
        public static TRet Resource<TRet>(
            string fileName,
            Func<Stream, TRet> fnForFileStream)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("ProjectEulerCSharp.EulerData." + fileName))
            {
                if (stream == null)
                {
                    throw new Exception($"Could not find embedded resource {fileName}!");
                }

                return fnForFileStream(stream);
            }
        }
    }
}
