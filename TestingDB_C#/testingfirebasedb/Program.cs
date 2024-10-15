using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System.Security.Cryptography;
/*IMPORTANT NOTES*/
/*
  1. Need to generate private key in the form of json file from the firebase web app 
  2. Add the json file that contains all your service account credentials into your project
  3. Make sure to add path
*/
namespace testingfirebasedb
{
    internal class Program
    {
        //initialize database
        public static FirestoreDb initializeDataBase()
        {
            //creating path (compulsory syntax -- just go with it)
            string path = AppDomain.CurrentDomain.BaseDirectory + @"firestore.json";
            //adding path into environment (compulsory syntax -- just go with it)
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            //creating database (argument passed: [your own project id of firebase])
            FirestoreDb tmp_database = FirestoreDb.Create("testdb-64de1");
            Console.WriteLine("Created database successful");
            return tmp_database;
        }

       
        /*main function needed to be async to wait for data to be saved before ending itself, and [Task] 
        instead of [void] is used is because we are now saving data which has delays*/
        
        static async Task Main(string[] args)
        {
            //store initialize database for usage
            FirestoreDb database = initializeDataBase();

            //async...await function to carry out data saving (it will wait for data to be successfully saved.)
            async Task saveData(transaction a_transaction)
            {
                DocumentReference docRef = database.Collection("transaction_5").Document("newDoc5");
                Dictionary<string,object> userdata1  = new Dictionary<string,object>()
                {
                    {"Name",a_transaction.getName() },
                    {"id",a_transaction.getID()},
                    {"transaction_value",a_transaction.getTransVal()},
                    {"transaction_datetime", a_transaction.getDateTime()}
                };
                await docRef.SetAsync(userdata1);
                Console.WriteLine("Data saved successfully");
            }

            //test case, creating new object
            transaction newTransaction = new transaction(3195.86, DateTime.UtcNow, "Alexa", "22010179");
            //saving information
            await saveData(newTransaction);

            //read data from database (1 of the way) (data read is in the form of DICTIONARY)

            async Task readData()
            {
                //accessing the database with (DocumentReference) w/ specification [collection_name] [document_name]
                DocumentReference docRef = database.Collection("transaction_5").Document("newDoc5");
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists == true)
                {
                    Console.WriteLine("Document data for {0} documents: ", snapshot.Id);
                    Dictionary<string, object> userdatatest = snapshot.ToDictionary();
                    foreach (KeyValuePair<string, object> data in userdatatest)
                    {
                        Console.WriteLine("{0}: {1}", data.Key, data.Value);
                    }
                }
                else
                {
                    Console.WriteLine("Document {0} does not exist", snapshot.Id);
                }
            }
            
            //read information
            await readData();

            //declaring the function to read data from database and return as custom object
            async Task<transaction> readDataCustomObject() //new syntax Task<[object_name]> Task (asynchronous operation) = allowing your CPU to perform other task eventhough it is performing other task.
            {
                DocumentReference docRef = database.Collection("transaction_5").Document("newDoc5");
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                //check if the snapshot/data block exists
                if (snapshot.Exists == true)
                {
                    Console.WriteLine("Getting data from {0} in the form of custom object...", snapshot.Id);
                    //create a new transaction class to contain the converted data block --> <transaction>
                    transaction tmp_transaction = snapshot.ConvertTo<transaction>();
                    return tmp_transaction;
                }
                else
                {
                    Console.WriteLine("Data demanded not found!");
                    return null;
                }
            }

            //gets the returned object
            transaction dataretrieved = await readDataCustomObject();
            //usage
            Console.WriteLine("Name: {0}", dataretrieved.getName());
            Console.WriteLine("ID: {0}", dataretrieved.getID());
            Console.WriteLine("Amount transacted: {0}", dataretrieved.getTransVal());
            Console.WriteLine("Date: {0}", dataretrieved.getDateTime());

        }
    }
}
