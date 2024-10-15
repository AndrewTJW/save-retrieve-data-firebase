using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using google library, namespace needed to be declared
using Google.Cloud.Firestore;

namespace testingfirebasedb
{
    //declaring to firestore that this class is for firestore data
    [FirestoreData]
    internal class Person
    {
        //declaring that this is a firestoreproperty and the bracket ("name") is for DATABASE MAPPING
        //case-sensitive!
        [FirestoreProperty("Name")] //the value inside bracket is the value inside ur database document field
        public string name { get; set; }

        [FirestoreProperty("id")]
        public string ID { get; set; }

        public Person() { }
        public Person (string a_name, string a_ID)
        {
            name = a_name;
            ID = a_ID;
        }

        public string getName ()
        {
            return name;
        }
        public string getID()
        {
            return ID;
        }
    }
}
