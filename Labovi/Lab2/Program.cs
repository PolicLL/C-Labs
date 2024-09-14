using System;
using System.Collections.Generic;
using System.Reflection;

class Patient
{
    public string OIB { get; set; }
    public string MBO { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Diagnosis { get; set; }

    public int ID;

    private static int tempID = 0;

    public Patient(string oib, string mbo, string name, string surname, DateTime dateOfBirth, string gender, string diagnosis)
    {
        OIB = oib;
        MBO = mbo;
        Name = name;
        Surname = surname;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Diagnosis = diagnosis;
        ID = tempID++;
    }
}

class Hospital
{
    private List<Patient> patients = new List<Patient>();


    public Patient CreatePatient()
    {
        string OIB = InputOIB();
        string MBO = InputMBO();
        string Name = EnterNewString("first name");
        string Surname = EnterNewString("second name");

        DateTime dateOfBirth = InputDateOfBirth();

        string gender = InputGender();
        string diagnosis = EnterNewString("diagnosis");

        var newPatient = new Patient(OIB, MBO, Name, Surname, dateOfBirth, gender, diagnosis);

        patients.Add(newPatient);

        return newPatient;
    }

    public void UpdatePatient()
    {
        // Retrieve patient

        Patient patientToUpdate = GetPatientById();

        if (patientToUpdate == null)
        {
            Console.WriteLine("There is no patient with this ID.");
            return;
        }

        Console.WriteLine("Current values of patient : ");
        PrintPatientInfo(patientToUpdate);

        // Inserting values

        UpdateProperties(patientToUpdate);

        Console.WriteLine("Patient after updating : ");
        PrintPatientInfo(patientToUpdate);

    }

    private Patient GetPatientById()
    {
        Console.WriteLine("Enter ID of patient you want to update: ");

        int patientID = int.Parse(Console.ReadLine());

        return patients.FirstOrDefault(patient => patient.ID == patientID);
    }

    private void UpdateProperties(Patient patientToUpdate)
    {
        bool updateMore = true;

        do
        {
            UpdateProperty(patientToUpdate);

            Console.WriteLine("Do you want to update more properties ? (YES/NO)");

            if (Console.ReadLine() != "YES")
                updateMore = false;
        }
        while (updateMore);
    }

