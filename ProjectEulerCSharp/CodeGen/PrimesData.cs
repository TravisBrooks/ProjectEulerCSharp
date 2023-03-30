using System.IO;
using System.Text;
using ProjectEulerCSharp.EulerMath;
using Xunit;

namespace ProjectEulerCSharp.CodeGen
{
    /// <summary>
    /// Build some files full of prime numbers so they don't have to be calculated again
    /// </summary>
    public class PrimesData
    {
        [Fact]
        public void WriteFirstMillionPrimes()
        {
            var primes = Primes.Calculate(1_000_000);
            var fileStreamOptions = new FileStreamOptions
            {
                Mode = FileMode.OpenOrCreate,
                Access = FileAccess.ReadWrite
            };

            using(var stream = File.OpenWrite("../../../EulerData/PrimesLessThanOneMillion.data"))
            {
                using (var binaryWriter = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: false))
                {
                    foreach (var prime in primes)
                    {
                        binaryWriter.Write(prime);
                    }
                }
            }
        }
    }
}
