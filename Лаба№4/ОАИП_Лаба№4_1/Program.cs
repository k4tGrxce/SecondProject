namespace OAIP_Laba4
{
    static class Program
    {
        const int ARRAY_SIZE = 12;

        static readonly string[] firstMassive = new string[]
        {
            "четный 0",
            "нечетный 1",
            "четный 2",
            "нечетный 3",
            "четный 4",
            "нечетный 5",
            "четный 6",
            "нечетный 7",
            "четный 8",
            "нечетный 9",
            "четный 10",
            "нечетный 11",
        };

        static readonly string[] secondMassive = new string[]
        {
            "четный 0",
            "нечетный 1",
            "четный 2",
            "нечетный 3",
            "четный 4",
            "нечетный 5",
            "четный 6",
            "нечетный 7",
            "четный 8",
            "нечетный 9",
            "четный 10",
            "нечетный 11",
        };

        static void Main(string[] args)
        {
            string[] newMassive = CreateAndGetNewMassive();
            PrintMassive(newMassive);
        }

        static void PrintMassive(string[] massive)
        {
            Console.WriteLine("Итоговый массив:");

            for (int i = 0; i < massive.Length; i++)
            {
                Console.WriteLine($"{i} = {massive[i]}");
            }
        }

        static string[] CreateAndGetNewMassive()
        {
            string[] newMassive = new string[ARRAY_SIZE];

            int firstIndex = 1; // начинаем с первого нечетного индекса
            int secondIndex = 0; // начинаем с первого четного индекса

            for (int i = 0; i < ARRAY_SIZE; i++)
            {
                if (i % 2 == 0)
                {
                    newMassive[i] = firstMassive[firstIndex];
                    firstIndex = (firstIndex + 2) >= firstMassive.Length ? 1 : firstIndex + 2;
                }
                else
                {
                    newMassive[i] = secondMassive[secondIndex];
                    secondIndex = (secondIndex + 2) >= secondMassive.Length ? 0 : secondIndex + 2;
                }
            }

            return newMassive;
        }
    }
}