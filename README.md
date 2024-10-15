# Retrive Data from database and using it

**Experimental**

Figured out how to retrieve data from database and using it

## IMPORTANT NOTES

1. Classes must declare [FirestoreData] & [FirestoreProperty] for database mapping
2. Class property encapsulation must be **_public_**

_**Example:**_

```ruby
using Google.Cloud.Firestore;

namespace testingfirebasedb
{
    //declaring to firestore that this class is for firestore data
    [FirestoreData]
    internal class transaction : Person
    {
        //declaring that this is a firestoreproperty and the bracket ("transaction_val") is for DATABASE MAPPING
        //case-sensitive!
        [FirestoreProperty("transaction_value")] //the value inside bracket is the value inside ur database document field
        public double transaction_val { get; set; }

        [FirestoreProperty("transaction_datetime")]
        public DateTime transaction_date { get; set; }

        [FirestoreProperty]
        public Person person_info { get; set; }
  }
}
```

2. Task<[object_name]> able to let function to return customized datatype from database

_**Example:**_

```ruby
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
```