    private void UpdateProperty(Patient patientToUpdate)
    {
        Console.WriteLine("Enter which value you want to update (OIB, MBO, Name, Surname, DateOfBirth, Gender, Diagnosis)");

        string propertyToUpdate = Console.ReadLine();

        switch (propertyToUpdate)
        {
            case "OIB":
                string newOIB = InputOIB();
                patientToUpdate.OIB = newOIB;
                break;
            case "MBO":
                string newMBO = InputMBO(); 
                patientToUpdate.MBO = newMBO;
                break;
            case "Name":
                string newName = EnterNewString("first name"); 
                patientToUpdate.Name = newName;
                break;
            case "Surname":
                string newSurname = EnterNewString("second name"); 
                patientToUpdate.Surname = newSurname;
                break;
            case "DateOfBirth":
                DateTime newDateOfBirth = InputDateOfBirth(); 
                patientToUpdate.DateOfBirth = newDateOfBirth;
                break;
            case "Gender":
                string newGender = InputGender(); 
                patientToUpdate.Gender = newGender;
                break;
            case "Diagnosis":
                string newDiagnosis = EnterNewString("diagnosis"); 
                patientToUpdate.Diagnosis = newDiagnosis;
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private string InputOIB()
    {
        string OIB = EnterNewString("OIB");

        while (OIB.Length != 11)
        {
            Console.WriteLine("OIB must be 11 characters long.");
            Console.WriteLine("Enter OIB again : ");
            OIB = Console.ReadLine();
        }

        return OIB;
    }


    private string InputMBO()
    {
        string MBO = EnterNewString("MBO");

        while (MBO.Length != 9)
        {
            Console.WriteLine("MBO must be 9 characters long.");
            Console.WriteLine("Enter MBO again : ");
            MBO = Console.ReadLine();
        }

        return MBO;
    }

    private DateTime InputDateOfBirth()
    {
        Console.WriteLine("Enter date of birth (format: dd/MM/yyyy) : ");
        string dobInput = Console.ReadLine();

        DateTime dateOfBirth;
        while (!DateTime.TryParseExact(dobInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateOfBirth))
        {
            Console.WriteLine("Invalid date format. Please enter date in dd/MM/yyyy format.");
            Console.Write("Enter date of birth (format: dd/MM/yyyy) : ");
            dobInput = Console.ReadLine();
        }
        return dateOfBirth;
    }


    private string InputGender()
    {
        Console.Write("Enter gender (M/F) : ");
        string genderInput = Console.ReadLine().ToUpper();

        while (genderInput != "M" && genderInput != "F")
        {
            Console.Write("Invalid gender input. Please enter M for Male or F for Female.");
            genderInput = Console.ReadLine().ToUpper();
        }

        return genderInput == "M" ? "Male" : "Female";
    }

    private string EnterNewString(string fieldName)
    {
        Console.Write($"Enter new {fieldName}: ");
        return Console.ReadLine();
    }

    // Discharge

    public void DischargePatient()
    {
        Console.WriteLine("Enter ID of patient you want to discharge: ");

        int patientID = int.Parse(Console.ReadLine());

        Patient patientToRemove = patients.FirstOrDefault(patient => patient.ID == patientID);

        if(patientToRemove != null)
        {
            patients.Remove(patientToRemove);
            Console.WriteLine("Patient removed.");
        }
        else
        {
            Console.WriteLine("Patient with this ID does not exist.");
        }
    }

    public void AddPatient(Patient patient)
    {
        patients.Add(patient);
    }

    // Printing

    public void PrintPatientList()
    {
        PrintHeader();

        foreach (var patient in patients)
        {
            PrintPatientInfo(patient);
        }

        PrintFooter();
    }

    public void PrintPatientInfo(Patient patient)
    {
        Console.WriteLine($"| {patient.ID,-5} | {patient.OIB,-20} | {patient.MBO,-15} | {patient.Name,-15} | {patient.Surname,-15} | {patient.DateOfBirth.ToString("dd/MM/yyyy"),-17} | {patient.Gender,-10} | {patient.Diagnosis,-12} |");
    }

    private void PrintHeader()
    {
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine($"| {"ID",-5} | {"OIB",-20} | {"MBO",-15} | {"Name",-15} | {"Surname",-15} | {"Date of Birth",-17} | {"Gender",-10} | {"Diagnosis",-12} |");
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
    }

    private void PrintFooter()
    {
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
    }

}

class Program
{

    private static void addPatients(Hospital hospital)
    {
        hospital.AddPatient(new Patient("12312312312", "907829345", "Pera", "Peric", new DateTime(), "Male", "Diagnosis 1."));
        hospital.AddPatient(new Patient("52334563423", "867893457", "John", "Doe", new DateTime(), "Female", "Diagnosis 2."));
        hospital.AddPatient(new Patient("58748953345", "893414234", "Mike", "Smith", new DateTime(), "Female", "Diagnosis 3."));
        hospital.AddPatient(new Patient("68934535345", "568974589", "Johnny", "Trevis", new DateTime(), "Male", "Diagnosis 4."));
    }

    static void Main1(string[] args)
    {
        Console.WriteLine("Patient information system.");

        bool running = true;

        Hospital hospital = new Hospital();
        addPatients(hospital);
        

        while (running)
        {
            Console.WriteLine("Menu: ");
            Console.WriteLine("1. Add a new patient");
            Console.WriteLine("2. Update patient info");
            Console.WriteLine("3. Discharge the patient");
            Console.WriteLine("4. See all patients");
            Console.WriteLine("5. Exit");
            Console.WriteLine("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    hospital.CreatePatient();
                    break;
                case 2:
                    hospital.UpdatePatient();
                    break;
                case 3:
                    hospital.DischargePatient();
                    break;
                case 4:
                    hospital.PrintPatientList();
                    break;
                case 5:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void Main(string[] args)
    {
        Patient patient = new Patient("12312312312", "907829345", "Pera", "Peric", new DateTime(2020, 10, 20), "Male", "A01");

       
    }
}
