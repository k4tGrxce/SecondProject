namespace OAIP_Laba4
{
    static class Program
    {
        const int MAX_LENGHT = 1000;
        const int MAX_HEIGHT = 1000;

        static Random random = new Random();

        static void Main(string[] args)
        {
            int length = GetValidNumber($"Введите длину замка до {MAX_LENGHT}: ");
            int height = GetValidNumber($"Введите высоту замка до {MAX_HEIGHT}: ");
            int[,] matrix = CreateAndGetMatrix(length, height);
            int stepsQuantity = length + height - 1;

            PrintMatrix(matrix);

            StartFindingPathQuantity(matrix, stepsQuantity);
        }

        static void StartFindingPathQuantity(int[,] matrix, int stepsQuantity)
        {
            int totalSteps = 0;
            int totalPaths = 0;

            int startX = matrix.GetLength(0) - 1; // начальная строка (нижний край)
            int startY = 0;                       // начальный столбец (левый край)

            int targetX = 0;                      // строка цели (верхний край)
            int targetY = matrix.GetLength(1) - 1; // столбец цели (правый край)

            bool[,] blocked = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            bool success = false;

            while (totalSteps < stepsQuantity)
            {
                (success, int steps) = FindPath(matrix, startX, startY, targetX, targetY, blocked, stepsQuantity);
                totalSteps += steps;

                if (success)
                {
                    totalPaths++;
                }

            }

            PrintTotalPathQuantity(totalPaths, success);
        }

        static (bool success, int steps) FindPath(int[,] matrix, int startX, int startY, int targetX, int targetY, bool[,] blocked, int stepsQuantity)
        {
            int currentX = startX;
            int currentY = startY;
            int steps = 0;

            bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            visited[currentX, currentY] = true;

            while (steps < stepsQuantity)
            {
                (int, int)? nextStep = GetNextStep(matrix, currentX, currentY, targetX, targetY, visited, blocked);

                if (nextStep.HasValue)
                {
                    steps++;

                    currentX = nextStep.Value.Item1;
                    currentY = nextStep.Value.Item2;

                    visited[currentX, currentY] = true;

                    Console.WriteLine($"Шаг: {steps}. Текущая позиция: {currentX}, {currentY}");

                    if (currentX == targetX && currentY == targetY)
                    {
                        return (true, steps);
                    }
                }
                else
                {
                    // блокируем клетку, так как из нее нет выхода
                    blocked[currentX, currentY] = true;
                    return (false, steps);
                }
            }

            return (false, steps);
        }

        static (int, int)? GetNextStep(int[,] matrix, int x, int y, int targetX, int targetY, bool[,] visited, bool[,] blocked)
        {
            int[,] directions = new int[,]
            {
                { -1, 0 }, // вверх
                { 0, 1 },   // вправо
                { 1, 0 }, // вниз
                { 0, -1 }, // влево
            };

            (int, int)? bestStep = null;
            int bestDistance = int.MaxValue;

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = x + directions[i, 0];
                int newY = y + directions[i, 1];

                if (CanStep(matrix, newX, newY, visited, blocked))
                {
                    int distance = Math.Abs(newX - targetX) + Math.Abs(newY - targetY);

                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestStep = (newX, newY);
                    }
                }
            }

            return bestStep;
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static bool CanStep(int[,] matrix, int currentLength, int currentHeight, bool[,] visited, bool[,] blocked)
        {
            if (currentLength < 0 || currentLength >= matrix.GetLength(0) ||
                currentHeight < 0 || currentHeight >= matrix.GetLength(1))
            {
                return false;
            }

            return matrix[currentLength, currentHeight] == 1 && !visited[currentLength, currentHeight] && !blocked[currentLength, currentHeight];
        }

        static void PrintTotalPathQuantity(int quantity, bool reached)
        {
            Console.WriteLine(quantity <= 0 ? "Невозможно" : $"Найдено путей: {quantity} \nЦель достигнута: {reached}");
        }

        static int GetValidNumber(string message)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                if (result > MAX_LENGHT || result > MAX_HEIGHT)
                {
                    Console.WriteLine("Вы ввели число больше максимального!");
                    return GetValidNumber(message);
                }
                else if (result <= 0)
                {
                    Console.WriteLine("Число не может быть меньше или равно 0!");
                    return GetValidNumber(message);
                }

                return result;
            }
            else
            {
                Console.WriteLine("Вы ввели неверное число!");
                return GetValidNumber(message);
            }
        }

        static int[,] CreateAndGetMatrix(int length, int height)
        {
            int[,] matrix = new int[length, height];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = (i == length - 1 && j == 0) || (i == 0 && j == height - 1) ? 1 : random.Next(0, 2);
                }
            }

            return matrix;
        }
    }
}
