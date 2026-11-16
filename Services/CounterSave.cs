using Counter.Models;
using System.Text.Json;

namespace Counter.Services
{
    internal class CounterSave
    {
        const string PATH = "counters.json";

        public static void Save(IEnumerable<CounterModel> cm)
        {
            string json = JsonSerializer.Serialize(cm);
            File.WriteAllText(PATH, json);
        }

        public static List<CounterModel> Load()
        {
            if (!File.Exists(PATH))
            {
                return new List<CounterModel>();
            }

            try
            {
                string json = File.ReadAllText(PATH);
                return JsonSerializer.Deserialize<List<CounterModel>>(json) ?? new List<CounterModel>();
            }
            catch (JsonException)
            {
                return new List<CounterModel>();
            }
            catch (IOException)
            {
                return new List<CounterModel>();
            }
        }
    }
}
