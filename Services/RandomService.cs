using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public interface IRandomService
    {
        string GetRandomName();
        string GetRandomDiagnose();
        int GetRandomInt(int from, int to);

        bool GetRandomBool();
    }
    public class RandomService : IRandomService
    {
        Random _random = new Random();
        string[] _diagnosies = ["Flu", "Appendicitis", "Cancer", "HIV", "Pneumonia", "Herat Attack", "Insult"];
        string[] _names = ["Tom", "Roman", "Harry", "Igor", "Vadim", "Arseniy", "Denis", "Mikhail", "Vladislave", "Alisa", "Svetlana", "Nadzezhda", "Andrew"];

        public bool GetRandomBool()
        {
            return _random.Next(0, 1) == 1;
        }

        public string GetRandomDiagnose()
        {
            return _diagnosies[_random.Next(0, _diagnosies.Length)];
        }

        public int GetRandomInt(int from, int to)
        {
            return (int)_random.Next(from, to);
        }

        public string GetRandomName()
        {
            return _names[_random.Next(0, _names.Length)];
        }
    }
}
