using System.Text;

namespace TravelRequisitionSystem.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

public string RandomPassword()
        {

        var passwordBuilder = new StringBuilder();

            passwordBuilder.Append(RandomString(1));

            passwordBuilder.Append(RandomNumber(0, 9));

            passwordBuilder.Append(RandomString(1));

            passwordBuilder.Append(RandomNumber(0, 9));
            return passwordBuilder.ToString();
        }
    }

}
