class Patient
{
    public int Id;
    public string Name;
    public int Age;
    public string Condition;

    // Parameterized Constructor
    public Patient(int id, string name, int age, string condition)
    {
        Id = id;
        Name = name;
        Age = age;
        Condition = condition;
    }
}

class HospitalManager
{
    private Dictionary<int,Patient> _patients = new Dictionary<int,Patient>();
    private Queue<Patient> _appointmentQueue = new Queue<Patient>();

    // Add new Patient to list
    int idindexer = 1;

    public void RegisterPatient(int id, string name, int age, string condition)
    {
        _patients.Add(idindexer,new Patient(id,name,age,condition));
        idindexer++;    
    } 

    // Add patient to appointment queue
    public void ScheduleAppointment(int patientId)
    {
        // Find patient & add to Queue.
        foreach(var patient in _patients)
        {
            if(patient.Key == patientId)
            {
                _appointmentQueue.Enqueue(patient.Value);
            }
        }
    }

    // Process next appointment
    public Patient ProcessNextAppointment()
    {
        // pops patient from queue and returns
        Patient p1 = _appointmentQueue.Dequeue();

        return p1;

    }

    // Find patients with specific conidtions using LINQ
    public List<Patient> FindPatientsByCondition(string condition)
    {
        List<Patient> result = new List<Patient>();

        foreach(var patient in _patients)
        {
            if(patient.Value.Condition == condition)
            {
                result.Add(patient.Value);
            }
        }

        return result;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        HospitalManager manager = new HospitalManager();
        manager.RegisterPatient(1, "John Doe", 45, "Hypertension");
        manager.RegisterPatient(2, "Jane Smith", 32, "Diabetes");
        manager.ScheduleAppointment(1);
        manager.ScheduleAppointment(2);

        Patient nextPatient = manager.ProcessNextAppointment();
        Console.WriteLine(nextPatient.Name); // Should output: John Doe

        List<Patient> diabeticPatients = manager.FindPatientsByCondition("Diabetes");
        Console.WriteLine(diabeticPatients.Count); // Should output: 1
    }
}