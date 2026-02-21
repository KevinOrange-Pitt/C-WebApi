using Newtonsoft.Json;
using PersonWebApi.Models;

namespace PersonWebApi.Services
{
    /// <summary>
    /// Thread-safe singleton for managing Person data persistence to JSON file
    /// </summary>
    public sealed class FileManager
    {
        private static readonly object _lock = new object();
        private static FileManager? _instance;
        private readonly string _filePath;
        private readonly object _fileLock = new object();

        private FileManager()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "persons.json");
            InitializeFile();
        }

        public static FileManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new FileManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private void InitializeFile()
        {
            lock (_fileLock)
            {
                if (!File.Exists(_filePath))
                {
                    File.WriteAllText(_filePath, "[]");
                }
            }
        }

        public List<Person> GetAllPersons()
        {
            lock (_fileLock)
            {
                try
                {
                    string json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<List<Person>>(json) ?? new List<Person>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file: {ex.Message}");
                    return new List<Person>();
                }
            }
        }

        public Person? GetPersonById(string id)
        {
            var persons = GetAllPersons();
            return persons.FirstOrDefault(p => p.Id == id);
        }

        public Person AddPerson(Person person)
        {
            lock (_fileLock)
            {
                var persons = GetAllPersons();
                
                // Generate ID if not provided
                if (string.IsNullOrEmpty(person.Id))
                {
                    person.Id = Guid.NewGuid().ToString();
                }
                
                persons.Add(person);
                SaveToFile(persons);
                return person;
            }
        }

        public bool UpdatePerson(string id, Person updatedPerson)
        {
            lock (_fileLock)
            {
                var persons = GetAllPersons();
                var existingPerson = persons.FirstOrDefault(p => p.Id == id);
                
                if (existingPerson == null)
                {
                    return false;
                }
                
                existingPerson.Name = updatedPerson.Name;
                existingPerson.School = updatedPerson.School;
                
                SaveToFile(persons);
                return true;
            }
        }

        public bool DeletePerson(string id)
        {
            lock (_fileLock)
            {
                var persons = GetAllPersons();
                var person = persons.FirstOrDefault(p => p.Id == id);
                
                if (person == null)
                {
                    return false;
                }
                
                persons.Remove(person);
                SaveToFile(persons);
                return true;
            }
        }

        private void SaveToFile(List<Person> persons)
        {
            try
            {
                string json = JsonConvert.SerializeObject(persons, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
                throw;
            }
        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}
