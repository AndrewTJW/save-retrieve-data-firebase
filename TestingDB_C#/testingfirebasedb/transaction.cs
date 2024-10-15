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

        //parameterless constructor

        public transaction () { }
        public transaction (double a_transaction_val, DateTime a_transaction_date, string a_name, string a_ID) : base(a_name, a_ID)
        {
            transaction_val = a_transaction_val;
            transaction_date = a_transaction_date;
        }

        public double getTransVal()
        {
            return transaction_val;
        }

        public DateTime getDateTime()
        {
            return transaction_date;
        }

    }
}
